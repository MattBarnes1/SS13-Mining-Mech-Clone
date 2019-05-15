using System;
using System.Collections.Generic;
namespace ItemAttributes
{
    [Serializable]
    public class WeaponDamageRoller
    {

        WeaponDamage[] myDamages = new WeaponDamage[0];

        int myMaxDamage;

        public List<WeaponDamage> rollForDamage()
        {
            List<WeaponDamage> myDamageReturned = new List<WeaponDamage>();
            foreach (WeaponDamage A in myDamages)
            {
                myDamageReturned.Add(A.GetRolledDamage(DamageAmountType.NORMAL));
            }
            return myDamageReturned;
        }

        public List<DamageRange> GetDamageRanges()
        {
            List<DamageRange> myDamageReturned = new List<DamageRange>();
            foreach (WeaponDamage A in myDamages)
            {
                myDamageReturned.Add(A.GetDamageRange());
            }
            return myDamageReturned;
        }

        public void setDamages(WeaponDamage[] myDamages)
        {
            this.myDamages = myDamages;
        }


        public int getMaxDamage()//TODO: legacy stuff that needs deleted.
        {
            return myMaxDamage;
        }

        public WeaponDamage[] getDamageRange()
        {
            if(myDamages != null)
            {
                return myDamages;
            }
            return null;
        }

    
    }
}
