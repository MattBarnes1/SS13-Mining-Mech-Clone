using System;
using System.Threading;

namespace SS13Clone
{
    /// <summary>
    /// The main class.
    /// </summary>
    public static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            try
            {
                using (var game = new Game1())
                    game.Run();
            }
            catch (Exception e)
            {
                String ToWrite = "Origin: " + e.Source + "Target: " + e.TargetSite + "Message: " + e.Message + "\nSource: " + e.Source + "\nStack: " + e.StackTrace;
                while (e.InnerException != null)
                {
                    e = e.InnerException;
                    ToWrite += "\n" + "Origin: " + e.Source + "Target: " + e.TargetSite + "Message: " + e.Message + "\nSource: " + e.Source + "\nStack: " + e.StackTrace;
                }
                LogHandler.LogHandler<Game1>.WriteToFile("Server Error:", LogHandler.LogHandler<Game1>.LoggerType.EXCEPTION_FATAL, ToWrite);
            }
#if DEBUG

#else

#endif

        }
    }
}
