using System;
using System.Collections.Generic;
using System.IO;
using DedicatedServer.GameDataClasses.Entities;
using GameData.GameDataClasses.Races;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Nez;
using Nez.BitmapFonts;
using Nez.Sprites;
using Nez.UI;
using PacketData.UDPServiceHandler;
using SS13Clone.Managers;

namespace SS13Clone.Scenes
{
    public class CharacterCreation : Scene
    {
        private UICanvas canvas;
        private ScrollPane Bodies;
        private ScrollPane Hair;

        private ScrollPane Head;
        private ScrollPane Chest;
        private ScrollPane Legs;

        public CommunicationManager Communication { get; private set; }
        public PlayerData PlayerData { get; private set; }

        private int SCREEN_SPACE_RENDER_LAYER = 999;
        private Label myRaceDescription;
        private VerticalGroup myCharacterViewerGroup;

        Button CharacterCreatorSwitch;

        public override void initialize()
        {
            this.Communication = Nez.Core.services.GetService<CommunicationManager>();
            setDesignResolution(1280, 720, Scene.SceneResolutionPolicy.NoBorderPixelPerfect);
            Screen.setSize(1280, 720);
            var myRenderer = new ScreenSpaceRenderer(100, SCREEN_SPACE_RENDER_LAYER);
            myRenderer.wantsToRenderAfterPostProcessors = false;
            addRenderer(myRenderer);
            var myGUIRenderer = new RenderLayerExcludeRenderer(0, SCREEN_SPACE_RENDER_LAYER);
            addRenderer(myGUIRenderer);
            canvas = createEntity("GUI").addComponent(new UICanvas());
            Texture2D LoginBackground = content.Load<Texture2D>("GUI/Login Elements/LoginBackground");
            //canvas.isFullScreen = true;
            canvas.stage.addElement(new Image(LoginBackground));
            ScrollPaneStyle myScrollStyle = new ScrollPaneStyle();
            myScrollStyle.hScrollKnob = new SubtextureDrawable(Nez.Core.content.Load<Texture2D>("GUI/Scrollbar/ScrollHandle"));
            myScrollStyle.hScroll = new SubtextureDrawable(Nez.Core.content.Load<Texture2D>("GUI/Scrollbar/HScrollBar"));
            Bodies = new ScrollPane(myBodyGroup, myScrollStyle);
            Hair = new ScrollPane(aHairGroup, myScrollStyle);
            HorizontalGroup myHGroup = new HorizontalGroup();
            myHGroup.setAlignment(Align.left);
            myHGroup.setY(70);
            myHGroup.setSpacing(0);
            VerticalGroup myCharacterDataGroup = new VerticalGroup();
            myCharacterDataGroup.setSpacing(20);
            myCharacterDataGroup.setAlignment(Align.left);
            myCharacterViewerGroup = new VerticalGroup();
            myCharacterViewerGroup.setY(70);
            HorizontalGroup mySexGroup = new HorizontalGroup();
            mySexGroup.setSpacing(10);


            CheckBox Male = new CheckBox("Male", new CheckBoxStyle(new SubtextureDrawable(Nez.Core.content.Load<Texture2D>("GUI/Checkbox Elements/Checkbox_Unchecked")), new SubtextureDrawable(Nez.Core.content.Load<Texture2D>("GUI/Checkbox Elements/Checkbox_Checked")), Nez.Core.content.Load<BitmapFont>("Fonts/NezDefaultBMFont"), Color.White));
            myDisplayedHair = new Image();
            myDisplayedBody = new Image();
            myImageStack.addElement(myDisplayedBody);
            myImageStack.addElement(myDisplayedHair);
            var ContainerStack = new Container(myImageStack);
            ContainerStack.setWidth(128);
            ContainerStack.setHeight(128);
            myCharacterViewerGroup.addElement(ContainerStack);

            CharacterCreatorSwitch = new Button(new ButtonStyle(new SubtextureDrawable(Core.content.Load<Texture2D>("GUI/Buttons/savebutton")), new SubtextureDrawable(Core.content.Load<Texture2D>("GUI/Buttons/savebutton")), new SubtextureDrawable(Core.content.Load<Texture2D>("GUI/Buttons/savebutton"))));
            CharacterCreatorSwitch.setTouchable(Touchable.Disabled);
            myCharacterViewerGroup.setAlignment(Align.bottom);
            myCharacterViewerGroup.addElement(Bodies);
            myCharacterViewerGroup.addElement(Hair);
            myCharacterViewerGroup.setPadTop(270);
            CheckBox Female = new CheckBox("Female", new CheckBoxStyle(new SubtextureDrawable(Nez.Core.content.Load<Texture2D>("GUI/Checkbox Elements/Checkbox_Unchecked")), new SubtextureDrawable(Nez.Core.content.Load<Texture2D>("GUI/Checkbox Elements/Checkbox_Checked")), Nez.Core.content.Load<BitmapFont>("Fonts/NezDefaultBMFont"), Color.White));
            Male.onChanged += delegate (bool myResult)
            {
                if (myResult)
                {
                    PlayerData.Sex = GameData.GameDataClasses.Entities.SEX.MALE;
                }
                else
                {
                    PlayerData.Sex = null;
                }
                Female.programmaticChangeEvents = false; //Prevents it from firing.
                Female.isChecked = false;
                Female.programmaticChangeEvents = true;
                UpdateCharacterWindow();
            };
            Female.onChanged += delegate (bool myResult)
            {
                if (myResult)
                {
                    PlayerData.Sex = GameData.GameDataClasses.Entities.SEX.FEMALE;
                }
                else
                {
                    PlayerData.Sex = null;
                }
                Male.programmaticChangeEvents = false; //Prevents it from firing.
                Male.isChecked = false;
                Male.programmaticChangeEvents = true;
                UpdateCharacterWindow();
            };

            mySexGroup.addElement(Male);
            mySexGroup.addElement(Female);
            myCharacterDataGroup.addElement(mySexGroup);
            HorizontalGroup myRaceGroupAndDescription = new HorizontalGroup();
            VerticalGroup myRaceGroup = new VerticalGroup();
            List<CheckBox> myRaceChecks = new List<CheckBox>();
            foreach (String A in Directory.EnumerateFiles("./Content/Races"))
            {
                String aRacename = Path.GetFileNameWithoutExtension(A);
                Race aLoadedRace = Nez.Core.content.Load<Race>("Races/" + aRacename);
                CheckBox aNewRace = new CheckBox(aRacename, new CheckBoxStyle(new SubtextureDrawable(Nez.Core.content.Load<Texture2D>("GUI/Checkbox Elements/Checkbox_Unchecked")), new SubtextureDrawable(Nez.Core.content.Load<Texture2D>("GUI/Checkbox Elements/Checkbox_Checked")), Nez.Core.content.Load<BitmapFont>("Fonts/NezDefaultBMFont"), Color.White));
                myRaceChecks.Add(aNewRace);
                aNewRace.onChanged += delegate (bool myResult)
                {
                    var Racebox = aNewRace;
                    if (myResult)
                    {
                        foreach (CheckBox a in myRaceChecks)
                        {
                            if (Racebox != a) a.isChecked = false;
                        }

                        Race myInternalRace = aLoadedRace;
                        SetRace(myInternalRace);
                        UpdateCharacterWindow();
                    }
                    else
                    {
                        SetRace(null);
                        UpdateCharacterWindow();
                    }
                };
                aNewRace.OnMouseOver += delegate (bool myResult)
                {
                    if (myResult)
                    {
                        Race myInternalRace = aLoadedRace;
                        myRaceDescription.setText(myInternalRace.Description);
                        myRaceDescription.setWrap(true);
                    }
                    else
                    {
                        myRaceDescription.setText("");

                    }
                };
                myRaceGroup.addElement(aNewRace);
            }
            myRaceDescription = new Label("Race Description HERE");
            myRaceDescription.setWidth(Screen.width * 0.25f);
            myRaceDescription.setHeight(30);
            myRaceDescription.setWrap(true);
            myRaceGroup.setAlignment(Align.bottomLeft);
            myRaceGroupAndDescription.setSpacing(10);
            myRaceGroupAndDescription.setWidth(100);
            myRaceGroupAndDescription.addElement(myRaceGroup);
            myRaceGroupAndDescription.setAlignment(Align.left);
            Container aRaceDescriptionHolder = new Container();

            aRaceDescriptionHolder.setSize(Screen.width * 0.25f, 30);
            aRaceDescriptionHolder.setY(0);
            aRaceDescriptionHolder.addElement(myRaceDescription);
            myRaceGroupAndDescription.addElement(aRaceDescriptionHolder);
            myCharacterDataGroup.addElement(myRaceGroupAndDescription);
            myHGroup.addElement(myCharacterDataGroup);
            myHGroup.addElement(myCharacterViewerGroup);
            //Professions GUI
            HorizontalGroup myHorizontalProfessions = new HorizontalGroup();
            VerticalGroup myProfessionsbox = new VerticalGroup();
            //Iterate professions however we're doing it
            myHorizontalProfessions.addElement(myProfessionsbox);
            VerticalGroup myProfessionsDesc = new VerticalGroup();
            Label myProfessionDescription = new Label("Profession Description HERE");
            myProfessionsDesc.addElement(myProfessionDescription);
            Label myProfessionAlert = new Label("Profession Alertbox HERE");
            myProfessionsDesc.addElement(myProfessionAlert);
            myHorizontalProfessions.addElement(myProfessionsDesc);
            myCharacterDataGroup.addElement(myHorizontalProfessions);
            canvas.stage.addElement(myHGroup);
            //myHGroup.add(myInternalHGroup);*/
            base.initialize();
        }

        private void SetRace(Race myInternalRace)
        {
            PlayerData.SetRace(myInternalRace);
            if (myInternalRace == null)
            {
                SetHairStyle(-1, null);
                SetBodyStyle(-1, null);
                myDisplayedHair.setIsVisible(false);
                myDisplayedBody.setIsVisible(false);
            }
            else
            {
                myDisplayedHair.setIsVisible(true);
                myDisplayedBody.setIsVisible(true);
            }
        }
        Stack myImageStack = new Stack();
        HorizontalGroup myBodyGroup = new HorizontalGroup();
        HorizontalGroup aHairGroup = new HorizontalGroup();
        private Image myDisplayedHair;
        private Image myDisplayedBody;

        public void canMoveOn()
        {
            if(PlayerData.GetRace() != null && PlayerData.GetHairstyle() != -1 && PlayerData.GetBodyStyle() != -1)
            {
                CharacterCreatorSwitch.setTouchable(Touchable.Enabled);
            }
            else
            {
                CharacterCreatorSwitch.setTouchable(Touchable.Disabled);
            }
        }

        private void UpdateCharacterWindow()
        {
            Race aRace = PlayerData.GetRace();
            if (myBodyGroup != null)
            {
                SetBodyStyle(-1, null);
                myBodyGroup.clearChildren();
            }
            if(aHairGroup != null)
            {
                SetHairStyle(-1, null);
                aHairGroup.clearChildren();
            }
            if (aRace == null) return;
            if (PlayerData?.Sex == GameData.GameDataClasses.Entities.SEX.MALE )
            {
                List<CheckBox> Bodyboxes = new List<CheckBox>();
                List<CheckBox> Hairboxes = new List<CheckBox>();
                for(int i = 0; i < aRace.MaleBodies.Length; i++)
                {
                    var ChekboxOnTexture = new SubtextureDrawable(aRace.MaleBodies[i]);
                    var CheckboxOffTexture = new SubtextureDrawable(aRace.MaleBodies[i]);
                    CheckboxOffTexture.tintColor = new Color(0, 0, 0, 100);
                    CheckBox myNewButton = new CheckBox("", new CheckBoxStyle(ChekboxOnTexture, CheckboxOffTexture, Nez.Core.content.Load<BitmapFont>("Fonts/NezDefaultBMFont"), Color.White));

                    int g = i;
                    myNewButton.onChanged += delegate (bool value)
                    {
                        int x = g;
                        CheckBox aNewButton = myNewButton;
                        Texture2D Aval = aRace.MaleBodies[x];
                        List<CheckBox> internalHairboxes = Bodyboxes;
                        if (value)
                        {
                            foreach (CheckBox B in internalHairboxes)
                            {
                                if (B != aNewButton)
                                {
                                    B.isChecked = false;
                                }
                            }
                            SetBodyStyle(x, Aval);
                        }
                        else
                        {
                            SetBodyStyle(-1, null);
                        }
                        canMoveOn();
                    };
                    myBodyGroup.addElement(myNewButton);
                }
                for (int i = 0; i < aRace.Hairstyles.Length; i++)
                {
                    var ChekboxOnTexture = new SubtextureDrawable(aRace.Hairstyles[i]);
                    var CheckboxOffTexture = new SubtextureDrawable(aRace.Hairstyles[i]);
                    CheckboxOffTexture.tintColor = new Color(0, 0, 0, 100);
                    CheckBox myNewButton = new CheckBox("", new CheckBoxStyle(ChekboxOnTexture, CheckboxOffTexture, Nez.Core.content.Load<BitmapFont>("Fonts/NezDefaultBMFont"), Color.White));
                    aHairGroup.addElement(myNewButton);
                    Hairboxes.Add(myNewButton);
                    int g = i;
                    myNewButton.onChanged += delegate (bool value)
                    {
                        int x = g;
                        CheckBox aNewButton = myNewButton;
                        Texture2D Aval = aRace.Hairstyles[x];
                        List<CheckBox> internalHairboxes = Hairboxes;
                        if (value)
                        {
                            foreach (CheckBox B in internalHairboxes)
                            {
                                if (B != aNewButton)
                                {
                                    B.isChecked = false;
                                }
                            }
                            SetHairStyle(x, Aval);
                        }
                        else
                        {
                            SetHairStyle(-1, null);
                        }
                        canMoveOn();
                    };
                }
            }
            else if(PlayerData?.Sex == GameData.GameDataClasses.Entities.SEX.FEMALE)
            {
                List<CheckBox> Bodyboxes = new List<CheckBox>();
                for (int i = 0; i < aRace.FemaleBodies.Length; i++)
                {
                    var ChekboxOnTexture = new SubtextureDrawable(aRace.FemaleBodies[i]);
                    var CheckboxOffTexture = new SubtextureDrawable(aRace.FemaleBodies[i]);
                    CheckboxOffTexture.tintColor = new Color(0, 0, 0, 100);
                    CheckBox myNewButton = new CheckBox("", new CheckBoxStyle(ChekboxOnTexture, CheckboxOffTexture, Nez.Core.content.Load<BitmapFont>("Fonts/NezDefaultBMFont"), Color.White));
                    Bodyboxes.Add(myNewButton);
                    int g = i;
                    myNewButton.onChanged += delegate (bool value)
                    {
                        int x = g;
                        CheckBox aNewButton = myNewButton;
                        Texture2D Aval = aRace.FemaleBodies[x];
                        List<CheckBox> internalHairboxes = Bodyboxes;
                        if (value)
                        {
                            foreach (CheckBox B in internalHairboxes)
                            {
                                if (B != aNewButton)
                                {
                                    B.isChecked = false;
                                }
                            }
                            SetBodyStyle(x, aRace.FemaleBodies[x]);
                        }
                        else
                        {
                            SetBodyStyle(-1,null);
                        }
                        canMoveOn();
                    };

                myBodyGroup.addElement(myNewButton);                    
                }
                List<CheckBox> Hairboxes = new List<CheckBox>();
                for (int i = 0; i < aRace.Hairstyles.Length; i++)
                {
                    var ChekboxOnTexture = new SubtextureDrawable(aRace.Hairstyles[i]);
                    var CheckboxOffTexture = new SubtextureDrawable(aRace.Hairstyles[i]);
                    CheckboxOffTexture.tintColor = new Color(0, 0, 0, 100);
                    CheckBox myNewButton = new CheckBox("", new CheckBoxStyle(ChekboxOnTexture, CheckboxOffTexture, Nez.Core.content.Load<BitmapFont>("Fonts/NezDefaultBMFont"), Color.White));
                    Hairboxes.Add(myNewButton);
                    int g = i;
                    myNewButton.onChanged += delegate (bool value)
                    {
                        int x = g;
                        CheckBox aNewButton = myNewButton;
                        Texture2D Aval = aRace.Hairstyles[x];
                        List<CheckBox> internalHairboxes = Hairboxes;
                        if (value)
                        {
                            foreach (CheckBox B in internalHairboxes)
                            {
                                if (B != aNewButton)
                                {
                                    B.isChecked = false;
                                }
                            }
                            SetHairStyle(x, Aval);
                        }
                        else
                        {
                            SetHairStyle(-1,null);
                        }
                        canMoveOn();
                    };
                    aHairGroup.addElement(myNewButton);
                }
            }


        }

        private void SetBodyStyle(int a, Texture2D aval)
        {
            PlayerData.SetBodySelected(a);
            if (aval != null)
            {
                myDisplayedBody.setIsVisible(true);
                myDisplayedBody.setDrawable(new SubtextureDrawable(aval));
            }
            else
            {
                myDisplayedBody.setDrawable(null);
                myDisplayedBody.setIsVisible(false);
            }
        }

        private void SetHairStyle(int a, Texture2D aval)
        {
            PlayerData.SetHairstyleSelected(a);
            if (aval != null)
            {
                myDisplayedHair.setIsVisible(true);
                myDisplayedHair.setDrawable(new SubtextureDrawable(aval));
            }
            else
            {
                myDisplayedHair.setDrawable(null);
                myDisplayedHair.setIsVisible(false);
            }
        }

        public void SetPlayerData(PlayerData playerData)
        {
            this.PlayerData = playerData;
        }



    }
}