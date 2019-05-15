using DedicatedServerFramework.Handlers;
using DedicatedServerFramework.Servers;
using System;


namespace DedicatedServer
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                ServerMessageDispatcher mapServer = new ServerMessageDispatcher(1400);
                ClientUpdateHandler myHandler = new ClientUpdateHandler(1401);
                ServerConsole.PrintMainMenuLoop();
            } catch(Exception e)
            {
                String ToWrite = "Origin: " + e.Source + "Target: " + e.TargetSite + "Message: " + e.Message + "\nSource: " + e.Source + "\nStack: " + e.StackTrace;
                while (e.InnerException != null)
                {
                    e = e.InnerException;
                    ToWrite += "\n" + "Origin: " + e.Source + "Target: " + e.TargetSite + "Message: " + e.Message + "\nSource: " + e.Source + "\nStack: " + e.StackTrace;
                }
                LogHandler.LogHandler<Program>.WriteToFile("Server Error:", LogHandler.LogHandler<Program>.LoggerType.EXCEPTION_FATAL, ToWrite);
            }
            }
        }
    }
