using DedicatedServer.GameDataClasses.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameData.Packets.ChatFolder
{
    public class SendMessageLocalAreaPacket : MessagePacketClass
    {
        PlayerData[] ThoseThatHeardMe;
    }
}
