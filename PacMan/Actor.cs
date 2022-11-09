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
        protected Vector2 position;
        protected Rectangle sourceRectangle;

        protected float rotation = 0;
        float scale = 1;
        Vector2 origin = new Vector2(Tile.tileSize/2, Tile.tileSize/2);

        protected Vector2 destination;
        protected Vector2 direction;
        protected float speed = 100;

        protected bool moving = false;

        protected SpriteEffects spriteEffect = SpriteEffects.None;

        public Actor(Texture2D texture, Vector2 position)
        {
            this.texture = texture;
            this.position = position + origin;
        }

        public void Draw(SpriteBatch spriteBatch)
        {   
            spriteBatch.Draw(texture, position, sourceRectangle, Color.White, rotation, origin, scale, spriteEffect, 0);
        }

        public void AdvanceFrame(int currentFrame, int maxFrames)
        {
            sourceRectangle.X = (currentFrame % maxFrames) * Tile.tileSize;
        }

        public void CheckDirection(Vector2 direction)
        {
            this.direction = direction;
            Vector2 newDestination = position + direction * Tile.tileSize;

            if (Game1.IsTileValidDestination(newDestination))
            {
                destination = newDestination;
                moving = true;
            }
        }

        public void Move(GameTime gameTime)
        {
            position += direction * speed * (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (Vector2.Distance(position, destination) < 1)
            {
                position = destination;
                moving = false;
            }
        }

        protected void ResetSpriteEffects()
        {
            rotation = 0;
            spriteEffect = SpriteEffects.None;
        }
    }
}
