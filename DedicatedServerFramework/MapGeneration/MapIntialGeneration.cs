using DedicatedServerFramework.MapGeneration;
using DedicatedServerFramework.MapGeneration.Materials;
using GameData.GameDataClasses.RuleEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using System.Diagnostics;
using Noise;
using GameData.GameDataClasses.ProbabilityTable;
using DedicatedServerFramework.MapGeneration.BuildingRule;

namespace GameData.GameDataClasses.Maps
{
    public class MapIntialGeneration
    {
        public MapIntialGeneration()
        {
           
        }

        public Map BuildMap(String MapID)
        {
            Map myMapInfo = new Map(MapID);
            List<Rule> myRules = new List<Rule>(){
                new MaterialRuleStart(),
                new RuleCreateSecondMaterialLayer(),
                new RuleCreateThirdMaterialLayer(),
                new RuleFourthLayer()
            };
            MaterialsRuleEngine myMaterialsEngine = new MaterialsRuleEngine(myRules, new List<Conclusion>());
            List<Rule> myBuildingRules = new List<Rule>();
            List<Material> myMaterials = myMaterialsEngine.Decide();
            SimpleProbabilityTable myTable = new SimpleProbabilityTable();
            myTable.AddProbabilityItem(myMaterials.ConvertAll<IProbabilityTableItem>(delegate(Material A)
            {
                return A;
            }));
            myMapInfo.SetMaterialChanceTable(myTable);
            BuildingsRuleEngine myBuildingsEngine = new BuildingsRuleEngine(myMapInfo, myBuildingRules, new List<Conclusion>());
            List<RoomData> myModifiedTiles = myBuildingsEngine.Decide();
            foreach(RoomData A in myModifiedTiles)
            {
                myMapInfo.PlaceRoomObjectAtPosition(Vector3.Zero, A); //Vector3 WorldPosition should be done in the building engine
            }
            SaveMap(myMapInfo);
            return myMapInfo;
        }

        private void SaveMap(Map myMapInfo)
        {
        }
    }
}
