using GameData.GameDataClasses.Characters.Clothing_Class;
using GameData.GameDataClasses.Races;
using Microsoft.Xna.Framework.Content.Pipeline;
using Microsoft.Xna.Framework.Content.Pipeline.Graphics;
using Microsoft.Xna.Framework.Content.Pipeline.Serialization.Compiler;
using PipelineExtension.IntermediateFiles;
using PipelineExtension.IntermediateFiles.Clothing;
using PipelineExtension.Races;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PipelineExtension
{
    [ContentTypeWriter]
    public class ClothWriter : ContentTypeWriter<ClothesData>
    {
        public override string GetRuntimeReader(TargetPlatform targetPlatform)
        {
            return typeof(ClothReader).AssemblyQualifiedName;
            //return "RaceReader, PipelineExtension";
        }

        public override string GetRuntimeType(TargetPlatform targetPlatform)
        {
            return typeof(Race).AssemblyQualifiedName;
        }

        protected override void Write(ContentWriter output, ClothesData value)
        {
            try
            {
                Clothing myClothes = new Clothing(value.SlotsUsed, value.myDamageResistances, value.myLocalPosition);
                output.WriteObject(myClothes);
                output.WriteObject(value.TextureData);
            }
            catch (Exception E)
            {
                LogHandler.LogHandler<RaceProcessor>.WriteToFile("RaceProcessor", LogHandler.LogHandler<RaceProcessor>.LoggerType.EXCEPTION_FATAL, E.Message + "\n" + E.StackTrace);
                throw new Exception();
            }
        }
    }
}
