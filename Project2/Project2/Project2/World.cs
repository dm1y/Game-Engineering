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
       public Song gameMusic;
        public SoundEffect jumpSound;
        public SoundEffectInstance jumpSoundInstance;
        public SoundEffect laughSound;
        public SoundEffectInstance laughSoundInstance;
        public SoundEffect deathSound;
        public SoundEffectInstance deathSoundInstance;
        public SoundEffect collapseSound;
        public SoundEffectInstance collapseSoundInstance;
        public SoundEffect bounceSound;
        public SoundEffectInstance bounceSoundInstance;

        List<MapTile> mapTiles;
        int level_counter = 0;

        public LevelInfo boundaries { get; private set; }

        public ParallaxingBackground background;
        Boolean isNewLevel;

        public World(Game1 g, Camera c)
        {
            game = g;
            camera = c;
            newView = g.GraphicsDevice.Viewport;
            mapTiles = new List<MapTile>();
            background = new ParallaxingBackground(g);
            isNewLevel = true;
        }

        public void LoadContent(ContentManager Content)
        {
            //Adding textures
            playerIdleRight = Content.Load<Texture2D>("idleright2");
            playerIdleLeft = Content.Load<Texture2D>("idleleft2");
            tileTexture = Content.Load<Texture2D>("cube");
            movingRightTexture = Content.Load<Texture2D>("walkright2");
            movingLeftTexture = Content.Load<Texture2D>("walkleft2");
            playerDeath = Content.Load<Texture2D>("playerdeath");
            gameMusic = Content.Load<Song>("Darkness_Pt_1v2");
            jumpSound = Content.Load<SoundEffect>("jump");
            jumpSoundInstance = jumpSound.CreateInstance();
            laughSound = Content.Load<SoundEffect>("Goblin");
            laughSoundInstance = laughSound.CreateInstance();
            deathSound = Content.Load<SoundEffect>("meow");
            deathSoundInstance = deathSound.CreateInstance();
            collapseSound = Content.Load<SoundEffect>("collapsing");
            collapseSoundInstance = collapseSound.CreateInstance();
            bounceSound = Content.Load<SoundEffect>("bouncesound");
            bounceSoundInstance = bounceSound.CreateInstance();

            LoadMap(0);
            PlayMusic(gameMusic);
            MediaPlayer.Volume = 0.7f;
        }

        public void LoadMap(int i)
        {
            
            boundaries = game.Content.Load<LevelInfo>("LevelBoundary" + i);

            if (isNewLevel)
            {
                background.Initialize(game.Content.Load<Texture2D>(boundaries.backgroundTexture), 1,
                    boundaries.x, boundaries.texture, boundaries.hasTexture);
            }

            MapTileData[] data = game.Content.Load<MapTileData[]>("LevelTester" + i);

            foreach (MapTileData d in data)
            { 
                mapTiles.Add(new MapTile((int)d.mapPosition.X, (int)d.mapPosition.Y,
                    game.Content.Load<Texture2D>(d.tileTexture), game, d.isBouncy, d.isBreakable, d.isTrap, 
                    d.isUnstable, d.isCake, d.isSaw, d.isLock, d.isKey, false));
            }

            /* So the player will begin on top of the blocks*/
            player = new Player(0, newView.Height - 3 * tileTexture.Height, game,
                playerIdleRight, playerIdleLeft, movingRightTexture, movingLeftTexture, null, null, playerDeath, this);

            player.setBoundaries(boundaries.x, boundaries.y);
            camera.setBoundaries(boundaries.x, boundaries.y); // Passes in Map Boundaries to Camera
            deathSoundInstance.Stop(); //stop death sound
            
            //PlayMusic(gameMusic);
           
        }

        public void removeTile(MapTile x)
        {
            mapTiles.Remove(x);
            //mapTiles.RemoveAt(i);

            //foreach (MapTile tile in mapTiles)
            //{
            //    if (tile.isLock)
            //        mapTiles.Remove(tile);
            //}
        }

        public void changeLevel()
        {
            isNewLevel = true;
            camera.ResetCamera();
            mapTiles.Clear();

            level_counter++;

            /* If the level is not the last level*/
            if (level_counter < 3)
            {
                
                LoadMap(level_counter);
                
            }
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
            
            background.Update();
            
            //previousGamePadState = currentGamePadState;
            previousKeyboardState = currentKeyboardState;

            // Read the current state of the keyboard and gamepad and store it
            currentKeyboardState = Keyboard.GetState();
            //currentGamePadState = GamePad.GetState(PlayerIndex.One);

            foreach (MapTile tile in mapTiles)
            {
                tile.Update(gametime);
            }
            player.Update(gametime, currentKeyboardState);
            UpdateCollisions();

            // Do stuff 

            camera.Update(gametime, player); //Update Camera

            // For convenience when testing, press [ESC] key to leave the game 
            if (currentKeyboardState.IsKeyDown(Keys.Escape))
                game.Exit();

            // If player is dead, reset the level. 
            if (player.CheckDeath() == true) {
                
                player.isDead = false;
            // PLAY THE SEQUENCE HERE. 
                LevelReset();
            }
            // ResetTiles();

            /* If player touches the cake, transition to new level / end the game */
            if (player.end)
            {
                laughSoundInstance.Play();
                player.end = false;
                
                changeLevel();
            }

        }

        /* Resets the current level */
        public void LevelReset() 
        {
            isNewLevel = false;
            camera.ResetCamera();
            mapTiles.Clear();
            LoadMap(level_counter);
        }

        /* This function isn't necessary if we're resetting the entire level instead of just the tile */
        public void ResetTiles()
        {
            foreach (MapTile tile in mapTiles)
            {
                if (tile.isActive == false)
                {
                    tile.isActive = true;
                    tile.ResetAnimation();
                }
            }
        }

        public void UpdateCollisions()
        {

            Rectangle terrainHitBox;
            Rectangle playerHitBox = player.getHitBox();

            player.isOnPlatform = false;

            foreach (MapTile tile in mapTiles)
            {
                terrainHitBox = new Rectangle((int)(tile.mapPositions.X),
                      (int)tile.mapPositions.Y, tile.Width, tile.Height);

                if (player.hasKey && tile.isLock)
                {
                    //turn block off, maybe play animation for it
                    tile.isActive = false;
                    tile.PlayAnimationOnce();
                }

                player.CheckCollisionSide(playerHitBox, terrainHitBox, tile);
                playerHitBox = player.getHitBox();

            }
        }


        public void Draw(SpriteBatch sb)
        {
            
            sb.End();

            game.shader.CurrentTechnique = game.shader.Techniques["FunkyBlur"];
            //sb.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, null, null, null, null, camera.GetViewMatrix());
            //game.shader.CurrentTechnique.Passes["Pass1"].Apply();
           
                
            //    background.Draw(sb);
                
            //    sb.End();
            //    sb.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, null, null, null, null, camera.GetViewMatrix());
            //    game.shader.CurrentTechnique.Passes["Pass2"].Apply();
            //    background.Draw(sb);
            //    sb.End();
            foreach (EffectPass pass in game.shader.CurrentTechnique.Passes)
            {
                sb.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, null, null, null, null, camera.GetViewMatrix());
                
                pass.Apply();
                background.Draw(sb);
                sb.End();
                
                
            }
                
            
            
            sb.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, null, null, null, null, camera.GetViewMatrix());
            foreach (MapTile tile in mapTiles)
            {
                tile.Draw(sb);
            }
            

            player.Draw(sb);
            sb.End();
        }

    }
}