using GameData.GameDataClasses.Professions;
using GameData.GameDataClasses.Races;
using Microsoft.Xna.Framework.Content.Pipeline;
using Microsoft.Xna.Framework.Content.Pipeline.Graphics;
using Microsoft.Xna.Framework.Content.Pipeline.Serialization.Compiler;
using PipelineExtension.IntermediateFiles;
using PipelineExtension.IntermediateFiles.Professions;
using PipelineExtension.Races;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PipelineExtension
{
    [ContentTypeWriter]
    public class ProfessionWriter : ContentTypeWriter<ProfessionIntermediateData>
    {
        public override string GetRuntimeReader(TargetPlatform targetPlatform)
        {
            return typeof(ProfessionReader).AssemblyQualifiedName;
            //return "RaceReader, PipelineExtension";
        }

        public override string GetRuntimeType(TargetPlatform targetPlatform)
        {
            return typeof(Profession).AssemblyQualifiedName;
        }

        protected override void Write(ContentWriter output, ProfessionIntermediateData value)
        {
            try
            {
               

            }
            catch (Exception E)
            {
                LogHandler.LogHandler<RaceProcessor>.WriteToFile("RaceProcessor", LogHandler.LogHandler<RaceProcessor>.LoggerType.EXCEPTION_FATAL, E.Message + "\n" + E.StackTrace);
                throw new Exception();
            }
        }
    }
}
