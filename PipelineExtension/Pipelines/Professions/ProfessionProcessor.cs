using System;
using Microsoft.Xna.Framework.Content.Pipeline;
using Microsoft.Xna.Framework.Content.Pipeline.Graphics;
using FreeImageAPI;
using PipelineExtension.IntermediateFiles;
using Microsoft.Xna.Framework.Graphics;
using PipelineExtension.IntermediateFiles.Characters.Bones;
using System.Collections.Generic;
using PipelineExtension.IntermediateFiles.Characters.Organs;
using PipelineExtension.IntermediateFiles.Characters;
using GameData.GameDataClasses.Races;
using GameData.GameDataClasses.Characters.Bones;
using GameData.GameDataClasses.Characters.Organs;
using PipelineExtension.Characters;
using GameData.GameDataClasses.Characters.Clothing_Class;
using System.Diagnostics;
using PipelineExtension.IntermediateFiles.Professions;
using PipelineExtension.Pipelines.Professions;
// TODO: replace these with the processor input and output types.

namespace PipelineExtension
{
    /// <summary>
    /// This class will be instantiated by the XNA Framework Content Pipeline
    /// to apply custom processing to content data, converting an object of
    /// type TInput to TOutput. The input and output types may be the same if
    /// the processor wishes to alter data without changing its type.
    ///
    /// This should be part of a Content Pipeline Extension Library project.
    ///
    /// TODO: change the ContentProcessor attribute to specify the correct
    /// display name for this processor.
    /// </summary>
    [ContentProcessor(DisplayName = "Profession Processor")]
    public class ProfessionProcessor : ContentProcessor<ProfessionIntermediateText, ProfessionIntermediateData>
    {
        public override ProfessionIntermediateData Process(ProfessionIntermediateText input, ContentProcessorContext context)
        {
            try
            {
                ProfessionIntermediateData myIntermediate = new ProfessionIntermediateData();

                
                return myIntermediate;
            }
            catch (Exception E)
            {
                LogHandler.LogHandler<RaceProcessor>.WriteToFile("RaceProcessor", LogHandler.LogHandler<RaceProcessor>.LoggerType.EXCEPTION_FATAL, E.Message + "\n" + E.StackTrace);
                throw new Exception();
            }
        }

        public Texture2DContent LoadTexture(String Path, ContentProcessorContext context)
        {
            ExternalReference<Texture2DContent> tex_ref = new ExternalReference<Texture2DContent>(Path);
            var Return = context.BuildAndLoadAsset<Texture2DContent, Texture2DContent>(tex_ref, "TextureProcessor"); ;
            //Debug.Assert(Return != null);
            return Return;
        }

    }
}