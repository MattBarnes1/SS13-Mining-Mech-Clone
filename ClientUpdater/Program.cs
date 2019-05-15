using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ClientUpdater
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            try
            {
                Application.Run(new GuestPass());
            }
            catch (Exception e)
            {
                String ToWrite = "Origin: " + e.Source + "Target: " + e.TargetSite + "Message: " + e.Message + "\nSource: " + e.Source + "\nStack: " + e.StackTrace;
                while(e.InnerException != null)
                {
                    e = e.InnerException;
                    ToWrite += "\n" + "Origin: " + e.Source + "Target: " + e.TargetSite + "Message: " + e.Message + "\nSource: " + e.Source + "\nStack: " + e.StackTrace;
                }

                LogHandler<Exception>.WriteToFile("Exception", LogHandler<Exception>.LoggerType.EXCEPTION_FATAL, ToWrite);
            }
        }
    }
}
