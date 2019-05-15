
using System;
using System.IO;
namespace ClientUpdater
{
    internal class LogHandler<T> where T : class
    {
        static String myDirectory = "./Logger/";
        public enum LoggerType
        {
            INFORMATIONAL,
            WARNING,
            EXCEPTION,
            EXCEPTION_FATAL
        }



        public static void WriteToFile(T myItem, String StringToSave)
        {
            WriteToFile(myItem.GetHashCode() + " " + myItem.GetType().ToString(), LoggerType.INFORMATIONAL, StringToSave);
        }

        public static void WriteToFile(String myItemWriting, LoggerType myType, String myText)
        {
            Enum.GetName(typeof(LoggerType), myType);
            if (!Directory.Exists(myDirectory))
            {
                Directory.CreateDirectory(myDirectory);
            }
            FileStream myStream = File.OpenWrite(myDirectory + myItemWriting + ".txt");
            myStream.Lock(0, myStream.Length);
            StreamWriter myWriter = new StreamWriter(myStream);
            myWriter.Write(myItemWriting + " " + myText);
            myWriter.Flush();
            myStream.Unlock(0, myStream.Length);
            myWriter.Close();
            myStream.Close();
        }



    }
}