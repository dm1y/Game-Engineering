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
        Texture2D playerTexture;
        
        float maxVelocity;

        Vector2 velocity;
        Vector2 slowdown = new Vector2(20f, 0);
        Vector2 gravity = new Vector2(0, -9.8f);

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
            this.playerTexture = playerTexture;
            maxVelocity = 10;
            //this.gameTime = gameTime;

        }

        public void setXVelocity(float velocity)
        {

        }
        public void setYVelocity(float velocity)
        {

        }

        public void Update(GameTime gameTime, KeyboardState keyboard)
        {
            //if moving positively in X direction, change slowdown acceleration to negative


            
            // Update:
            float time = (float)gameTime.ElapsedGameTime.TotalSeconds;
            velocity.Y += gravity.Y * time;

            //if velocity < 0, add to velocity until it reaches 0
            // if velocity > 0, subtract until it reaches 0
            if (velocity.X > 0 )
            {
                velocity.X -= slowdown.X * time;
            }
            else
            {
                velocity.X += slowdown.X * time;
            }

            position += velocity * time;

            if (keyboard.IsKeyDown(Keys.Right))
            {
                velocity.X += 10;
            }
            if (keyboard.IsKeyDown(Keys.Left))
            {
                velocity.X -= 10;
                //accelerate right
            }
            if (keyboard.IsKeyDown(Keys.Down))
            {
                //velocity.Y -= 20;
            }

            if (keyboard.IsKeyDown(Keys.Up))
            {
                //jump!
                velocity.Y -= 20;
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
            //position.X += deltax * direction.X;
            //position.Y += deltay * direction.Y;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(playerTexture, new Rectangle((int)position.X,
                (int)position.Y,
                playerTexture.Width, playerTexture.Height), Color.White);
        }

    }
}
