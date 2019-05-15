using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Content;

namespace GameData.GameDataClasses.Characters.Organs
{
    public class Organ : InternalObject
    {
        public Organ()
        {
        }

        public Organ(string name, int hPStartAmount, Microsoft.Xna.Framework.Vector3 myLocalPosition)
        {
            base.LocalPosition = myLocalPosition;
            base.InternalName = name;
            base.MaxHP = hPStartAmount;
            base.CurrentHP = hPStartAmount;
        }

        public void Read(ContentReader input)
        {
            throw new NotImplementedException();
        }
    }
}
