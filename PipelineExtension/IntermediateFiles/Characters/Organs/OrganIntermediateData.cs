using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Content.Pipeline.Serialization.Compiler;

namespace PipelineExtension.IntermediateFiles.Characters.Bones
{
    public class OrganIntermediateData
    {
        public TextureIntermediateData myData;
        public String Name;
        public int HPStartAmount;

        internal void Write(ContentWriter output)
        {
            throw new NotImplementedException();
        }
    }
}
