using System;
using GameData.GameDataClasses.Maps;
using System.Collections.Concurrent;
using DedicatedServer.GameDataClasses.Entities;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net;
using Lidgren.Network;
using System.IO;
using PacketData.UDPServiceHandler;

namespace DedicatedServerFramework.Servers
{
    public class MapServerHandler
    {
        ConcurrentDictionary<String, Map> myLoadedMaps = new ConcurrentDictionary<String, Map>();
        ConcurrentStack<Map> UnderPlayerAmount = new ConcurrentStack<Map>();
        public const int MAX_CONNECTIONS = 120;
        MapIntialGeneration myGenerator = new MapIntialGeneration();

        public MapServerHandler()
        {
            Directory.CreateDirectory("./Maps");
        }
        private object MapLock = new object();
        public Map PlaceInFirstAvailable(NetConnection myPlayerEnd, PlayerData myValue)
        {
            lock (MapLock)
            {
                Map myMap = null;
                while (UnderPlayerAmount.Count > 0)
                {
                    if (UnderPlayerAmount.TryPop(out myMap))
                    {
                        myMap.AddPlayer(myPlayerEnd, myValue);
                        return myMap;
                    }
                }

                if(myMap == null)
                {
                    myMap = BuildMap();
                }
                myValue.HomeMapID = myMap.GetID();
                myValue.CurrentMapID = myValue.HomeMapID;
                return myMap;
            }
        }
        public Map GetPlayerMap(PlayerData myValue)
        {
            Debug.Assert(!String.IsNullOrEmpty(myValue.HomeMapID));
            Map aMap;
            myLoadedMaps.TryGetValue(myValue.HomeMapID, out aMap);
            return aMap;
        }

        public Map BuildMap()
        {
            Map aMap = myGenerator.BuildMap(Guid.NewGuid().ToString());
            Debug.Assert(myLoadedMaps.TryAdd(aMap.GetID(), aMap));
            return aMap;
        }

        internal void Logout(IConnectionWrapper wrapper)
        {
            throw new NotImplementedException();
        }

        internal void Login(IConnectionWrapper myData)
        {
            throw new NotImplementedException();
        }
    }
}