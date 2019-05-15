using Nez.Sprites;
using System;

[Serializable]
public abstract class ItemData {

    public int Value;
    public int UniqueID;
    public bool isUniqueItem;
    public String Name;
    public bool DropsOnOwnerDeath = true;
    public int myWeight = 0;
    public String OnEquipString;
    public Sprite MySprite;
    public bool Stackable;
    public string Description;


    public string GetOnEquipSound()
    {
        return OnEquipString;
    }

    public int Weight {
        get
        {
            return myWeight;
        }
    }

    public virtual String getDescription()
    {
        return Description;
    }

    public abstract ItemData getCopy();

    internal bool isUniqueInstance()
    {
        return !isUniqueItem;
    }


    public Sprite GetIcon()
    {
        return MySprite;
    }

    public string GetName()
    {
        return Name;
    }

}
