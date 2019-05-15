using System.Collections.Generic;
using DedicatedServerFramework.MapGeneration.Materials;
using GameData.GameDataClasses.RuleEngine;

namespace DedicatedServerFramework.MapGeneration
{
    public class RuleFourthLayer : Rule
    {
        public RuleFourthLayer()
        {
            base.RemoveAfterUsed = true;
        }


        public override bool PerformRule(Dictionary<string, object> externalData, List<Conclusion> myConclusionSet)
        {
            Conclusion PriorSet = myConclusionSet.Find(delegate (Conclusion C)
            {
                return (C.GetConclusion<Material>().GetName().CompareTo("Aluminum") == 0);
            });
            if (PriorSet == null) return false;
            if (PriorSet.GetConclusion<Material>().GetChance() != 7.3333f * 0.66f)
            {
                Material Aluminum = new Material("Calcium", 2.666667f * 0.66f);
                Conclusion cAluminum = new Conclusion(0, Aluminum);
                Material Nitro = new Material("Water", 2.666667f + 2.666667f * 0.33f);
                Conclusion cNitro = new Conclusion(0, Nitro);
                Material Nickel = new Material("Cobolt", 2.666667f + 2.666667f * 0.33f);
                Conclusion cNickel = new Conclusion(0, Nickel);
                myConclusionSet.Add(cAluminum);
                myConclusionSet.Add(cNitro);
                myConclusionSet.Add(cNickel);
            }
            else
            {
                Material Aluminum = new Material("Calcium", 2.666667f + 2.666667f * 0.33f);
                Conclusion cAluminum = new Conclusion(0, Aluminum);
                Material Nitro = new Material("Water", 2.666667f * 0.66f);
                Conclusion cNitro = new Conclusion(0, Nitro);
                Material Nickel = new Material("Cobolt", 2.666667f + 2.666667f * 0.33f);
                Conclusion cNickel = new Conclusion(0, Nickel);
                myConclusionSet.Add(cAluminum);
                myConclusionSet.Add(cNitro);
                myConclusionSet.Add(cNickel);
            }
            return true;
        }
    }
}