
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace ItemAttributes
{
    public class DamageResistanceHolder
    {
        Dictionary<String, DamageResistance> myResistanceLookup = new Dictionary<string, DamageResistance>();

        public void CopyResistancesTo(DamageResistanceHolder aHolder)
        {
            if(aHolder.myResistanceLookup == null)
            {
                Debug.Write("Null resistances added together!");
                return;
            }
            Debug.Assert(myResistanceLookup != null);
            List<DamageResistance> myResistancesToSend = myResistanceLookup.Values.ToList();
            aHolder.AddArmorResistances(myResistancesToSend);
        }

        public void AddArmorResistance(DamageResistance myResistance)
        {
            Debug.Assert(!String.IsNullOrEmpty(myResistance.ResistanceType));
            if(!myResistanceLookup.ContainsKey(myResistance.ResistanceType))
            {
                myResistanceLookup.Add(myResistance.ResistanceType, myResistance);
            }
            else
            {
                myResistanceLookup[myResistance.ResistanceType] += myResistance;
            }
        }

        public void AddArmorResistances(List<DamageResistance> myResistancesSent)
        {
            if (myResistancesSent.Count == 0) return;
            for (int i = 0; i < myResistanceLookup.Count; i++)
            {
                if(myResistanceLookup.ContainsKey(myResistancesSent[i].ResistanceType))
                {
                    myResistanceLookup[myResistancesSent[i].ResistanceType] += myResistancesSent[i];
                }
                else
                {
                    myResistanceLookup.Add(myResistancesSent[i].ResistanceType, myResistancesSent[i]);
                }       
            }
        }        
    }
}
