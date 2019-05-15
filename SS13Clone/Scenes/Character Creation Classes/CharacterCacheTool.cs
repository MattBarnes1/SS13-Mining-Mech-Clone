using System;
using System.Collections.Generic;
using System.IO;
using GameData.GameDataClasses.Entities;
using GameData.GameDataClasses.Races;
using Microsoft.Xna.Framework.Graphics;
using Nez.Systems;

namespace SS13Clone.GUI.Windows
{
    public class CharacterLoadingUtilitiy
    {
        private NezContentManager myCache;
        const String myLocation = "Character Sprites";
        public CharacterLoadingUtilitiy()
        {
            this.myCache = Nez.Core.content;
        }

        public T[] LoadAllInDirectoryAs<T>(String myDirectory)
        {
            return null;
        }

        public Race[] LoadRaces()
        {
            string[] myArray = Directory.GetDirectories(myLocation + "/pawns/");
            Race[] myRaces = new Race[myArray.Length];
            for(int i = 0; i < myArray.Length; i++)
            {
                myRaces[i] = new Race(myArray[i].Remove(myArray[i].LastIndexOf(Path.DirectorySeparatorChar)));
                string[] Parts = Directory.GetDirectories(myArray[i]);
                string ret = Parts[i].Remove(Parts[i].LastIndexOf(Path.DirectorySeparatorChar));
            }




            return myRaces;
        }

        public void GetHairstylesFor(Race myRace)
        {

        }

        public void GetBodiesFor(Race myRace)
        {
            
        }

        public void GetClothes(Race myRace)
        {
            String mySexLocation = null;

            mySexLocation = myLocation + "/clothing/" + myRace.RaceName.ToLower() + "/female/";
            List <Tuple<string, Texture2D[]>> myLoadedFiles = RecurseInDirectoryAs<Texture2D>(mySexLocation);
            myRace.AddClothing("female", myLoadedFiles);

            mySexLocation = myLocation + "/clothing/" + myRace.RaceName.ToLower() + "/male/";
            myLoadedFiles = RecurseInDirectoryAs<Texture2D>(mySexLocation);
            myRace.AddClothing("male", myLoadedFiles);

            mySexLocation = myLocation + "/clothing/" + myRace.RaceName.ToLower() + "/all/";
            myLoadedFiles = RecurseInDirectoryAs<Texture2D>(mySexLocation);
            myRace.AddClothing("all", myLoadedFiles);

        }

        private List<Tuple<string, T[]>> RecurseInDirectoryAs<T>(string mySexLocation)
        {
            throw new NotImplementedException();
        }
    }
}