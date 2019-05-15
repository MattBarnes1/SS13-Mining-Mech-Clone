using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using DedicatedServer.GameDataClasses.Entities;
using DedicatedServer.Packets;
using GameData.VersionHandling;
using Lidgren.Network;
using LogHandler;
using PacketData.Packets.PacketTypes;
using PacketData.Packets.Superclasses;
using PacketData.Packets.Superclasses.Login;
using PacketData.Packets.Superclasses.Player;
using PacketData.UDPServiceHandler;

namespace DedicatedServerFramework.Servers
{
    public class LoginServer : ServerBaseClass
    {
        SQLServerWrapper myWrapper = new SQLServerWrapper();
        PacketFactory myFactory;
        ConcurrentDictionary<byte[], ConnectionWrapper> ActiveConnections = new ConcurrentDictionary<byte[], ConnectionWrapper>();

        public LoginServer() : base(InternalSettings.myLoginServerPort.Port)
        {
            myFactory = new PacketFactory();
           myFactory.AssignProcessor(HandleLogin, typeof(LoginPacket));
           myFactory.AssignProcessor(HandleDisconnect, typeof(DisconnectPacket));

        }

        private void HandleDisconnect(Packet myPacket)
        {
           // ConnectionWrapper Wrapper;
           // if (ActivePlayers.TryRemove(P.UserID, out Wrapper))
            //{
            //    PlayerData myPlayer = myPlayerDataServer.DisconnectPlayer(P.UserID);
            //    Console.WriteLine("Disconnect was detected from: " + ((PlayerData)Wrapper.MyPlayerData));
           // }
        }

        private void OnReceived(object state)
        {
            MemoryStream myStream = new MemoryStream();
            BinaryFormatter mySendFormatter = new BinaryFormatter();
            var myMessage = (NetIncomingMessage)state;
            switch (myMessage.MessageType)
            {
                case NetIncomingMessageType.Error:
                    break;
                case NetIncomingMessageType.StatusChanged:
                    break;
                case NetIncomingMessageType.UnconnectedData:
                    break;
                case NetIncomingMessageType.ConnectionApproval:
                    break;
                case NetIncomingMessageType.Data:
                    myStream.Write(myMessage.Data, 0, myMessage.Data.Length);
                    Packet myPacket = (Packet)mySendFormatter.Deserialize(myStream);
                    myPacket.Sender = myMessage.SenderConnection;
                    myFactory.SetProcessor(myPacket);
                    myPacket.Process();
                    myMessage = null;
                    break;
                case NetIncomingMessageType.Receipt:
                    break;
                case NetIncomingMessageType.DiscoveryRequest:
                    break;
                case NetIncomingMessageType.DiscoveryResponse:
                    break;
                case NetIncomingMessageType.VerboseDebugMessage:
                    var myError = myMessage.ReadString();
                    LogHandler<LoginServer>.WriteToFile("Lidgren Warning: ", LogHandler<LoginServer>.LoggerType.WARNING, myError);
                    break;
                case NetIncomingMessageType.DebugMessage:
                    myError = myMessage.ReadString();
                    LogHandler<LoginServer>.WriteToFile("Lidgren Warning: ", LogHandler<LoginServer>.LoggerType.WARNING, myError);
                    break;
                case NetIncomingMessageType.WarningMessage:
                    myError = myMessage.ReadString();
                    LogHandler<LoginServer>.WriteToFile("Lidgren Error: ", LogHandler<LoginServer>.LoggerType.WARNING, myError);
                    break;
                case NetIncomingMessageType.ErrorMessage:
                    myError = myMessage.ReadString();
                    LogHandler<LoginServer>.WriteToFile("Lidgren Error: ", LogHandler<LoginServer>.LoggerType.EXCEPTION, myError);
                    break;
                case NetIncomingMessageType.NatIntroductionSuccess:
                    break;
                case NetIncomingMessageType.ConnectionLatencyUpdated:
                    break;
            }
        }


        ConcurrentDictionary<byte[], NetConnection> myActiveConnectionsByUserID = new ConcurrentDictionary<byte[], NetConnection>();
        ConcurrentDictionary<String, NetConnection> myActiveConnectionsByUsername = new ConcurrentDictionary<string, NetConnection>();
        ConcurrentDictionary<String, NetConnection> myActiveConnectionsByPublicUserID = new ConcurrentDictionary<string, NetConnection>();
        public void HandleLogin(Packet aPacket)
        {
            LoginPacket myPacket = (LoginPacket)aPacket;
            Console.WriteLine(myPacket.Username + " attempted to connect!");
            if (myWrapper.VerifyUser(myPacket.Username, myPacket.GetCreationTime(), myPacket.GetSHA()))
            {
                if (File.Exists("./Users/" + myPacket.Username + ".chr"))
                {
                    BinaryFormatter myConverter = new BinaryFormatter();
                    var Stream = File.Open("./Users/" + myPacket.Username + ".chr", FileMode.Open);//TODO: handle this for threads
                    PlayerData myData = (PlayerData)myConverter.Deserialize(Stream);
                    ThreadPool.QueueUserWorkItem(delegate (object State)
                    {
                        PlayerInfoPacket myNewPlayerPacket = new PlayerInfoPacket();
                        myNewPlayerPacket.SetPlayerData(myData);
                        myNewPlayerPacket.NewChar = false;
                        String myID = Guid.NewGuid().ToString();

                        while (myActiveConnectionsByPublicUserID.ContainsKey(myID))
                        {
                            myID = Guid.NewGuid().ToString();
                        }
                        myActiveConnectionsByUsername.TryAdd(myPacket.Username, myPacket.Sender);
                        myActiveConnectionsByUserID.TryAdd(myPacket.UserID, myPacket.Sender);
                        myActiveConnectionsByPublicUserID.TryAdd(myID, myPacket.Sender);
                        myData.PublicUserID = myID;                    
                        SendMapServer(myNewPlayerPacket, NetDeliveryMethod.ReliableOrdered);
                        Send(myNewPlayerPacket, myNewPlayerPacket.Sender, NetDeliveryMethod.ReliableOrdered);
                    });
                }
                else
                {
                    ThreadPool.QueueUserWorkItem(delegate (object State)
                    {
                        if (!myActiveConnectionsByUserID.ContainsKey(myPacket.UserID))
                        {
                            PlayerInfoPacket myNewPlayerPacket = new PlayerInfoPacket();
                            myNewPlayerPacket.Sender = myPacket.Sender;
                            PlayerData myData = new PlayerData();
                            myNewPlayerPacket.NewChar = true;
                            myNewPlayerPacket.SetPlayerData(myData);
                            String myID = Guid.NewGuid().ToString();
                            while (myActiveConnectionsByPublicUserID.ContainsKey(myID))
                            {
                                myID = Guid.NewGuid().ToString();
                            }
                            myActiveConnectionsByUsername.TryAdd(myPacket.Username, myPacket.Sender);
                            myActiveConnectionsByUserID.TryAdd(myPacket.UserID, myPacket.Sender);
                            myActiveConnectionsByPublicUserID.TryAdd(myID, myPacket.Sender);
                            myData.PublicUserID = myID;
                            SendMapServer(myNewPlayerPacket, NetDeliveryMethod.ReliableOrdered);
                            Send(myNewPlayerPacket, myNewPlayerPacket.Sender, NetDeliveryMethod.ReliableOrdered);
                        }
                    });
                }
                Console.WriteLine("User Authenticated Successfully!");
            }
            else
            {
                LoginFailedPacket myAck = new LoginFailedPacket();
                myAck.Sender = myPacket.Sender;
                Send(myAck, myPacket.Sender, NetDeliveryMethod.ReliableOrdered);
                Console.WriteLine("User Authentication Failed!");
            }
        }




        internal NetConnection GetNetConnectionFromPlayerData(PlayerData playerData)
        {
            throw new NotImplementedException();
        }
    }
}
