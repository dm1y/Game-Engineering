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
    public class LevelManager
    {
        Game game;

        public LevelManager(Game g)
        {
            game = g;
        }

        public void LoadContent(ContentManager Content)
        {
            MapTileData[] data = Content.Load<MapTileData[]>("LevelTester1");
            MapTileData[] data2 = Content.Load<MapTileData[]>("LevelTester2");
        }

        public void Update() 
        {

        }

        public void MoveToNextLevel()
        { 
        }

        public void RestartLevel()
        { 
        }

    }
}
