using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Project2
{

    public class MapTile
    {
        Game game;
        
        public Texture2D tileTexture;
        public Vector2 mapPositions;
        
        /* The different tile attributes */
        public Boolean isTrap;
        public Boolean isBouncy;
        public Boolean isBreakable;
        public Boolean isCake;

        public int Width
        {
            get { return tileTexture.Width; }
        }

        public int Height
        {
            get { return tileTexture.Height; }
        }

        public MapTile(int X, int Y, Texture2D tileTexture, Game game, Boolean bounce, 
            Boolean br, Boolean trap, Boolean cake)
        {
            this.game = game;
            this.tileTexture = tileTexture;
            mapPositions = new Vector2((X * Width), (game.GraphicsDevice.Viewport.Height 
                - (Y * Height) - Height));

            isBouncy = bounce;
            isBreakable = br;
            isTrap = trap;
            isCake = cake;
            //Console.Write("Game Window Width:" + game.GraphicsDevice.Viewport.Width + "\nGame Window Height:" + game.GraphicsDevice.Viewport.Height);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            //Console.Write("draw");
            //Console.Write("\n CurrentX: " + position.X + " X coordinate:" + position.X * Width);
           
            spriteBatch.Draw(tileTexture, new Rectangle((int)mapPositions.X, (int)mapPositions.Y,
                tileTexture.Width, tileTexture.Height), Color.White);
        }
    }
}
