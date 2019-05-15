using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ItemAttributes;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameData.GameDataClasses.Characters.Clothing_Class
{
    public class Clothing
    {
        public Texture2D Texture;
        private string[] slotsUsed;
        private Vector3 myLocalPosition;
        private DamageResistanceHolder myResistances;
        private Clothing() { }

        public Clothing(string[] slotsUsed, ItemAttributes.DamageResistanceHolder myDamageResistances, Vector3 myLocalPosition)
        {
            this.myResistances = myDamageResistances;
            this.slotsUsed = slotsUsed;
            this.myLocalPosition = myLocalPosition;
        }
    }
}
