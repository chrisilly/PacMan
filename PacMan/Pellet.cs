using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PacMan
{
    internal class Pellet : Actor
    {
        public Pellet(Texture2D texture, Vector2 position) : base(texture, position)
        {
            sourceRectangle = new Rectangle(Tile.tileSize * 3, 0, Tile.tileSize, Tile.tileSize);
        }
    }
}
