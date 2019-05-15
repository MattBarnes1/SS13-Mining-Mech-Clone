using PacketData.Packets.PacketTypes;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace GameData.Packets.Update_Program
{
    [Serializable]
    public class LoginUpdatePacket : Packet
    {
        public List<FileHashEntry> myEntries { get; set; }
        public LoginUpdatePacket() : base()
        {
        }

        byte[] mySHAHash;
        public string Username;

        public byte[] CreateUsernamePasswordHash(String Username, String Password)
        {
            this.Username = Username;
            SHA256 mySha = SHA256Cng.Create();
            MemoryStream mYStream = new MemoryStream();
            byte[] ID = Encoding.ASCII.GetBytes(Username);
            mYStream.Write(ID, 0, ID.Length);
            ID = Encoding.ASCII.GetBytes(Password);
            mYStream.Write(ID, 0, ID.Length);
            byte[] byteReturn = mySha.ComputeHash(mYStream.ToArray());
            mYStream = new MemoryStream();
            Debug.WriteLine("Created " + BitConverter.ToString(byteReturn));
            ID = BitConverter.GetBytes(base.GetCreationTime());
            Debug.WriteLine("TIME STAMP " + BitConverter.ToString(ID));
            mYStream.Write(ID, 0, ID.Length);
            mYStream.Write(byteReturn, 0, byteReturn.Length);
            Debug.WriteLine("PREHASH " + BitConverter.ToString(mYStream.ToArray()));
            mySHAHash = mySha.ComputeHash(mYStream.ToArray());
            Debug.WriteLine("GENERATED HASH " + BitConverter.ToString(mySHAHash));
            return byteReturn;
        }

        public byte[] GetSHA()
        {
            return mySHAHash;
        }
    }
}
