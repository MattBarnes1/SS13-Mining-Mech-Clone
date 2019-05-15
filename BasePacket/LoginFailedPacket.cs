using PacketData.Packets.PacketTypes;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PacketData.Packets.Superclasses.Login
{
    [Serializable]
    public class LoginFailedPacket : Packet
    {
        public LoginFailedPacket() : base()
        {
        }
       
    }
}
