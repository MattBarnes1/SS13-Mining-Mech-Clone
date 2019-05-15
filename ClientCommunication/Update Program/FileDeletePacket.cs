using PacketData.Packets.PacketTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameData.Packets.Update_Program
{
    [Serializable]
    public class FileDeletePacket : Packet
    {
        public FileHashEntry myEntry { get; set; }
        public FileDeletePacket() : base()
        {

        }
    }
}
