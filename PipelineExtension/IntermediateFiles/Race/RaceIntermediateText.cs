using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PipelineExtension.Characters;
using PipelineExtension.IntermediateFiles.Characters;
using PipelineExtension.IntermediateFiles.Characters.Bones;
using PipelineExtension.IntermediateFiles.Characters.Organs;

namespace PipelineExtension.IntermediateFiles
{
    public class RaceIntermediateText
    {
        public String RaceName;
        public String RaceDescription;
        public TextureIntermediateText[] MaleBodies;
        public TextureIntermediateText[] FemaleBodies;
        public BoneIntermediateText[] Bones;
        public TextureIntermediateText[] Hair;
        public OrganIntermediateText[] Organs;
    }
}
