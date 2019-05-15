using DedicatedServerFramework.Servers;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace DedicatedServerFramework.Handlers
{
    public static class ServerConsole
    {
        static bool PrintServerLog;
        public static void PrintLine(String myString)
        {
            if(PrintServerLog)
            {
                Console.WriteLine(myString);
            }
        }

        public static void PrintMainMenuLoop()
        {
            string checkString = "";
            while(checkString.ToUpper().CompareTo("Q") != 0)
            {
                Console.WriteLine("l/L - to enable verbose logging");
                Console.WriteLine("a/A - Add User");
                checkString = Console.ReadLine();
                checkString = checkString.Trim();
                if(!String.IsNullOrEmpty(checkString))
                {
                    switch (checkString.ToUpper()[0])
                    {
                        case 'A':
                            AddUser();
                            break;
                        case 'L':
                            break;
                    }
                }                
            }
        }

        public static SQLServerWrapper myWrapper = new SQLServerWrapper();

        public static void AddUser()
        {
            string checkString = "";
            
            while (!CheckExitString(checkString))
            {
                String Username = "";
                while(true)
                {
                    Console.WriteLine("Username: (q to cancel)");
                    Username = Console.ReadLine();
                    Username = Username.Trim();
                    if (CheckExitString(Username))
                    {
                        return;
                    }
                    else if(myWrapper.IsValidUsername(Username))
                    {
                        break;
                    }
                }
                String Password = "";
                while (true)
                {
                    Console.WriteLine("Password: (q to cancel)");
                    Password = Console.ReadLine();
                    Password = Password.Trim();
                    if (CheckExitString(Password))
                    {
                        return;
                    }
                    else if (myWrapper.IsValidPassword(Password))
                    {
                        myWrapper.AddUser(Username, Password);
                        break;
                    }
                }
                checkString = checkString.Trim();
            }
        }
        

        private static bool CheckExitString(string checkString)
        {
            return checkString.Length == 1 && checkString.ToUpper().CompareTo("Q") == 0;
        }

        public static void PrintLoopForLogging()
        {
            string checkString = "";
            while (checkString.ToUpper().CompareTo("Q") != 0)
            {
                Console.WriteLine("q - to return");
                PrintServerLog = true;
                checkString = Console.ReadLine();
                checkString = checkString.Trim();
            }
            PrintServerLog = false;
        }



    }
}
