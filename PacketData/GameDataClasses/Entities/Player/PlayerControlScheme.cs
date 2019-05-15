using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameData.GameDataClasses.Entities
{
    public class PlayerControlScheme
    {

        public PlayerControlScheme()
        {
            foreach (Keys A in Enum.GetValues(typeof(Keys)))
            {
                KeyToFunctionDictionary.Add(A, null);
            }
        }

        public delegate void KeyPressed();
        const String ControlsInitializationFile = "./Controls.ini";
        public void AddKeyFunction(KeyPressed myFunction)
        {
            AssignableFunctions.Add(myFunction.Method.Name, myFunction);
        }
        Dictionary<Keys, KeyPressed> KeyToFunctionDictionary = new Dictionary<Keys, KeyPressed>();
        Dictionary<String, KeyPressed> AssignableFunctions = new Dictionary<string, KeyPressed>();
        public void SetFunction(Keys myKey, String myKeyFunction)
        {
            KeyToFunctionDictionary.Remove(myKey);
            if (AssignableFunctions.ContainsKey(myKeyFunction))
            {
                KeyToFunctionDictionary.Add(myKey, AssignableFunctions[myKeyFunction]);
            }
            else
            {
                KeyToFunctionDictionary.Add(myKey, null);
            }
        }

        public void SaveToJSONFile()
        {
            String ToWrite = "";
            foreach (KeyValuePair<Keys, KeyPressed> A in KeyToFunctionDictionary)
            {
                ToWrite += A.Key.ToString() + " " + A.Value.Method.Name + ",\n";
            }
            File.WriteAllText(ControlsInitializationFile, ToWrite);
        }
        public void LoadFromJSONFile()
        {
            if (File.Exists(ControlsInitializationFile))
            {
                string Result = File.ReadAllText(ControlsInitializationFile);
                ParseControls(Result);
            }
            else
            {
                BuildDefaultControls();
                SaveToJSONFile();
            }
        }

        private void ParseControls(string result)
        {
            var Value = result.Split(',');
            foreach (String A in Value)
            {
                if (!String.IsNullOrEmpty(A) && !String.IsNullOrWhiteSpace(A))
                {
                    var ControlStructure = A.Split(' ');
                    Keys myKeys = (Keys)Enum.Parse(typeof(Keys), ControlStructure[0]);
                    KeyPressed myKeysFunction = AssignableFunctions[ControlStructure[1]];
                    KeyToFunctionDictionary.Remove(myKeys);
                    KeyToFunctionDictionary.Add(myKeys, myKeysFunction);
                }
            }
        }

        private void BuildDefaultControls()
        {
            SetFunction(Keys.W, "GoForward");
            SetFunction(Keys.A, "GoLeft");
            SetFunction(Keys.D, "GoRight");
            SetFunction(Keys.S, "GoBack");
        }
    }
}
