using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using SharpDX.Direct3D9;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PacMan
{
    internal class Player : Actor
    {
        public Player(Texture2D texture, Vector2 position) : base(texture, position)
        {
            sourceRectangle = new Rectangle(0, 0, Tile.tileSize, Tile.tileSize);
        }

        public void CheckInput(GameTime gameTime)
        {
            if (!moving)
            {
                if (Keyboard.GetState().IsKeyDown(Keys.Left))
                {
                    SetDestinationIfValidDirection(new Vector2(-1, 0));
                    ResetSpriteEffects();
                    spriteEffect = SpriteEffects.FlipHorizontally;
                }
                else if (Keyboard.GetState().IsKeyDown(Keys.Right))
                {
                    SetDestinationIfValidDirection(new Vector2(1, 0));
                    ResetSpriteEffects();
                }
                else if (Keyboard.GetState().IsKeyDown(Keys.Up))
                {
                    SetDestinationIfValidDirection(new Vector2(0, -1));
                    ResetSpriteEffects();
                    rotation = MathHelper.ToRadians(-90);
                }
                else if (Keyboard.GetState().IsKeyDown(Keys.Down))
                {
                    SetDestinationIfValidDirection(new Vector2(0, 1));
                    ResetSpriteEffects();
                    rotation = MathHelper.ToRadians(90);
                }
            }
            else
            {
                Move(gameTime);
            }
        }
    }
}
