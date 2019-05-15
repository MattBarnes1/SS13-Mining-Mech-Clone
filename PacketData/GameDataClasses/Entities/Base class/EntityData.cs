using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;
using GameData.GameDataClasses.AnimationData;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace DedicatedServer.GameDataClasses.Entities.Base_class
{

    [Serializable]
    public abstract class EntityData : ISerializable
    {
        [NonSerialized]
        public Vector3 myLocation;
        public bool Active { get; set; } = true;
        public Animation myAnimation;
        public string CurrentMapID { get; set; }
        public void Draw(SpriteBatch spriteBatch)
        {
            myAnimation.Draw(spriteBatch, myLocation);
        }

        public EntityData() { }
        public EntityData(SerializationInfo info, StreamingContext context)
        {
            myLocation = new Vector3((float)info.GetValue("LocationX", typeof(float)), (float)info.GetValue("LocationY", typeof(float)), (float)info.GetValue("LocationZ", typeof(float)));
            myAnimation = (Animation)info.GetValue("myAnimation", typeof(Animation));
            CurrentMapID = (String)info.GetValue("MapID", typeof(String));
        }


        public void SetAnimation(Animation animationTransferData)
        {
            myAnimation = animationTransferData;
        }

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("LocationX", myLocation.X);
            info.AddValue("LocationY", myLocation.Y);
            info.AddValue("LocationZ", myLocation.Z);
            info.AddValue("myAnimation", myAnimation);
            info.AddValue("MapID", CurrentMapID);
        }
    }
}
