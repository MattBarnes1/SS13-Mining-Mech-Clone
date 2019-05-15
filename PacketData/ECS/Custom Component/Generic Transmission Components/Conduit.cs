using Nez;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameData.Custom_Component.Generic_Transmission_Components
{
    public class Conduit<T> : Component, IUpdatable where T : IGetSetDataObject
    {
        public const float MaxValue = 10;
        public virtual void update()
        {
            float deltaTime = Data.GetValue() - (Data.GetValue() * TransmissionRate);
            foreach(Conduit<T> A in myConduits)
            {
                if(A.TransmissionRate != 0f)
                {
                    A.EqualizeData(this); //Equalizes with all surrounding conduits
                }
            }
        }

        private void EqualizeData(Conduit<T> conduit)
        {

        }


        float TransmissionRate = 1f;
        private readonly T Data;
        List<Conduit<T>> myConduits;
        public Conduit(T MyData, List<Conduit<T>> myConduits)
        {
            this.myConduits = myConduits;
            this.Data = MyData;
        }
    }
}
