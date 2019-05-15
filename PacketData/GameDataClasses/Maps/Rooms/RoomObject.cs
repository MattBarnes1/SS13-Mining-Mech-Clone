using GameData.GameDataClasses.Maps;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DedicatedServerFramework.MapGeneration.BuildingRule
{
    [Serializable]
    public class RoomData
    {
        public float maxRoomOxygen { get; private set; }
        public float currentRoomOxygen { get; set; }



        public RoomData(String ID)
        {
            this.Name = ID;

        }


        public List<TileData> TileData { get; private set; } = new List<TileData>();

        public void AddTextureAtlas(string v1, string v2)
        {
            throw new NotImplementedException();
        }

        public string Name { get; }


        public void SetFloorTileAt(int x, int y, int z, int TexturePositionID, string TextureToUses)
        {
            throw new NotImplementedException();
        }
        public void SetWallTileAt(int x, int y, int z, int TexturePositionID, string TextureToUses)
        {
            throw new NotImplementedException();
        }

    }
}
