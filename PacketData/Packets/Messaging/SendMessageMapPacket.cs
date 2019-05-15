using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameData.Packets.ChatFolder
{
    public class SendMessageMapAreaPacket : MessagePacketClass
    {
        public short[] Restrictions = { 0x00 };

    }
}
