using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Project2
{
    class Player
    {
        Physics playerPhysics;
        Game game;
        
        Vector2 spawnPosition;
        public Vector2 position;
        Vector2 direction;
        Texture2D playerTexture;
        //GameTime gameTime;
        float maxVelocity;
        float xvelocity;
        float yvelocity;
        int deltax;
        int deltay;

        public int Width
        {
            get { return playerTexture.Width; }
        }

        public int Height
        {
            get { return playerTexture.Height; }
        }

        public Player(int X, int Y, Texture2D playerTexture, Game1 g)
        {
            game = g;
            spawnPosition = new Vector2(X, Y);
            position = new Vector2(X, Y);
            direction = new Vector2(0, 0);
            this.playerTexture = playerTexture;
            maxVelocity = 5;
            //this.gameTime = gameTime;

        }

        public void setXVelocity(float velocity)
        {

        }
        public void setYVelocity(float velocity)
        {

        }

        //public void UpdateVelocity()
        //{
        //    //if idle and decelerating

        //    //if moving and not decelerating
        //    if (deltax > 0)
        //    {
        //        if (xvelocity < maxVelocity)
        //        {
        //        }
        //    }

        //    if (deltay > 0)
        //    {
        //        if (yvelocity < maxVelocity)
        //        {
        //        }
        //    }
            
        //}

            //        /* Starts the game */
            //if (keyboardState.IsKeyDown(Keys.Enter) && lastState.IsKeyUp(Keys.Enter))

        public void Update(GameTime gameTime, KeyboardState keyboard)
        {
            if (deltax > 0)
            {
                deltax -= 1;
            }
            if (deltay > 0)
            {
                deltay -= 1;
            }

            if (keyboard.IsKeyDown(Keys.Right))
            {
                deltax += 3;
                direction.X = 1;
                //accelerate left
            }
            if (keyboard.IsKeyDown(Keys.Left))
            {
                deltax += 3;
                direction.X = -1;
                //accelerate right
            }
            if (keyboard.IsKeyDown(Keys.Down))
            {
                //do nothing?
                direction.Y = 1;
                //accelerate up
            }

            if (keyboard.IsKeyDown(Keys.Up))
            {
                //jump!
                if (deltay == 0)
                {
                    deltay += 10;
                    direction.Y = -1;

                }
                //accelerate up
            }

            //UpdateVelocity();
            UpdatePosition();

            StayWithinBounds();
        }

        /* Player stays within the bounds of the game screen */
        public void StayWithinBounds()
        {
            if (position.X <= 0)
                position.X = 0;

            if (position.X >= game.GraphicsDevice.Viewport.Width - playerTexture.Width)
                position.X = game.GraphicsDevice.Viewport.Width - playerTexture.Width;

            if (position.Y <= 0)
                position.Y = 0;

            if (position.Y >= game.GraphicsDevice.Viewport.Height - playerTexture.Height)
                position.Y = game.GraphicsDevice.Viewport.Height - playerTexture.Height;
        }

        public void UpdatePosition()
        {
            position.X += deltax * direction.X;
            position.Y += deltay * direction.Y;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(playerTexture, new Rectangle((int)position.X,
                (int)position.Y,
                playerTexture.Width, playerTexture.Height), Color.White);
        }

    }
}
