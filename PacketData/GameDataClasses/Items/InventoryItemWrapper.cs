using Nez.Sprites;
using System;
using System.Collections;
using System.Collections.Generic;

[Serializable]
public class InventoryItemWrapper {
    
    public ItemData MyData;

    internal int GetUID()
    {
        return MyData.UniqueID;
    }

    public string GetOnEquipSound()
    {
        return MyData.OnEquipString;
    }

    public int Weight {
        get
        {
            return MyData.Weight;
        }
    }

    public void SetItemData(ItemData myUnityObject)
    {
        MyData = myUnityObject;
    }
    
    public bool GetStackable()
    {
       return MyData.Stackable;
    }

    public String getDescription()
    {
        return MyData.getDescription();
    }
    
    internal bool isUniqueInstance()
    {
        return !MyData.isUniqueItem;
    }


    public Sprite GetIcon()
    {
        return MyData.MySprite;
    }

    public string GetName()
    {
        return MyData.Name;
    }

    public int GetValue()
    {
        return MyData.Value;
    }
    
    bool DropOnDeath = true;
    internal bool GetDropOnDeath()
    {
        return DropOnDeath;
    }

    public Type GetItemType()
    {
        return MyData.GetType();
    }

    internal ItemData GetItemData()
    {
        return MyData;
    }
}
