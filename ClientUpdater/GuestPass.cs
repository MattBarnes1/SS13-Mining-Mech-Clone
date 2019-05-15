using GameData.Packets.Update_Program;
using PacketData.Packets.PacketTypes;
using PacketData.Packets.Superclasses.Login;
using PacketData.UDPServiceHandler;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Security.Cryptography;
using System.Windows.Forms;

namespace ClientUpdater
{
    public partial class GuestPass : Form
    {
        private byte[] mySHAHash;

        private bool isRunning = true;
        public string Username { get; private set; }        

        private UDPClient myServerConnection;
        public GuestPass()
        {
            myServerConnection = new UDPClient();
            base.HandleDestroyed += myServerConnection.OnExit;
            myServerConnection.AssignProcessor(delegate (Packet P) {
                FileDeletePacket myPacket = (FileDeletePacket)P;
                File.Delete("./Game/" + myPacket.myEntry.Location.Remove(0, 2));
            }, typeof(FileDeletePacket));
            myServerConnection.AssignProcessor(delegate (Packet P)
            {
                Action myAction = new Action(FailedToLogin);
                Status.Invoke(myAction);
            }, typeof(LoginFailedPacket));




            myServerConnection.AssignProcessor(delegate (Packet P) {
                FileUpdatePacket myPacket = (FileUpdatePacket)P;
                Action myAction2;
                Action<int> myAction3 = new Action<int>(setTotal);
                Status.Invoke(myAction3, myPacket.TotalFiles);
                if (myPacket.TotalFiles == 0)
                {
                    myAction2 = new Action(Finish);
                    return;
                }
                else
                {
                    myAction2 = new Action(incFinish);
                }
                String Data = Path.Combine("Game", myPacket.myEntry.GetDirectoryStripped());
                Directory.CreateDirectory(Data.Remove(Data.LastIndexOf(Path.DirectorySeparatorChar)));
                File.WriteAllBytes(Data, myPacket.myBytes);
                Status.Invoke(myAction2);
                var myNextPacket = new GetNextFilePacket();
                myNextPacket.Username = Username;
                myServerConnection.Send(myNextPacket, Lidgren.Network.NetDeliveryMethod.ReliableOrdered);
            }, typeof(FileUpdatePacket));

            InitializeComponent();
        }

        int? Packets;

        private void setTotal(int aTotal)
        {
            if (!Total.HasValue)
            {
                if(aTotal != 0)
                {
                    Total = aTotal;
                    DownloadProgress.Maximum = aTotal;
                }
            }
        }

        private void FailedToLogin()
        {
            Status.Text = "Login Failed!";
        }

        private void Finish()
        {
            Status.Text = "Completed!";

        }

        private void incFinish()
        {
            Finished++;
            if(Total.HasValue)
            {
                Status.Text = Finished + " / " + Total + " files";
                DownloadProgress.Value = Finished;
                if(Finished == Total)
                {
                    Finish();
                }
            }
        }

        int Finished = 0;
        int? Total;
        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void folderBrowserDialog1_HelpRequest(object sender, EventArgs e)
        {

        }
        private void LoginButton_Click(object sender, EventArgs e)
        {
            String Password = PasswordText.Text;
            Username = UsernameTextBox.Text;
            LoginUpdatePacket myLogin = new LoginUpdatePacket();
            myLogin.CreateUsernamePasswordHash(Username, Password);
            Directory.CreateDirectory("./Game");
            if(myHash != null && myHash.Count != 0)
            {
                myHash.Clear();
            }
            RecurseIntoDirectory("./Game");
            myLogin.myEntries = myHash;
            myAction3 = new Action(OnConnected);
            myServerConnection.OnConnect += LidgrenConnectedCallback;
            myAction4 = new Action(OnDisconnected);
            myServerConnection.Send(myLogin, Lidgren.Network.NetDeliveryMethod.ReliableOrdered);
            Status.Text = "Connecting!";
        }

        private void ServerStatusUpdate(bool obj)
        {
            if(obj)
            {
                OnlineOfflineStatus.Text = "Online";
                OnlineOfflineStatus.ForeColor = Color.Green;
            }
            else
            {
                OnlineOfflineStatus.Text = "Offline";
                OnlineOfflineStatus.ForeColor = Color.Red;
            }
        }

        private void isServerActive(bool obj)
        {
            OnlineOfflineStatus.Invoke(myAction5, obj);
        }

        private void OnDisconnected()
        {
            Status.Text = "Disconnected!";
        }

        private void LidgrenDisconnectedCallback()
        {
            Status.Invoke(myAction4);
        }

        Action myAction4;
        Action myAction3;

        public void LidgrenConnectedCallback()
        {
            Status.Invoke(myAction3);
        }


        public void OnConnected()
        {
            Status.Text = "Connected!";
        }

        List<FileHashEntry> myHash = new List<FileHashEntry>();

        Dictionary<String, FileHashEntry> myEntry = new Dictionary<string, FileHashEntry>();

        SHA256 mySha = SHA256Cng.Create();
        private Action<bool> myAction5;

        public void RecurseIntoDirectory(String myDirectory)
        {
            foreach (string A in Directory.EnumerateDirectories(myDirectory))
            {
                RecurseIntoDirectory(A);
            }
            foreach (String B in Directory.EnumerateFiles(myDirectory))
            {
                FileHashEntry myFileHash = new FileHashEntry(mySha,B);
                myHash.Add(myFileHash);
            }
        }

        private void GuestPass_Load(object sender, EventArgs e)
        {
            myServerConnection.OnDisconnect += LidgrenDisconnectedCallback;
            myServerConnection.IsUpdateServerActive += isServerActive;
            myAction5 = new Action<bool>(ServerStatusUpdate);
            myServerConnection.SetTimeout(10);
            myServerConnection.StartClient(1303, "107.9.185.247", 1401);
        }
    }
}
