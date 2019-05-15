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
    public class DisconnectPacket : Packet
    {
        public string Username;

        byte[] SHAToDisconnect { get; set; }
        public DisconnectPacket(String Username) : base()
        {
            this.Username = Username;
        }
       
    }
}
