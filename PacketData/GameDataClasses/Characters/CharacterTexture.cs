using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameData.GameDataClasses.Characters
{
    [Serializable]
    public class CharacterTexture
    {
        public const int CharacterTileWidth = 64;
        public const int CharacterTileHeight = 64;

        public enum DirectionFace
        {
            NORTH,
            EAST,
            SOUTH,
            WEST
        }

        public CharacterTexture(String SpriteAtlasName)
        {
            this.SpriteAtlasName = SpriteAtlasName;
        }

        public float DrawLayer { get; private set; }

        [NonSerialized]
        Texture2D myAtlas;
        public Rectangle SourceRectangle { get;  private set; }
        Rectangle myNorth = new Rectangle(CharacterTileWidth, CharacterTileHeight, CharacterTileWidth, CharacterTileHeight);
        Rectangle myEast = new Rectangle(0, 0, CharacterTileWidth, CharacterTileHeight);
        Rectangle mySouth = new Rectangle(0, CharacterTileHeight, CharacterTileWidth, CharacterTileHeight);
        Rectangle myWest = new Rectangle(CharacterTileWidth, 0, CharacterTileWidth, CharacterTileHeight);
        public void SetDirection(DirectionFace myFace)
        {
            switch (myFace)
            {
                case DirectionFace.NORTH:
                    SourceRectangle = myNorth;
                    break;
                case DirectionFace.EAST:
                    SourceRectangle = myEast;
                    break;
                case DirectionFace.SOUTH:
                    SourceRectangle = mySouth;
                    break;
                case DirectionFace.WEST:
                    SourceRectangle = myWest;
                    break;
            }
        }

        public string SpriteAtlasName { get; }
        public Texture2D SpriteAtlas { set
            {
                myAtlas = value;
                if(SourceRectangle ==null)
                {
                    SourceRectangle = myNorth;
                }
            }
            get
            {
                return myAtlas;
            }
        }

    }
}
