using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace Project2
{
    public class World
    {

        public Game1 game;

        //// ---- MOVEMENT ---- //
        KeyboardState currentKeyboardState;
        KeyboardState previousKeyboardState;
        //GamePadState currentGamePadState;
       // GamePadState previousGamePadState;

        Player player;

        List<MapTile> mapTiles;
        Texture2D playerTexture;
        Texture2D tileTexture;

        public World(Game1 game)
        {
            this.game = game;
            mapTiles = new List<MapTile>();
            
            // Do stuff 
        }

        public void LoadContent(ContentManager Content)
        {
            playerTexture = Content.Load<Texture2D>("triangle");
            tileTexture = Content.Load<Texture2D>("cube");


            for (int i = 0; i < 20; i++)
            {

                mapTiles.Add(new MapTile(i, 0, tileTexture, game));
                //Console.Write("'n New Maptile at: (" + i + ", " + 0 + ")\n"); 
            }

            player = new Player(playerTexture.Width, game.GraphicsDevice.Viewport.Height / 2, playerTexture);
            // Do stuff 
        }

        public void Update(GameTime gametime)
        {

            //previousGamePadState = currentGamePadState;
            previousKeyboardState = currentKeyboardState;

            // Read the current state of the keyboard and gamepad and store it
            currentKeyboardState = Keyboard.GetState();
            //currentGamePadState = GamePad.GetState(PlayerIndex.One);

            UpdateCollisions();
            player.Update(gametime, currentKeyboardState);
            // Do stuff 
        }

        public void UpdateCollisions()
        {
            Rectangle playerHitBox;
            Rectangle terrainHitBox;

            playerHitBox = new Rectangle((int)player.position.X, (int)player.position.Y, player.Width, player.Height);

            foreach (MapTile tile in mapTiles)
            {
                terrainHitBox = new Rectangle((int)(tile.mapPosition.X),
                    (int)tile.mapPosition.Y,
                    tile.Width, tile.Height);
                if (playerHitBox.Intersects(terrainHitBox))
                {
                    Console.Write("Collided");
                    player.setXVelocity(0);
                    player.setYVelocity(0);
                    //do more stuff
                }
            }
        }
        public void Draw(SpriteBatch sb)
        {
            foreach (MapTile tile in mapTiles)
            {
                //Console.Write("drawing");
                tile.Draw(sb);
            }
            player.Draw(sb);
            // Do stuff
        }

    }
}
