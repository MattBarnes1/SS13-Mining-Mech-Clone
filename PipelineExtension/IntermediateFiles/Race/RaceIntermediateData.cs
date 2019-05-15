using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameData.GameDataClasses.Characters.Bones;
using GameData.GameDataClasses.Characters.Organs;
using Microsoft.Xna.Framework.Content.Pipeline.Graphics;
using PipelineExtension.IntermediateFiles.Characters;
using PipelineExtension.IntermediateFiles.Characters.Bones;

namespace PipelineExtension.IntermediateFiles
{
    public class RaceIntermediateData
    {
        public Bone[] Bones;
        public Texture2DContent[] myBonesLoadData;
        public Organ[] Organs;
        public Texture2DContent[] myOrgansLoadData;
        public Texture2DContent[] Hair;
        public Texture2DContent[] MaleBodies;
        public Texture2DContent[] FemaleBodies;
        public string RaceName;

        public RaceIntermediateData(string raceName)
        {
            RaceName = raceName;
        }

        public string Description;
    }
}
