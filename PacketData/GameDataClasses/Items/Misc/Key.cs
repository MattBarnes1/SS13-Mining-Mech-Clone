
using System;

public class Key : Misc {
    String myKeyName;
    String myKeyDescription;

    public string KeyName
    {
        get
        {
            return myKeyName;
        }

        set
        {
            myKeyName = value;
        }
    }

    public string KeyDescription
    {
        get
        {
            return myKeyDescription;
        }

        set
        {
            myKeyDescription = value;
        }
    }

    public override ItemData getCopy()
    {
        throw new Exception();
    }
}
