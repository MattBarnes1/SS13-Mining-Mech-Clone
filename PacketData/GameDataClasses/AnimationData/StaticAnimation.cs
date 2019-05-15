using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameData.GameDataClasses.Maps;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameData.GameDataClasses.AnimationData
{
    [Serializable]
    public class StaticAnimation : Animation
    {
        public StaticAnimation(Texture2D aTexture, String TextureName)
        {
            base.SpriteAtlasName = TextureName;
            base.SpriteAtlas = aTexture;
        }


        public override void Draw(SpriteBatch myAnimation, Vector3 myPosition)
        {
            myAnimation.Draw(base.SpriteAtlas, new Vector2(myPosition.X, myPosition.Y) * TileData.TileSize, Color.White);
        }

        public override void SetAtlas(Texture spriteAtlas)
        {

        }

        public override void UpdateAnimation(GameTime T)
        {

        }
    }
}
