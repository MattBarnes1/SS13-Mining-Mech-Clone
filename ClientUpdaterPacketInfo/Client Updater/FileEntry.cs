using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace GameData.Packets.Update_Program
{
    [Serializable]
    public class FileHashEntry
    {
        public byte[] myHash { get; }
        public string Location { get; protected set; }

        public String GetDirectoryStripped()
        {
            return Location.Remove(0, Location.IndexOf("\\")+1);
        }

        public FileHashEntry(SHA256 mySha, String Location)
        {
            this.Location = Location;
            byte[] myFile = File.ReadAllBytes(Location);
            this.myHash = mySha.ComputeHash(myFile);            
        }     
    }
}
