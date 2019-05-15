using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameData.ECS.Custom_Component.AudioSFX_component;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Media;
using Nez;
using Nez.Systems;

namespace SS13Clone.Managers
{
    public class AudioSystem : Nez.EntityProcessingSystem
    {
        private static AudioSystem myHandler;
        private static Dictionary<String, SoundEffect> mySounds = new Dictionary<string, SoundEffect>();
        Transform PlayerTransform;
        public AudioSystem() : base(new Matcher().all(new Type[] { typeof(AudioSFX) }))
        {
            Nez.Core.services.AddService(this);
            AudioSystem.myHandler = this;
        }





        protected override void process(List<Entity> entities)
        {
            foreach(Entity A in entities)
            {
                AudioSFX myObject = A.getComponent<AudioSFX>();
                if(!mySounds.ContainsKey(myObject.SoundString))
                {
                    if (myObject.isDistanceDependent)
                    {
                        if(myObject.SoundEffectDistance < Vector2.Distance(PlayerTransform.position, A.position))
                        {
                            mySounds.Add(myObject.SoundString, Nez.Core.content.Load<SoundEffect>(myObject.SoundString));
                        }
                    }
                    else
                    {
                        mySounds.Add(myObject.SoundString, Nez.Core.content.Load<SoundEffect>(myObject.SoundString));
                    }
                }
            }
            base.process(entities);
        }

        

        public override void process(Entity entity)
        {

        }
    }
}
