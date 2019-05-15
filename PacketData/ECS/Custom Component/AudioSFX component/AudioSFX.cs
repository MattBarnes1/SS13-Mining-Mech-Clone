using Nez;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameData.ECS.Custom_Component.AudioSFX_component
{
    public class AudioSFX : Component
    {
        internal bool isDistanceDependent;

        public string SoundString { get; internal set; }
        public float SoundEffectDistance { get; internal set; }
    }
}
