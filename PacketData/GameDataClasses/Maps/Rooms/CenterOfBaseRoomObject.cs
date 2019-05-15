using DedicatedServerFramework.MapGeneration.BuildingRule;
using GameData.GameDataClasses.Maps;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DedicatedServerFramework.MapGeneration
{
    [Serializable]
    public class CenterOfBaseRoomObject : RoomData
    {
        public CenterOfBaseRoomObject() : base("Base")
        {

        }
    }
}
