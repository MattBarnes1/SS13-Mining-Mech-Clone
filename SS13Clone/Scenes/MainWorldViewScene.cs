using DedicatedServer.GameDataClasses.Entities;
using GameData.Custom_Component;
using GameData.GameDataClasses.Maps;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Nez;
using Nez.Sprites;
using Nez.Tiled;
using PacketData.UDPServiceHandler;
using SS13Clone.Managers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SS13Clone.Scenes
{
    public class MainWorldViewScene : Nez.Scene
    {
        private int SCREEN_SPACE_RENDER_LAYER = 999;
        private Map myMapData;
        private UDPClient myService;

        public PlayerData myPlayerInfo { get; private set; }
        public Vector2 ScreenVector { get; private set; }
        public Entity PlayerEntity { get; private set; }

        List<Entity> myActiveEntities = new List<Entity>();
        TiledTileLayer myCollisionLayer;
        public override void onStart()
        {
            myMapData.Load(content, ScreenVector);
            myCollisionLayer = myMapData.GetCollisionLayer();
            List<PlayerData> myPlayers = myMapData.GetAllPlayersData();
            foreach(PlayerData A in myPlayers)
            {
                if(A != myPlayerInfo)
                {
                    myActiveEntities.Add(CreateLivingEntity(A, myCollisionLayer));
                }
            }
            base.onStart();
        }
        DirectionalSprite PlayerDirectional;
        private TiledMapMover PlayerTileMapMover;

        public void LoadInPlayer()
        {          
            PlayerEntity = createEntity(myPlayerInfo.UserID);
            PlayerDirectional = new DirectionalSprite(myPlayerInfo.GetLastSpriteDirection());
            PlayerEntity.addComponent(PlayerDirectional);
            PlayerTileMapMover = new TiledMapMover(myCollisionLayer);
            PlayerEntity.addComponent(PlayerTileMapMover);            
        }

        private Entity CreateLivingEntity(PlayerData aPlayersInfo, TiledTileLayer myCollisionLayer)
        {
            var myEntity = createEntity(aPlayersInfo.UserID);
            myEntity.addComponent(new DirectionalSprite(aPlayersInfo.GetLastSpriteDirection()));
            return myEntity;
        }

        public void setScreenWidthHeight(Vector2 myWidthHeightScreen)
        {
            this.ScreenVector = myWidthHeightScreen;
        }

        public override void initialize()
        {
            var myMapRenderer = new ScreenSpaceRenderer(100, SCREEN_SPACE_RENDER_LAYER);
            base.addRenderer(myMapRenderer);
            this.myService = (CommunicationManager)Nez.Core.services.GetService(typeof(CommunicationManager));
            base.addEntityProcessor(new PlayerMovementSystem());
            base.initialize();
        }

        public void SetPlayerData(PlayerData myData)
        {
            this.myPlayerInfo = myData;
        }
        public void SetMapData(Map aMap)
        {
            this.myMapData = aMap;
        }
    }
}
