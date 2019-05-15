using GameData.GameDataClasses.AnimationData;
using GameData.GameDataClasses.Maps.Air_Pressure;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace GameData.GameDataClasses.Maps
{
    [Serializable]
    public class TileData : ISerializable
    {
        public Animation AnimationData { get; set; }
        public TileData(Vector3 vector2, Texture2D aTexture, String TextureSpriteAtlas)
        {
            AnimationData = new StaticAnimation(aTexture, TextureSpriteAtlas);
        }

        public void SetPosition(Vector3 vector2)
        {
            WorldPosition = vector2;
        }

        short[] myWallTexture { get; set; }

        public static int CurrentLayer { get; set; } = 0;

        public override string ToString()
        {
            return "[" +  myHeightTiles.Count + "]";
        }

       

        private TileData _north;

        public void Update(GameTime t)
        {

        }



        private TileData _east;
        private TileData _south;
        private TileData _west;
        private TileData _northeast;
        private TileData _northwest;
        private TileData _southeast;
        private TileData _southwest;
        public const int TileSize = 64;
        public TileData North { get => _north; set => _north = value; }
        public TileData East { get => _east; set => _east = value; }
        public TileData South { get => _south; set => _south = value; }
        public TileData West { get => _west; set => _west = value; }
        public TileData Northeast { get => _northeast; set => _northeast = value; }
        public TileData Northwest { get => _northwest; set => _northwest = value; }


        public TileData Southeast { get => _southeast; set => _southeast = value; }
        public TileData Southwest { get => _southwest; set => _southwest = value; }

        bool ShowAirPressure = false;
        public bool Collidable { get; set; }

        public Vector3 LocalPosition { get; set; }
        public Vector3 WorldPosition { get; set; }
        public int TileSetID { get; set; }
        public int TextureAtlasTileHeight { get; set; }
        public int TextureAtlasTileWidth { get; set; }
        public string TilesetName { get; set; }

       
        protected TileData(SerializationInfo info, StreamingContext context)
        {
            Collidable = (bool)info.GetValue("Collidable", typeof(bool));
            myHeightTiles = (List<TileData>)info.GetValue("ListHeightTiles", typeof(List<TileData>));
            LocalPosition = new Vector3((float)info.GetValue("LocationX", typeof(float)), (float)info.GetValue("LocationY", typeof(float)), (float)info.GetValue("LocationZ", typeof(float)));
            AnimationData = (Animation)info.GetValue("myAnimation", typeof(Animation));
        }

        public List<TileData> myHeightTiles = new List<TileData>();
        public List<TileData> myNegativeHeightTiles = new List<TileData>();

        public object TileEditLock = new object();

      
        private int GetHeight()
        {
            return Math.Max(myHeightTiles.Count() - 1, 0);
        }

        private int GetDepth()
        {
            return Math.Max(myNegativeHeightTiles.Count() - 1, 0);
        }

        public const int MaxHeight = 8;

        public TileData(Vector3 vector2)
        {
            int TileHeight = (int)Math.Round(MaxHeight * (vector2.Z));
            this.LocalPosition = new Vector3(vector2.X, vector2.Y, 0);
            if(vector2.Z > 0)
            {
                myHeightTiles.Add(this);
                for (int i = 1; i < TileHeight; i++)
                {
                    myHeightTiles.Add(new TileData(new Vector3(vector2.X, vector2.Y, 0), i + 1));
                }
                this.AnimationData = new StaticAnimation(null, DefaultWallTile);
            }
            else
            {
                this.AnimationData = new StaticAnimation(null, DefaultFloorTile);
            }

            this.LocalPosition = new Vector3(vector2.X, vector2.Y, 0);
        }

        public TileData(Vector3 vector2, int v) 
        {
            this.LocalPosition = new Vector3(vector2.X, vector2.Y, v);
            this.AnimationData = new StaticAnimation(null, DefaultWallTile);

        }
        [NonSerialized]
        public static Texture2D myWall;
        [NonSerialized]
        public static Texture2D myFloor;
        internal bool isWall;
        public const String DefaultFloorTile = "Tile Sprites/64_floor";
        public const String DefaultWallTile = "Tile Sprites/64_wall";


        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("ListHeightTiles", myHeightTiles);
            info.AddValue("LocationX", LocalPosition.X);
            info.AddValue("LocationY", LocalPosition.Y);
            info.AddValue("LocationZ", LocalPosition.Z);
            info.AddValue("myAnimation", AnimationData);
            info.AddValue("Collidable", Collidable);
        }

        public Vector3 GetPosition()
        {
            return WorldPosition;
        }
    }

}
