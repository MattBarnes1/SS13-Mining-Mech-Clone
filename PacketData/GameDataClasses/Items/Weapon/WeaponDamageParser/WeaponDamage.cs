
using System;

namespace ItemAttributes
{
    [Serializable]
    public class WeaponDamage
    {
        
        String damageType;
        int minDamage;
        int maxDamage;
        private int v;

        public WeaponDamage(string damageType, int v)
        {
            this.damageType = damageType;
            this.v = v;
        }

        public WeaponDamage()
        {
        }

        public string DamageType
        {
            get
            {
                return damageType;
            }

            set
            {
                damageType = value;
            }
        }

        public int MinDamage
        {
            get
            {
                return minDamage;
            }

            set
            {
                minDamage = value;
            }
        }


        public WeaponDamage GetRolledDamage(DamageAmountType myDamageType)
        {
            WeaponDamage myNewDamage = null;
            switch (myDamageType)
            {
                case DamageAmountType.MINIMUM:
                    myNewDamage = new WeaponDamage(damageType, minDamage);
                    break;
                case DamageAmountType.MAXIMUM:
                    myNewDamage = new WeaponDamage(damageType, maxDamage);
                    break;
                case DamageAmountType.NORMAL:
                    myNewDamage = new WeaponDamage(damageType, RollDamage());
                    break;
            }
            return myNewDamage;
        }

        public DamageRange GetDamageRange()
        {
            return new DamageRange(damageType, minDamage, maxDamage);
        }

        public int MaxDamage
        {
            get
            {
                return maxDamage;
            }

            set
            {
                maxDamage = value;
            }
        }

        public int RollDamage()
        {
            throw new Exception();
        }
    }
}


