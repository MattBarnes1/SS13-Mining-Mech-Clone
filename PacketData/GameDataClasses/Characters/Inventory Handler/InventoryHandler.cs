#define DEBUG
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;

[Serializable]
public class InventoryHandler
{

    List<ItemStack> myInventoryItems = new List<ItemStack>();


    EquipmentHandler myEquipmentHandler;

    public void InitializeEquipmentHandler()
    {
        myEquipmentHandler = new EquipmentHandler();
        myEquipmentHandler.SetItemHandler(this);
    }

    public void EquipItemFromInventory(ItemStack myWrapper)
    {
       InventoryItemWrapper myStackedItem = myWrapper.RemoveItemFromStack();
        if(myWrapper.IsEmpty())
        {
            if (!myEquipmentHandler.TryToAutoEquipItem(myStackedItem))
            {
                myWrapper.AddItem(myStackedItem);
            }
            else
            {
                RemoveItemStack(myWrapper);
            }
        }
        else
        {
            if (!myEquipmentHandler.TryToAutoEquipItem(myStackedItem))
            {
                myWrapper.AddItem(myStackedItem);
            }
        }
    }

    Action<int> WeightWatcher;

    public void AddWeightWatcher(Action<int> aFunc)
    {
        WeightWatcher += aFunc;
    }

    public void UpdateWeight()
    {
        TotalWeight = 0;
        foreach(ItemStack A in myInventoryItems)
        {
            TotalWeight += A.GetWeight();
        }
        WeightWatcher.Invoke(TotalWeight);
    }


    private void RemoveItemStack(ItemStack myWrapper)
    {
        myInventoryItems.Remove(myWrapper);
    }

    public void UnequipItemInSlot()
    {

    }

    public void EquipItemFromExternal(InventoryItemWrapper myWrapper)
    {
        

    }

    public bool hasItem(ItemData myDataToCheckFor)
    {//MUST EXIST
        foreach(ItemStack A in myInventoryItems)
        {
            if (A.hasItemData(myDataToCheckFor)) return true;
        }
        return false;
    }


    Action<InventoryItemWrapper> myAddWatchers;
    Action<InventoryItemWrapper> myRemoveWatchers;


    public void StatModChanged(int myStat)
    {

    }
    

    int TotalWeight;


    int maxWeight = -1;

    public int ItemCount
    {
        get
        {
            return myInventoryItems.Count;
        }
    }

    internal static void hasDied()
    {
        throw new NotImplementedException();//TODO: has died handling
    }

    public int GetItemAmount(ItemData myItem)
    {
        foreach(ItemStack A in myInventoryItems)
        {
            if(A.hasItemData(myItem))
            {
                return A.GetSize();
            }
        }
        return 0;
    }
    
    
    private void SortItems()
    {
        myInventoryItems.Sort(delegate (ItemStack x, ItemStack y)
        {
            if (x.GetUID() == y.GetUID()) return 0;
            if (x.GetUID() > y.GetUID())
            {
                return 1;
            }
            return -1;
        });
    }


    public List<ItemStack> RemoveAll(ItemData anItemID)
    {
        List<ItemStack> myStacks = new List<ItemStack>();
        foreach(ItemStack A in myInventoryItems)
        {
            if(A.hasItemData(anItemID))
            {
                myStacks.Add(A);
            }
        }
        foreach(ItemStack myStacksToRemove in myStacks)
        {
            myInventoryItems.Remove(myStacksToRemove);
            TotalWeight -= myStacksToRemove.GetWeight();
        }
        return myStacks;
    }

    private ItemStack GetItemByType(Type type)
    {
        foreach(ItemStack A in myInventoryItems)
        {
            if (A.hasItemData(type))
            {
                return A;
            }
        }
        return null;
    }

    public void AddObserverItemAdded(Action<InventoryItemWrapper> anObserver)
    {
        myAddWatchers += anObserver;
    }
    public void RemoveObserverItemAdded(Action<InventoryItemWrapper> anObserver)
    {
        myAddWatchers -= anObserver;
    }
    public void AddObserverItemRemoved(Action<InventoryItemWrapper> anObserver)
    {
        myRemoveWatchers += anObserver;
    }
    public void RemoveObserverItemRemoved(Action<InventoryItemWrapper> anObserver)
    {
        myRemoveWatchers -= anObserver;
    }
   
    
    public void OnEnable()
    {
        if(myInventoryItems == null)
        {
            TotalWeight = 0;
            maxWeight = -1;
            myInventoryItems = new List<ItemStack>();
        }
    }

    public bool isEmpty()
    {
        return myInventoryItems.Count == 0;
    }

    public float getMaxWeight()
    {
        return maxWeight;
    }

    public bool hasItemInQuantity(InventoryItemWrapper plantItem, int v)
    {
        return GetItemAmount(plantItem.GetItemData()) >= v;
    }
    
    public InventoryItemWrapper getItemAtPosition(int tryParse)
    {
        InventoryItemWrapper myItem = null;
        if (myInventoryItems.Count > tryParse && tryParse > -1)
        {
            myItem = myInventoryItems[tryParse].RemoveItemFromStack();
        }
        return myItem;
    }
  
    public void setMaxWeight(int myMax)
    {
        maxWeight = myMax;
    }

    public virtual string[][] getInventoryInformation()
    {
        List<String[]> myStrings = new List<string[]>();
        foreach (ItemStack A in myInventoryItems)
        {
            if (A.GetUID() != -1)
            {
                String[] myStringToAdd =
                {
                    A.GetName(),
                     A.GetWeight().ToString(),
                     A.GetValue().ToString()
                };
                myStrings.Add(myStringToAdd);
            }
        }
        return myStrings.ToArray();
    }

    public ItemStack RemoveItem(ItemData myData, int Quantity)
    {
       var aType = this.GetItemByType(myData.GetType());        
       return aType.RemoveItem(Quantity);
    }

    public ItemStack RemoveItem(InventoryItemWrapper myItem, int quantity)
    {
        ItemStack myAffectedStack = null;
        ItemStack myRetVal = null;
        foreach (ItemStack A in myInventoryItems)
        {
            if(A.Contains(myItem))
            {
                myRetVal = A.RemoveItem(quantity);
                TotalWeight -= myRetVal.GetWeight();
                if(A.IsEmpty())
                {
                    myAffectedStack = A;
                }
            }
        }
        if(myAffectedStack != null)
        {
            myInventoryItems.Remove(myAffectedStack);
        }
        return myRetVal;
    }

    public ItemStack RemoveItem(InventoryItemWrapper myItem)
    {
      return RemoveItem(myItem, 1);
    }



    public float GetCurrentWeight()
    {
        if(myEquipmentHandler != null)
        {
            return TotalWeight + myEquipmentHandler.GetCurrentWeight();
        }
        else
        {
            return TotalWeight;
        }
    }

    public bool canAddWeight(float v)
    {
        return (TotalWeight + v < maxWeight);
    }

    public bool isFull()
    {
        return (maxWeight <= TotalWeight);
    }

    public void AddItem(InventoryItemWrapper myWrapper, int Amount)
    {
        for (int i = 0; i < Amount; i++)
        {
           //TODO: AddItem(Instantiate(myWrapper));
        }
    }

    public void AddItem(InventoryItemWrapper A)
    {
        if (A == null) return;
        Debug.Assert(canAddWeight(A.Weight), "ADDED MORE WEIGHT THAN COULD CARRY!");
        TotalWeight += A.Weight;
        if(A.GetStackable() && !A.isUniqueInstance())
        {
            foreach(ItemStack B in myInventoryItems)
            {
                if(B.hasItemData(A.GetItemData()))
                {
                    B.AddItem(A);
                    return;
                }
            }
        } else
        {
            ItemStack myNewStack = new ItemStack();
            myNewStack.AddItem(A);
            myInventoryItems.Add(myNewStack);
        }
        SortItems();
    }

    public void AddItem(ItemStack inventoryItem)
    {
        foreach(ItemStack A in myInventoryItems)
        {
            if(A.hasItemData(inventoryItem))
            {
                A.CombineStack(inventoryItem);
            }
        }
    }


}
