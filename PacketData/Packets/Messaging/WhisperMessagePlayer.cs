using DedicatedServer.GameDataClasses.Entities;
using GameData.Packets.ChatFolder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameData.Packets.Messaging
{
    public class WhisperMessagePlayer : MessagePacketClass
    {
        public byte[] RecipientUserID;
    }
}
