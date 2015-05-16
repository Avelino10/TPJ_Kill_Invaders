using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TP_DJA
{
    class MainMenu
    {
        public Texture2D Texture { get; set; }
        public Vector2 Position { get; set; }
        public GraphicsDeviceManager Graphics { get; set; }
        public ContentManager Content { get; set; }

        public MainMenu(GraphicsDeviceManager graphics, ContentManager content)
        {
            this.Graphics = graphics;
            this.Content = content;
            Position = new Vector2(0, 0);
        }

        public void LoadContent()
        {
            this.Texture = Content.Load<Texture2D>("Background main");
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Texture, Position, Color.White);
        }
    }
}
