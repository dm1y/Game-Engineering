using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace Project2
{
    public class Boundaries
    {
        public int x;
        public int y;
    }

    public class MapTileData
    {
        public String tileTexture;
        public Boolean isTrap;
        public Boolean isBouncy;
        public Boolean isBreakable;
        public Boolean isCake;
        public Vector2 mapPosition;
    }
}
