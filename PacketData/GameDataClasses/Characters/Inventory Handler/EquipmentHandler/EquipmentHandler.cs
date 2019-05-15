using System;

[Serializable]
public class EquipmentHandler 
{

    /* TODO:
    InventoryItemWrapper myChestArmor;
    Action<Sprite> myChestArmorWatcher;
    
    DamageResistance myResistances;
    public bool EquipToChestSlot(Object mySlot)
    {
        var myNewItem = mySlot as InventoryItemWrapper;
        if(myNewItem && myItemHandler.canAddWeight(myNewItem.Weight) && myNewItem.GetItemData() is ChestArmor)
        {
            if (myChestArmor)
            {
                myItemHandler.AddItem(myChestArmor);            
            }
            myChestArmor = myNewItem;
            return true;
        }
        return false;
    }

    public bool UnequipToChestSlot(Object mySlot)
    {
        myChestArmor = null;
        return true;
    }
     //TODO: needs the equipping logic implemented.
    InventoryItemWrapper myLeftRing;
    Action<Sprite> myLeftRingWatcher;
    public bool EquipToLeftRingSlot(Object mySlot)
    {
        var myNewItem = mySlot as InventoryItemWrapper;
        if (myNewItem && myItemHandler.canAddWeight(myNewItem.Weight) && myNewItem.GetItemData() is Ring)
        {
            if (myChestArmor)
            {
                myItemHandler.AddItem(myChestArmor);
            }
            myChestArmor = myNewItem;
            return true;
        }
        return false;
    }


    public bool UnequipToLeftRingSlot(Object mySlot)
    {
        myLeftRing = null;
        return true;
    }

    
    InventoryItemWrapper myRightRing;
    Action<Sprite> myRightRingWatcher;
    public bool EquipToRightRingSlot(Object mySlot)
    {
        var myNewItem = mySlot as InventoryItemWrapper;
        if (myNewItem && myItemHandler.canAddWeight(myNewItem.Weight) && myNewItem.GetItemData() is Ring)
        {
            if (myChestArmor)
            {
                myItemHandler.AddItem(myChestArmor);
            }
            myChestArmor = myNewItem;
            return true;
        }
        return false;
    }
    public bool UnequipToRightRingSlot(Object mySlot)
    {
        myRightRing = null;
        return true;
    }

    
    InventoryItemWrapper myLegArmor;
    Action<Sprite> myLegArmorWatcher;
    public bool EquipToLegSlot(Object mySlot)
    {
        var myNewItem = mySlot as InventoryItemWrapper;
        if (myNewItem && myItemHandler.canAddWeight(myNewItem.Weight) && myNewItem.GetItemData() is LegArmor)
        {
            if (myChestArmor)
            {
                myItemHandler.AddItem(myChestArmor);
            }
            myChestArmor = myNewItem;
            return true;
        }
        return false;
    }
    public bool UnequipToLegSlot(Object mySlot)
    {
        myLegArmor = null;
        return true;
    }
    
    InventoryItemWrapper myHandArmor;
    Action<Sprite> myHandArmorWatcher;
    public bool EquipToHandSlot(Object mySlot)
    {
        var myNewItem = mySlot as InventoryItemWrapper;
        if (myNewItem && myItemHandler.canAddWeight(myNewItem.Weight) && myNewItem.GetItemData() is HandArmor)
        {
            if (myChestArmor)
            {
                myItemHandler.AddItem(myChestArmor);
            }
            myChestArmor = myNewItem;
            return true;
        }
        return false;
    }
    public bool UnequipToHandSlot(Object mySlot)
    {
        myHandArmor = null;
        return true;
    }
    
    InventoryItemWrapper myHelmet;
    Action<Sprite> myHelmetArmorSpriteWatcher;    
    public bool EquipToHelmetSlot(Object mySlot)
    {
        var myNewItem = mySlot as InventoryItemWrapper;
        if (myNewItem && myItemHandler.canAddWeight(myNewItem.Weight) && myNewItem.GetItemData() is Helmet)
        {
            if (myChestArmor)
            {
                myItemHandler.AddItem(myChestArmor);
            }
            myChestArmor = myNewItem;
            return true;
        }
        return false;
    }
    public bool UnequipToHelmetSlot(Object mySlot)
    {
        myHelmet = null;
        return true;
    }

    
    InventoryItemWrapper LeftSlot;
    Action<Sprite> LeftSlotWatcher;
    public bool EquipToLeftSlot(Object mySlot)
    {
        var myNewItem = mySlot as InventoryItemWrapper;
        if (myNewItem && myItemHandler.canAddWeight(myNewItem.Weight) && myNewItem.GetItemData() is Weapon)
        {
            if (myChestArmor)
            {
                myItemHandler.AddItem(myChestArmor);
            }
            myChestArmor = myNewItem;
            return true;
        }
        return false;
    }
    public bool UnequipToLeftSlot(Object mySlot)
    {
        LeftSlot = null;
        return true;
    }

    InventoryItemWrapper TwoHanded;
    Action<Sprite> TwoHandedWatcher;
    InventoryItemWrapper RightSlot;
    DamageResistanceHolder EquippedResistances;

    internal void GetDamageResistances(ref List<string> v1, ref List<int> v2)
    {
        
    }

    Action<Sprite> RightSlotWatcher;
    
    InventoryItemWrapper RightRangedSlot;
    Action<Sprite> RightRangedSlotWatcher;
    public bool EquipToRightSlot(Object mySlot)
    {
        var myNewItem = mySlot as InventoryItemWrapper;
        if (myNewItem && myItemHandler.canAddWeight(myNewItem.Weight) && myNewItem.GetItemData() is Weapon)
        {
            if (myChestArmor)
            {
                myItemHandler.AddItem(myChestArmor);
            }
            myChestArmor = myNewItem;
            return true;
        }
        return false;
    }
    public bool UnequipToRightSlot(Object mySlot)
    {
        RightSlot = null;
        RightRangedSlot = null;
        return true;
    }

    public void SetHelmetWatcher(Slot headArmor)
    {
        headArmor.SetSlotSwappingFromTo(EquipToHelmetSlot);
        headArmor.SetSlotSwappingToFrom(UnequipToHelmetSlot);
        myHelmetArmorSpriteWatcher += headArmor.SetSprite;
    }

    public void SetLeftRingWatcher(Slot leftRing)
    {
        leftRing.SetSlotSwappingFromTo(EquipToLeftRingSlot);
        leftRing.SetSlotSwappingToFrom(UnequipToLeftRingSlot);
        myLeftRingWatcher += leftRing.SetSprite;
    }

    public void SetLeftWeaponWatcher(Slot rightRing)
    {
        rightRing.SetSlotSwappingFromTo(EquipToRightRingSlot);
        rightRing.SetSlotSwappingToFrom(UnequipToRightRingSlot);
        myRightRingWatcher += rightRing.SetSprite;
    }

    public void SetTwoHandedWatcher(Slot rightWeaponSlot)
    {
        rightWeaponSlot.SetSlotSwappingFromTo(EquipToLeftSlot);
        rightWeaponSlot.SetSlotSwappingToFrom(EquipToLeftSlot);
        RightSlotWatcher += rightWeaponSlot.SetSprite;
    }

    public void SetRightWeaponWatcher(Slot leftWeaponSlot)
    {
        leftWeaponSlot.SetSlotSwappingFromTo(EquipToLeftSlot);
        leftWeaponSlot.SetSlotSwappingToFrom(UnequipToLeftSlot);
        LeftSlotWatcher += leftWeaponSlot.SetSprite;
    }

    public void SetRightRingWatcher(Slot rightRing)
    {
        rightRing.SetSlotSwappingFromTo(EquipToRightRingSlot);
        rightRing.SetSlotSwappingToFrom(UnequipToRightRingSlot);
        myRightRingWatcher += rightRing.SetSprite;
    }

    public void SetLegArmorWatcher(Slot legArmor)
    {
        legArmor.SetSlotSwappingFromTo(EquipToLegSlot);
        legArmor.SetSlotSwappingToFrom(UnequipToLegSlot);
        myLegArmorWatcher += legArmor.SetSprite;
    }

    public void SetChestArmorWatcher(Slot chestArmor)
    {
        chestArmor.SetSlotSwappingFromTo(EquipToChestSlot);
        chestArmor.SetSlotSwappingToFrom(UnequipToChestSlot);
        myChestArmorWatcher += chestArmor.SetSprite;
    }


    //Shield LeftSlot; //TODO:

    internal bool hasItem(ItemData myDataToCheckFor)
    { //MUST EXIST
        throw new NotImplementedException();
    }

    
    
    InventoryHandler myItemHandler;

    Action<InventoryItemWrapper> OnDequipWatcher;
    Action<InventoryItemWrapper> OnEquipWatcher;

    #region Initialization
    
    public void SetItemHandler(InventoryHandler aItemHandler)
    {
        myItemHandler = aItemHandler;
    }

    public bool TryToAutoEquipItem(InventoryItemWrapper myStackedItem)
    {
        if(myStackedItem.GetItemData() is MeleeWeapon)
        {
            MeleeWeapon myWeapon = myStackedItem.GetItemData() as MeleeWeapon;
            if(myWeapon.Subtype == MeleeWeapon.SubType.ONE_HANDED)
            {
                if (RightSlot)
                {
                    myItemHandler.AddItem(RightSlot);
                    OnDequipWatcher(RightSlot);
                }
                RightSlot = myStackedItem;
                OnEquipWatcher(RightSlot);
                return true;
            }
            else
            {
                if (RightSlot)
                {
                    myItemHandler.AddItem(RightSlot);
                    OnDequipWatcher(RightSlot);
                }
                RightSlot = null;
                if (LeftSlot)
                {
                    myItemHandler.AddItem(LeftSlot);
                    OnDequipWatcher(LeftSlot);
                }
                LeftSlot = null;
                TwoHanded = myStackedItem;
                OnEquipWatcher(TwoHanded);
                return true;
            }

        }
        else if(myStackedItem.GetItemData() is ChestArmor)
        {
            if(myChestArmor)
            {
                myItemHandler.AddItem(myChestArmor);
                OnDequipWatcher(myChestArmor);
            }
            myChestArmor = myStackedItem;
            OnEquipWatcher(myChestArmor);
        }
        else if (myStackedItem.GetItemData() is LegArmor)
        {
            if (myLegArmor)
            {
                myItemHandler.AddItem(myLegArmor);
                OnDequipWatcher(myLegArmor);
            }
            myLegArmor = myStackedItem;
            OnEquipWatcher(myLegArmor);
        }
        else if (myStackedItem.GetItemData() is Helmet)
        {
            if (myHelmet)
            {
                myItemHandler.AddItem(myHelmet);
                OnDequipWatcher(myHelmet);
            }
            myHelmet = myStackedItem;
            OnEquipWatcher(myHelmet);
        }
        else if (myStackedItem.GetItemData() is HandArmor)
        {
            if (myHandArmor)
            {
                myItemHandler.AddItem(myHandArmor);
                OnDequipWatcher(myHandArmor);
            }
            myHandArmor = myStackedItem;
            OnEquipWatcher(myHandArmor);
        }
        else if (myStackedItem.GetItemData() is Ring)
        {
            if(myLeftRing != null && myRightRing != null)
            {
                myItemHandler.RemoveItem(myLeftRing);
                OnDequipWatcher(myLeftRing);
                myLeftRing = myStackedItem;
                OnEquipWatcher(myLeftRing);
            }
            else if(myLeftRing)
            {
                myRightRing = myStackedItem;
                OnEquipWatcher(myRightRing);
            }
            else if(myRightRing)
            {
                myLeftRing = myStackedItem;
                OnEquipWatcher(myLeftRing);
            }
            return true;
        }
        return false;
    }


    public InventoryHandler GetItemHandler()
    {
        return myItemHandler;
    }
    */

    public float GetCurrentWeight()
    {
        float weightInEquipment = 0;
        /*if(myLegArmor)
        {
            weightInEquipment += myLegArmor.Weight;
        }
        if(myChestArmor)
        {
            weightInEquipment += myChestArmor.Weight;
        }
        if(myHandArmor)
        {
            weightInEquipment += myHandArmor.Weight;
        }
        if(myHelmet)
        {
            weightInEquipment += myHelmet.Weight;
        }
        if (RightSlot)
        {
            weightInEquipment += RightSlot.Weight;
        }
        if (LeftSlot)
        {
            weightInEquipment += LeftSlot.Weight;
        }
        if(TwoHanded)
        {
            weightInEquipment += TwoHanded.Weight;
        }
        if(RightRangedSlot)
        {
            weightInEquipment += RightRangedSlot.Weight;
        }*/
        return weightInEquipment;
    }

    internal void SetItemHandler(InventoryHandler inventoryHandler)
    {
        throw new NotImplementedException();
    }

    internal bool TryToAutoEquipItem(InventoryItemWrapper myStackedItem)
    {
        throw new NotImplementedException();
    }
    /*
#endregion Initialization


public void hasDied()
{

}



public bool HasEquipped(ItemData itemID)
{

return false;
}

public void OnBeforeSerialize()
{

}

public void OnAfterDeserialize()
{

}
*/
}
