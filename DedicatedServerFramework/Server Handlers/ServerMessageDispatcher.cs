using System;
using System.Collections.Generic;
using System.Collections.Concurrent;
using System.Text;
using System.Net.Sockets;
using System.Threading;
using DedicatedServer.Packets;
using System.Net;
using DedicatedServer.GameDataClasses.Entities;
using PacketData.Packets.PacketTypes;
using GameData.GameDataClasses.Maps;
using System.Threading.Tasks;
using PacketData.Packets.Superclasses;
using System.Data.SqlClient;
using System.IO;
using PacketData.Packets.Superclasses.Player;
using System.Runtime.Serialization.Formatters.Binary;
using DedicatedServerFramework.Handlers;
using PacketData.Packets.Superclasses.Login;
using System.Diagnostics;
using ThreadState = System.Threading.ThreadState;
using PacketData.Packets.Superclasses.Map_Loading;
using PacketData.UDPServiceHandler;
using PacketData.Packets.Acknowledge;
using Lidgren.Network;
using DedicatedServerFramework.Server_Handlers;
using GameData.Packets.ChatFolder;
using GameData.Packets.Messaging;

namespace DedicatedServerFramework.Servers
{

    public class ServerMessageDispatcher
    {
        MapServerHandler myMapHandler = new MapServerHandler();
        LoginServerHandler myLoginHandler = new LoginServerHandler();
        UDPServer myServer = new UDPServer();
        public ServerMessageDispatcher(int PortNumber)
        {
            LoadProcessors();
            myServer.StartServer(PortNumber);
        }

        public void LoadProcessors()
        {
            myServer.AssignProcessor(delegate (Packet P)
            {
                LoginPacket myPacket = (LoginPacket)P;     
                Console.WriteLine(myPacket.Username + " attempted to connect!");
                IConnectionWrapper myData;
                if (myLoginHandler.VerifyPlayerLogin(myPacket.Username, myPacket.Sender, myPacket.GetCreationTime(), myPacket.GetSHA(), out myData))
                {
                    var PDataInfo = (PlayerData)myData.MyExternalData;
                    if (PDataInfo != null)
                    {
                        PlayerInfoPacket myInfo = new PlayerInfoPacket();
                        myInfo.NewChar = false;
                        myInfo.myMap = myMapHandler.GetPlayerMap(PDataInfo);
                        myInfo.Sender = myPacket.Sender;
                        myServer.Send(myInfo, NetDeliveryMethod.ReliableOrdered);
                        myMapHandler.Login(myData);
                    }
                    else
                    { 
                        ThreadPool.QueueUserWorkItem(delegate (object State)
                        {                          
                            PlayerInfoPacket myInfo = new PlayerInfoPacket();
                            myInfo.Sender = myPacket.Sender;
                            PDataInfo = new PlayerData();
                            myInfo.NewChar = true;
                            myInfo.SetPlayerData(PDataInfo);
                            myInfo.myMap = myMapHandler.PlaceInFirstAvailable(myInfo.Sender, PDataInfo);
                            PDataInfo.CurrentMapID = PDataInfo.CurrentMapID;
                            myLoginHandler.UpdateInfo(myPacket.GetSHA(), PDataInfo);
                            myServer.Send(myInfo, NetDeliveryMethod.ReliableOrdered);                           
                        });
                    }
                    Console.WriteLine("User Authenticated Successfully!\n Awaiting Character Creation!");
                }
                else
                {
                    LoginFailedPacket myAck = new LoginFailedPacket();
                    myAck.Sender = myPacket.Sender;
                    myServer.Send(myAck, NetDeliveryMethod.ReliableOrdered);
                    Console.WriteLine("User Authentication Failed!");
                }
            }, typeof(LoginPacket));

            myServer.AssignProcessor(delegate (Packet P)
            {
                IConnectionWrapper mySender;
                if ((mySender = myLoginHandler.isLoggedIn(P.UserID)) != null)
                {
                    AnnounceToMapPacket myPacket = (AnnounceToMapPacket)P;
                }
            }, typeof(AnnounceToMapPacket));

            myServer.AssignProcessor(delegate (Packet P)
            {
                IConnectionWrapper mySender;
                if ((mySender = myLoginHandler.isLoggedIn(P.UserID)) != null)
                {
                    IConnectionWrapper Recipient;
                    WhisperMessagePlayer myPacket = (WhisperMessagePlayer)P;
                    if ((Recipient = myLoginHandler.isLoggedIn(myPacket.RecipientUserID)) != null)
                    {
                        PlayerData Recip = (PlayerData)Recipient.MyExternalData;
                        PlayerData Sender = (PlayerData)mySender.MyExternalData;
                        if (Recip.CurrentMapID.CompareTo(Sender.CurrentMapID) == 0)
                        {
                            myPacket.Sender = Recipient.GetConnection();
                            myServer.Send(myPacket, NetDeliveryMethod.ReliableOrdered);
                        }
                    }
                    else
                    {
                        //TODO: send response packet.
                    }
                }
            }, typeof(WhisperMessagePlayer));

            myServer.AssignProcessor(delegate (Packet P)
            {
                IConnectionWrapper mySender;
                if ((mySender = myLoginHandler.isLoggedIn(P.UserID)) != null)
                {
                    SendMessageLocalAreaPacket myPacket = (SendMessageLocalAreaPacket)P;
                }
            }, typeof(SendMessageLocalAreaPacket));

            myServer.AssignProcessor(delegate (Packet P)
            {
                IConnectionWrapper mySender;
                if ((mySender = myLoginHandler.isLoggedIn(P.UserID)) != null)
                {
                    SendMessageMapAreaPacket myPacket = (SendMessageMapAreaPacket)P;
                    //List<PlayerData> myData = myMapHandler.GetPlayersOnMap();
                   
                }
            }, typeof(SendMessageMapAreaPacket));

            myServer.AssignProcessor(delegate (Packet P)
            {
                IConnectionWrapper mySender;
                if ((mySender = myLoginHandler.isLoggedIn(P.UserID)) != null)
                {
                    AnnounceToAllMapsPacket myPacket = (AnnounceToAllMapsPacket)P;
                }
            }, typeof(AnnounceToAllMapsPacket));


            myServer.AssignProcessor(delegate (Packet P)
            {
                IConnectionWrapper Wrapper;
                if ((Wrapper = myLoginHandler.isLoggedIn(P.UserID)) != null)
                {
                   
                    myLoginHandler.Logout(P.UserID);
                    myMapHandler.Logout(Wrapper);
                }
            }, typeof(DisconnectPacket));

            myServer.AssignProcessor(delegate (Packet P)
            {
                IConnectionWrapper myValue = myLoginHandler.isLoggedIn(P.UserID);
                if (myValue!= null)
                {
                    Map myMap = myMapHandler.GetPlayerMap((PlayerData)myValue.MyExternalData);
                    foreach (NetConnection A in myMap.GetPlayerIPs())
                    {
                        PlayerMapConnectionPacket myNewPlayer = new PlayerMapConnectionPacket();
                        myNewPlayer.SetNewPlayerData((PlayerData)myValue.MyExternalData);
                        myNewPlayer.Sender = A;
                        myServer.Send(myNewPlayer, NetDeliveryMethod.ReliableOrdered);
                    }
                }
            }, typeof(MapLoadedPacket));
            myServer.AssignProcessor(delegate (Packet P)
            {
                CharacterCreationPacket Packet = ((CharacterCreationPacket)P);
                IConnectionWrapper myValue = myLoginHandler.isLoggedIn(Packet.UserID);
                if (myValue != null)
                {
                    ((PlayerData)myValue.MyExternalData).SetAnimation(Packet.GetAnimationInfo());
                    ((PlayerData)myValue.MyExternalData).SetName(Packet.GetName());
                    Map myMap = myMapHandler.GetPlayerMap(((PlayerData)myValue.MyExternalData));
                    foreach (NetConnection A in myMap.GetPlayerIPs())
                    {
                        PlayerMapConnectionPacket myNewPlayer = new PlayerMapConnectionPacket();
                        myNewPlayer.SetNewPlayerData(((PlayerData)myValue.MyExternalData));
                        myNewPlayer.Sender = A;
                        myServer.Send(myNewPlayer, NetDeliveryMethod.ReliableOrdered);
                    }
                }
            }, typeof(CharacterCreationPacket));
        }
    }
}
