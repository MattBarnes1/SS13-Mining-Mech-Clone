using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameData.GameDataClasses.ProbabilityTable
{
    [Serializable]
    public class SimpleProbabilityTable
    {
        List<ProbabilityEntry> Probabilities = new List<ProbabilityEntry>();
        public void AddProbabilityItem(List<IProbabilityTableItem> myItems)
        {
            myItems.Sort(delegate (IProbabilityTableItem A, IProbabilityTableItem B)
            {
                return A.GetChance().CompareTo(B.GetChance());
            });
            int i;
            for (i = 0; i < myItems.Count - 1; i++)
            {
                Probabilities.Add(new ProbabilityEntry((int)myItems[i].GetChance(), (int)myItems[i+1].GetChance(), myItems[i]));
            }

            Probabilities.Add(new ProbabilityEntry((int)myItems[i].GetChance(), 100, myItems[i]));
        }

        public IProbabilityTableItem RollForItem()
        {
            int Result = DiceRoller.RollDice(1, 101, 0);
            foreach(ProbabilityEntry A in Probabilities)
            {
                if(A.MinProbability <= Result && A.MaxChance > Result)
                {
                    return A.Item;
                }
            }
            Debug.Assert(false, "Invalid IProbabilityTableItem detected!");
            return null;
        }



    }
}
