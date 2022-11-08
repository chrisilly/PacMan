using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PacMan
{
    internal class Actor
    {
        Texture2D texture;
        Vector2 position;
        Vector2 velocity;
        Rectangle sourceRectangle;

        public Actor(Texture2D texture, Vector2 position, Rectangle sourceRectangle)
        {
            this.texture = texture;
            this.position = position;
            this.sourceRectangle = sourceRectangle;
        }

        public void Draw(SpriteBatch spriteBatch)
        {   
            spriteBatch.Draw(texture, position, sourceRectangle, Color.White);
        }

        public void SetSourceRectangleX(int frame)
        {
            sourceRectangle.X = frame;
        }
    }
}
