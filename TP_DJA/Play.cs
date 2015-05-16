using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TP_DJA
{
    class Play
    {
        Texture2D texture;
        Vector2 position; public Vector2 size;
        Rectangle rectangle;
        public bool isClicked;
        bool down;
        Color colour = new Color(255, 255, 255, 255);

        public Play(Texture2D newtexture, GraphicsDevice graphics)
        {
            texture = newtexture;
            size = new Vector2(graphics.Viewport.Width / 8, graphics.Viewport.Height / 30);
        }

        public void Update(MouseState mouse)
        {
            rectangle = new Rectangle((int)position.X, (int)position.Y, (int)size.X, (int)size.Y);
            Rectangle mouseRectangle = new Rectangle(mouse.X, mouse.Y, 1, 1);

            if (mouseRectangle.Intersects(rectangle))
            {
                if (colour.A == 255) down = false;
                if (colour.A == 100) down = true;
                if (down)
                {
                    colour.A += 1;

                }
                else colour.A -= 1;
                if (mouse.LeftButton == ButtonState.Pressed)
                {
                    isClicked = true;
                }
            }
            else if (colour.A < 255)
            {
                colour.A += 1;
                isClicked = false;
            }
        }

        public void setPosition(Vector2 newPosition)
        {
            position = newPosition;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, rectangle, colour);
        }
    }
}
