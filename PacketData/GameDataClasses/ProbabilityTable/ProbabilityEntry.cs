using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameData.GameDataClasses.ProbabilityTable
{
    [Serializable]
    public class ProbabilityEntry
    {
        public int MinProbability;
        public int MaxChance;
        public IProbabilityTableItem Item;

        public ProbabilityEntry(int v1, int v2, IProbabilityTableItem t)
        {
            this.MinProbability = v1;
            this.MaxChance = v2;
            this.Item = t;
        }
    }
}
