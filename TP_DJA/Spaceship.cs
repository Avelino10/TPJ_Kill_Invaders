using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TP_DJA
{
    public class Spaceship : Sprite
    {
        public Rectangle Bounds { get; set; }
        public Spaceship(ContentManager content, GraphicsDeviceManager graphics)
            : base(content, graphics)
        {

        }

        public void LoadContent()
        {
            base.LoadContent("Ship/spaceship73");
            Position = new Vector2(this.graphics.PreferredBackBufferWidth / 2 - Texture.Width / 2, this.graphics.PreferredBackBufferHeight - Texture.Height);
            Bounds = new Rectangle((int)Position.X, (int)Position.Y, Texture.Width, Texture.Height);
        }

        public new void Update()
        {
            KeyboardState newstate = Keyboard.GetState();
            
            if (newstate.IsKeyDown(Keys.Left) && Position.X > 0)
            {
                Position = new Vector2(Position.X - 5, Position.Y);
                Bounds = new Rectangle((int)Position.X, (int)Position.Y, Texture.Width, Texture.Height);
            }
            else if (newstate.IsKeyDown(Keys.Right) && Position.X < graphics.PreferredBackBufferWidth - Texture.Width)
            {
                Position = new Vector2(Position.X + 5, Position.Y);
                Bounds = new Rectangle((int)Position.X, (int)Position.Y, Texture.Width, Texture.Height);
            }
        }
        public new void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Texture, Position, Color.White);
        }
    }
}
