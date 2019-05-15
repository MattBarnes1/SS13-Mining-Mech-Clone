using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using DedicatedServer.GameDataClasses.Entities;
using PacketData.Packets.PacketTypes;

namespace PacketData.Packets.Superclasses
{
    public class PlayerMapConnectionPacket : Packet
    {
        public PlayerMapConnectionPacket() : base()
        {
        }


        public void SetNewPlayerData(PlayerData myValue)
        {
            throw new NotImplementedException();
        }

        public PlayerData GetPlayerData()
        {
            throw new NotImplementedException();
        }
    }
}
