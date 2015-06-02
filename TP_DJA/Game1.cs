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
    public enum GameState {MainMenu, Play, Pause, HowTo, Ranking, Exit}

    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        SpriteFont font, fontLucida;
        bool GameOver = false;
        bool Winner = false;
        MainMenu main;
        Play play;
        Ranking rankings;
        Exit exit;
        Back back;
        Spaceship spaceship;
        Bullet bullet;
        Enemies enemie;
        public GameState CurrentGameState = GameState.MainMenu;
        int ScreenWidth = 1200, ScreenHeight = 650;
        public int Currentlevel = 0;
        public int Lastlevel = 0;
        KeyboardState oldState;
        
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
            this.graphics.PreferredBackBufferWidth = 1200;
            this.graphics.PreferredBackBufferHeight = 650;
            this.graphics.ApplyChanges();

            IsMouseVisible = true;
            this.Window.Title = "KILL INVADERS";

            this.main = new MainMenu(this.graphics, this.Content);            
            spaceship = new Spaceship(Content, graphics);
            bullet = new Bullet(Content, graphics, spaceship);
            enemie = new Enemies(Content, graphics);
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

            main.LoadContent();
            play = new Play(Content.Load<Texture2D>("Buttons/START"), graphics.GraphicsDevice);
            play.setPosition(new Vector2(500, 350));
            rankings = new Ranking(Content.Load<Texture2D>("Buttons/RANKING"), graphics.GraphicsDevice);
            rankings.setPosition(new Vector2(500, 400));
            exit = new Exit(Content.Load<Texture2D>("Buttons/EXIT"), graphics.GraphicsDevice);
            exit.setPosition(new Vector2(500, 450));

            spaceship.LoadContent();
            bullet.LoadContent();
            enemie.LoadContent("Invaders/invader48");
            font = Content.Load<SpriteFont>("DefaultFont");
            fontLucida = Content.Load<SpriteFont>("Lucida");
            
            /*back = new Back(Content.Load<Texture2D>("BACK"), graphics.GraphicsDevice);
            back.setPosition(new Vector2(475, 600));*/

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

                if (play.isClicked == true)
                {
                    Currentlevel = 0;
                    CurrentGameState = GameState.Play;
                    spaceship.LoadContent();
                    bullet.LoadContent();
                    //enemie = new Enemies(Content, graphics);
                    enemie.LoadContent("Invaders/invader48");
                    //MediaPlayer.Stop();
                   // MediaPlayer.Play(Level1);
                }
                if (exit.isClicked == true)
                {
                    CurrentGameState = GameState.Exit;
                }
                
            }
            else if (CurrentGameState == GameState.Play)
            {
                //Currentlevel = 1;
                spaceship.Update();
                bullet.Update(gameTime);
                enemie.Update(gameTime, bullet, spaceship);
                if (enemie.inimigosMortos == 60)
                {
                    Winner = true;
                    CurrentGameState = GameState.Pause;
                }
                if (bullet.falhados == 0)
                {
                    GameOver = true;
                    CurrentGameState = GameState.Pause;
                }

            }
            else if (CurrentGameState == GameState.Pause)
            {
                KeyboardState newstate = Keyboard.GetState();

                if (Currentlevel == 0)
                {
                    if (Winner == false && GameOver == false)
                    {
                        CurrentGameState = GameState.Play;
                        Winner = false;
                        GameOver = false;
                        bullet.falhados = 3;
                        enemie = new Enemies(Content, graphics);
                        enemie.LoadContent("Invaders/invader48");
                    }
                    if (Winner == true)
                    {
                        if (newstate.IsKeyDown(Keys.R))
                        {                           
                            CurrentGameState = GameState.Play;
                            Winner = false;
                            GameOver = false;
                            bullet.falhados = 3;
                            enemie = new Enemies(Content, graphics);
                            enemie.LoadContent("Invaders/invader_2");
                            enemie.speed += 50;
                            Currentlevel = 2;
                        }
                    }

                    if (GameOver == true)
                    {
                        if (newstate.IsKeyDown(Keys.Escape))
                        {
                            CurrentGameState = GameState.Exit;
                        }
                        if (newstate.IsKeyDown(Keys.R))
                        {
                            Winner = false;
                            GameOver = false;
                            bullet.falhados = 3;
                            Currentlevel = 0;
                        }
                    }
                }

                if (Currentlevel == 2)
                {
                    if (Winner == true)
                    {
                        if (newstate.IsKeyDown(Keys.R))
                        {
                            CurrentGameState = GameState.Play;
                            Winner = false;
                            GameOver = false;
                            bullet.falhados = 3;
                            
                            enemie = new Enemies(Content, graphics);
                            enemie.LoadContent("Invaders/invader_3");
                            enemie.speed += 100;
                            Currentlevel = 3;
                        }
                    }

                    if (GameOver == true)
                    {
                        if (newstate.IsKeyDown(Keys.Escape))
                        {
                            CurrentGameState = GameState.Exit;
                        }
                        if (newstate.IsKeyDown(Keys.R))
                        {
                            Winner = false;
                            GameOver = false;
                            bullet.falhados = 3;
                            Currentlevel = 0;
                        }
                    }
                }
                if (Currentlevel == 3)
                {
                    if (Winner == true)
                    {
                        if (newstate.IsKeyDown(Keys.R) && oldState.IsKeyUp(Keys.R))
                        {
                            Winner = false;
                            GameOver = false;
                            bullet.falhados = 3;
                            Currentlevel = 0;
                        }
                        
                    }

                    if (GameOver == true)
                    {
                        if (newstate.IsKeyDown(Keys.Escape))
                        {
                            CurrentGameState = GameState.Exit;
                        }
                        if (newstate.IsKeyDown(Keys.R))
                        {
                            Winner = false;
                            GameOver = false;
                            bullet.falhados = 3;
                            Currentlevel = 0;
                        }
                    }
                }

            }

            else
            {
                this.Exit();
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
            else if (CurrentGameState == GameState.Play)
            {                
                if (Currentlevel == 0)
                {
                    spriteBatch.Draw(Content.Load<Texture2D>("Background Marte"), new Rectangle(0, 0, ScreenWidth, ScreenHeight), Color.White);
                    spriteBatch.DrawString(fontLucida, "Points: " + enemie.GamePoints, new Vector2(0, 0), Color.White, 0, new Vector2(0, 0), 0.5f, SpriteEffects.None, 0);
                    spriteBatch.DrawString(fontLucida, "Fails: " + bullet.falhados, new Vector2(0, 20), Color.White, 0, new Vector2(0, 0), 0.5f, SpriteEffects.None, 0);
                    spriteBatch.DrawString(fontLucida, "Press ESC to exit the game!!", new Vector2(0, 40), Color.White, 0, new Vector2(0, 0), 0.5f, SpriteEffects.None, 0);
                }
                else if (Currentlevel == 2)
                {
                    spriteBatch.Draw(Content.Load<Texture2D>("Background Jupiter"), new Rectangle(0, 0, ScreenWidth, ScreenHeight), Color.White);
                    spriteBatch.DrawString(fontLucida, "Points: " + enemie.GamePoints, new Vector2(0, 0), Color.White, 0, new Vector2(0, 0), 0.5f, SpriteEffects.None, 0);
                    spriteBatch.DrawString(fontLucida, "Fails: " + bullet.falhados, new Vector2(0, 20), Color.White, 0, new Vector2(0, 0), 0.5f, SpriteEffects.None, 0);
                    spriteBatch.DrawString(fontLucida, "Press ESC to exit the game!!", new Vector2(0, 40), Color.White, 0, new Vector2(0, 0), 0.5f, SpriteEffects.None, 0);
                }
                else if (Currentlevel == 3)
                {
                    spriteBatch.Draw(Content.Load<Texture2D>("Background Neptuno"), new Rectangle(0, 0, ScreenWidth, ScreenHeight), Color.White);
                    spriteBatch.DrawString(fontLucida, "Points: " + enemie.GamePoints, new Vector2(0, 0), Color.White, 0, new Vector2(0, 0), 0.5f, SpriteEffects.None, 0);
                    spriteBatch.DrawString(fontLucida, "Fails: " + bullet.falhados, new Vector2(0, 20), Color.White, 0, new Vector2(0, 0), 0.5f, SpriteEffects.None, 0);
                    spriteBatch.DrawString(fontLucida, "Press ESC to exit the game!!", new Vector2(0, 40), Color.White, 0, new Vector2(0, 0), 0.5f, SpriteEffects.None, 0);
                }
                spaceship.Draw(spriteBatch);
                bullet.Draw(spriteBatch);
                enemie.Draw(spriteBatch);
                //if (Winner == true)
                //{
                //    spriteBatch.DrawString(font, "YOU WIN!!!!", new Vector2(ScreenWidth/2 - 50, ScreenHeight/2), Color.Green);
                //    spriteBatch.DrawString(font, "Your Score: " + enemie.GamePoints + ".", new Vector2(ScreenWidth / 2 - 70, ScreenHeight / 2 + 35), Color.White);                   
                //}

                //if (GameOver == true)
                //{
                //    spriteBatch.DrawString(font, "GAME OVER!!!!", new Vector2(ScreenWidth / 2 - 50, ScreenHeight / 2), Color.Red);
                //    spriteBatch.DrawString(font, "Your Score: " + enemie.GamePoints + ".", new Vector2(ScreenWidth / 2 - 40, ScreenHeight / 2 + 35), Color.Black);
                //    spriteBatch.DrawString(font, "Press SPACE to restart or ESC to exit.", new Vector2(ScreenWidth / 2 - 230, ScreenHeight / 2 + 80), Color.Black);
                //}

                           
            }
            else if (CurrentGameState == GameState.Pause)
            {
                spaceship.Draw(spriteBatch);
                bullet.Draw(spriteBatch);
                enemie.Draw(spriteBatch);
                if (Winner == true)
                {
                    
                    if (Currentlevel == 0)
                    {
                        spriteBatch.Draw(Content.Load<Texture2D>("Background Marte"), new Rectangle(0, 0, ScreenWidth, ScreenHeight), Color.White);
                        spriteBatch.DrawString(font, "Press R to continue to the next level.", new Vector2(ScreenWidth / 2 - 230, ScreenHeight / 2 + 80), Color.White);
                        spriteBatch.DrawString(font, "YOU WIN!!!!", new Vector2(ScreenWidth / 2 - 50, ScreenHeight / 2), Color.Green);
                        spriteBatch.DrawString(font, "Your Score: " + enemie.GamePoints + ".", new Vector2(ScreenWidth / 2 - 70, ScreenHeight / 2 + 35), Color.White);
                    }
                    else if (Currentlevel == 2)
                    {
                        spriteBatch.Draw(Content.Load<Texture2D>("Background Jupiter"), new Rectangle(0, 0, ScreenWidth, ScreenHeight), Color.White);
                        spriteBatch.DrawString(font, "YOU WIN!!!!", new Vector2(ScreenWidth / 2 - 50, ScreenHeight / 2), Color.Green);
                        spriteBatch.DrawString(font, "Your Score: " + enemie.GamePoints + ".", new Vector2(ScreenWidth / 2 - 70, ScreenHeight / 2 + 35), Color.White);
                        spriteBatch.DrawString(font, "Press R to continue to the next level.", new Vector2(ScreenWidth / 2 - 230, ScreenHeight / 2 + 80), Color.White);

                    }
                    else if (Currentlevel == 3)
                    {
                        spriteBatch.Draw(Content.Load<Texture2D>("Background Neptuno"), new Rectangle(0, 0, ScreenWidth, ScreenHeight), Color.White);
                        spriteBatch.DrawString(font, "YOU WIN!!!!", new Vector2(ScreenWidth / 2 - 50, ScreenHeight / 2), Color.Green);
                        spriteBatch.DrawString(font, "Your Score: " + enemie.GamePoints + ".", new Vector2(ScreenWidth / 2 - 70, ScreenHeight / 2 + 35), Color.White);
                        spriteBatch.DrawString(font, "Press R to restart or ESC to exit.", new Vector2(ScreenWidth / 2 - 230, ScreenHeight / 2 + 80), Color.White);
                    }
                }

                if (GameOver == true)
                {
                    if (Currentlevel == 0)
                    {
                        spriteBatch.Draw(Content.Load<Texture2D>("Background Marte"), new Rectangle(0, 0, ScreenWidth, ScreenHeight), Color.White);
                        spriteBatch.DrawString(font, "Press R to restart or ESC to exit.", new Vector2(ScreenWidth / 2 - 230, ScreenHeight / 2 + 80), Color.White);
                        spriteBatch.DrawString(font, "GAME OVER!!!!", new Vector2(ScreenWidth / 2 - 50, ScreenHeight / 2), Color.Red);
                        spriteBatch.DrawString(font, "Your Score: " + enemie.GamePoints + ".", new Vector2(ScreenWidth / 2 - 70, ScreenHeight / 2 + 35), Color.White);
                    }
                    else if (Currentlevel == 2)
                    {
                        spriteBatch.Draw(Content.Load<Texture2D>("Background Jupiter"), new Rectangle(0, 0, ScreenWidth, ScreenHeight), Color.White);
                        spriteBatch.DrawString(font, "GAME OVER!!!!", new Vector2(ScreenWidth / 2 - 50, ScreenHeight / 2), Color.Red);
                        spriteBatch.DrawString(font, "Your Score: " + enemie.GamePoints + ".", new Vector2(ScreenWidth / 2 - 70, ScreenHeight / 2 + 35), Color.White);
                        spriteBatch.DrawString(font, "Press R to restart or ESC to exit.", new Vector2(ScreenWidth / 2 - 230, ScreenHeight / 2 + 80), Color.White);

                    }
                    else if (Currentlevel == 3)
                    {
                        spriteBatch.Draw(Content.Load<Texture2D>("Background Neptuno"), new Rectangle(0, 0, ScreenWidth, ScreenHeight), Color.White);
                        spriteBatch.DrawString(font, "GAME OVER!!!!", new Vector2(ScreenWidth / 2 - 50, ScreenHeight / 2), Color.Red);
                        spriteBatch.DrawString(font, "Your Score: " + enemie.GamePoints + ".", new Vector2(ScreenWidth / 2 - 70, ScreenHeight / 2 + 35), Color.White);
                        spriteBatch.DrawString(font, "Press R to restart or ESC to exit.", new Vector2(ScreenWidth / 2 - 230, ScreenHeight / 2 + 80), Color.White);
                    }
                }
                //spriteBatch.DrawString(fontLucida, "Points: " + enemie.GamePoints, new Vector2(0, 0), Color.Black, 0, new Vector2(0, 0), 0.5f, SpriteEffects.None, 0);
                //spriteBatch.DrawString(fontLucida, "Press ESC to exit the game!!", new Vector2(0, 20), Color.Black, 0, new Vector2(0, 0), 0.5f, SpriteEffects.None, 0);
            }


            // TODO: Add your drawing code here
            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
