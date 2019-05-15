using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
[Serializable]
public class ItemStack {

    //public List<ICustomItem> ICustomItem.RemoveItem(int Amount)
   // {
        //return Stack.TakeAmount(Amount);
  //  }

    //public List<ICustomItem> RemoveAll()
    //{
       // return Stack.TakeAmount(Stack.Quantity - 1);
  //  }

    public void CombineStack(ItemStack a)
    {
        foreach(InventoryItemWrapper A in a.myStack)
        {
            myStack.Add(A);
        }
    }

    public void AddItem(InventoryItemWrapper anItem)
    {
        myStack.Add(anItem);
    }

   

    public InventoryItemWrapper RemoveItemFromStack()
    {
        Debug.Assert(myStack.Count > 0);
        var myReturn = myStack[myStack.Count - 1];
        myStack.RemoveAt(myStack.Count - 1);
        return myReturn;
    }

    public ItemData GetItemData()
    {
        return myStack[0].GetItemData();
    }

 
    List<InventoryItemWrapper> myStack = new List<InventoryItemWrapper>();

    internal bool IsEmpty()
    {
        return myStack.Count == 0;
    }


    Action<InventoryItemWrapper> myAddWatchers;

    public void AddInventoryAddWatcher()
    {

    }
    public void RemoveInventoryAddWatcher()
    {

    }


    Action<InventoryItemWrapper> myRemoveWatchers;
    public void AddInventoryRemoveWatcher()
    {//TODO: This

    }
    public void RemoveInventoryRemoveWatcher()
    {

    }

    public int Quantity {
        get
        {
            if (myStack != null)
                return myStack.Count;
            else
                return 0;
        }
    }

    private void OnEnable()
    {
        if(myStack == null)
        {
            myStack = new List<InventoryItemWrapper>();
        }
    }


    public void addItem(InventoryItemWrapper anItem)
    {
        myStack.Add(anItem);
    }

    public void removeItem(InventoryItemWrapper anItem)
    {
        myStack.Add(anItem);
    }

    public bool hasItemData(ItemData myDataToCheckFor)
    {
        return myStack[0].GetItemData() == myDataToCheckFor;
    }

    public void SetSize(int v)
    {
        Debug.Assert(v > 0);
        if(myStack.Count > v)
        {
            myStack.RemoveRange(0, myStack.Count - v);
        }
        else if(myStack.Count < v)
        {
            for(int i= myStack.Count; i <= v; i++)
            {
                InventoryItemWrapper myWrapper = new InventoryItemWrapper();
                myWrapper.SetItemData(myStack[0].MyData);
                myStack.Add(myWrapper);
            }
        }
    }

    public int GetSize()
    {
       return myStack.Count;
    }

    public int GetUID()
    {
        if (myStack.Count == 0) return -1;
        return (myStack[0].GetUID());
    }

    public ItemStack RemoveItem(int quantity)
    {
        ItemStack myRet = new ItemStack();
        for(int i = 0; i < quantity; i++)
        {
            myRet.AddItem(myStack[0]);
            myStack.RemoveAt(0);
        }
        return myRet;
    }

    public bool Contains(InventoryItemWrapper myItem)
    {
        return (myStack.Contains(myItem)) ;
    }

    public int GetWeight()
    {
       return myStack.Count*myStack[0].Weight;
    }

    public bool hasItemData(ItemStack inventoryItem)
    {
        return (hasItemData(inventoryItem.myStack[0].GetItemData()));
    }

    public bool hasItemData(Type type)
    {
        return (myStack[0].GetItemType() == type) ;
    }

    public string GetName()
    {
        return myStack[0].GetItemData().Name;
    }

    public int GetValue()
    {
        return myStack[0].GetItemData().Value;
    }
}
