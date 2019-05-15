using DedicatedServer.GameDataClasses.Entities;
using GameData.GameDataClasses.Maps;
using PacketData.Packets.PacketTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PacketData.Packets.Superclasses.Player
{
    [Serializable]
    public class PlayerInfoPacket : Packet
    {
        public PlayerInfoPacket( ) : base()
        {
        }

        PlayerData myData;
        public bool NewChar { get; set; }
        public Map myMap { get; set; }

        public void SetPlayerTransferData(PlayerData myData)
        {
            this.myData = myData;
        }

        public PlayerData GetPlayerData()
        {
            return this.myData;
        }

        public void SetPlayerData(PlayerData myData)
        {
            this.myData = myData;
        }
    }
}
