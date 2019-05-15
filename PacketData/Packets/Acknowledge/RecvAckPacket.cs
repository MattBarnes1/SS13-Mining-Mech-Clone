using PacketData.Packets.PacketTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PacketData.Packets.Acknowledge
{
    [Serializable]
    public class RecvAckPacket : Packet
    {
        public RecvAckPacket() : base()
        {
        }
    }
}
