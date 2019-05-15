using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DedicatedServerFramework.MapGeneration.BuildingRule;
using Microsoft.Xna.Framework;

namespace GameData.GameDataClasses.Maps.Air_Pressure
{
    [Serializable]
    public class AirProducer
    {
        RoomData myRoomToProduceTo;
        private float IntialProduction;
        public AirProducer(int IntialProductionPerSecond, int StartingPressure)
        {
            this.IntialProduction = IntialProductionPerSecond;
        }        

        public void UpdateAirpressure(GameTime myUpdate)
        {
            if(myRoomToProduceTo.maxRoomOxygen > myRoomToProduceTo.currentRoomOxygen)
            {
                myRoomToProduceTo.currentRoomOxygen += IntialProduction * (myUpdate.ElapsedGameTime.Milliseconds / 1000);
            }     
        }
    }
}
