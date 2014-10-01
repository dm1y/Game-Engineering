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

            // i = 25 covers the entire floor of current screen. 
            for (int i = 0; i < 20; i++)
            {
                /* Use to build upwards to cover the entire level space*/
                    mapTiles.Add(new MapTile(i, 0, tileTexture, game));
            }

            for (int i = 7; i < 12; i++)
            {
                /* Use to build upwards to cover the entire level space*/
                mapTiles.Add(new MapTile(i, 3, tileTexture, game));
            }

            for (int i = 13; i < 15; i++)
            {
                /* Use to build upwards to cover the entire level space*/
                mapTiles.Add(new MapTile(i, 6, tileTexture, game));
            }
            /* So the player will begin on top of the blocks*/
            player = new Player(playerTexture.Width, game.GraphicsDevice.Viewport.Height - 3*tileTexture.Height, playerTexture, game);

//            player = new Player(playerTexture.Width, game.GraphicsDevice.Viewport.Height / 2, playerTexture);
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

            // For convenience when testing, press [ESC] key to leave the game 
            if (currentKeyboardState.IsKeyDown(Keys.Escape))
                game.Exit();

        }

        public void UpdateCollisions()
        {
            Rectangle playerHitBox;
            Rectangle terrainHitBox;

            playerHitBox = new Rectangle((int)player.position.X, (int)player.position.Y, player.Width, player.Height);

            player.isOnPlatform = false; 

            foreach (MapTile tile in mapTiles)
            {
                terrainHitBox = new Rectangle((int)(tile.mapPosition.X),
                    (int)tile.mapPosition.Y,
                    tile.Width, tile.Height);
                if (playerHitBox.Intersects(terrainHitBox))
                {
                    player.isFalling = false;
                    player.setYVelocity(0);
                    player.isOnPlatform = true;
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
