using Microsoft.Xna.Framework;
using Nez;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameData.Custom_Component.Generic_Transmission_Components
{
    public class Consumer<T> : Conduit<T> where T : IGetSetDataObject
    {
        public override void update()
        {
            ConduitData.SetValue(ConduitData.GetValue() - ConduitData.GetValue() * ConsumptionRate* Time.deltaTime); 
            base.update();
        }

        float ConsumptionRate;
        private T ConduitData;

        public Consumer(float ConsumptionRate, T MyData, List<Conduit<T>> myConduits) : base(MyData, myConduits)
        {
            this.ConsumptionRate = ConsumptionRate;
            this.ConduitData = MyData;
        }
    }
}
