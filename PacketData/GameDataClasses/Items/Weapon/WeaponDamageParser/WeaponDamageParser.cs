using System;
using System.Collections.Generic;
using System.Diagnostics;
namespace ItemAttributes
{
    public static class WeaponDamageParser
    {
        public static WeaponDamageRoller ParseString(String myString)
        {
            myString = myString.ToUpper();
            String[] myStrings = myString.Split(' ');
            List<WeaponDamage> myDamages = new List<WeaponDamage>();
            bool NormalDamageType = false;
            int charCounter = 0;
            for (int i = 0; i < myStrings.Length; i += 2)
            {
                WeaponDamage myNewDamageType = new WeaponDamage();
                //myNewDamageType.DamageType1 = myStrings[i].Replace(" ", "");
                if (!NormalDamageType)
                {
                    //NormalDamageType = ContainsNormalDamageType(myNewDamageType.DamageType1);
                }
                int numToRoll = 0;
                int dieToRoll = 0;
                int myBonus = 0;
                DiceRoller.parseRollAndDiceAndBonus(myStrings[i + 1].Replace(" ", ""), ref numToRoll, ref dieToRoll, ref myBonus);
                myNewDamageType.MaxDamage = numToRoll * (dieToRoll + myBonus);
                myNewDamageType.MinDamage = numToRoll + (myBonus * numToRoll);
                myDamages.Add(myNewDamageType);
               //TODO: fix Debug.Log("Created item with damage type: " + myNewDamageType.DamageType1 + " with min damage: " + myNewDamageType.MinDamage + " and Max Damage: " + myNewDamageType.maxDamage);
            }

            Debug.Assert(NormalDamageType, "WEAPON DIDN'T ASSIGN NORMAL DAMAGE TYPE! DIDN'T ADD IT!");

            var MyNewRoller = new WeaponDamageRoller();
            MyNewRoller.setDamages(myDamages.ToArray());
            return MyNewRoller;
        }


        public static bool ContainsNormalDamageType(String isNormalDamageType)
        {
            if (isNormalDamageType.CompareTo("BLUNT") == 0)
            {
                return true;
            }
            else if (isNormalDamageType.CompareTo("PIERCE") == 0)
            {
                return true;
            }
            else if (isNormalDamageType.CompareTo("SLASH") == 0)
            {
                return true;
            }
            return false;
        }

    }

}