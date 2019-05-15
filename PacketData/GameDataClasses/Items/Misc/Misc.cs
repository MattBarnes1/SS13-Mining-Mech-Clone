using System;
using System.Collections;
using System.Collections.Generic;
public class Misc : ItemData
{
    

    public bool isQuestItem()
    {
        return isUniqueItem;
    }

    public override string getDescription()
    {
        return Description;
    }

    public override ItemData getCopy()
    {
        throw new NotImplementedException();
    }
}
