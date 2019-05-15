using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;

namespace ItemAttributes
{
    public class DamageResistance : IEquatable<DamageResistance>, IComparable<String>
    {
        public String ResistanceType; //TODO: remove from public
        public float percentAmount;

        public int CompareTo(string other)
        {
            return ResistanceType.CompareTo(other);
        }

        public bool Equals(DamageResistance other)
        {
            return (other.ResistanceType.ToUpper().CompareTo(other.ResistanceType.ToUpper()) == 0);
        }

        public static DamageResistance operator +(DamageResistance A, DamageResistance B)
        {
            Debug.Assert(A.Equals(B));
            Debug.Assert(A.percentAmount > 0 && A.percentAmount <= 1);
            Debug.Assert(A.percentAmount > 0 && A.percentAmount <= 1);

            DamageResistance myNewResistance = new DamageResistance();
            myNewResistance.percentAmount = A.percentAmount + B.percentAmount;
            if (A.percentAmount + B.percentAmount < 0)
            {
                myNewResistance.percentAmount = 0;
            }
            else if(A.percentAmount + B.percentAmount > 1)
            {
                myNewResistance.percentAmount = 1f;
            }
            return myNewResistance;
        }
        public static DamageResistance operator -(DamageResistance A, DamageResistance B)
        {
            Debug.Assert(A.Equals(B));
            Debug.Assert(A.percentAmount > 0 && A.percentAmount <= 1);
            Debug.Assert(A.percentAmount > 0 && A.percentAmount <= 1);
            DamageResistance myNewResistance = new DamageResistance();
            myNewResistance.percentAmount = A.percentAmount - B.percentAmount;

            if (myNewResistance.percentAmount < 0)
            {
                myNewResistance.percentAmount = 0;
            }
            else if(myNewResistance.percentAmount > 1)
            {
                myNewResistance.percentAmount = 1;
            }
            return myNewResistance;
        }

        internal DamageResistance Copy()
        {
            return new DamageResistance()
            {
                percentAmount = this.percentAmount,
                ResistanceType = (String)this.ResistanceType.Clone()
            };
        }
    }
}


