using ItemAttributes;
using PipelineExtension.IntermediateFiles.Characters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PipelineExtension.IntermediateFiles.Clothing
{
    public class ClothesData : TextureIntermediateData
    {
        public String[] SlotsUsed;
        public String RoleName;
        public DamageResistanceHolder myDamageResistances = null;
    }
}
