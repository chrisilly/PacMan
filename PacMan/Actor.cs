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

        public Actor(Texture2D texture, Vector2 position)
        {
            this.texture = texture;
            this.position = position;
        }
    }
}
