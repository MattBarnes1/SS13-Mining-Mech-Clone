using DedicatedServerFramework.MapGeneration.Materials;
using GameData.GameDataClasses.RuleEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DedicatedServerFramework.MapGeneration
{
    public class MaterialsRuleEngine : RuleEngine<Material>
    {
        public MaterialsRuleEngine(List<Rule> RuleSet, List<Conclusion> myConclusions) : base(RuleSet, myConclusions)
        {
        }
    }
}
