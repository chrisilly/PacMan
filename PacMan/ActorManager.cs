using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PacMan
{
    internal class ActorManager
    {
        public Player player { get; set; }
        public int playerLives { get; private set; } = 3;

        public List<Ghost> ghostList { get; } = new List<Ghost>();
        public List<Pellet> pelletList { get; } = new List<Pellet>();

        public void Update(GameTime gameTime)
        {
            //Update Actors
            player.CheckInput(gameTime);
            foreach (Ghost ghost in ghostList)
                ghost.UpdateGhost(gameTime);

            CheckForCollision();
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            player.Draw(spriteBatch);
            //player.DrawHitbox(spriteBatch, tileTexture);
            foreach (Ghost ghost in ghostList)
            {
                ghost.Draw(spriteBatch);
                //ghost.DrawHitbox(spriteBatch, tileTexture);
            }
            foreach (Pellet pellet in pelletList)
            {
                pellet.Draw(spriteBatch);
            }
        }

        public void CheckForCollision()
        {
            // Get hit by Ghost
            for (int i = 0; i < ghostList.Count; i++)
            {
                if (player.GetHitbox().Intersects(ghostList[i].GetHitbox()))
                {
                    player.ResetPosition();
                    playerLives--;
                }
            }

            // Consume pellet
            for (int i = 0; i < pelletList.Count; i++)
            {
                if (player.GetHitbox().Intersects(pelletList[i].GetHitbox()))
                {
                    pelletList.RemoveAt(i);
                    break;
                }
            }
        }
    }
}
