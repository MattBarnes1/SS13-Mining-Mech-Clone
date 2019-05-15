using Lidgren.Network;
using PacketData.Packets.PacketTypes;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DedicatedServerFramework.Servers
{
    public class ServerBaseClass
    {
        public enum ServerConnection
        {

        }

        NetClient myClient;
        public ServerBaseClass(int StartingPort)
        {
            NetPeerConfiguration myConfig = new NetPeerConfiguration("Server");
            myConfig.Port = StartingPort;
            myServer = new NetServer(myConfig);


            SynchronizationContext.SetSynchronizationContext(new SynchronizationContext());
        }

        BinaryFormatter mySendFormatter = new BinaryFormatter();
        MemoryStream myStream = new MemoryStream();
        protected NetServer myServer;

        //TODO: do this in other places
        public void Send(Packet v, NetConnection ToThis, NetDeliveryMethod DeliveryType)
        {
            var SendMessage = myServer.CreateMessage();
            mySendFormatter.Serialize(myStream, v);
            SendMessage.Write(myStream.ToArray());
            myServer.SendMessage(SendMessage, ToThis, DeliveryType); //TODO: tweak this so it works for game and update
            myStream.SetLength(0);
            myStream.Position = 0;
        }
        NetConnection MapClient;
        //TODO: do this in other places
        public void SendMapServer(Packet v, NetDeliveryMethod DeliveryType)
        {
            var SendMessage = myServer.CreateMessage();
            mySendFormatter.Serialize(myStream, v);
            SendMessage.Write(myStream.ToArray());
            myServer.SendMessage(SendMessage, MapClient, DeliveryType); //TODO: tweak this so it works for game and update
            myStream.SetLength(0);
            myStream.Position = 0;
        }

    }
}
