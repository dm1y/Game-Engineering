using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace Project2
{
    public class Level
    {
        List<MapTile> mapTiles;
        MapTileData[] data;
        Game game; 

        public Level(Game g, MapTileData[] data)
        {
            this.data = data; 
            mapTiles = new List<MapTile>();
            game = g; 
        }

        public void LoadContent(ContentManager Content)
        {
            foreach (MapTileData d in data)
            {
                mapTiles.Add(new MapTile((int)d.mapPosition.X, (int)d.mapPosition.Y,
                    Content.Load<Texture2D>(d.tileTexture), game, d.isBouncy, d.isBreakable, d.isTrap, d.isCake));
            }
        }

        public void Draw(SpriteBatch sb)
        {
            foreach (MapTile tile in mapTiles)
            {
                //Console.Write("drawing");
                tile.Draw(sb);
            }
        }

    }
}
