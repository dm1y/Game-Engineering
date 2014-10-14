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
        
        public Animation tileAnimation;
        public Vector2 mapPositions;
        
        /* The different tile attributes */
        public Boolean isTrap;
        public Boolean isBouncy;
        public Boolean isBreakable;
        public Boolean isUnstable;
        public Boolean isCake;
        public Boolean isActive;

        public int Width
        {
            get { return tileAnimation.FrameWidth; }
        }

        public int Height
        {
            get { return tileAnimation.FrameHeight; }
        }

        public MapTile(int X, int Y, Texture2D tileTexture, Game game, Boolean bounce, 
            Boolean breakable, Boolean trap, Boolean unstable, Boolean cake)
        {
            this.game = game;
            mapPositions = new Vector2((X * Width), (game.GraphicsDevice.Viewport.Height 
                - (Y * Height) - Height));
            tileAnimation = new Animation();
            isBouncy = bounce;
            isBreakable = breakable;
            isTrap = trap;
            isCake = cake;
            isActive = true;

            //Unstable tiles
            if (unstable)
            {
                tileAnimation.Initialize(tileTexture, mapPositions, 64, 64, 1, 100, Color.White, 1f, false, true);
            }

            //Breakable tiles
            else if (breakable)
            {
                tileAnimation.Initialize(tileTexture, mapPositions, 64, 64, 1, 100, Color.White, 1f, false, true);

            }
            //Bouncing tiles
            else if (isBouncy)
            {
                tileAnimation.Initialize(tileTexture, mapPositions, 64, 64, 1, 100, Color.White, 1f, true, false);

            }
            //Normal tiles, trap tiles, and cake tiles only have one frame
            else
            {
                tileAnimation.Initialize(tileTexture, mapPositions, 64, 64, 1, 100, Color.White, 1f, true, false);

            }
            
        }

        public void PlayAnimationOnce()
        {
            tileAnimation.PlayFirstFrame = false;
        }

        public void ResetAnimation()
        {
            tileAnimation.PlayFirstFrame = true;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            //Console.Write("draw");
            //Console.Write("\n CurrentX: " + position.X + " X coordinate:" + position.X * Width);
           
            //spriteBatch.Draw(tileTexture, new Rectangle((int)mapPositions.X, (int)mapPositions.Y,
            //    tileTexture.Width, tileTexture.Height), Color.White);
            tileAnimation.Draw(spriteBatch);
        }
    }
}
