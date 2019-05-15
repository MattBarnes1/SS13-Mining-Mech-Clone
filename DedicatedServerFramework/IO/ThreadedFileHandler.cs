using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using DedicatedServer.GameDataClasses.Entities;

namespace DedicatedServerFramework.IO
{
    public class ThreadedFileHandler
    {
        public class ThreadedFile
        {
            public int Readers {
                get
                {
                    return _Readers;
                }
                private set {
                     _Readers = value;
                }
            }
            int _Readers = 0;
            FileStream myStream;
            object myData;
            public ThreadedFile(String myFile)
            {
                myStream = File.Open(myFile, FileMode.Open, FileAccess.ReadWrite, FileShare.Read);
                FileStream.Synchronized(myStream);
            }
            BinaryFormatter myConverter = new BinaryFormatter();
            public T Read<T>()
            {
                Interlocked.Increment(ref _Readers);
                lock (myLock)
                {
                    T Temp;
                    myData = myConverter.Deserialize(myStream);
                    Temp = (T)myData;
                    if (myData != null)
                    {
                        Temp = (T)myData;
                        if (Interlocked.Decrement(ref _Readers) == 0)
                        {
                            myData = null;
                        }
                        return Temp;
                    }
                    else
                    {
                        if (Interlocked.Decrement(ref _Readers) == 0)
                        {
                            myData = null;
                        }
                    }
                    return default(T);
                }
            }
            object myLock = new object();
            public object ReadBytes()
            {
                Interlocked.Increment(ref _Readers);
                lock(myLock)
                {
                    byte[] Temp;   
                    if(myData != null)
                    {
                        Temp = (byte[])myData;
                        if (Interlocked.Decrement(ref _Readers) == 0)
                        {
                            myData = null;
                        }
                        return Temp;
                    }
                    else
                    {
                        myData = new byte[myStream.Length];
                        myStream.Read((byte[] )myData, 0, (int)myStream.Length); //TODO: what if file goes beyond it
                        Temp = (byte[])myData;
                        if (Interlocked.Decrement(ref _Readers) == 0)
                        {
                            myData = null;
                        }
                        return Temp;
                    }
                }
            }

            public void Write<T>(byte[] DataToWrite, int offset, int count)
            {
                Interlocked.Increment(ref _Readers);
                lock (myLock)
                {
                    myStream.Write(DataToWrite, offset, count);
                }
                Interlocked.Decrement(ref _Readers);
            }
        }

        
        internal T OpenReadClass<T>(string v)
        {
            ThreadedFile myReturned;
            if (!myActivelyReadFiles.TryGetValue(v, out myReturned))
            {
                myReturned = new ThreadedFile(v);
                myActivelyReadFiles.TryAdd(v, myReturned);
            }
            return myReturned.Read<T>();
        }

        ConcurrentDictionary<String, ThreadedFile> myActivelyReadFiles = new ConcurrentDictionary<string, ThreadedFile>();

        public bool isQueued(String aPath)
        {
            ThreadedFile aFile;
            if (myActivelyReadFiles.TryGetValue(aPath, out aFile))
            {
                return aFile.Readers > 0;
            }
            else
            {
                return false;
            }
        }

        internal byte[] OpenReadAll(string myReturn)
        {
            ThreadedFile myReturned;
            if (!myActivelyReadFiles.TryGetValue(myReturn, out myReturned))
            {
                myReturned = new ThreadedFile(myReturn);
                myActivelyReadFiles.TryAdd(myReturn, myReturned);
            }
            return (byte[])myReturned.ReadBytes();
        }
    }
}
