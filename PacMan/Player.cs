using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PacMan
{
    internal class Player : Actor
    {
        int health;

        public Player(Texture2D texture, Vector2 position, Rectangle sourceRectangle) : base(texture, position, sourceRectangle)
        {
            
        }
    }
}
