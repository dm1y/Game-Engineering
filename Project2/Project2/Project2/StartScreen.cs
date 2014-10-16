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
    public class StartScreen
    {
        private Game1 game;
        private KeyboardState lastState;
        private Texture2D texture; /* Place holder if you want to have a picture for the start game screen */
        private SpriteFont font; /* Place holder if you want to have text display instructions */

        public StartScreen(Game1 game)
        {
            this.game = game;
            lastState = Keyboard.GetState();
            texture = game.Content.Load<Texture2D>("startscreen");
            font = game.Content.Load<SpriteFont>("SpriteFont1");
        }

        public void Update()
        {
            KeyboardState keyboardState = Keyboard.GetState();

            /* Starts the game */
            if (keyboardState.IsKeyDown(Keys.Enter) && lastState.IsKeyUp(Keys.Enter))
                game.StartGame();

            /* Exits the game */
            else if (keyboardState.IsKeyDown(Keys.Escape) && lastState.IsKeyUp(Keys.Escape))
                game.Exit();

            lastState = keyboardState;
        }

        public void Draw(SpriteBatch spriteBatch)
        {





            if (texture != null)
                spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, null, null, null, game.shader);
                //spriteBatch.Begin();
                
                spriteBatch.Draw(texture, new Vector2(spriteBatch.GraphicsDevice.Viewport.X, spriteBatch.GraphicsDevice.Viewport.Y), Color.White);
            
            spriteBatch.End();
            // Used as placeholders 

            // Used if you want to draw the instructions on 
            //spriteBatch.DrawString(font, "START SCREEN ",
            //   new Vector2(game.GraphicsDevice.Viewport.Width / 2, game.GraphicsDevice.Viewport.Height / 2),
            //   Color.Black);
            
            
            
            

            // Used if you want to draw the instructions on 
             //spriteBatch.DrawString(font, "PRESS [ENTER] TO START \n PRESS [ESC] TO EXIT",
                //new Vector2(game.GraphicsDevice.Viewport.Width, game.GraphicsDevice.Viewport.Height),
                //Color.GhostWhite); 
        }
    }
}
