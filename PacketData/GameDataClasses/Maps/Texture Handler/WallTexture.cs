using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameData.GameDataClasses.Maps.Texture_Handler
{
    public static class WallTexture
    {
        public static short[] circleWallCenterPillar { get; } = { 0, 0 };
        public static short[] southsidewall { get; } = { 1, 0 };
        public static short[] NorthSouthwall { get; } = { 2, 0 };
        public static short[] NorthSouthEastwall { get; } = { 3, 0 };
        public static short[] nowall { get; } = { 0, 1 };
        public static short[] SouthwallNortheastNorthwestCorner { get; } = { 1, 1 };
        public static short[] SouthEastwall { get; } = { 2, 1 };
        public static short[] SouthEastwallNorthwestCorner { get; } = { 3, 1 };
        public static short[] NortheastNorthwestSoutheastSouthwestCorner { get; } = { 0, 2 };
        public static short[] SouthwallNorthwestCorner { get; } = { 1, 2 };
        public static short[] SouthwallNortheastCorner { get; } = { 2, 2 };
        public static short[] SouthWestwallNortheastCorner { get; } = { 3, 2};
        public static short[] Northeastcorner { get; } = { 0, 3};
        public static short[] NortheastNorthwestcorner { get; } = { 1, 3 };
        public static short[] NortheastNorthwestSoutheastcorner { get; } = { 2, 3 };
        public static short[] NortheastNorthwestSoutheastSouthwestcorner { get; } = { 3, 3 };
    }
}
