using GameData.GameDataClasses.Races;
using Microsoft.Xna.Framework.Content.Pipeline;
using Microsoft.Xna.Framework.Content.Pipeline.Graphics;
using Microsoft.Xna.Framework.Content.Pipeline.Serialization.Compiler;
using PipelineExtension.IntermediateFiles;
using PipelineExtension.Races;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PipelineExtension
{
    [ContentTypeWriter]
    public class RaceWriter : ContentTypeWriter<RaceIntermediateData>
    {
        public override string GetRuntimeReader(TargetPlatform targetPlatform)
        {
            return typeof(RaceReader).AssemblyQualifiedName;
            //return "RaceReader, PipelineExtension";
        }

        public override string GetRuntimeType(TargetPlatform targetPlatform)
        {
            return typeof(Race).AssemblyQualifiedName;
        }

        protected override void Write(ContentWriter output, RaceIntermediateData value)
        {
            try
            {
                Race aRace = new Race(value.RaceName);
                aRace.Organs = value.Organs;
                aRace.Bones = value.Bones;
                aRace.Description = value.Description;
                output.WriteObject(aRace);
                output.Write(value.MaleBodies.Length);
                foreach (Texture2DContent A in value.MaleBodies)
                {
                    output.WriteObject(A);
                }
                output.Write(value.FemaleBodies.Length);
                foreach (Texture2DContent A in value.FemaleBodies)
                {
                    output.WriteObject(A);
                }
                output.Write(value.Hair.Length);
                foreach (Texture2DContent A in value.Hair)
                {
                    output.WriteObject(A);
                }
                foreach (Texture2DContent A in value.myOrgansLoadData)
                {
                    output.WriteObject(A);
                }
                foreach (Texture2DContent A in value.myBonesLoadData)
                {
                    output.WriteObject(A);
                }

            }
            catch (Exception E)
            {
                LogHandler.LogHandler<RaceProcessor>.WriteToFile("RaceProcessor", LogHandler.LogHandler<RaceProcessor>.LoggerType.EXCEPTION_FATAL, E.Message + "\n" + E.StackTrace);
                throw new Exception();
            }
        }
    }
}
