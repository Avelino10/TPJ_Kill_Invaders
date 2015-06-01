using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TP_DJA
{
    public class Sprite
    {
        public Texture2D Texture { get; set; }
        public Vector2 Position { get; set; }
        public ContentManager Content { get; set; }
        public GraphicsDeviceManager graphics { get; set; }

        public Sprite(ContentManager content, GraphicsDeviceManager graphics)
        {
            this.Content = content;
            this.graphics = graphics;
            this.Position = Vector2.Zero;
        }

        public void LoadContent(string assetName)
        {
            Texture = Content.Load<Texture2D>(assetName);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Texture, Position, Color.White);
        }
    }
}
