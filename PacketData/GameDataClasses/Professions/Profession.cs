using GameData.GameDataClasses.Characters.Clothing_Class;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameData.GameDataClasses.Professions
{
    /// <summary>
    /// Contains the R component of the RGB text to warn the player.
    /// </summary>
    public enum ProfessionImportance
    {
        LOW = 0x22,
        MED = 0x55,
        HIGH = 0x99,
        CRITICAL = 0xFF,
    }


    public class Profession
    {
        ProfessionImportance Importance { get; }
        String ProfessionName { get; } = "";
        String ProfessionDescription { get; } = "";
        Clothing[] ProfessionClothes { get; }
        
        private Profession() { }
        public Profession(ProfessionImportance anIMport, String ProfessionName, String ProfessionDescription, Clothing[] ProfessionClothes)
        {
            Importance = anIMport;
            this.ProfessionName = ProfessionName;
            this.ProfessionDescription = ProfessionDescription;
            this.ProfessionClothes = ProfessionClothes;
        }
    }
}
