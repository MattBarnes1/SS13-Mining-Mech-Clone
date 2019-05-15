using System;
using System.Collections.Generic;
public class MeleeWeapon : Weapon
{
    public enum SubType
    {
        ONE_HANDED,
        TWO_HANDED
    }

    public SubType Subtype;

    /*internal override void OnUse(CharacterData user)
    {
       // user.mySkills.BattleSkillUsed(Subtype);//TODO: bonus effects here?
    }
   


    public override string GetOnHitSoundForWeapon(List<CreatureProperties> myCreatureProperties)
    {
        return "";
    }*/

    public override string GetOnAttemptedHitSound()
    {
        return "";
    }

    public override ItemData getCopy()
    {
        throw new NotImplementedException();
    }

}
