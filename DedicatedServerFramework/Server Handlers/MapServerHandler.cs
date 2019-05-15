using System;
using GameData.GameDataClasses.Maps;
using System.Collections.Concurrent;
using DedicatedServer.GameDataClasses.Entities;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net;
using Lidgren.Network;
using GameData.Packets.Player.Chat;
using GameData.Networking.Packets.Player.Communication;
using System.Threading;
using GameData.VersionHandling;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using PacketData.Packets.PacketTypes;
using LogHandler;

namespace DedicatedServerFramework.Servers
{
    public class MapServerHandler : ServerBaseClass
    {
        ConcurrentDictionary<String, Map> myLoadedMaps = new ConcurrentDictionary<String, Map>();
        ConcurrentDictionary<String, List<PlayerData>> MapToPlayersLookup = new ConcurrentDictionary<string, List<PlayerData>>();

        ConcurrentStack<Map> UnderPlayerAmount = new ConcurrentStack<Map>();
        public const int MAX_CONNECTIONS = 120;
        MapIntialGeneration myGenerator = new MapIntialGeneration();

        public MapServerHandler() : base(InternalSettings.myLoginServerPort.Port)
        {
            myServer.RegisterReceivedCallback(OnReceived, SynchronizationContext.Current);
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
                    short myPacketID = myMessage.PeekInt16();

                    Packet myPacket = (Packet)mySendFormatter.Deserialize(myStream);
                    myPacket.Sender = myMessage.SenderConnection;

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
                    LogHandler<MapServerHandler>.WriteToFile("Lidgren Warning: ", LogHandler<MapServerHandler>.LoggerType.WARNING, myError);
                    break;
                case NetIncomingMessageType.DebugMessage:
                    myError = myMessage.ReadString();
                    LogHandler<MapServerHandler>.WriteToFile("Lidgren Warning: ", LogHandler<MapServerHandler>.LoggerType.WARNING, myError);
                    break;
                case NetIncomingMessageType.WarningMessage:
                    myError = myMessage.ReadString();
                    LogHandler<MapServerHandler>.WriteToFile("Lidgren Error: ", LogHandler<MapServerHandler>.LoggerType.WARNING, myError);
                    break;
                case NetIncomingMessageType.ErrorMessage:
                    myError = myMessage.ReadString();
                    LogHandler<MapServerHandler>.WriteToFile("Lidgren Error: ", LogHandler<MapServerHandler>.LoggerType.EXCEPTION, myError);
                    break;
                case NetIncomingMessageType.NatIntroductionSuccess:
                    break;
                case NetIncomingMessageType.ConnectionLatencyUpdated:
                    break;
            }
        }

        private object MapLock = new object();
        private LoginServer myPlayerDataServer;
        private ServerHandler serverHandler;

        public Map PlaceInFirstAvailable(NetConnection myPlayerEnd, PlayerData myValue)
        {
            lock (MapLock)
            {
                Map myMap = null;
                while (UnderPlayerAmount.Count > 0)
                {
                    if (UnderPlayerAmount.TryPop(out myMap))
                    {
                        return myMap;
                    }
                }

                if(myMap == null)
                {
                    myMap = BuildMap();
                }
                myValue.MapID = myMap.GetID();
                return myMap;
            }
        }

        

        public Map GetPlayerMap(PlayerData myValue)
        {
            Debug.Assert(!String.IsNullOrEmpty(myValue.MapID));
            Map aMap;
            myLoadedMaps.TryGetValue(myValue.MapID, out aMap);
            return aMap;
        }
        public Map BuildMap()
        {
            Map aMap = myGenerator.BuildMap(Guid.NewGuid().ToString());
            Debug.Assert(myLoadedMaps.TryAdd(aMap.GetID(), aMap));
            return aMap;
        }

        public Map GetMap(string toMapID)
        {
            Map myMap;
            if (!myLoadedMaps.TryGetValue(toMapID, out myMap))
            {
                Console.WriteLine("Warning! Map by ID: " + toMapID + "could not be found!");
                return null;
            }
            return myMap;
        }

        internal void SendMapMessage(SendWhisperPacket g)
        {
            throw new NotImplementedException();
        }

        internal void SendMapMessage(SendLocalChatPacket g)
        {
            throw new NotImplementedException();
        }

        internal void AnnouncePlayer(string mapID, PlayerData playerData)
        {
            List<PlayerData> PlayersList;
            if(MapToPlayersLookup.TryGetValue(mapID, out PlayersList))
            {
                foreach(PlayerData A in PlayersList)
                {
                    SendGlobalMapChatPacket myLocal = new SendGlobalMapChatPacket();
                    NetConnection myReturnedNetConnect = myPlayerDataServer.GetNetConnectionFromPlayerData(playerData);
                    if(myReturnedNetConnect != null)
                    {
                        myLocal.Message = playerData.Playername + " has awaken from stasis!";
                        myLocal.Sender = myReturnedNetConnect;
                        //serverHandler.SendPacket(myLocal);
                    }
                }
            }
        }


        internal void UpdatePlayer(NetConnection sender, PlayerData playerData, bool announce = true)
        {
            if(playerData.MapID != null)
            {
                Map myMap = null;
                myLoadedMaps.TryGetValue(playerData.MapID, out myMap);
                if(myMap != null)
                {
                    if (announce)
                        AnnouncePlayer(playerData.MapID, playerData);
                }
            }
        }

        internal void DisconnectPlayer(byte[] userID)
        {
            throw new NotImplementedException();
        }
    }
}