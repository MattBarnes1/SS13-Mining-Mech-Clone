using System;
using System.Collections;
using System.Collections.Generic;

public class RangedWeapon : Weapon
{
   
    public override ItemData getCopy()
    {
        throw new NotImplementedException();
    }

    public override string GetOnAttemptedHitSound()
    {
        throw new NotImplementedException();
    }

    /*TODO: public override string GetOnHitSoundForWeapon(List<CreatureProperties> myCreatureProperties)
    {
        throw new NotImplementedException();
    }

    
    internal override void OnUse(CharacterData user)
    {
        throw new NotImplementedException();
    }*/
}
