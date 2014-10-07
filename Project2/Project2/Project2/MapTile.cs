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
        public Texture2D tileTexture;
        //public Vector2 arrayPosition;
        public Boolean isSpecial;
        public Vector2 mapCoordinates;

        Game game;

        public int Width
        {
            get { return tileTexture.Width; }
        }

        public int Height
        {
            get { return tileTexture.Height; }
        }

        public MapTile(int X, int Y, Texture2D tileTexture, Game game)
        {
            isSpecial = false;
            this.tileTexture = tileTexture;
            //arrayPosition = new Vector2(X, Y);
            this.game = game;
            mapCoordinates = new Vector2((int)(X * Width),
            (int)(game.GraphicsDevice.Viewport.Height - (Y * Height) - Height));

//            mapPosition = new Vector2((int)(arrayPosition.X * Width),
//                (int)(game.GraphicsDevice.Viewport.Height - (arrayPosition.Y * Height) - Height));
            //Console.Write("Game Window Width:" + game.GraphicsDevice.Viewport.Width + "\nGame Window Height:" + game.GraphicsDevice.Viewport.Height);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            //Console.Write("draw");
            //Console.Write("\n CurrentX: " + position.X + " X coordinate:" + position.X * Width);
           
            spriteBatch.Draw(tileTexture, new Rectangle((int)mapCoordinates.X, (int)mapCoordinates.Y,
                tileTexture.Width, tileTexture.Height), Color.White);
        }
    }
}
