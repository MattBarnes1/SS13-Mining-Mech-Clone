using System;
[Serializable]
public class Food : Misc
{
    public float NutritionalValue;
    public String OnEquip;

    public override ItemData getCopy()
    {
        return null;
    }  

}
