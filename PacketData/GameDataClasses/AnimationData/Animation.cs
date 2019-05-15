using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameData.GameDataClasses.AnimationData
{
    [Serializable]
    public abstract class Animation
    {
        public string SpriteAtlasName { get; set; }
        [NonSerialized]
        public Texture2D SpriteAtlas;
        public abstract void UpdateAnimation(GameTime T);
        public abstract void Draw(SpriteBatch myAnimation, Vector3 myPosition);
        public abstract void SetAtlas(Texture spriteAtlas);
    }
}
