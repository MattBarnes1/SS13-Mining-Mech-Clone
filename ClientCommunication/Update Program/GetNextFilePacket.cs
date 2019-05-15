using PacketData.Packets.PacketTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameData.Packets.Update_Program
{
    [Serializable]
    public class GetNextFilePacket : Packet
    {
        public String Username {get; set;}
        public GetNextFilePacket() : base()
        {

        }

    }
}
