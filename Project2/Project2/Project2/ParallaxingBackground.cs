using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace Project2
{
    public class ParallaxingBackground
    {
        //Parallaxing Background Image
        Texture2D texture;

        // Positions of the background
        Vector2[] background_Pos;

        // Background movement speed
        public int background_speed { set; get; }

        public ParallaxingBackground()
        {
        }

        public void Initialize(Texture2D texture, int speed, int screenWidth)
        {
            // Loads the background texture 
            this.texture = texture;

            this.background_speed = speed;

            // Divide the screen width by the texture width to determine the number of tiles necessary.
            // Add 1 so there won't be a gap in the tiling
            background_Pos = new Vector2[screenWidth / texture.Width + 1];

            // Sets the first positions of the parallaxing background
            for (int i = 0; i < background_Pos.Length; i++)
           {
                //  Tiles need to be side by side in order to create a tiling effect
                background_Pos[i] = new Vector2(i * texture.Width, 0);
            }
        }

        public void Update()
        {
            // Updates the positions of the background
            for (int i = 0; i < background_Pos.Length; i++)
            {
                // Updates the position of the screen by adding the speed
                background_Pos[i].X += background_speed;
                // If the speed is negative and has the background moving left
                if (background_speed <= 0)
                {
                    // See if the texture is out of view and put that texture at the end of the screen
                   if (background_Pos[i].X <= -texture.Width)
                   {
                        background_Pos[i].X = texture.Width * (background_Pos.Length - 1);
                    }
                }
                // If the speed is positive and the background moves right
                else
                {
                    // See if the texture is out of view and then position it to the beginning of the viewable screen
                    if (background_Pos[i].X >= texture.Width * (background_Pos.Length - 1))
                    {
                        background_Pos[i].X = -texture.Width;
                    }
                }
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            for (int i = 0; i < background_Pos.Length; i++)
            {
                spriteBatch.Draw(texture, background_Pos[i], Color.White);
            }
        }
    }
}
