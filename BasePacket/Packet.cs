using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using GameData.VersionHandling;
using Lidgren.Network;

namespace PacketData.Packets.PacketTypes
{
    [Serializable]
    public abstract class Packet
    {
        long myCreation;
        public Packet()
        {
            myCreation = DateTime.Now.ToBinary();
            UserID = InternalSettings.UserID;
        }

        public delegate void PacketProcessor(Packet myPacket);
        [NonSerialized]
        PacketProcessor myProcess;
        [NonSerialized]
        public NetConnection Sender;

        public IPEndPoint ReceivedFrom { get; set; }
        public string EntityID { get; set; }
        public byte[] UserID;

        public void Process()
        {
            if (myProcess != null) myProcess(this);
        }

        public void SetProcessor(PacketProcessor myProcess)
        {
            this.myProcess = myProcess;
        }
       
        public long GetCreationTime()
        {
            return myCreation;
        }

    }
}
