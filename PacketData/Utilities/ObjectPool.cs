using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;

public static class ObjectPool<T>
{
    static T myInstance;
    static Stack<T> myInstanceAvailable = new Stack<T>();
    static List<T> myInstancesInUse = new List<T>();

    public static void CreatePool(T myUniqueInstance, int startingNumber)
    {

    }

    /*public static bool IsInstanceSet()
    {

    }


    private static T CreateNewAvailableInstance()
    {
        
    }

    public static T GetNextInstance()
    {
       
    }*/

    public delegate void DoReset(ref T ObjectToReset);
    public static DoReset myReset { set; get; }


    public static void PrintPoolDebugData()
    {
        Debug.Write("PoolType: " + typeof(T));
        Debug.Write("Instances available: " + myInstanceAvailable.Count);
        Debug.Write("Instances in Use: " + myInstancesInUse.Count);
    }

    public static void FreeInstance(T myReturningInstance)
    {

    }
}