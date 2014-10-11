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
        public Camera camera { get; private set; }

        Viewport newView;

        Texture2D playerIdleRight;
        Texture2D playerIdleLeft;
        Texture2D movingRightTexture;
        Texture2D movingLeftTexture;
        Texture2D jumpRight;
        Texture2D jumpLeft;
        Texture2D playerDeath;

        Texture2D tileTexture;

        List<MapTile> mapTiles;
        int level_counter = 0;

        public Boundaries boundaries { get; private set; }

        public World(Game1 g, Camera c)
        {
            game = g;
            camera = c;
            newView = g.GraphicsDevice.Viewport;
            mapTiles = new List<MapTile>();
        }

        public void LoadContent(ContentManager Content)
        {
            //Adding textures
            playerIdleRight = Content.Load<Texture2D>("idleright2");
            playerIdleLeft = Content.Load<Texture2D>("idleleft2");
            tileTexture = Content.Load<Texture2D>("cube");
            movingRightTexture = Content.Load<Texture2D>("walkright2");
            movingLeftTexture = Content.Load<Texture2D>("walkleft2");
            LoadMap(0);
        }

        public void LoadMap(int i)
        {
            MapTileData[] data = game.Content.Load<MapTileData[]>("LevelTester" + i);
            boundaries = game.Content.Load<Boundaries>("LevelBoundary" + i);

            foreach (MapTileData d in data)
            {
                mapTiles.Add(new MapTile((int)d.mapPosition.X, (int)d.mapPosition.Y,
                    game.Content.Load<Texture2D>(d.tileTexture), game, d.isBouncy, d.isBreakable, d.isTrap, d.isCake));
            }

            /* So the player will begin on top of the blocks*/
            player = new Player(0, newView.Height - 3 * tileTexture.Height, game,
                playerIdleRight, playerIdleLeft, movingRightTexture, movingLeftTexture, null, null, null);

            player.setBoundaries(boundaries.x, boundaries.y);
            camera.setBoundaries(boundaries.x, boundaries.y); // Passes in Map Boundaries to Camera
        }

        public void changeLevel()
        {
            level_counter++;

            /* If the level is not the last level*/
            if (level_counter < 3)
                LoadMap(level_counter);
            else
                game.EndGame();
        }

        private void PlayMusic(Song song)
        {
            try
            {
                MediaPlayer.Play(song);


                // Loop the song
                MediaPlayer.IsRepeating = true;
            }
            catch { }
        }


        public void Update(GameTime gametime)
        {

            //previousGamePadState = currentGamePadState;
            previousKeyboardState = currentKeyboardState;

            // Read the current state of the keyboard and gamepad and store it
            currentKeyboardState = Keyboard.GetState();
            //currentGamePadState = GamePad.GetState(PlayerIndex.One);


            player.Update(gametime, currentKeyboardState);
            UpdateCollisions();

            // Do stuff 

            //camera.Limits = new Rectangle(0, 0, newView.X / 2, newView.Y / 2);
            camera.Update(gametime, player); //Update Camera


            // For convenience when testing, press [ESC] key to leave the game 
            if (currentKeyboardState.IsKeyDown(Keys.Escape))
                game.Exit();

            /* If player touches the cake, transition to new level / end the game */
            if (player.end)
            {
                player.end = false;
                camera.ResetCamera(); //Testing
                mapTiles.Clear();
                changeLevel();
            }

        }

        public void UpdateCollisions()
        {

            Rectangle terrainHitBox;
            Rectangle playerHitBox;

            player.isOnPlatform = false;

            foreach (MapTile tile in mapTiles)
            {
                terrainHitBox = new Rectangle((int)(tile.mapPositions.X),
                      (int)tile.mapPositions.Y,
                    tile.Width, tile.Height);
                playerHitBox = player.getHitBox();

                player.CheckCollisionSide(playerHitBox, terrainHitBox, tile);

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