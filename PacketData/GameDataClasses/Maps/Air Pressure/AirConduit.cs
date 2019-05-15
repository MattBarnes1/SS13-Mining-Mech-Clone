using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using DedicatedServerFramework.MapGeneration.BuildingRule;
using Microsoft.Xna.Framework;

namespace GameData.GameDataClasses.Maps.Air_Pressure
{
    [Serializable]
    public class AirConduit : ISerializable
    {
        List<RoomData> ConnectedRooms = new List<RoomData>();
        Vector3 RateOfFlow = new Vector3();


        public AirConduit(SerializationInfo info, StreamingContext context) 
        {
            RateOfFlow = new Vector3((float)info.GetValue("LocationX", typeof(float)), (float)info.GetValue("LocationY", typeof(float)), (float)info.GetValue("LocationZ", typeof(float)));
        }

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("LocationZ", RateOfFlow.Z);
            info.AddValue("LocationX", RateOfFlow.X);
            info.AddValue("LocationY", RateOfFlow.Y);
        }

        public AirConduit(Vector3 myFlowRate, int StartingPressure)
        {
            RateOfFlow = myFlowRate;
        }
        
        public void EqualizeAir()
        {
            /*var Difference = Math.Abs(this.TotalAirVolume - ConnectedAirPressure.TotalAirVolume);
            if (ConnectedAirPressure.TotalAirVolume < this.TotalAirVolume)
            {
                ConnectedAirPressure.TotalAirVolume += Difference * RateOfFlow;
                this.TotalAirVolume -= Difference * RateOfFlow;
            }
            else if (ConnectedAirPressure.TotalAirVolume > this.TotalAirVolume)
            {
                this.TotalAirVolume += Difference * RateOfFlow;
                ConnectedAirPressure.TotalAirVolume -= Difference * RateOfFlow;
            }*/
        }

    }
}
