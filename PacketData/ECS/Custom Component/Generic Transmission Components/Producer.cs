using Nez;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameData.Custom_Component.Generic_Transmission_Components
{
    /// <summary>
    /// A producer pulses an amount such that it equals the TimeDelta * ProductionRate.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class Producer<T> : Conduit<T> where T : IGetSetDataObject
    {
        public override void update()
        {
            ProducersConnection.SetValue(ProducersConnection.GetValue() - ProducersConnection.GetValue() * ProductionRate * Time.deltaTime);
            base.update();
        }
        float ProductionRate;
        T ProducersConnection;
        public Producer(float ProductionRate, List<Conduit<T>> myConduits, T ProducersConduitAmount) : base(ProducersConduitAmount, myConduits)
        {
            this.ProducersConnection = ProducersConduitAmount;
            this.ProductionRate = ProductionRate;
        }
    }
}
