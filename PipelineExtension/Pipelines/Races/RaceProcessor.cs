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
    [ContentProcessor(DisplayName = "Race Processor")]
    public class RaceProcessor : ContentProcessor<RaceIntermediateText, RaceIntermediateData>
    {
        public override RaceIntermediateData Process(RaceIntermediateText input, ContentProcessorContext context)
        {
            try
            {
                RaceIntermediateData myIntermediate = new RaceIntermediateData(input.RaceName);

                List<Bone> myData = new List<Bone>();
                List<Texture2DContent> BoneImage = new List<Texture2DContent>();
                foreach (BoneIntermediateText A in input.Bones)
                {
                    Bone myNewBone = new Bone((float)A.HPStartAmount, (float)A.HPStartAmount, A.Name, A.myTextureData.myLocalPosition);
                    BoneImage.Add(LoadTexture(A.myTextureData.TexturePath, context));
                    myData.Add(myNewBone);
                }
                myIntermediate.Bones = myData.ToArray();
                myIntermediate.myBonesLoadData = BoneImage.ToArray();
                List<Organ> myOData = new List<Organ>();
                List<Texture2DContent> OrganImage = new List<Texture2DContent>();
                foreach (OrganIntermediateText A in input.Organs)
                {
                    Organ myNewOrgan = new Organ(A.Name, A.HPStartAmount, A.myTextureData.myLocalPosition);
                    OrganImage.Add(LoadTexture(A.myTextureData.TexturePath, context));
                    myOData.Add(myNewOrgan);
                }
                myIntermediate.Organs = myOData.ToArray();
                myIntermediate.myOrgansLoadData = OrganImage.ToArray();
                List<Texture2DContent> myHMData = new List<Texture2DContent>();
                foreach (TextureIntermediateText A in input.Hair)
                {
                    myHMData.Add(LoadTexture(A.TexturePath, context));
                }
                myIntermediate.Hair = myHMData.ToArray();
            
                List<Texture2DContent> myBMData = new List<Texture2DContent>();
                foreach (TextureIntermediateText A in input.MaleBodies)
                {
                    myBMData.Add(LoadTexture(A.TexturePath, context));
                }
                myIntermediate.MaleBodies = myBMData.ToArray();
         
                List<Texture2DContent> myBFData = new List<Texture2DContent>();
                foreach (TextureIntermediateText A in input.FemaleBodies)
                {
                    myBFData.Add(LoadTexture(A.TexturePath, context));
                }
                myIntermediate.FemaleBodies = myBFData.ToArray();
                myIntermediate.Description = input.RaceDescription;
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