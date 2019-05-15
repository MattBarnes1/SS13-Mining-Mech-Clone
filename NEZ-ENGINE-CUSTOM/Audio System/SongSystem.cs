using Microsoft.Xna.Framework.Media;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameData.ECS.Custom_Systems
{
    public class SongSystem
    {
        public static void StopSong()
        {
            MediaPlayer.Stop();
        }

        public static void LoadSong(String SongName)
        {

            Song aSong = Nez.Core.content.Load<Song>("./Songs/" + SongName);
            if (CurrentSong != aSong)
            {
                CurrentSong = aSong;
                MediaPlayer.IsRepeating = true;
                MediaPlayer.Play(aSong);
            }
        }

        static Song CurrentSong;
    }
}
