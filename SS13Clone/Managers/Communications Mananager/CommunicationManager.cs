using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using DedicatedServer.GameDataClasses.Entities;
using DedicatedServer.Packets;
using GameData.GameDataClasses.Maps;
using GameData.VersionHandling;
using Lidgren.Network;
using Microsoft.Xna.Framework;
using PacketData.Packets.Acknowledge;
using PacketData.Packets.PacketTypes;
using PacketData.Packets.Superclasses;
using PacketData.Packets.Superclasses.Login;
using PacketData.Packets.Superclasses.Map_Loading;
using PacketData.Packets.Superclasses.Player;
using PacketData.UDPServiceHandler;

namespace SS13Clone.Managers
{
    public class CommunicationManager : UDPClient
    {
        public CommunicationManager()
        {
            StartClient(1000, "107.9.185.247", 1400);
        }

       
        public void SendLoginPacket(LoginPacket myPacket, byte[] UserUneditedSHA)
        {
            InternalSettings.UserID = myPacket.UserID;
            base.Send(myPacket, NetDeliveryMethod.ReliableOrdered);
        }      

    }
}
