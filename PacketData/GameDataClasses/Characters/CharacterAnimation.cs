using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameData.GameDataClasses.Characters;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using static GameData.GameDataClasses.Characters.CharacterTexture;

namespace GameData.GameDataClasses.AnimationData
{
    [Serializable]
    public class CharacterAnimation : Animation
    {
        CharacterTexture[] myLayers = new CharacterTexture[4];

        public static int Layers { get; set; }

        public CharacterAnimation(CharacterTexture[] myLayers)
        {
            this.myLayers = myLayers;
        }

        public override void Draw(SpriteBatch myAnimation, Vector3 myPosition)
        {
            for(int i= 0; i < myLayers.Length;i++)
            {
                myAnimation?.Draw(myLayers[i].SpriteAtlas, new Vector2(myPosition.X, myPosition.Y), myLayers[i].SourceRectangle, Color.White, 0, Vector2.Zero, 1, SpriteEffects.None, 0);
            }
        }

        public void SetDirection(DirectionFace A)
        {
            foreach(CharacterTexture B in myLayers)
            {
                B.SetDirection(A);
            }
        }

        public override void SetAtlas(Texture spriteAtlas)
        {
        }

        public override void UpdateAnimation(GameTime T)
        {

        }
    }
}
