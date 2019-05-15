using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Nez;
using PacketData.UDPServiceHandler;
using PipelineExtension.Characters;
using PipelineExtension.IntermediateFiles;
using SS13Clone.GUI.Windows;
using SS13Clone.Managers;
using SS13Clone.Managers.SceneManager;
using SS13Clone.Scenes;
using System;
using System.IO;
/// <summary>
/// https://docs.microsoft.com/en-us/dotnet/api/system.net.sockets.udpclient?redirectedfrom=MSDN&view=netframework-4.7.2
/// </summary>
namespace SS13Clone
{

    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Core
    {
        AudioSystem myAudioHandler;
        SceneManager myManager;
        private CommunicationManager Communications;







        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        /// 
        protected override void Initialize()
        {
            //
            base.Initialize();
            myManager = new SceneManager(this);
            IsMouseVisible = true;
            Window.AllowUserResizing = true;
            Communications = new CommunicationManager();
            Nez.Core.services.AddService(Communications);
            RaceIntermediateText MyTexture = new RaceIntermediateText();
            MyTexture.FemaleBodies = new PipelineExtension.IntermediateFiles.Characters.TextureIntermediateText[1];
            MyTexture.FemaleBodies[0] = new PipelineExtension.IntermediateFiles.Characters.TextureIntermediateText();

            MyTexture.MaleBodies = new PipelineExtension.IntermediateFiles.Characters.TextureIntermediateText[1];
            MyTexture.MaleBodies[0] = new PipelineExtension.IntermediateFiles.Characters.TextureIntermediateText();

            MyTexture.Bones = new PipelineExtension.IntermediateFiles.Characters.Bones.BoneIntermediateText[1];
            MyTexture.Bones[0] = new PipelineExtension.IntermediateFiles.Characters.Bones.BoneIntermediateText();

           

            MyTexture.Hair = new PipelineExtension.IntermediateFiles.Characters.TextureIntermediateText[2];
            MyTexture.Hair[0] = new PipelineExtension.IntermediateFiles.Characters.TextureIntermediateText();
            MyTexture.Hair[1] = new PipelineExtension.IntermediateFiles.Characters.TextureIntermediateText();

            MyTexture.Organs = new PipelineExtension.IntermediateFiles.Characters.Organs.OrganIntermediateText[1];
            MyTexture.Organs[0] = new PipelineExtension.IntermediateFiles.Characters.Organs.OrganIntermediateText();

            MyTexture.RaceName = "Example";
            string myRet = Newtonsoft.Json.JsonConvert.SerializeObject(MyTexture,Newtonsoft.Json.Formatting.Indented);
            File.WriteAllText("./RaceExample.race", myRet);

            ClothingText myText = new ClothingText();
            myText.myLocalPosition = new Vector3();
            myText.RoleName = null;
            myText.TexturePath = "D:\\";
            myText.myDamageResistances = new ItemAttributes.DamageResistanceHolder();
            myText.myDamageResistances.AddArmorResistance(new ItemAttributes.DamageResistance()
            {
                percentAmount = 1f,
                ResistanceType = "Ehcid"
            });
            myRet = Newtonsoft.Json.JsonConvert.SerializeObject(myText, Newtonsoft.Json.Formatting.Indented);
            File.WriteAllText("./ClothExample.cloth", myRet);

            myAudioHandler = new AudioSystem();
            myManager.AddSceneToList(new LoginScene(), "LoginScene");
            var Scene = new CharacterCreation(); //Used for debug
            //Scene.SetPlayerData(new DedicatedServer.GameDataClasses.Entities.PlayerData());
            myManager.AddSceneToList(Scene, "CharacterCreator");
            myManager.AddSceneToList(new MainWorldViewScene(), "MainWorldViewScene");
            SceneManager.ChangeScenes("LoginScene");
        }

        





        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            base.LoadContent();
        }

        protected override void OnExiting(object sender, EventArgs args)
        {
            //myCommunicationsManager.Shutdown();
            base.OnExiting(sender, args);
        }

        protected override void Update(GameTime gameTime)
        {
            myManager.Update();
            base.Update(gameTime);
        }
    }
}
