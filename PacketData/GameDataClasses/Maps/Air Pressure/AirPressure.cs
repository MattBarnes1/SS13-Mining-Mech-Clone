using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace GameData.GameDataClasses.Maps.Air_Pressure
{
    [Serializable]
    public class AirPressure : ISerializable
    {
        public const float MaxAirVolume = 100;
        public float TotalAirVolume;
        Vector3 myAirFlow = new Vector3();

        public AirPressure(Vector3 myAirFlow, int StartingPressure)
        {
            this.myAirFlow = myAirFlow;
            TotalAirVolume = StartingPressure;
        }

        public AirPressure(SerializationInfo info, StreamingContext context)
        {

        }

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {

        }

        public void ConsumeAir(float Amount)
        {
            TotalAirVolume = Math.Min(TotalAirVolume -= Amount, 0);
        }
        public virtual void EqualizeAir(AirPressure ConnectedAirPressure)
        {
            float Difference = Math.Abs(this.TotalAirVolume - ConnectedAirPressure.TotalAirVolume);
            if (ConnectedAirPressure.TotalAirVolume < this.TotalAirVolume)
            {
                ConnectedAirPressure.TotalAirVolume += Difference;
                this.TotalAirVolume -= Difference;
            }
            else if (ConnectedAirPressure.TotalAirVolume > this.TotalAirVolume)
            {
                this.TotalAirVolume += Difference;
                ConnectedAirPressure.TotalAirVolume -= Difference;
            }
        }
        public void AddAir(float intialProduction)
        {
            this.TotalAirVolume += intialProduction;
        }

       

        public virtual void UpdateAirpressure(GameTime myUpdate)
        {

        }

    }

}
