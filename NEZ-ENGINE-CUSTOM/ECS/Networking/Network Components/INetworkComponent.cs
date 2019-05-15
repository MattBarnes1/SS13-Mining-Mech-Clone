using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PacketData.Packets.PacketTypes;

namespace NEZ_ENGINE_CUSTOM.ECS.Networking.Network_Components
{
    public interface INetworkComponent
    {
        List<Packet> GetPacketsToSend();
        void AddReceivedPacket(Packet myPacket);
    }
}
