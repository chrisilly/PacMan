﻿using Microsoft.Xna.Framework;
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
        protected Random random = new Random();

        Texture2D texture;
        Rectangle hitbox;
        Vector2 startPosition;
        protected Vector2 position;
        protected Rectangle sourceRectangle;

        protected float rotation = 0;
        float scale = 1;
        Vector2 origin = new Vector2(Tile.tileSize/2, Tile.tileSize/2);

        protected Vector2 destination;
        protected Vector2 direction;
        protected float speed = 50;

        protected bool moving = false;

        protected SpriteEffects spriteEffect = SpriteEffects.None;

        public Actor(Texture2D texture, Vector2 position)
        {
            this.texture = texture;
            this.position = position + origin;
            this.hitbox = new Rectangle((int)position.X, (int)position.Y, Tile.tileSize, Tile.tileSize);
            this.startPosition = this.position;
        }

        public void Draw(SpriteBatch spriteBatch)
        {   
            spriteBatch.Draw(texture, position, sourceRectangle, Color.White, rotation, origin, scale, spriteEffect, 0);
        }

        public void DrawHitbox(SpriteBatch spriteBatch, Texture2D texture)
        {
            spriteBatch.Draw(texture, hitbox, Color.FromNonPremultiplied(155, 255, 61, 122));
        }

        public void AdvanceFrame(int currentFrame, int maxFrames)
        {
            sourceRectangle.X = (currentFrame % maxFrames) * Tile.tileSize;
        }

        // Move in specified direction if possible
        public void SetDestinationIfValidDirection(Vector2 direction)
        {
            Vector2 newDestination = position + direction * Tile.tileSize;

            if (Game1.IsTileValidDestination(newDestination))
            {
                this.direction = direction;
                destination = newDestination;
                moving = true;
            }
        }

        public void Move(GameTime gameTime)
        {
            position += direction * speed * (float)gameTime.ElapsedGameTime.TotalSeconds;
            hitbox = new Rectangle((int)position.X-Tile.tileSize/2, (int)position.Y-Tile.tileSize/2, Tile.tileSize, Tile.tileSize);

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

        public void ResetPosition()
        {
            hitbox = new Rectangle((int)startPosition.X, (int)startPosition.Y, Tile.tileSize, Tile.tileSize);
            moving = false;
            position = startPosition;
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

        public Rectangle GetHitbox()
        {
            return hitbox;
        }
    }
}
