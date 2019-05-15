
using System;

[Serializable]
public class EquippableSlot  {

    
    public string SlotName;

    
    public InventoryItemWrapper EquippedItem;

    
    public String myAllowedType;


    
    EquipmentHandler myEquipmentHandler;


    //TODO:Slot myConnectedSlot;

    
    public void setEquipmentHandler(EquipmentHandler aWatcher)
    {
        myEquipmentHandler = aWatcher;
    }

    internal void putInSlot(InventoryItemWrapper myItem)
    {
        EquippedItem = myItem;
    }

  /*
    public bool isValidItemHolder(ICustomItem myItemToValidate)
    {
        return myItemToValidate.GetType().ToString().ToUpper().CompareTo(myAllowedType.ToUpper()) == 0;
    }

    public bool AddItem(ICustomItem myItemsToAdd)
    {
        InventoryItemWrapper myNewItem = myItemsToAdd as InventoryItemWrapper;
        return false;
    }*/

   

    internal bool Unequip()
    {
        EquippedItem = null;
        return true;
    }

   /* public bool RemoveItem(ICustomItem myItemsToAdd)
    {
        return false;
    }*/


}
