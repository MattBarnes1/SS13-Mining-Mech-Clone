using DedicatedServer.GameDataClasses.Entities;
using GameData.GameDataClasses.AnimationData;
using PacketData.Packets.PacketTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace PacketData.Packets.Superclasses.Player
{
    public class CharacterCreationPacket : Packet
    {
        public CharacterCreationPacket() : base()
        {
        }

        public Animation GetAnimationInfo()
        {
            throw new NotImplementedException();
        }

        public string GetName()
        {
            throw new NotImplementedException();
        }

        public void SetPlayerData(PlayerData myData)
        {
            throw new NotImplementedException();
        }
    }
}
