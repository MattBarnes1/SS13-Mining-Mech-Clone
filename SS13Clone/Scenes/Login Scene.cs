using GameData.Custom_Component;
using GameData.ECS.Custom_Systems;
using GameData.VersionHandling;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Nez;
using Nez.Sprites;
using Nez.UI;
using PacketData.Packets.PacketTypes;
using PacketData.Packets.Superclasses.Login;
using PacketData.Packets.Superclasses.Player;
using PacketData.UDPServiceHandler;
using SS13Clone.Managers;
using SS13Clone.Managers.SceneManager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SS13Clone.Scenes
{
    public class LoginScene : Scene
    {
        private int SCREEN_SPACE_RENDER_LAYER = 999;

        public LoginScene() : base()
        {
        }

        TextField myUsername;
        TextField myPassword;
        UICanvas canvas;

        public override void initialize()
        {
            this.myService = (CommunicationManager)Nez.Core.services.GetService(typeof(CommunicationManager));
            // default to 1280x720 with no SceneResolutionPolicy
            setDesignResolution(1280, 720, Scene.SceneResolutionPolicy.ExactFit);
            Screen.setSize(1280, 720);
            var myRenderer = new ScreenSpaceRenderer(100, SCREEN_SPACE_RENDER_LAYER);
            myRenderer.wantsToRenderAfterPostProcessors = false;
            addRenderer(myRenderer);
            var myGUIRenderer = new RenderLayerExcludeRenderer(0, SCREEN_SPACE_RENDER_LAYER);
            addRenderer(myGUIRenderer);
            canvas = createEntity("GUI").addComponent(new UICanvas());
            canvas.isFullScreen = true;           // this.myCommunicationsManager = myCommunicationsManager;
            SpriteFont myFont = content.Load<SpriteFont>("Arial");
            Texture2D TextboxBackground = content.Load<Texture2D>("GUI/Login Elements/TextField");
            Texture2D PasswordHeader = content.Load<Texture2D>("GUI/Login Elements/PasswordButtonHeader");
            Texture2D LoginHeader = content.Load<Texture2D>("GUI/Login Elements/LoginButtonHeader");
            Texture2D LoginBackground = content.Load<Texture2D>("GUI/Login Elements/LoginBackground");
            canvas.renderLayer = SCREEN_SPACE_RENDER_LAYER;
            
            
            TextFieldStyle myStyle = TextFieldStyle.create(Color.White, Color.White, Color.Blue, Color.Black);
            VerticalGroup myVerticalGroup = new VerticalGroup();
            Image myLabel1 = new Image(LoginHeader);
            myVerticalGroup.addElement(myLabel1);
            myStyle.background = new SubtextureDrawable(TextboxBackground);
            
            myUsername = new TextField("", myStyle);
            myUsername.setHeight(TextboxBackground.Height);
            myUsername.setBounds(0, 0, TextboxBackground.Width, TextboxBackground.Height);
            myUsername.setPreferredWidth(TextboxBackground.Width);
            myUsername.setAlignment(Align.center);
            myUsername.setMaxLength(10);

            myVerticalGroup.addElement(myUsername);
            Image myLabel2 = new Image(PasswordHeader);
            myVerticalGroup.addElement(myLabel2);
            myPassword = new TextField("", myStyle);
            myPassword.setPasswordMode(true);
            myPassword.setPreferredWidth(TextboxBackground.Width);
            myPassword.setAlignment(Align.center);
            myPassword.setMaxLength(10);
            myVerticalGroup.addElement(myPassword);
            myLoginFailure = new Label("Invalid Login!");
            myVerticalGroup.addElement(myLoginFailure);
            canvas.stage.addElement(new Image(LoginBackground, Scaling.Fit, 0));
            canvas.stage.addElement(myVerticalGroup);
            myVerticalGroup.setPosition(Screen.width/2, Screen.height/2);
            var moonTex = content.Load<Texture2D>("GUI/Mouse/Green");
            var playerEntity = createEntity("player", new Vector2(200,200));
            playerEntity.addComponent(new Sprite(moonTex));
            myBUtton = new VirtualButton();
            var versionLabel = new Label(InternalSettings.Version);

            versionLabel.setPosition(0, Screen.height - versionLabel.preferredHeight);
            canvas.stage.addElement(versionLabel);
            myLoginFailure.setIsVisible(false);
            myBUtton.nodes.Add(new Nez.VirtualButton.KeyboardKey(Microsoft.Xna.Framework.Input.Keys.Enter));
            myService.AssignProcessor(delegate (Packet P)
            {
                myLoginFailure.setIsVisible(true);
            }, typeof(LoginFailedPacket));


            myService.AssignProcessor(delegate (Packet P)
            {
                if(((PlayerInfoPacket)P).NewChar)
                {
                    CharacterCreation myScene = SceneManager.GetScene<CharacterCreation>("CharacterCreator");
                    myScene.SetPlayerData(((PlayerInfoPacket)P).GetPlayerData());
                    MainWorldViewScene myWorldScene = SceneManager.GetScene<MainWorldViewScene>("MainWorldViewScene");
                    myWorldScene.SetPlayerData(((PlayerInfoPacket)P).GetPlayerData());
                    SceneManager.ChangeScenes("CharacterCreator");
                    myWorldScene.SetMapData(((PlayerInfoPacket)P).myMap);                    
                    myService.AssignProcessor(null, typeof(PlayerInfoPacket));
                    myService.AssignProcessor(null, typeof(LoginFailedPacket));
                }
                else
                {
                    MainWorldViewScene myScene = SceneManager.GetScene<MainWorldViewScene>("MainWorldViewScene");
                    myScene.SetPlayerData(((PlayerInfoPacket)P).GetPlayerData());
                    myScene.LoadInPlayer();
                    myScene.SetMapData(((PlayerInfoPacket)P).myMap);
                    SceneManager.ChangeScenes("MainWorldViewScene");
                    myService.AssignProcessor(null, typeof(PlayerInfoPacket));
                    myService.AssignProcessor(null, typeof(LoginFailedPacket));
                }

            }, typeof(PlayerInfoPacket));
            base.initialize();
        }

        public override void onStart()
        {
            SongSystem.LoadSong("Ruskerdax - Pondering the Cosmos");
            base.onStart();
        }

        public override void unload()
        {
            base.unload();
        }

        VirtualButton myBUtton;
        private CommunicationManager myService;
        private Label myLoginFailure;

        public override void update()
        {
            if (myBUtton.isPressed)
            {
                PacketData.Packets.Superclasses.LoginPacket myPacket = new PacketData.Packets.Superclasses.LoginPacket();
                byte[] myUserSH = myPacket.CreateUsernamePasswordHash(myUsername.getText(), myPassword.getText());
                if(myUserSH != null)
                {
                    myService.Send(myPacket, Lidgren.Network.NetDeliveryMethod.ReliableOrdered);
                }
            }
            base.update();
        }
    }
}
