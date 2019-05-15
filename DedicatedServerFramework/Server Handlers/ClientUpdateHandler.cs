using DedicatedServer.Packets;
using DedicatedServerFramework.IO;
using GameData.Packets.Update_Program;
using Lidgren.Network;
using PacketData.Packets.PacketTypes;
using PacketData.Packets.Superclasses.Login;
using PacketData.UDPServiceHandler;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Runtime.Serialization.Formatters.Binary;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DedicatedServerFramework.Servers
{
    public class ClientUpdateHandler
    {
        SQLServerWrapper myWrapper = new SQLServerWrapper();
        List<FileHashEntry> myHash = new List<FileHashEntry>();
        Dictionary<String, FileHashEntry> myEntry = new Dictionary<string, FileHashEntry>();
        SHA256 mySha = SHA256Cng.Create();
        
        Dictionary<NetConnection, Dictionary<String, FileHashEntry>> myConnectedClientHashes = new Dictionary<NetConnection, Dictionary<String, FileHashEntry>>();

        public void RecurseIntoDirectory(String myDirectory)
        {
            
            foreach (string A in Directory.EnumerateDirectories(myDirectory))
            {
                RecurseIntoDirectory(A);
            }
            foreach(String B in Directory.EnumerateFiles(myDirectory))
            {
                FileHashEntry myFileHash = new FileHashEntry(mySha, B);
                FileStream myFileOpenStream = new FileStream(myFileHash.Location, FileMode.Open, FileAccess.Read);
                Stream myFile = FileStream.Synchronized(myFileOpenStream);
                myHash.Add(myFileHash);
            }
        }

        private bool CompareHash(byte[] myHash1, byte[] myHash2)
        {
            if(myHash1.Length != myHash2.Length)
            {
                return false;
            }
            for(int i= 0; i < myHash1.Length; i++)
            {
                if(myHash1[i] != myHash2[i])
                {
                    return false;
                }
            }
            return true;
        }

        UDPServer myServer = new UDPServer();

        //ConcurrentDictionary<String, Stream> myFileStreams = new ConcurrentDictionary<string, Stream>();
        ThreadedFileHandler myReader = new ThreadedFileHandler();
        public void LoadProcessors()
        {
            Console.WriteLine("Intializing Update Server Processors...");
            myServer.AssignProcessor(delegate (Packet P)
            {
                LoginUpdatePacket myPacket = (LoginUpdatePacket)P;
                if (myWrapper.VerifyUser(myPacket.Username, myPacket.GetCreationTime(), myPacket.GetSHA()))
                {
                    if (myConnectedClientHashes.ContainsKey(myPacket.Sender))
                    {
                        myConnectedClientHashes.Remove(myPacket.Sender);
                        Console.WriteLine("Double update request from: " + myPacket.Username + " from IP: " + myPacket.Sender.RemoteEndPoint.Address);
                    }
                    Console.WriteLine("Client update succeded for: " + myPacket.Username + " from IP: " + myPacket.Sender.RemoteEndPoint.Address);

                    Dictionary<String, FileHashEntry> myClientHashes = new Dictionary<String, FileHashEntry>();                   
                    foreach (FileHashEntry A in myPacket.myEntries)
                    {
                        if(!myClientHashes.ContainsKey(A.GetDirectoryStripped()))
                        {
                            myClientHashes.Add(A.GetDirectoryStripped(), A);
                        }
                    }
                    
                    //TODO: restrict files so they can't go out of server directory

                    //Send First file out!
                    foreach (FileHashEntry A in myHash)
                    {
                        String myEntry = A.GetDirectoryStripped();
                        if (myClientHashes.ContainsKey(myEntry) && CompareHash(myClientHashes[myEntry].myHash, A.myHash))
                        {
                            myClientHashes.Remove(myEntry);
                        }
                        else
                        {
                            myClientHashes.Add(A.Location, A);
                        }
                    }
                    if(myClientHashes.Count > 1)
                    {
                        myConnectedClientHashes.Add(myPacket.Sender, myClientHashes);
                        ThreadPool.QueueUserWorkItem(delegate (object State)
                        {
                            String myReturn = "";
                            byte[] Update = null;
                            var myEnum = myClientHashes.Keys.GetEnumerator();
                            while (myEnum.MoveNext())
                            {
                                if (!myReader.isQueued(myEnum.Current))
                                {
                                    myReturn = myEnum.Current;
                                    Update = myReader.OpenReadAll(myEnum.Current);
                                    break;
                                }
                            }
                            if(Update == null)
                            {
                                myReturn = myClientHashes.Keys.First();
                                Update = myReader.OpenReadAll(myReturn);
                            }
                            FileUpdatePacket myUpdate = new FileUpdatePacket();
                            myUpdate.myBytes = Update;
                            myUpdate.myEntry = myClientHashes[myReturn];
                            myUpdate.Sender = myPacket.Sender;
                            myUpdate.TotalFiles = myClientHashes.Count;
                            myClientHashes.Remove(myReturn);
                            myServer.Send(myUpdate, NetDeliveryMethod.ReliableOrdered);
                        }, null);
                    }
                    else if(myClientHashes.Count == 1)
                    {
                        ThreadPool.QueueUserWorkItem(delegate (object State)
                        {
                            var myReturn = myClientHashes.Keys.First();
                            byte[] Update = myReader.OpenReadAll(myReturn);
                            FileUpdatePacket myUpdate = new FileUpdatePacket();
                            myUpdate.myBytes = Update;
                            myUpdate.myEntry = myClientHashes[myReturn];
                            myUpdate.Sender = myPacket.Sender;
                            myUpdate.TotalFiles = myClientHashes.Count;
                            myClientHashes.Remove(myReturn);
                            myServer.Send(myUpdate, NetDeliveryMethod.ReliableOrdered);
                        }, null);
                    }
                    else
                    {
                        ThreadPool.QueueUserWorkItem(delegate (object State)
                        {
                            FileUpdatePacket myUpdate = new FileUpdatePacket();
                            myUpdate.Sender = myPacket.Sender;
                            myUpdate.TotalFiles = 0;
                            myServer.Send(myUpdate, NetDeliveryMethod.ReliableOrdered);
                        }, null);
                    }
                }
                else
                {
                    LoginFailedPacket myFailPacket = new LoginFailedPacket();
                    myFailPacket.Sender = myPacket.Sender;
                    myServer.Send(myFailPacket, NetDeliveryMethod.ReliableOrdered);
                }
            }, typeof(LoginUpdatePacket));

            myServer.AssignProcessor(delegate (Packet P)
            {
               var G = (DisconnectPacket)P;
                if (G.UserID != null)
                {
                    if(myConnectedClientHashes.ContainsKey(G.Sender))
                    {
                        var Result = myConnectedClientHashes[G.Sender];
                        //TODO: add more security here imo.
                        myConnectedClientHashes.Remove(G.Sender);
                    }
                }
            }, typeof(DisconnectPacket));

            myServer.AssignProcessor(delegate (Packet P)
            {
                GetNextFilePacket G = (GetNextFilePacket)P;
                if (myConnectedClientHashes.ContainsKey(G.Sender))
                {
                    var myClient = myConnectedClientHashes[G.Sender];
                    if (myClient.Count > 0)
                    {
                        ThreadPool.QueueUserWorkItem(delegate (object State)
                        {
                            var myReturn = myClient.Values.First();
                            byte[] Update = myReader.OpenReadAll(Path.Combine("./Update", myReturn.GetDirectoryStripped()));
                            FileUpdatePacket myUpdate = new FileUpdatePacket();
                            myUpdate.myBytes = Update;
                            myUpdate.myEntry = myReturn;
                            myUpdate.Sender = G.Sender;
                            myUpdate.TotalFiles = myClient.Count;
                            myClient.Remove(myReturn.Location);
                            myServer.Send(myUpdate, NetDeliveryMethod.ReliableOrdered);
                        }, null);
                    }
                    else
                    {
                        myConnectedClientHashes.Remove(G.Sender);
                    }
                }
            }, typeof(GetNextFilePacket));
            myServer.OnDisconnect += Disconnected;
        }

        private void Disconnected(NetConnection obj)
        {
            if(myConnectedClientHashes.ContainsKey(obj))
            {
                myConnectedClientHashes.Remove(obj);
            }
        }

        public void RestartServer()
        {
            myServer.Stop();
            myHash.Clear();
            RecurseIntoDirectory("./Update");
            myServer.Start(); //TODO:
        }

        public ClientUpdateHandler(int PortNumber)
        {
            Directory.CreateDirectory("./Update");
            RecurseIntoDirectory("./Update");
            LoadProcessors();
            myServer.StartServer(PortNumber);
        }
    }
}

