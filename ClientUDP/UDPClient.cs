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
    public class UDPClient
    {
        public const int MAX_UDP_PACKET_SIZE = 65507;
        Timer myTimer;
        ConcurrentQueue<Packet> IncomingPackets = new ConcurrentQueue<Packet>();
        IPEndPoint myEndpoint = null;
        MemoryStream incoming = new MemoryStream();
       
        public void OnExit(object sender, EventArgs e)
        {
            //TODO: gracefully close player connections
            DisconnectPacket myDisconnect = new DisconnectPacket(InternalSettings.Username);
            Send(myDisconnect, NetDeliveryMethod.ReliableOrdered);
        }
     
        NetPeer myPeer;
        IPEndPoint myEndpoint2;// = new IPEndPoint(IPAddress.Parse("107.9.185.247"), 1400);
        NetConnection myServerConnection;
        NetPeerConfiguration myConfig = new NetPeerConfiguration("Client");
        public void SetTimeout(int Timeout)
        {
            myConfig.ConnectionTimeout = Timeout;
        }


        public void StartClient(int fromPortNumber, String RemoteIPAddress, int RemotePort)
        { //TODO: test this and remove the start client string
            myEndpoint2 = new IPEndPoint(IPAddress.Parse(RemoteIPAddress), RemotePort);
            myConfig.Port = fromPortNumber;
            myPeer = new NetPeer(myConfig);
            SynchronizationContext myContext = new SynchronizationContext();
            SynchronizationContext.SetSynchronizationContext(myContext);
            myPeer.RegisterReceivedCallback(ProcessIncoming, SynchronizationContext.Current);
            myPeer.Start();
            myTimer = new Timer(new TimerCallback(AttemptToReconnect), this, 0, 500);
        }

        public event Action OnDisconnect;
        public event Action OnConnect;

        private void ProcessIncoming(object state)
        {
            NetPeer myPeer = (NetPeer)state;
            NetIncomingMessage myMessage = myPeer.ReadMessage();

            switch (myMessage.MessageType)
            {
                case NetIncomingMessageType.Error:
                    break;
                case NetIncomingMessageType.StatusChanged:
                    switch ((NetConnectionStatus)myMessage.ReadByte())
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
                            isConnected = true;
                            OnConnect?.Invoke();
                            break;
                        case NetConnectionStatus.Disconnecting:
                            break;
                        case NetConnectionStatus.Disconnected:
                            isConnected = false;
                            myTimer = new Timer(new TimerCallback(AttemptToReconnect), this,0, 500);
                            OnDisconnect?.Invoke();
                            break;
                    }
                    int i = 0;
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
                    PacketFactory.SetProcessor(myPacket);
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
                    Debug.WriteLine(myMessage.ReadString());
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

        public void AttemptToReconnect(object state)
        {
            UDPClient aTimer = (UDPClient)state;
            if(myServerConnection == null || myServerConnection.Peer.ConnectionsCount != 1)
            {
                IsUpdateServerActive?.Invoke(false);
                aTimer.isConnected = false;
                myServerConnection = myPeer.Connect(myEndpoint2);
            }
            else
            {
                IsUpdateServerActive?.Invoke(true);
                aTimer.isConnected = true;
                aTimer.OnConnect?.Invoke();
                
            }
        }

       
        public PacketFactory PacketFactory { get; } = new PacketFactory();
        public bool isConnected { get; private set; }

        public void AssignProcessor(PacketProcessor MyProc, Type T)
        {
            PacketFactory.AssignProcessor(MyProc, T);
        }

        public UDPClient()
        {
            //The code below is to prevent the server from blowing up.
        }

        BinaryFormatter mySendFormatter = new BinaryFormatter();
        MemoryStream myStream = new MemoryStream();

        public object Lock = new object();
        public event Action<bool> IsUpdateServerActive;

        //TODO: do this in other places
        public void Send(Packet v, NetDeliveryMethod DeliveryType)
        {
            lock(Lock)
            {
                var Message = myPeer.CreateMessage();
                mySendFormatter.Serialize(myStream, v);
                Message.Write(myStream.ToArray());
                myPeer.SendMessage(Message, myServerConnection, DeliveryType); //TODO: tweak this so it works for game and update
                myStream.SetLength(0);
                myStream.Position = 0;
            }
        }
    }
}
