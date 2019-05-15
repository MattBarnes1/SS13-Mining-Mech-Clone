using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using DedicatedServerFramework.MapGeneration.BuildingRule;
using GameData.GameDataClasses.Maps;
using GameData.GameDataClasses.RuleEngine;
using Microsoft.Xna.Framework;
using Noise;

namespace DedicatedServerFramework.MapGeneration
{
    public class BuildingsRuleEngine : RuleEngine<RoomData>
    {
        FastNoise myNoise = new FastNoise();
        object Locker = new object();
        Vector2? FInalValue;
        Vector2? BaseLocation {
            get
            {
                lock (Locker)
                {
                    return FInalValue;
                }
            }
            set
            {
                lock (Locker)
                {
                    if (!FInalValue.HasValue)
                    {
                        FInalValue = value;
                    }
                }
            }
        }

        public int Seed { get; }

        Vector2 BaseStart;
        public BuildingsRuleEngine(Map myMap, List<Rule> RuleSet, List<Conclusion> myConclusions) : base(RuleSet, myConclusions)
        {
            this.Seed = myMap.MapSeed;
            Random myRand = new Random();
            myNoise.SetSeed(myMap.MapSeed);
            myNoise.SetNoiseType(FastNoise.NoiseType.SimplexFractal);
            Vector2 BaseStart = new Vector2(myRand.Next(0, 99999), myRand.Next(0, 99999));
            var myResult = Parallel.For(0, 5, delegate (int StartingYOffset)
            {
                Process(StartingYOffset, myMap.MapSeed, BaseStart.X, BaseStart.Y);
            });         
            CenterOfBaseRoomObject myCenterBase = new CenterOfBaseRoomObject();
            myMap.PlaceRoomObjectAtPosition(new Vector3(BaseStart, 0), myCenterBase);
            BaseStartConclusion myStart = new BaseStartConclusion(BaseStart, 100, myCenterBase);
            myMap.NewPlayerLoadPosition = new float[] { BaseStart.X + 1, BaseStart.Y + 1, 0 };
        }
       

        private void Process(int StartingYOffset, int Seed, float X, float Y)
        {
            FastNoise myNoise = new FastNoise();
            myNoise.SetSeed(Seed);
            myNoise.SetNoiseType(FastNoise.NoiseType.SimplexFractal);
            Y = Y + StartingYOffset * 10;
            while (BaseLocation == null)
            {
                float xCheck;
                float yCheck;
                int TileHeight = (int)Math.Round(TileData.MaxHeight * (myNoise.GetNoise(X, Y)));
                if (TileHeight > 1)
                {
                    bool CheckPassed = true;
                    for (xCheck = X; xCheck < X + 4; xCheck++)
                    {
                        for (yCheck = Y; yCheck < Y + 4; yCheck++)
                        {
                            TileHeight = (int)Math.Round(TileData.MaxHeight * (myNoise.GetNoise(xCheck, yCheck)));
                            if(TileHeight <= 1)
                            {
                                CheckPassed = false;
                                break;
                            }
                        }
                    }
                    X = xCheck;
                    if(CheckPassed)
                    {
                        BaseLocation = new Vector2(X, Y);
                        Console.WriteLine(String.Format("Found base vector at {0}, {1}!",BaseLocation.Value.X, BaseLocation.Value.Y));
                        return;
                    }
                }
                else
                {
                    X += 1;
                }
            }
        }        
    }
}
