using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameData.GameDataClasses.Characters
{
    public class InternalObject
    {
        public float MaxHP { get; protected set; } = 999f;
        public float CurrentHP { get; protected set; } = 999f;
        public String InternalName { get; protected set; }
        public Vector3 LocalPosition { get; internal set; }

        public bool isMissing;
        public float DrawLayer;
        public Texture2D Texture;
    }
}
