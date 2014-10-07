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

    enum Screen
    {
        StartScreen,
        World, /* This will be the GamePlayScreen */
        GameOverScreen,
        EndGameScreen
    }
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        public GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
       

        Screen currentScreen;
        StartScreen startScreen;
        GameOverScreen gameOverScreen;
        Viewport viewPort;
        

        World gameWorld;

        private const int width = 960;
        private const int height = 640;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            graphics.PreferredBackBufferHeight = height;
            graphics.PreferredBackBufferWidth = width;
            graphics.ApplyChanges();
            viewPort = GraphicsDevice.Viewport;
            viewPort.Width = width;
            viewPort.Height = height;
            GraphicsDevice.Viewport = viewPort;
            
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
           
            
            

            gameWorld = new World(this, GraphicsDevice.Viewport); 
            gameWorld.LoadContent(this.Content);

            base.Initialize();
            startScreen = new StartScreen(this);
            currentScreen = Screen.StartScreen;
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            
            //gameWorld.LoadContent(Content);
            // TODO: use this.Content to load your game content here
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();
            gameWorld.Update(gameTime);
            // TODO: Add your update logic here
            //switch (currentScreen)
            //{
            //    case Screen.StartScreen:
            //        if (startScreen != null)
            //            startScreen.Update();
            //        break;
            //    case Screen.World:
            //        if (gameWorld != null)
            //            gameWorld.Update(gameTime);
            //        break;
            //    case Screen.GameOverScreen:
            //        if (gameOverScreen != null)
            //            gameOverScreen.Update();
            //        break;
            //}

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            
            spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, null, null, null, null, gameWorld.camera.transform); 
            gameWorld.Draw(spriteBatch);
            //switch (currentScreen)
            //{
            //    case Screen.StartScreen:
            //        if (startScreen != null)
            //            startScreen.Draw(spriteBatch);
            //        break;
            //    case Screen.World:
            //        if (gameWorld != null)
            //            gameWorld.Draw(spriteBatch);
                      
            //        break;
            //    case Screen.GameOverScreen:
            //        gameOverScreen.Draw(spriteBatch);
            //        break;
            //}


            spriteBatch.End();
           
            base.Draw(gameTime);
        }

        public void StartGame()
        {
           // gameWorld = new World(this,viewPort);  //Added viewport //was GraphicsDevice.Viewport
            gameWorld.LoadContent(this.Content);

            currentScreen = Screen.World;

            startScreen = null;
            gameOverScreen = null;
        }

        public void EndGame()
        {
            gameOverScreen = new GameOverScreen(this);
            currentScreen = Screen.GameOverScreen;

            gameWorld = null;
        }
    }
}
