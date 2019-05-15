using GameData.GameDataClasses.Characters.Bones;
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
    public class RaceReader : ContentTypeReader<Race>
    {
        protected override Race Read(ContentReader input, Race existingInstance)
        {
            Race myRace = input.ReadObject<Race>();
            myRace.MaleBodies = new Texture2D[input.ReadInt32()];
            for (int i = 0; i < myRace.MaleBodies.Length; i++)
            {
                myRace.MaleBodies[i] = input.ReadObject<Texture2D>();
            }
            myRace.FemaleBodies = new Texture2D[input.ReadInt32()];
            for (int i = 0; i < myRace.FemaleBodies.Length; i++)
            {
                myRace.FemaleBodies[i] = input.ReadObject<Texture2D>();
            }
            myRace.Hairstyles = new Texture2D[input.ReadInt32()];
            for (int i = 0; i < myRace.Hairstyles.Length; i++)
            {
                myRace.Hairstyles[i] = input.ReadObject<Texture2D>();
            }
            foreach (Organ B in myRace.Organs)
            {
                B.Texture = input.ReadObject<Texture2D>();
            }
            foreach (Bone A in myRace.Bones)
            {
                A.Texture = input.ReadObject<Texture2D>();
            }
            return myRace;
        }
    }
}
