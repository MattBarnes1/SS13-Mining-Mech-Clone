using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nez.Systems;
using Nez.Tiled;

namespace GameData.GameDataClasses.Maps
{
    public static class TileSetResourceHandler
    {
        public static NezContentManager Content;
        public static void Create(NezContentManager myManager)
        {
            Content = myManager;
        }

        
        internal static TiledTileset TryGet(string tilesetName)
        {
            throw new NotImplementedException();
        }
    }
}
