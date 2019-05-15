using Nez;
using SS13Clone.ECS.Custom_Component.Generic_Transmission_Components.Resevoir;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameData.Custom_Component.Generic_Transmission_Components
{
    public class Reservoir<Q> : Conduit<Q> where Q : IGetSetDataObject
    {
        public override void update()
        {
            if(OnOff && ReservoirData.GetValue() > 0)
            {
                ConnectedConduit.SetValue(ReservoirData.GetValue() - ReservoirData.GetValue() * ExpulsionRate * Time.deltaTime);
            }
            base.update();
        }
        private readonly Q ReservoirData;
        private Q ConnectedConduit;
        private float MaxReservoir;
        float ExpulsionRate = 0.02f;
        //TODO: recharging res
        IReservoirRecharge myRecharge;
        


        public bool OnOff { get; set; }
        public Reservoir(float ExpulsionRate, float SyphonRate, float maxRes, Q ResvoirData, Q ConduitStartingAmount, List<Conduit<Q>> myConduits) : base(ConduitStartingAmount, myConduits)
        {
            this.MaxReservoir = maxRes;
            this.ExpulsionRate = ExpulsionRate;
            this.ReservoirData = ResvoirData;
            this.ConnectedConduit = ConduitStartingAmount;
        }
    }
}
