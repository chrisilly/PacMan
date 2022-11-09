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

        public Vector2 GetRandomDirection()
        {
            int number = random.Next(0, 4);
            Vector2 randomDirection;
            
            if (number == 0)
                randomDirection = new Vector2(-1, 0);
            else if (number == 1)
                randomDirection = new Vector2(1, 0);
            else if (number == 2)
                randomDirection = new Vector2(0, -1);
            else if (number == 3)
                randomDirection = new Vector2(0, 1);
            else
                randomDirection = Vector2.Zero;

            return randomDirection;
        }
    }
}
