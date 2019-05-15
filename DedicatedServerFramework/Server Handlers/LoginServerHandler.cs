using DedicatedServer.GameDataClasses.Entities;
using DedicatedServerFramework.IO;
using DedicatedServerFramework.Servers;
using Lidgren.Network;
using PacketData.UDPServiceHandler;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace DedicatedServerFramework.Server_Handlers
{
    public class LoginServerHandler
    {
        SQLServerWrapper myWrapper = new SQLServerWrapper();
        ConcurrentDictionary<String, IConnectionWrapper> myUsernameToConnectionWrapper = new ConcurrentDictionary<String, IConnectionWrapper>();
        ConcurrentDictionary<byte[], String> myUserIDToUsernameString = new ConcurrentDictionary<byte[], String>();
        ThreadedFileHandler myFiles = new ThreadedFileHandler();
        public LoginServerHandler()
        {
            Directory.CreateDirectory("./Users");
        }

        public bool VerifyPlayerLogin(string Username, NetConnection Sender, long creationTime, byte[] OriginalSHA, out IConnectionWrapper myData)
        {

            if (myWrapper.VerifyUser(Username, creationTime, OriginalSHA))
            {
                PlayerData PlayerDataFile;
                if (File.Exists("./Users/" + Username + ".chr"))
                {
                    PlayerDataFile = myFiles.OpenReadClass<PlayerData>("./Users/" + Username + ".chr");
                }
                else
                {
                    PlayerDataFile = null;
                }
                var myWrapper = new IConnectionWrapper(PlayerDataFile, Sender, OriginalSHA);
                myUsernameToConnectionWrapper.AddOrUpdate(Username, myWrapper, OnLoginDataUpdate);
                myUserIDToUsernameString.AddOrUpdate(OriginalSHA, Username, updateUserIDFunction);
                myData = myWrapper;
                return true;
            }
            else
            {
                myData = null;
                return false;
            }
        }

        private string updateUserIDFunction(byte[] arg1, string arg2)
        {
            return arg2;
        }

        private IConnectionWrapper OnLoginDataUpdate(string arg1, IConnectionWrapper arg2)
        {
            return arg2;
        }

        internal void UpdateInfo(byte[] v, PlayerData myData)
        {
            String Value;
            if (myUserIDToUsernameString.TryGetValue(v, out Value))
            {
                IConnectionWrapper myWrapper;
                if (myUsernameToConnectionWrapper.TryGetValue(Value, out myWrapper))
                {
                    myWrapper = new IConnectionWrapper(myData, myWrapper.myConnection, myWrapper.GetUserID());
                    myUsernameToConnectionWrapper.AddOrUpdate(Value, myWrapper, OnLoginDataUpdate);
                }
            }
        }

        internal IConnectionWrapper isLoggedIn(byte[] userID)
        {
            String Value;
            if (myUserIDToUsernameString.TryGetValue(userID, out Value))
            {
                IConnectionWrapper myWrapper;
                if (myUsernameToConnectionWrapper.TryGetValue(Value, out myWrapper))
                {
                    return myWrapper;
                }
            }
            return null;
        }

        internal void Logout(byte[] userID)
        {
            String Username;
            if (myUserIDToUsernameString.TryGetValue(userID, out Username))
            {
                IConnectionWrapper myWrapper;
                if (myUsernameToConnectionWrapper.TryRemove(Username, out myWrapper))
                {
                    SavePlayerData(Username, (PlayerData)myWrapper.MyExternalData);
                }
            }
        }

        private void SavePlayerData(string username, PlayerData myExternalData)
        {

        }
    }
}
