#region Using Statements
using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Storage;
using Microsoft.Xna.Framework.GamerServices;
#endregion

namespace TP_DJA
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    enum GameState {MainMenu, Play, HowTo, Ranking, Exit}
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        MainMenu main;
        Play play;
        Ranking rankings;
        Exit exit;
        Back back;
        GameState CurrentGameState = GameState.MainMenu;
        int ScreenWidth = 1366, ScreenHeight = 768;
        
        public Game1()
            : base()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            
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
            this.graphics.PreferredBackBufferWidth = 1366;
            this.graphics.PreferredBackBufferHeight = 768;
            this.graphics.ApplyChanges();
            IsMouseVisible = true;
            this.Window.Title = "KILL INVADERS";

            if (CurrentGameState == GameState.MainMenu)
            {
                this.main = new MainMenu(this.graphics, this.Content);
            }
            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            if (CurrentGameState == GameState.MainMenu)
            {
                main.LoadContent();
                play = new Play(Content.Load<Texture2D>("Buttons/START"), graphics.GraphicsDevice);
                play.setPosition(new Vector2(500, 350));
                rankings = new Ranking(Content.Load<Texture2D>("Buttons/RANKING"), graphics.GraphicsDevice);
                rankings.setPosition(new Vector2(500, 400));                
                exit = new Exit(Content.Load<Texture2D>("Buttons/EXIT"), graphics.GraphicsDevice);
                exit.setPosition(new Vector2(500, 450));
            }
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
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            MouseState mouse = Mouse.GetState();

            // TODO: Add your update logic here

            if (CurrentGameState == GameState.MainMenu)
            {
                play.Update(mouse);
                rankings.Update(mouse);
                exit.Update(mouse);
            }
            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            spriteBatch.Begin();
            if (CurrentGameState == GameState.MainMenu)
            {
                spriteBatch.Draw(Content.Load<Texture2D>("Background main"), new Rectangle(0, 0, ScreenWidth, ScreenHeight), Color.White);
                play.Draw(spriteBatch);
                rankings.Draw(spriteBatch);
                exit.Draw(spriteBatch);
            }
            // TODO: Add your drawing code here
            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
