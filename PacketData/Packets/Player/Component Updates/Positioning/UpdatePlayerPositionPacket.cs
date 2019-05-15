using PacketData.Packets.PacketTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PacketData.Packets.Superclasses.Player
{
    [Serializable]
    public class UpdatePlayerPositionPacket : Packet
    {
        public UpdatePlayerPositionPacket( ) : base()
        {
        }

    }
}
