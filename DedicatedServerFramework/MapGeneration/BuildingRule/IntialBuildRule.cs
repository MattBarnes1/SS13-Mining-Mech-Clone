

using System.Collections.Generic;
using GameData.GameDataClasses.RuleEngine;

namespace DedicatedServerFramework.MapGeneration.BuildingRule
{
    public class IntialBuildRule : Rule
    {
        public IntialBuildRule()
        {
            base.RemoveAfterUsed = true;
        }

        public override bool PerformRule(Dictionary<string, object> externalData, List<Conclusion> myConclusionSet)
        {
            return false;
        }
    }
}
