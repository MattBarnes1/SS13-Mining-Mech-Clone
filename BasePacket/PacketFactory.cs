using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Linq;
using PacketData.Packets.PacketTypes;
using static PacketData.Packets.PacketTypes.Packet;
using System.Diagnostics;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

namespace DedicatedServer.Packets
{
    public class PacketFactory
    {
        ConcurrentDictionary<int, Type> myPacketsToCreate;
        ConcurrentDictionary<Type, PacketProcessor> myPacketProcessesor;
        public PacketFactory()
        {
            CreatePacketTable();
        }

        private void CreatePacketTable()
        {
            myPacketsToCreate = new ConcurrentDictionary<int, Type>();
            myPacketProcessesor = new ConcurrentDictionary<Type, PacketProcessor>();
            var Results = typeof(Packet).Assembly.GetTypes().Where(delegate (Type Q)
            {
                return Q.IsSubclassOf(typeof(Packet));
            });
            var myIterator = Results.GetEnumerator();
            Type[] myEmpty = { };
            while (myIterator.MoveNext())
            {
                //var RetVal = (Packet)myIterator.Current.GetConstructor(myEmpty).Invoke(null);
               // Debug.Assert(myPacketsToCreate.TryAdd(RetVal.GetPacketIntID(), myIterator.Current));
                myPacketProcessesor.TryAdd(myIterator.Current, null);
            }

        }
        /// <summary>
        /// Assigns the PacketProcessor to a packet of Type myType.
        /// </summary>
        /// <param name="myProcessor"></param>
        /// <param name="myType"></param>
        public void AssignProcessor(PacketProcessor myProcessor, Type myType)
        {
            myPacketProcessesor.AddOrUpdate(myType, myProcessor, onUnknownPacketAdded);
        }

        private PacketProcessor onUnknownPacketAdded(Type arg1, PacketProcessor arg2)
        {
            return arg2;
        }

        byte[] PacketData = new byte[4];
        BinaryFormatter myFormatter = new BinaryFormatter();
       
        public void SetProcessor(Packet myPacket)
        {
            PacketProcessor myValue;
            if (myPacketProcessesor.TryGetValue(myPacket.GetType(), out myValue))
            {
                myPacket.SetProcessor(myValue);
            }
        }
    }
}
