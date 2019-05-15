using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameData.GameDataClasses.Characters.Bones
{
    public class Bone : InternalObject
    {
        public Bone()
        {
        }

        public Bone(float MaxHP, float currentHP, string name, Vector3 DrawPosition)
        {
            base.MaxHP = MaxHP;
            base.CurrentHP = currentHP;
        }

    }
}
