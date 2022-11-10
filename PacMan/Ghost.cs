using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PacMan
{
    internal class Ghost : Actor
    {
        public Ghost(Texture2D texture, Vector2 position) : base(texture, position)
        {
            sourceRectangle = new Rectangle(0, Tile.tileSize * 2, Tile.tileSize, Tile.tileSize);
        }

        public void UpdateGhost(GameTime gameTime)
        {
            if (!moving)
            {
                Vector2 randomDirection = GetRandomDirection();

                // Check if there are available paths in all directions, but don't go back to where you came from
                if (randomDirection != direction*-1)
                {
                    SetDestinationIfValidDirection(randomDirection);
                }
            }
            else
            {
                Move(gameTime);
            }
        }
    }
}
