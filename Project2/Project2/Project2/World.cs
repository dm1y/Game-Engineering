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

        List<MapTile> mapTiles;
        Texture2D playerTexture;
        Texture2D tileTexture;

        public World(Game1 game, Camera camera)  
        {
            this.game = game;
            mapTiles = new List<MapTile>();
            newView = game.GraphicsDevice.Viewport;
            //this.camera = new Camera(game.GraphicsDevice.Viewport);
            this.camera = camera;
        }

        public void LoadContent(ContentManager Content)
        {
            
            playerTexture = Content.Load<Texture2D>("triangle");
            tileTexture = Content.Load<Texture2D>("cube");
            MapTileData[] data = Content.Load<MapTileData[]>("LevelTester");

            foreach (MapTileData d in data)
            {
                mapTiles.Add(new MapTile((int) d.mapPosition.X, (int) d.mapPosition.Y, 
                    Content.Load<Texture2D>(d.tileTexture), game, d.isBouncy, d.isBreakable, d.isTrap, d.isCake));
            }


            /* So the player will begin on top of the blocks*/
            player = new Player(playerTexture.Width, newView.Height - 3*tileTexture.Height, playerTexture, game);
            player.setBoundaries(960, 640);

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
            camera.Update(gametime, player); //Update Camera
           

            // For convenience when testing, press [ESC] key to leave the game 
            if (currentKeyboardState.IsKeyDown(Keys.Escape))
                game.Exit();

        }

        public void UpdateCollisions()
        {
            //Rectangle playerHitBox;
            Rectangle terrainHitBox;

            //playerHitBox = new Rectangle((int)player.position.X, (int)player.position.Y, player.Width, player.Height);

            player.isOnPlatform = false; 

            foreach (MapTile tile in mapTiles)
            {
                terrainHitBox = new Rectangle((int)(tile.mapPositions.X),
                      (int)tile.mapPositions.Y,
                    tile.Width, tile.Height);

                player.CheckCollisionSide(terrainHitBox, tile);

            }

            /* If player touches the cake, transition to new level / end the game */
            /* Commented out due to the blank screen for EndGame() */
            //if (player.end)
            //    game.EndGame();


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
