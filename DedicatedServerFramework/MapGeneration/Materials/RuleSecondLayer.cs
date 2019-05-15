using GameData.GameDataClasses.RuleEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DedicatedServerFramework.MapGeneration.Materials
{
    public class RuleCreateSecondMaterialLayer : Rule
    {
        public RuleCreateSecondMaterialLayer()
        {
            base.RemoveAfterUsed = true;
        }

        public override bool PerformRule(Dictionary<string, object> externalData, List<Conclusion> myConclusionSet)
        {
            Conclusion Silicon = myConclusionSet.Find(delegate (Conclusion C)
            {
                return (C.GetConclusion<Material>().GetName().CompareTo("Silicon") == 0);
            });
            Conclusion Iron = myConclusionSet.Find(delegate (Conclusion C)
            {
                return (C.GetConclusion<Material>().GetName().CompareTo("Iron") == 0);
            });

            if (Silicon == null) return false;

            if(Silicon.GetConclusion<Material>().GetChance() > Iron.GetConclusion<Material>().GetChance())
            {
                Material Magnesium = new Material("Magnesium", 10*0.66f);
                Conclusion cMagnesium = new Conclusion(0, Magnesium);
                myConclusionSet.Add(cMagnesium);
                Material Oxygen = new Material("Oxygen", 13.3f);
                Conclusion cOxygen = new Conclusion(0, Oxygen);
                myConclusionSet.Add(cOxygen);
                Material Clay = new Material("Clay", 13.3f);
                Conclusion cClay = new Conclusion(0, Clay);
                myConclusionSet.Add(cClay);
                return true;
            }
            else
            {
                Material Magnesium = new Material("Magnesium", 13.3f);
                Conclusion cMagnesium = new Conclusion(0, Magnesium);
                myConclusionSet.Add(cMagnesium);
                Material Oxygen = new Material("Oxygen", 13.3f);
                Conclusion cOxygen = new Conclusion(0, Oxygen);
                myConclusionSet.Add(cOxygen);
                Material Clay = new Material("Clay", 10f * 0.66f);
                Conclusion cClay = new Conclusion(0, Clay);
                myConclusionSet.Add(cClay);
                return true;
            }
        }
    }
}
