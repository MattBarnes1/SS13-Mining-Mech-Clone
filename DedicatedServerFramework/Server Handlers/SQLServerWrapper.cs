using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace DedicatedServerFramework.Servers
{
    public class SQLServerWrapper
    {
        SqlConnection ConnectToDatabase = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=D:\SS13\DedicatedServerFramework\UsersDatabase.mdf;Integrated Security=True");
        private object Command1 = new object();
        private object Command2 = new object();
        public SQLServerWrapper()
        {
            
            ConnectToDatabase.Open();
        }

        public bool IsValidPassword(string v)
        {
            if (v.Length >= 8)
            {
                if (v.Contains("#") || v.Contains("~") || v.Contains("!") || v.Contains("@")
                    || v.Contains("#") || v.Contains("$") || v.Contains("%") ||
                    v.Contains("^") || v.Contains("&") || v.Contains("*") || v.Contains("(") ||
                    v.Contains(")") || v.Contains("-") || v.Contains("=") || v.Contains("~") ||
                    v.Contains("!") || v.Contains("@") || v.Contains("1") || v.Contains("2") ||
                    v.Contains("3") || v.Contains("4") || v.Contains("5") || v.Contains("6") ||
                    v.Contains("7") || v.Contains("8") || v.Contains("9") || v.Contains("0"))
                {
                    return true;
                }
            }
            return false;
        }

        public byte[] GetUserPasswordHash(string username)
        {
            lock(Command2) //TODO: put in locks again!
            {
                SqlCommand myCommand = ConnectToDatabase.CreateCommand();
                myCommand.CommandText = "SELECT * FROM UsersTable where Username=@Username";
                myCommand.Parameters.Add(new SqlParameter("Username", username));
                SqlDataReader myReader = myCommand.ExecuteReader();
                bool Result = myReader.HasRows;
                byte[] myByteBuffer = null;
                if (Result)
                {
                    myByteBuffer = new byte[32];
                    myReader.Read();
                    myReader.GetBytes(2, 0, myByteBuffer, 0, 32);
                }
                myReader.Close();
                return myByteBuffer;
            }
        }

        public void AddUser(string username, string password)
        {
            SqlCommand myCommand = ConnectToDatabase.CreateCommand();
            myCommand.CommandText = "INSERT INTO UsersTable(Username, Hash) VALUES (@Username, @Hash)";
            myCommand.Parameters.Add(new SqlParameter("Username", username));
            SHA256 myInstance = SHA256Cng.Create();
            MemoryStream mYStream = new MemoryStream();
            byte[] ID = Encoding.ASCII.GetBytes(username);
            mYStream.Write(ID, 0, ID.Length);
            ID = Encoding.ASCII.GetBytes(password);
            mYStream.Write(ID, 0, ID.Length);
            byte[] byteReturn = myInstance.ComputeHash(mYStream.ToArray());
            mYStream = new MemoryStream(byteReturn);
            myCommand.Parameters.Add(new SqlParameter("Hash", mYStream.ToArray()));
            Console.Write("Lines affected : " + myCommand.ExecuteNonQuery());
            Console.WriteLine("HASH: " + BitConverter.ToString(mYStream.ToArray()));


        }

        public bool IsValidUsername(string v)
        {
            if (v.Length >= 4)
            {
                SqlCommand myCommand = ConnectToDatabase.CreateCommand();
                myCommand.CommandText = "SELECT * FROM UsersTable where Username=@Username";
                myCommand.Parameters.Add(new SqlParameter("Username", v));
                if (ConnectToDatabase.State == System.Data.ConnectionState.Closed)
                {
                    ConnectToDatabase.Open();
                }
                SqlDataReader myReader = myCommand.ExecuteReader();
                bool Result = myReader.HasRows;
                myReader.Close();
                if (!Result)
                {
                    return true;
                }
                ConnectToDatabase.Close();
            }
            return false;
        }

        public bool VerifyUser(string username, long dateTime, byte[] v)
        { //TODO Lock this
            lock(Command1)
            {
                SHA256 mySha = SHA256Cng.Create();
                byte[] OriginalHash = GetUserPasswordHash(username);
                if (OriginalHash == null)
                {
                    Console.WriteLine("Unregistered user attempted a connection!");
                    return false;
                }
                Console.WriteLine("SQL Server Hash Recv:" + BitConverter.ToString(OriginalHash));
                if (OriginalHash == null) return false;
                long myBinData = dateTime;
                byte[] myBytes = BitConverter.GetBytes(myBinData);
                Console.WriteLine("CLIENT HASH:" + BitConverter.ToString(v));
                Console.WriteLine("TIME STAMP RECEIVED:" + BitConverter.ToString(myBytes));
                byte[] FinalBytes = new byte[OriginalHash.Length + myBytes.Length];
                Array.Copy(myBytes, FinalBytes, myBytes.Length);
                Array.Copy(OriginalHash, 0, FinalBytes, myBytes.Length, OriginalHash.Length);
                Console.WriteLine("PREHASH:" + BitConverter.ToString(FinalBytes));
                byte[] myFinal = mySha.ComputeHash(FinalBytes);
                Console.WriteLine("GENERATED HASH:" + BitConverter.ToString(myFinal));
                for (int i= 0;i < myFinal.Length; i++)
                {
                    if(myFinal[i] != v[i])
                    {
                        return false;
                    }
                }
                return true;
                
            }
        }
    }
}
