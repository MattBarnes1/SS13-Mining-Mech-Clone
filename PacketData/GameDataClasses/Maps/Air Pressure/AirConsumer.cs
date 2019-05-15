using DedicatedServerFramework.MapGeneration.BuildingRule;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameData.GameDataClasses.Maps.Air_Pressure
{
    [Serializable]
    public class AirConsumer
    {
        RoomData myRoomToConsumeFrom;
        private float IntialConsumption;
        public AirConsumer(float IntialConsumptionPerSecond)
        {
            this.IntialConsumption = IntialConsumptionPerSecond;
        }

        public void UpdateAirpressure(GameTime myUpdate)
        {
            myRoomToConsumeFrom.currentRoomOxygen -= IntialConsumption * (myUpdate.ElapsedGameTime.Milliseconds / 1000);
        }
    }
}
