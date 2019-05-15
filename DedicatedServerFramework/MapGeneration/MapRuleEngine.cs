using DedicatedServerFramework.MapGeneration;
using DedicatedServerFramework.MapGeneration.Materials;
using GameData.GameDataClasses.RuleEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameData.GameDataClasses.Maps.MapGeneration
{
    public class MapRuleEngine : RuleEngine<TileData>
    {
        public MapRuleEngine(List<Rule> RuleSet, List<Conclusion> myConclusions, TileData[,] myTiles, Material myMaterials) : base(RuleSet, myConclusions)
        {
            base.AddDictionaryItem("mapData", myTiles);

        }
    }

}
