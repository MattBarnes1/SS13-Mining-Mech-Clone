using DedicatedServer.Packets;
using GameData.VersionHandling;
using Lidgren.Network;
using PacketData.Packets.PacketTypes;
using PacketData.Packets.Superclasses;
using PacketData.Packets.Superclasses.Login;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Runtime.Serialization.Formatters.Binary;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using static PacketData.Packets.PacketTypes.Packet;


namespace PacketData.UDPServiceHandler
{
    public class UDPServer
    {
        public const int MAX_UDP_PACKET_SIZE = 65507;        
        IPEndPoint myEndpoint = null;
        MemoryStream incoming = new MemoryStream();
      
        NetServer myServer;

        public void StartServer(int PortNumber)
        {
            NetPeerConfiguration myConfig = new NetPeerConfiguration("Client");
            myConfig.Port = PortNumber;
            myServer = new NetServer(myConfig);
            //myConfig.EnableMessageType(NetIncomingMessageType.ConnectionApproval);
            Console.WriteLine("Intializing Server Connection...");
            SynchronizationContext myContext = new SynchronizationContext();
            SynchronizationContext.SetSynchronizationContext(myContext);
            myServer.RegisterReceivedCallback(ProcessServerRequests, SynchronizationContext.Current);
            myServer.Start();
        }

        private void ProcessServerRequests(object state)
        {
            NetPeer myPeer = (NetPeer)state;
            NetIncomingMessage myMessage = myPeer.ReadMessage();
            switch (myMessage.MessageType)
            {
                case NetIncomingMessageType.Error:
                    break;
                case NetIncomingMessageType.StatusChanged:
                    NetConnectionStatus myStatus = (NetConnectionStatus)myMessage.ReadByte();
                    switch (myStatus)
                    {
                        case NetConnectionStatus.None:
                            break;
                        case NetConnectionStatus.InitiatedConnect:

                            break;
                        case NetConnectionStatus.ReceivedInitiation:
                            break;
                        case NetConnectionStatus.RespondedAwaitingApproval:
                            break;
                        case NetConnectionStatus.RespondedConnect:
                            break;
                        case NetConnectionStatus.Connected:
                            break;
                        case NetConnectionStatus.Disconnecting:
                            break;
                        case NetConnectionStatus.Disconnected:
                            int i = 0;
                            OnDisconnect?.Invoke(myMessage.SenderConnection);
                            break;
                    }
                    break;
                case NetIncomingMessageType.UnconnectedData:
                    break;
                case NetIncomingMessageType.ConnectionApproval:


                    break;
                case NetIncomingMessageType.Data:
                    myStream.Write(myMessage.Data, 0, myMessage.Data.Length);
                    myStream.Position = 0;
                    Packet myPacket = (Packet)mySendFormatter.Deserialize(myStream);
                    myStream.SetLength(0);
                    myPacket.Sender = myMessage.SenderConnection;
                    myFactory.SetProcessor(myPacket);
                    myPacket.Process();
                    break;
                case NetIncomingMessageType.Receipt:
                    break;
                case NetIncomingMessageType.DiscoveryRequest:
                    break;
                case NetIncomingMessageType.DiscoveryResponse:
                    break;
                case NetIncomingMessageType.VerboseDebugMessage:
                    break;
                case NetIncomingMessageType.DebugMessage:

                    break;
                case NetIncomingMessageType.WarningMessage:
                    break;
                case NetIncomingMessageType.ErrorMessage:
                    break;
                case NetIncomingMessageType.NatIntroductionSuccess:
                    break;
                case NetIncomingMessageType.ConnectionLatencyUpdated:
                    break;
            }
            myPeer.Recycle(myMessage);
        }

        NetConnection myServerConnection;
     
     


       
        public PacketFactory myFactory = new PacketFactory();
       
        public void AssignProcessor(PacketProcessor MyProc, Type T)
        {
            myFactory.AssignProcessor(MyProc, T);
        }

        public UDPServer()
        {
           // LoadProcessors(myFactory, IncomingPackets, OutgoingPackets, ActiveConnections);
            //The code below is to prevent the server from blowing up.
        }

        BinaryFormatter mySendFormatter = new BinaryFormatter();
        MemoryStream myStream = new MemoryStream();
        public event Action<NetConnection> OnDisconnect;
        public object Lock = new object();
        //TODO: do this in other places
        public void Send(Packet v, NetDeliveryMethod DeliveryType)
        {
            lock(Lock)
            {
                var Message = myServer.CreateMessage();
                mySendFormatter.Serialize(myStream, v);
                Message.Write(myStream.ToArray());
                myServer.SendMessage(Message, v.Sender, DeliveryType); //TODO: tweak this so it works for game and update
                myStream.SetLength(0);
                myStream.Position = 0;
            }
        }

        internal void Stop()
        {
            throw new NotImplementedException();
        }

        internal void Start()
        {
            throw new NotImplementedException();
        }
    }
}
