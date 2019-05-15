using GameData.GameDataClasses.RuleEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DedicatedServerFramework.MapGeneration.Materials
{
    public class MaterialRuleStart : Rule
    {
        public MaterialRuleStart()
        {
            base.RemoveAfterUsed = true;
        }
        public override bool PerformRule(Dictionary<string, object> externalData, List<Conclusion> myConclusionSet)
        {
            if ((myConclusionSet.Find(delegate (Conclusion P) { return P.GetConclusion<Material>() != null && P.GetConclusion<Material>().GetName().CompareTo("Iron") == 0; }) == null))
            {
                Random myRandom = new Random();
                int Rand = myRandom.Next(0, 1);
                if(Rand == 0)
                {
                    Material Silicon = new Material("Silicon", 14);
                    Conclusion cSilicon = new Conclusion(0, Silicon);
                    Material Iron = new Material("Iron", 26);
                    Conclusion cIron = new Conclusion(0, Iron);
                    myConclusionSet.Add(cSilicon);
                    myConclusionSet.Add(cIron);
                }
                else
                {
                    Material Silicon = new Material("Silicon", 26);
                    Conclusion cSilicon = new Conclusion(0, Silicon);
                    Material Iron = new Material("Iron", 14);
                    Conclusion cIron = new Conclusion(0, Iron);
                    myConclusionSet.Add(cSilicon);
                    myConclusionSet.Add(cIron);
                }
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
