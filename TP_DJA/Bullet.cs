using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TP_DJA
{
    public class Bullet : Sprite
    {
        Spaceship ship;
        public bool Fired;
        public int falhados = 3;
        KeyboardState oldState;
        SoundEffect shoot;
        public Rectangle Bounds { get; set; }

        public Bullet(ContentManager content, GraphicsDeviceManager graphics, Spaceship ship)
            : base(content, graphics)
        {
            this.ship = ship;
            this.Fired = false;
        }

        public void LoadContent()
        {
            base.LoadContent("Bullets/projectile02");
            shoot = Content.Load<SoundEffect>("Audio/shoot");
        }

        public void Update(GameTime gameTime)
        {
            KeyboardState newState = Keyboard.GetState();
            if (Fired)
            {
                if (Position.Y < 0)
                {
                    Fired = false;
                    falhados--;
                }
                else
                {
                    Vector2 direction = new Vector2(0, -1);
                    float speed = 500;
                    float elapsed = (float)gameTime.ElapsedGameTime.TotalSeconds;
                    Position = Position + direction * speed * elapsed;
                    Bounds = new Rectangle((int)Position.X, (int)Position.Y, Texture.Width, Texture.Height);
                }
            }
            if (newState.IsKeyDown(Keys.Space) && !Fired && oldState.IsKeyUp(Keys.Space))
            {
                Fired = true;
                Position = new Vector2(ship.Position.X + ship.Texture.Width / 2, ship.Position.Y - ship.Texture.Height / 2);
                Bounds = new Rectangle((int)Position.X, (int)Position.Y, Texture.Width, Texture.Height);
                shoot.Play();
            }
            oldState = newState;

        }

        public new void Draw(SpriteBatch spriteBatch)
        {
            if (Fired)
            {
                spriteBatch.Draw(Texture, Position, Color.White);
            }
        }
    }
}
