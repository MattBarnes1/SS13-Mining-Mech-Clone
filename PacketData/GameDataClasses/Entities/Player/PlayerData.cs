using DedicatedServer.GameDataClasses.Entities.Base_class;
using GameData.GameDataClasses.AnimationData;
using GameData.GameDataClasses.Entities;
using GameData.GameDataClasses.Races;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Nez;
using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using static GameData.GameDataClasses.Characters.CharacterTexture;

namespace DedicatedServer.GameDataClasses.Entities
{
    [Serializable]
    public class PlayerData : EntityData, ISerializable
    {


        
        public void SetName(string v)
        {
            Playername = v;
        }

        public string UserID { get; private set; }
        public string Playername { get; private set; }
        public string HomeMapID { get; set; }
        public Transform Transform { get; internal set; }
        public SEX? Sex { get; set; } = null;

        [NonSerialized]
        PlayerControlScheme myControls;
        private SEX mySex;
        private Race myRace;

        public PlayerData() { }
        protected PlayerData(SerializationInfo info, StreamingContext context) : base(info, context)
        {
            HomeMapID = (String)info.GetValue("HomeMapID", typeof(string));
            Playername = (String)info.GetValue("Playername", typeof(String));
            UserID = (String)info.GetValue("Username", typeof(String));
            myRace = (Race)info.GetValue("Race", typeof(Race));
            mySex = (SEX)info.GetValue("Sex", typeof(SEX));
        }

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            base.GetObjectData(info, context);
            info.AddValue("HomeMapID", HomeMapID);
            info.AddValue("Username", UserID);
            info.AddValue("Playername", Playername);
            info.AddValue("Race", myRace);
            info.AddValue("Sex", mySex);
        }
        DirectionFace myFace = DirectionFace.EAST;
        private int bodySelected = -1;
        private int hairstyleSelected = -1;

        public DirectionFace GetLastSpriteDirection()
        {
            return myFace;
        }

        public void SetRace(Race aRace)
        {
            this.myRace = aRace;
        }

        public void SetSex(SEX aSex)
        {
            this.mySex = aSex;
        }


        public Race GetRace()
        {
            return myRace;
        }

        public void SetBodySelected(int a)
        {
            bodySelected = a;
        }

        public void SetHairstyleSelected(int a)
        {
            hairstyleSelected = a;
        }

        public int GetHairstyle()
        {
            return hairstyleSelected;
        }

        public int GetBodyStyle()
        {
            return bodySelected;
        }
    }
}
