using GameData.GameDataClasses.RuleEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DedicatedServerFramework.MapGeneration.Materials
{
    public class RuleCreateThirdMaterialLayer : Rule
    {
        public RuleCreateThirdMaterialLayer()
        {
            base.RemoveAfterUsed = true;
        }

        public override bool PerformRule(Dictionary<string, object> externalData, List<Conclusion> myConclusionSet)
        {
            Conclusion PriorSet = myConclusionSet.Find(delegate (Conclusion C)
            {
                return (C.GetConclusion<Material>().GetName().CompareTo("Clay") == 0);
            });
            if (PriorSet == null) return false;
            if (PriorSet.GetConclusion<Material>().GetChance() == 10f + 10f * 0.33f)
            {
                Material Aluminum = new Material("Aluminum", 7.3333f + 7.3333f * 0.33f);
                Conclusion cAluminum = new Conclusion(0, Aluminum);
                Material Nitro = new Material("Nitrogen", 7.3333f * 0.66f);
                Conclusion cNitro = new Conclusion(0, Nitro);
                Material Nickel = new Material("Nickel", 7.3333f + 7.3333f * 0.33f);
                Conclusion cNickel = new Conclusion(0, Nickel);
                myConclusionSet.Add(cAluminum);
                myConclusionSet.Add(cNitro);
                myConclusionSet.Add(cNickel);
            }
            else
            {
                Material Aluminum = new Material("Aluminum", 7.3333f * 0.66f);
                Conclusion cAluminum = new Conclusion(0, Aluminum);
                Material Nitro = new Material("Nitrogen", 7.3333f + 7.3333f * 0.33f);
                Conclusion cNitro = new Conclusion(0, Nitro);
                Material Nickel = new Material("Nickel", 7.3333f + 7.3333f * 0.33f);
                Conclusion cNickel = new Conclusion(0, Nickel);
                myConclusionSet.Add(cAluminum);
                myConclusionSet.Add(cNitro);
                myConclusionSet.Add(cNickel);
            }
            return true;
        }
    }
}
