using System;
using System.IO;

namespace LogHandler
{
    public static class LogHandler<T> where T : class
    {
        static String Directory = "./Logger/";
        public enum LoggerType
        {
            INFORMATIONAL,
            WARNING,
            EXCEPTION,
            EXCEPTION_FATAL
        }

        static FileStream myStream; 

        public static void WriteToFile(String myItemWriting, LoggerType myType, String myText)
        {
            if (myStream == null)
            { //TODO: thread safety
                myStream = File.OpenWrite(Directory + typeof(T) + ".txt");
            }

            Enum.GetName(typeof(LoggerType), myType);
            StreamWriter myWriter = new StreamWriter(myStream);
            myWriter.Write(myItemWriting + " " + myText);
            myWriter.Close();
            myStream.Close();
        }

    }
}