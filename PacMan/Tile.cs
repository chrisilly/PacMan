using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PacMan
{
    internal class Tile
    {
        public static int tileSize{get;} = 16;

        Texture2D texture;
        Vector2 position;
        Color color;
        bool solid;

        public Tile(Texture2D texture, Vector2 position, Color color, bool solid)
        {
            this.texture = texture;
            this.position = position;
            this.color = color;
            this.solid = solid;
        }
        
        // short-hand constructor for non-solid tiles
        public Tile(Texture2D texture, Vector2 position, Color color)
        {
            this.texture = texture;
            this.position = position;
            this.color = color;
            this.solid = false;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, position, color);
        }
    }
}
