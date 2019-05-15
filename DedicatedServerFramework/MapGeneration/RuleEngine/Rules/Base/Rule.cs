using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameData.GameDataClasses.RuleEngine
{
    public abstract class Rule
    {
        public float Priority { get; internal set; }
        protected bool RemoveAfterUsed;
        public abstract bool PerformRule(Dictionary<string, object> externalData, List<Conclusion> myConclusionSet);

        public bool RemoveAfterUse()
        {
            return RemoveAfterUsed;
        }
    }
}
