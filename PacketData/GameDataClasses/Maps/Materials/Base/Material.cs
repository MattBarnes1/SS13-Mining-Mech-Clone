using GameData.GameDataClasses.ProbabilityTable;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DedicatedServerFramework.MapGeneration.Materials
{
    [Serializable]
    public class Material : IProbabilityTableItem
    {
        private string MateriaName;
        private float MaterialChance;


        public Material(string MateriaName, float MaterialChance)
        {
            this.MateriaName = MateriaName;
            this.MaterialChance = MaterialChance;
        }

        public void AdjustChance(float Amount)
        {
            MaterialChance -= Amount;
        }

        public String GetName()
        {
            return this.MateriaName;
        }

        public float GetChance()
        {
            return MaterialChance;
        }

    }
}
