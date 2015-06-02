using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TP_DJA
{
    public class Enemies : Sprite
    {
        const int noInvadersX = 15;
        public int speed;
        const int noInvadersY = 4;
        Enemy[,] enemies; 
        bool[,] enemiesDead;
        SoundEffect Hit;
        int direction = 1;
        public int inimigosMortos = 0;
        public int GamePoints = 0;
        public Enemies(ContentManager content, GraphicsDeviceManager graphics)
            : base(content, graphics)
        {
            enemies = new Enemy[noInvadersX, noInvadersY];
            enemiesDead = new bool[noInvadersX, noInvadersY];
            for (int i = 0; i < noInvadersX; i++)
            {
                for (int j = 0; j < noInvadersY; j++)
                {
                    Rectangle r = new Rectangle(i * 50 + 15, j * 50 + 30, 48, 48);
                    enemies[i, j] = new Enemy(content, graphics, r);
                    enemiesDead[i, j] = false;
                }
            }
        }

        public void LoadContent(string name)
        {
            Hit = Content.Load<SoundEffect>("Audio/invaderkilled");
            for (int i = 0; i < noInvadersX; i++)
            {
                for (int j = 0; j < noInvadersY; j++)
                {
                        enemies[i, j].LoadContent(name);
                }
            }
        }
        public bool ColisaoParedeDireita()
        {
            Enemy last = null;
            bool colisao = false;
            for (int i = noInvadersX - 1; i >= 0 && last == null; i--)
            {
                for (int j = noInvadersY - 1; j >= 0 && last == null; j--)
                {
                   
                    if (!enemiesDead[i, j])
                    {
                        last = enemies[i, j];
                    }
                }
            }
            if (last != null && last.bounds.X >= graphics.PreferredBackBufferWidth - last.bounds.Width)
            {
                return true;
            }
            return false;
        }
        public bool ColisaoParedeEsquerda()
        {
            Enemy last = null;

            for (int i = 0; i < noInvadersX && last == null; i++)
            {
                for (int j = 0; j < noInvadersY && last == null; j++)
                {
                    if (!enemiesDead[i, j])
                        last = enemies[i, j];
                }
            }

            if (last != null && last.bounds.X <= 0)
            {
                return true;
            }
            return false;
        }

        public void MoveDown()
        {
            for (int i = 0; i < noInvadersX; i++)
            {
                for (int j = 0; j < noInvadersY; j++)
                {
                    enemies[i, j].MoveDown(24);
                }
            }
        }
        public void Update(GameTime gameTime, Bullet bullet, Spaceship ship)
        {
            speed = 100;
            bool colide = false;
            for (int i = 0; i < noInvadersX; i++)
            {
                for (int j = 0; j < noInvadersY; j++)
                {
                    float ellapsed = (float)gameTime.ElapsedGameTime.TotalSeconds * speed;
                    
                    if (direction == 1)
                    {
                        enemies[i, j].MoveRight((int)ellapsed);
                    }
                    else
                    {
                        enemies[i, j].MoveLeft((int)ellapsed);
                    }
                }
            }
            if (ColisaoParedeDireita())
            {
                direction = -1;
                MoveDown();
            }

            if (ColisaoParedeEsquerda())
            {
                direction = 1;
                MoveDown();
            }
            for (int i = 0; i < noInvadersX && !colide; i++)
            {
                for (int j = 0; j < noInvadersY && !colide; j++)
                {
                    if (bullet.Fired && !enemiesDead[i,j] && enemies[i, j].ColideCom(bullet))
                    {
                        Hit.Play();
                        bullet.Fired = false;
                        enemiesDead[i, j] = true;
                        colide = true;
                        inimigosMortos++;
                        GamePoints += 5;
                     }

                    if(enemies[i,j].ColideComShip(ship))
                    {
                        bullet.falhados = 0;
                    }
                }
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            for (int i = 0; i < noInvadersX; i++)
            {
                for (int j = 0; j < noInvadersY; j++)
                {
                    if(!enemiesDead[i,j])
                        enemies[i, j].Draw(spriteBatch);
                }
            }
        }
    }
}
