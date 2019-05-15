using PacketData.Packets.PacketTypes;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PacketData.Packets.Superclasses
{
    [Serializable]
    public class AckPacket : Packet
    {
        public AckPacket() : base()
        {

        }
    }
}
