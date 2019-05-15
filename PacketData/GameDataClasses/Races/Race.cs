using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameData.GameDataClasses.Entities;
using Microsoft.Xna.Framework.Graphics;
using System.Collections;
using GameData.GameDataClasses.Characters.Organs;
using GameData.GameDataClasses.Characters.Bones;

namespace GameData.GameDataClasses.Races
{
    public class Race
    {
        private Race() { }
        public Race(String Racename)
        {
            this.RaceName = Racename;
        }

        public string RaceName { get; }

        public Texture2D[] Hairstyles { get; set; }
        public void AddHairTextures(Texture2D[] myLoadedFiles)
        {
            Hairstyles = myLoadedFiles;
        }

        Dictionary<String, List<Tuple<string, Texture2D[]>>> myClothingSets = new Dictionary<string, List<Tuple<string, Texture2D[]>>>();
        public void AddClothing(string v, List<Tuple<string, Texture2D[]>> myList)
        {
            myClothingSets.Add(v, myList);
        }

       
        public Bone[] Bones { get; set; }
        public Organ[] Organs { get; set; }
        public Texture2D[] FemaleBodies { get; set; }
        public Texture2D[] MaleBodies { get; set; }

        public string Description = ""; //TODO: add support in editor

        public void AddBodyTextures(SEX mySEx, Texture2D[] myLoadedFiles)
        {
            switch (mySEx)
            {
                case SEX.FEMALE:
                    FemaleBodies = myLoadedFiles;
                    break;
                case SEX.MALE:
                    MaleBodies = myLoadedFiles;
                    break;
                case SEX.NEUTRAL:
                    
                    break;
            }
        }

        

       
    }
}
