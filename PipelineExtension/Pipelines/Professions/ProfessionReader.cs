using GameData.GameDataClasses.Characters.Bones;
using GameData.GameDataClasses.Characters.Organs;
using GameData.GameDataClasses.Professions;
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
    public class ProfessionReader : ContentTypeReader<Profession>
    {
        protected override Profession Read(ContentReader input, Profession existingInstance)
        {
            Profession myRace = input.ReadObject<Profession>();
            
            return myRace;
        }
    }
}
