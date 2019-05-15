using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Nez;

namespace SS13Clone.Managers.SceneManager
{
    public class SceneManager
    {
        static Scene ActiveScene;

        public SceneManager(Nez.Core myCore)
        {
            //Assert.isFalse(myManager == null, "Two scene managers were created!");
            myManager = this;
            this.Core = myCore;
        }

        static Dictionary<String, Scene> mySceneHandler = new Dictionary<string, Scene>();

        public void AddSceneToList(Scene myScene, String mySceneID)
        {
            mySceneHandler.Add(mySceneID, myScene);
        }

        static SceneManager myManager;
        public Core Core { get; private set; }

        public static T GetScene<T>(String SceneName) where T : Scene
        {
            return (T)mySceneHandler[SceneName];
        }


        public void Update()
        {
            if(ActiveScene != null)
            {
                Core.scene = ActiveScene;
                ActiveScene = null;
            }
        }

        public static void ChangeScenes(String SceneID)
        {
           ActiveScene = mySceneHandler[SceneID];
        }
    }
}
