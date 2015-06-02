using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TP_DJA
{
    public class Enemy : Sprite
    {
        public Rectangle bounds { get; set; }
        public Enemy(ContentManager content, GraphicsDeviceManager graphics, Rectangle rec)
            : base(content, graphics)
        {
            this.bounds = rec;            
        }

        public void LoadContent(string name)
        {
            base.LoadContent(name);
        }

        public void Update()
        {

        }
        public void MoveRight(int val)
        {
            bounds = new Rectangle(bounds.X + val, bounds.Y, bounds.Width, bounds.Height);
        }

        public void MoveLeft(int val)
        {
            bounds = new Rectangle(bounds.X - val, bounds.Y, bounds.Width, bounds.Height);
        }

        public void MoveDown(int val)
        {
            bounds = new Rectangle(bounds.X, bounds.Y + val, bounds.Width, bounds.Height);
        }
        public bool ColideCom(Bullet bala)
        {
            return this.bounds.Intersects(bala.Bounds);
        }
        public bool ColideComShip(Spaceship ship)
        {
            return this.bounds.Intersects(ship.Bounds);
        }
        public void Draw(SpriteBatch spriteBatch)
        {
                spriteBatch.Draw(Texture, bounds, Color.White);
        }

    }
}
