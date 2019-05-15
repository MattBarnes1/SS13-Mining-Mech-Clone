using System;
using System.Collections.Concurrent;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading;
using System.Threading.Tasks;
using DedicatedServer.GameDataClasses.Entities;
using GameData.GameDataClasses.Maps;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SS13Clone.Managers;

namespace SS13Clone
{
    public class MapHandler
    {
        TileData[,] myDrawRegions;

        ConcurrentDictionary<String, Texture2D> TileTextureLookup = new ConcurrentDictionary<string, Texture2D>();
        private CommunicationManager myCommunicationsManager;
        private GraphicsDevice graphicsDevice;

        public MapHandler(CommunicationManager myCommunicationsManager, GraphicsDevice myDevice)
        {
            this.myCommunicationsManager = myCommunicationsManager;
            this.graphicsDevice = myDevice;
            //TODO: figure out map region width and height.
            //CREATE REGION THREADS: figure out map region width and height.
            int RegionWidth = (int)Math.Ceiling((double)(myDevice.DisplayMode.Width / (TileData.TileSize)));
            int RegionHeight = (int)Math.Ceiling((double)(myDevice.DisplayMode.Height / (TileData.TileSize)));
            myDrawRegions = new TileData[RegionWidth, RegionHeight];
        }

        private void MyPlayerLoaded(object arg1)
        {
            PlayerData mYdata = arg1 as PlayerData;
            
        }

        Task MapLoading;
        Map myMap;
        private void StartMapLoading(object arg1)
        {
           MapLoading = Task.Factory.StartNew(delegate(Object obj) 
           {
               myMap = obj as Map;
               SaveMapLocally(myMap);

           }, arg1, TaskCreationOptions.None);
           
        }

        private void SaveMapLocally(Map myData)
        {
            BinaryFormatter myMap = new BinaryFormatter();
            Directory.CreateDirectory("./MapData");           
        }

        public void LoadRegionsBasedOnPosition(Vector2 myPlayerVector)
        {
            float DisplayedRegionX = myDrawRegions.GetLength(0);
            float DisplayedRegionY = myDrawRegions.GetLength(1);


        }

        public bool PlayerHasMoved()
        {
            return false;
        }

        public void Update(GameTime T)
        {
            if(myDrawRegions != null && MapLoading == null)
            {
                for (int x = 0; x < myDrawRegions.GetLength(0); x++)
                {
                    for (int y = 0; y < myDrawRegions.GetLength(1); y++)
                    {
                        if (myDrawRegions[x, y] != null) myDrawRegions[x, y].Update(T);
                    }
                }
            }
            //myEntities.Update(T);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (myDrawRegions != null && MapLoading == null)
            {
                for (int x = 0; x < myDrawRegions.GetLength(0); x++)
                {
                    for (int y = 0; y < myDrawRegions.GetLength(1); y++)
                    {
                       // if (myDrawRegions[x, y] != null) myDrawRegions[x, y].Draw(spriteBatch, vec);
                    }
                }
            }
            
            //myEntities.Draw(spriteBatch);
        }

    }
}