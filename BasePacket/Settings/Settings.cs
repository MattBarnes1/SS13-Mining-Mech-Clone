using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace GameData.VersionHandling
{
    public class InternalSettings
    {
        public static readonly string Version = "0.00.5a";
        public static readonly IPEndPoint myServerAddress = new IPEndPoint(IPAddress.Parse("107.9.185.247"), 1400);
        public static readonly IPEndPoint myMapServerPort = new IPEndPoint(IPAddress.Parse("107.9.185.247"), 1402);
        public static readonly IPEndPoint myLoginServerPort = new IPEndPoint(IPAddress.Parse("107.9.185.247"), 1403);
        public static readonly IPEndPoint myUpdateServerPort = new IPEndPoint(IPAddress.Parse("107.9.185.247"), 1401);
        public static byte[] UserID;

        public static string Username { get; internal set; }
    }
}
