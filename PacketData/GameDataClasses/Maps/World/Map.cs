using DedicatedServer.GameDataClasses.Entities;
using DedicatedServerFramework.MapGeneration;
using DedicatedServerFramework.MapGeneration.BuildingRule;
using GameData.GameDataClasses.ProbabilityTable;
using Lidgren.Network;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Nez;
using Nez.Systems;
using Nez.Tiled;
using Noise;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Random = System.Random;

namespace GameData.GameDataClasses.Maps
{
    [Serializable]
    public class Map
    {
        string FileID;
        private string mapID;
        [NonSerialized]
        ConcurrentDictionary<NetConnection, PlayerData> myPlayers = new ConcurrentDictionary<NetConnection, PlayerData>();
        List<PlayerData> myPlayers2 = new List<PlayerData>();
        public SimpleProbabilityTable Materials { get; private set; }
        public int MapSeed { get; private set; } = new Random().Next(1000000, int.MaxValue) ;//TODO: make this use unique ID's
        public float[] NewPlayerLoadPosition { get; set; }

        [NonSerialized]
        public TiledMap myMap;

        List<RoomData> myObjects = new List<RoomData>();

        public TiledTileLayer GetCollisionLayer()
        {
            return Collidables;
        }

        [NonSerialized]
        TiledTileLayer Collidables;
        public void Load(NezContentManager content, Vector2 ScreenVector)
        {
            myMap = new TiledMap(0, (int)ScreenVector.X, (int)ScreenVector.Y, TileData.TileSize, TileData.TileSize, TiledMapOrientation.Orthogonal);
            //TODO: multilayer support
            TiledTileLayer Floors = (TiledTileLayer)myMap.createTileLayer("Floors", (int)ScreenVector.X, (int)ScreenVector.Y);
            Collidables = (TiledTileLayer)myMap.createTileLayer("Walls", (int)ScreenVector.X, (int)ScreenVector.Y);
            int XOffset = (int)Mathf.ceil(0.5f * (ScreenVector.X / TileData.TileSize));
            int YOffset = (int)Mathf.ceil(0.5f * (ScreenVector.Y / TileData.TileSize));
            TiledTileset BaseMapTileset = myMap.createTileset(content.Load<Texture2D>(this.MapTextureAtlas), 0, 64, 64, true, 0, 0, this.TextureAtlasTileWidth, this.TextureAtlasTileHeight);
            for (int x = 0; x < Mathf.ceil(ScreenVector.X / TileData.TileSize); x++)
            {
                for (int y = 0; y < Mathf.ceil(ScreenVector.X / TileData.TileSize); x++)
                {
                    TileData myTileData;
                    TiledTile myTile;
                    Tuple<float, float, float> tuple = new Tuple<float, float, float>(x, y, (float)Math.Ceiling(TileData.MaxHeight * myNoise.GetNoise(x, y)));
                    if (myModifiedTiles.TryGetValue(tuple, out myTileData))
                    {
                        myTile = new TiledTile(myTileData.TileSetID);
                        myTile.tileset = myMap.createTileset(content.Load<Texture2D>(myTileData.TilesetName), 0, TileData.TileSize, TileData.TileSize, true, 0, 0, myTileData.TextureAtlasTileWidth, myTileData.TextureAtlasTileHeight);
                        if(myTileData.isWall)
                        {
                            Collidables.setTile(myTile);
                        } else
                        {
                            Floors.setTile(myTile);
                        }
                    }
                    else
                    {
                        if(tuple.Item3 > 0)
                        {
                            myTile = new TiledTile(1);
                            myTile.tileset = BaseMapTileset;
                            Collidables.setTile(myTile);
                        }
                        else if(tuple.Item3 == 0)
                        {
                            myTile = new TiledTile(0);
                            myTile.tileset = BaseMapTileset;
                            Floors.setTile(myTile);
                        }
                        else
                        {
                            continue;
                        }
                    } 
                }
            }
        }

        List<RoomData> myRooms = new List<RoomData>();

        public void PlaceRoomObjectAtPosition(Vector3 vector3, RoomData myObject)
        {
            foreach (TileData A in myObject.TileData)
            {
                A.WorldPosition = vector3 + A.LocalPosition;
            }
            myRooms.Add(myObject);
        }

        public List<PlayerData> GetAllPlayersData()
        {
            return myPlayers2;
        }

        public PlayerData PlayerInfo;

        public string MapTextureAtlas { get; set; }
        public int TextureAtlasTileWidth { get; set; }
        public int TextureAtlasTileHeight { get; set; }

        public Dictionary<Tuple<float, float, float>, TileData> myModifiedTiles = new Dictionary<Tuple<float, float, float>, TileData>();
        [NonSerialized]
        FastNoise myNoise;
        private PlayerData mySentToData;

        public Map(string mapID)
        {
            myNoise = new FastNoise();
            myNoise.SetSeed(MapSeed);
            myNoise.SetNoiseType(FastNoise.NoiseType.SimplexFractal);
            this.mapID = mapID;
        }

        public IEnumerable<NetConnection> GetPlayerIPs()
        {
            return myPlayers.Keys.AsEnumerable();
        }

        public TileData GetTileAt(int x, int y, int z)
        {
            TileData myResult;
            if (myModifiedTiles.TryGetValue(new Tuple<float, float, float>(x, y, z), out myResult))
            {
                return myResult;
            }
            return new TileData(new Vector3(x, y, myNoise.GetNoise(x, y)));
        }

        public string GetID()
        {
           return mapID;
        }


        public void AddModifiedTiles(TileData[,] tile)
        {
            for(int x= 0; x < tile.GetLength(0); x++)
            {
                for (int y = 0; y < tile.GetLength(1); y++)
                {
                    myModifiedTiles.Add(new Tuple<float, float, float>(tile[x, y].GetPosition().X, tile[x, y].GetPosition().Y, tile[x, y].GetPosition().Z), tile[x, y]);
                }
            }
        }

       
        public void AddPlayer(NetConnection myPlayerEndpoint, PlayerData myPlayer)
        {
            while (!myPlayers.TryAdd(myPlayerEndpoint, myPlayer)) ;
        }

        public void SetMaterialChanceTable(SimpleProbabilityTable myTable)
        {
            this.Materials = myTable;
        }
    }
}
