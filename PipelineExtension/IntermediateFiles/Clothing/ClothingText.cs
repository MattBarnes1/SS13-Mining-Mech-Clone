using ItemAttributes;
using PipelineExtension.IntermediateFiles.Characters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PipelineExtension.Characters
{
    public class ClothingText : TextureIntermediateText
    {
        public String[] SlotsUsed = new string[]
        {
            "Head",
            "Legs",
            "Chest"
        };
        public String RoleName;
        public DamageResistanceHolder myDamageResistances = null;
    }
}
