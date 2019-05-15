using GameData.GameDataClasses.Characters.Bones;
using GameData.GameDataClasses.Characters.Clothing_Class;
using GameData.GameDataClasses.Characters.Organs;
using GameData.GameDataClasses.Races;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Graphics.PackedVector;
using PipelineExtension.IntermediateFiles.Characters.Bones;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PipelineExtension.Races
{
    public class ClothReader : ContentTypeReader<Clothing>
    {
        private Clothing myRace;

        protected override Clothing Read(ContentReader input, Clothing existingInstance)
        {
            Clothing myClothes = input.ReadObject<Clothing>();
            myClothes.Texture = input.ReadObject<Texture2D>();
            return myClothes;
        }
    }
}
