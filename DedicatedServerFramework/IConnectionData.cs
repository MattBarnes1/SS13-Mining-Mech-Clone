using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DedicatedServer.GameDataClasses.Entities;
using Lidgren.Network;

namespace PacketData.UDPServiceHandler
{
    public class IConnectionWrapper
    {
        public object MyExternalData { get; }
        byte[] UserID;

        public IConnectionWrapper(object myData)
        {
            MyExternalData = myData;
        }

        public IConnectionWrapper(object myData, NetConnection myConnector,  byte[] p)
        {
            myConnection = myConnector;
            this.MyExternalData = myData;
            this.UserID = p;
        }
        public NetConnection myConnection { get; private set; }
        DateTime LastConnection = DateTime.Now;
        private PlayerData myData;
        private object p;

        public DateTime GetLastReceivedPacketTime()
        {
            return LastConnection;
        }
        public void UpdateLastPacketTime()
        {
            LastConnection = DateTime.Now;
        }
        public byte[] GetUserID()
        {
            return UserID;
        }

        internal NetConnection GetConnection()
        {
            throw new NotImplementedException();
        }
    }
}
