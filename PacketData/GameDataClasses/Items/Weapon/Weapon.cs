using System;
using System.Collections.Generic;
using ItemAttributes;

[Serializable]
public abstract class Weapon : ItemData
{

    WeaponDamageRoller Damage;
    String OnAttackClip;

    private void Awake()
    {
        if (Damage == null) Damage = new WeaponDamageRoller();       
    }


    public int getMaxDamage()
    {
       return Damage.getMaxDamage();
    }
    

    public void getDamage(ref List<String> DamageType, ref List<int> DamageDone)
    {
       // TODO: Damage.rollForDamage(ref DamageDone, ref DamageType);
    }
    


    //public abstract String GetOnHitSoundForWeapon(List<CreatureProperties> myCreatureProperties);
    public abstract String GetOnAttemptedHitSound();

    //internal abstract void OnUse(CharacterData user);

   
    public WeaponDamageRoller GetDamageRoller()
    {
        return Damage;
    }
}
