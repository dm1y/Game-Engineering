using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Project2
{

   public class Player
    {
       public Game game;

       //Gameplay Mechanics//

        int mapWidth;
        int mapHeight;

       /* Player states and attributes */
        Texture2D playerTexture;

        public Boolean isFalling;
        public Boolean isOnPlatform;
        public Boolean isDead;


       /* Player lives. Can be moved to HUD class if we create one. */
        public int lives;

       /* Player positions */
        public Vector2 spawnPosition;  //changed to public..might change back
        public Vector2 position;

        float max_x_velocity = 300;
        float max_y_velocity = 300;

        public Vector2 velocity;
        Vector2 slowdown = new Vector2(15, 0);
        Vector2 gravity = new Vector2(0, 20);

        public int Width
        {
            get { return playerTexture.Width; }
        }

        public int Height
        {
            get { return playerTexture.Height; }
        }

        public Boolean end = false; 


        public void setBoundaries(int mapWidth, int mapHeight)
        {
            this.mapHeight = mapHeight;
            this.mapWidth = mapWidth;
        }

        public Vector2 getBoundaries()
        {
            return new Vector2(mapWidth, mapHeight);
        }

        public Player(int X, int Y, Texture2D playerTexture, Game1 g)
        {
            lives = 3;
            isDead = false;
            game = g;
            spawnPosition = new Vector2(X, Y);
            position = new Vector2(X, Y);
            this.playerTexture = playerTexture;
            isFalling = true;
            isOnPlatform = false;
            end = false;
        }

        public void setXVelocity(float velocity)
        {

        }
        public void setYVelocity(float velocity)
        {
            this.velocity.Y = velocity;
        }

        public void Update(GameTime gameTime, KeyboardState keyboard)
        {
            // Update:
            float time = (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (!isOnPlatform)
            {
                if (velocity.Y < max_y_velocity)
                {
                    velocity.Y += gravity.Y;
                }
                if (velocity.Y > max_y_velocity)
                {
                    velocity.Y = max_y_velocity;
                }
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
                if ((Math.Abs(velocity.X) < max_x_velocity))
                {
                    velocity.X += 50;
                }
            }
            if (keyboard.IsKeyDown(Keys.Left))
            {

                if ((Math.Abs(velocity.X) < max_x_velocity))
                {
                    velocity.X -= 50;
                }
            }
            if (keyboard.IsKeyDown(Keys.Down))
            {
            }

            if (keyboard.IsKeyDown(Keys.Up))
            {
                if(isOnPlatform) {
                    
                    velocity.Y += -500;
                    isOnPlatform = false;
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

            if (position.X >= mapWidth - playerTexture.Width)  
                position.X = mapWidth - playerTexture.Width;

            if (position.Y <= 0)
                position.Y = 0;

            if (position.Y >= mapHeight - playerTexture.Height)
            {
                position.X = spawnPosition.X;
                position.Y = spawnPosition.Y;
            }
        }

        public void UpdatePosition(float time)
        {
                  
            position.X += (int)(velocity.X * time);
            position.Y += (int)(velocity.Y * time);
        }

        public Rectangle getHitBox()
        {
            return new Rectangle((int)position.X, (int)position.Y, Width, Height);
        }

       //Check bug when player jumps first, then moves right/left. 
        public void CheckCollisionSide(Rectangle player, Rectangle tile, MapTile mapTile)
        {
            if (player.Intersects(tile))
            {
                checkTile(mapTile);

                int ydiff = (int)(tile.Y - player.Y);
                int xdiff = (int)(tile.X - player.X);
                int min_translation;
                
                //if -x, +y - player is topRight
                //if -x, -y - player is bottomright
                //if +x, +y - player is topLeft
                //if +x, -y - player is bottomleft

                /* If player is colliding with the top left corner of tile*/
                if (xdiff >= 0 && ydiff >= 0)
                {

                    /* If player's difference from left of tile is greater than difference from top of tile,
                     * shift to the left*/
                    if (Math.Abs(player.Left - tile.Left) > Math.Abs(player.Top - tile.Top))
                    {
                        if (!mapTile.isBreakable)
                        {
                            min_translation = player.Right - tile.Left;
                            position.X -= min_translation;
                            velocity.X = 0;
                        }
                        
                        
                    }
                    /* If player's difference from top of tile is greater than difference from left of tile,
                     * shift upwards */
                    else
                    {

                        /* Implement this where the player hits the tile */
                        if (mapTile.isBouncy)
                        {
                            velocity.Y += -1000;
                        }

                        else
                        {                        
                            min_translation = player.Bottom - tile.Top;
                            position.Y -= min_translation;
                            isOnPlatform = true;
                            velocity.Y = 0;
                        
                        }

                    }
                }
                /* If player is colliding with top right corner of tile*/
                else if (xdiff <= 0 && ydiff >= 0)
                {


                    /* If player's difference from right of tile is smaller than difference from top of tile,
                     * shift upwards*/
                    if (Math.Abs(player.Right - tile.Right) < Math.Abs(player.Top - tile.Top))
                    {
                        if (mapTile.isBouncy)
                        {
                            velocity.Y += -1000;
                        }
                        else
                        {
                            min_translation = player.Bottom - tile.Top;
                            position.Y -= min_translation;
                            isOnPlatform = true;
                            velocity.Y = 0;
                        }
                        
                    }
                    /* If player's difference from right of tile is greater than difference from top of tile,
                     * shift to the right*/
                    else
                    {
                        if (!mapTile.isBreakable)
                        {
                            min_translation = player.Left - tile.Right;
                            position.X -= min_translation;
                            velocity.X = 0;
                        }
                        
                    }
                }
                /* If player is colliding with bottom-left corner of tile*/
                else if (xdiff >= 0 && ydiff <= 0)
                {

                    if (!mapTile.isBreakable)
                    {

                        /* If player's difference from left of tile is greater than difference from bottom of tile,
                         * shift to the left*/
                        if (Math.Abs(player.Left - tile.Left) > Math.Abs(player.Bottom - tile.Bottom))
                        {
                            min_translation = player.Right - tile.Left;
                            position.X -= min_translation;
                            velocity.X = 0;

                        }
                        /* If player's difference from left of tile is less than difference from bottom of tile,
                         * shift downwards*/
                        else
                        {
                            /* Implement this where the player hits the tile */
                            min_translation = player.Top - tile.Bottom;
                            position.Y -= min_translation;
                            velocity.Y = 0;
                            // TODO: Have the player phase through the tile when colliding from the bottom

                        }
                    }
                }
                /* If player is colliding with bottom-right corner of tile*/
                else if (xdiff <= 0 && ydiff <= 0)
                {


                    /* Implement this where the player hits the tile */
                    if (!mapTile.isBreakable)
                    {
                        /* If player's difference from right of tile is greater than difference from bottom of tile,
                        * shift to the right*/
                        if (Math.Abs(player.Right - tile.Right) > Math.Abs(player.Bottom - tile.Bottom))
                        {

                            min_translation = player.Left - tile.Right;
                            position.X -= min_translation;
                            velocity.X = 0;
                        }
                        /* If player's difference from right of tile is smaller than difference from bottom of tile,
                         * shift downwards*/
                        else
                        {
                            min_translation = player.Top - tile.Bottom;
                            position.Y -= min_translation;
                            velocity.Y = 0;

                        }
                        // TODO: Have the player phase through the tile when colliding from the bottom
                    }
                }
            }
        }

        private void checkTile(MapTile mapTile)
        {
            if (mapTile.isCake)
            {
                end = true;
                return;
            }

            if (mapTile.isTrap)
            {
                position.X = spawnPosition.X;
                position.Y = spawnPosition.Y;
            }

        }

        public Vector2 GetPosition()
        {
            return position;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(playerTexture, new Rectangle((int)position.X,
                (int)position.Y,
                playerTexture.Width, playerTexture.Height), Color.White);
        }

    }
}
