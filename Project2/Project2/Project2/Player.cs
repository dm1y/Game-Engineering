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

        public Boolean isFalling;
        private Boolean isCollidingLeft;
        private Boolean isCollidingRight;
        private Boolean isCollidingTop;
        private Boolean isCollidingBottom;

        public Boolean isOnPlatform;
        Vector2 spawnPosition;
        public Vector2 position;
        Texture2D playerTexture;

        float max_x_velocity = 300;
        float max_y_velocity = 400;
        int max_jump_height = 600;

        Vector2 velocity;
        Vector2 slowdown = new Vector2(15, 0);
        Vector2 gravity = new Vector2(0, 25);

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
            isFalling = true;
            isOnPlatform = false;
            isColliding = false;

        }

        public void setXVelocity(float velocity)
        {

        }
        public void setYVelocity(float velocity)
        {
            this.velocity.Y = velocity;
        }

        //private void ArchingFlight(GameTime timePassed)
        //{
        //    prevPos = pos;
        //    // accumulate overall time
        //    totalTimePassed += (float)timePassed.ElapsedGameTime.Milliseconds / 4096.0f;

        //    // flight path where y-coordinate is additionally effected by gravity
        //    pos = pos + velocity * ((float)timePassed.ElapsedGameTime.Milliseconds / 90.0f);
        //    pos.Y = pos.Y - 0.5f * GRAVITY * totalTimePassed * totalTimePassed;
        //}

        public void Update(GameTime gameTime, KeyboardState keyboard)
        {
            if (!isOnPlatform)
            {
                if (!isColliding)
                {
                    isFalling = true;
                }
                //isFalling = true;
            }
            // Update:
            float time = (float)gameTime.ElapsedGameTime.TotalSeconds;


            if (velocity.Y < max_y_velocity)
            {
                velocity.Y += gravity.Y;
            }
            if (velocity.Y > max_y_velocity)
            {
                velocity.Y = max_y_velocity;
            }


            //if velocity < 0, add to velocity until it reaches 0
            // if velocity > 0, subtract until it reaches 0
            if (velocity.X < 25 && velocity.X >= 0)
            {
                velocity.X = 0;
            }
            if (velocity.X > 0 )
            {
                
                velocity.X -= slowdown.X;
            }
            else
            {
                velocity.X += slowdown.X;
            }

            if (velocity.X < 25 && velocity.X >= 0)
            {
                velocity.X = 0;
            }


            if (keyboard.IsKeyDown(Keys.Right))
            {
                if (Math.Abs(velocity.X) < max_x_velocity)
                {
                    velocity.X += 50;
                }
            }
            if (keyboard.IsKeyDown(Keys.Left))
            {

                if (Math.Abs(velocity.X) < max_x_velocity)
                {
                    velocity.X -= 50;
                }
            }
            if (keyboard.IsKeyDown(Keys.Down))
            {
            }

            if (keyboard.IsKeyDown(Keys.Up))
            {
                if (!isFalling)
                {
                    if (Math.Abs(velocity.Y) <= max_y_velocity)
                    {
                        isFalling = true;
                        velocity.Y -= 600;
                    }
                }

            }
            UpdatePosition(time);
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

        public void UpdatePosition(float time)
        {
            if (!isFalling)
            {
               //if it is not falling, it must be colliding with something
                if (isCollidingRight)
                {
                    //do not let position increase
                }
                if (isCollidingLeft)
                {
                    //do not let position decrease
                }
               position.X += velocity.X * time;
            }
            else
            {
                position += velocity * time;
            }

        }

        public void CheckCollisionSide(Rectangle player, Rectangle tile)
        {

            if (player.Intersects(tile))
            {
            }
            //{

            //    if (collidingBottom(player, tile)) {
            //        //player is simply landing on platform
            //        isFalling = false;
            //        player.Y -= 5;
            //        setYVelocity(0);
            //        isOnPlatform = true;
            //    }
            //    //if player's top Y coordinate is above tile bottom's y and player's bottom Y is lower than tile's bottom
            //    else if (collidingTop(player, tile))
            //    {
            //        Console.Write("\n Collide Top ");
            //        //hitting head

            //        isFalling = true;
            //        setYVelocity(-10);
            //        isOnPlatform = false;
            //    }
            //    //coming in from the left
            //    // if player's left's X is smaller than tile's X and player's right is bigger than tile's right
            //    else if (collidingLeft(player, tile))
            //    {
            //        //if player is touching by bottom, it will not fall
            //        Console.Write("\n Collide Left ");
            //        //player collided on left-hand side
            //        isColliding = true;
            //        setXVelocity(0);
            //        isOnPlatform = false;
            //    }

            //    else if (collidingRight(player, tile))
            //    {
            //        //if player is touching by bottom, it will not fall
            //        Console.Write("\n Collide Right ");
            //        //player collided on left-hand side
            //        isColliding = true;
            //        setXVelocity(0);
            //        isOnPlatform = false;
            //    }
            //}
        }

        private Boolean collidingLeft(Rectangle A, Rectangle B)
        {
            if (A.Left <= B.Right && A.Right > B.Right)
            {
                return true;
            }
            return false;
        }

        private Boolean collidingRight(Rectangle A, Rectangle B)
        {
            if (A.Right >= B.Left && A.Left < B.Left)
            {
                return true;
            }
            return false;
        }

        private Boolean collidingTop(Rectangle A, Rectangle B)
        {
            if (A.Top <= B.Bottom && A.Bottom > B.Bottom)
            {
                return true;
            }
            return false;
        }

        private Boolean collidingBottom(Rectangle A, Rectangle B)
        {
            if (A.Bottom >= B.Top && A.Top < B.Top) 
            {
                return true;
            }
            return false;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(playerTexture, new Rectangle((int)position.X,
                (int)position.Y,
                playerTexture.Width, playerTexture.Height), Color.White);
        }

    }
}
