using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.IO;

namespace PacMan
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;

        static GameState gameState { get; set; } = GameState.Play;

        static Tile[,] levelTiles;
        List<string> levelRowList;

        Player player;
        List<Ghost> ghostList = new List<Ghost>();
        List<Pellet> pelletList = new List<Pellet>();

        Texture2D tileTexture;
        Texture2D spriteSheet;

        int frame;
        double frameTimer, frameInterval;

        int lives;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            graphics.PreferredBackBufferWidth = 1080;
            graphics.PreferredBackBufferHeight = 1080;
            graphics.ApplyChanges();

            frameInterval = 100;
            frameTimer = frameInterval;

            lives = 3;

            ReadLevel();

            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);

            tileTexture = Content.Load<Texture2D>("tile16");
            spriteSheet = Content.Load<Texture2D>("SpriteSheet");

            LoadLevel();
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            UpdateFrameTimer(gameTime);

            switch (gameState)
            {
                case GameState.Play:

                    //Update Actors
                    player.CheckInput(gameTime);
                    foreach (Ghost ghost in ghostList)
                        ghost.UpdateGhost(gameTime);

                    CheckForCollision();

                    CheckGameState();

                    break;
                case GameState.Win:
                    break;
                case GameState.Lose:
                    break;
                default:
                    break;
            }

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            spriteBatch.Begin();

            DrawTiles();
            player.Draw(spriteBatch);
            player.DrawHitbox(spriteBatch, tileTexture);
            foreach (Ghost ghost in ghostList)
            {
                ghost.Draw(spriteBatch);
                ghost.DrawHitbox(spriteBatch, tileTexture);
            }
            foreach (Pellet pellet in pelletList)
            {
                pellet.Draw(spriteBatch);
            }

            spriteBatch.End();

            base.Draw(gameTime);
        }

        private void CheckGameState()
        {
            if (pelletList.Count <= 0)
                gameState = GameState.Win;
            if (lives <= 0)
                gameState = GameState.Lose;
        }

        public void UpdateFrameTimer(GameTime gameTime)
        {
            frameTimer -= gameTime.ElapsedGameTime.TotalMilliseconds;
            if (frameTimer <= 0)
            {
                frameTimer = frameInterval; frame++;
                player.AdvanceFrame(frame, 3);
                foreach (Ghost ghost in ghostList)
                    ghost.AdvanceFrame(frame, 8);
            }
        }

        public void CheckForCollision()
        {
            for (int i = 0; i < ghostList.Count; i++)
            {
                if (player.GetHitbox().Intersects(ghostList[i].GetHitbox()))
                {
                    player.ResetPosition();
                    lives--;
                }
            }

            for (int i = 0; i < pelletList.Count; i++)
            {
                if (player.GetHitbox().Intersects(pelletList[i].GetHitbox()))
                {
                    pelletList.RemoveAt(i);
                    break;
                }
            }
        }

        // Check if destination is a non-solid tile
        public static bool IsTileValidDestination(Vector2 destination)
        {
            return !levelTiles[(int)destination.X / Tile.tileSize, (int)destination.Y / Tile.tileSize].IsSolid();
        }

        private void ReadLevel()
        {
            StreamReader levelFile = new StreamReader(@"..\..\..\Content\level.txt");
            levelRowList = new List<string>();

            while (!levelFile.EndOfStream)
            {
                levelRowList.Add(levelFile.ReadLine());
            }
            levelFile.Close();
        }

        private void LoadLevel()
        {
            levelTiles = new Tile[levelRowList[0].Length, levelRowList.Count];

            for (int i = 0; i < levelRowList.Count; i++)
            {
                for (int j = 0; j < levelRowList[0].Length; j++)
                {
                    Color tileColor = Color.Black;
                    Vector2 tilePosition = new Vector2(Tile.tileSize * j, Tile.tileSize * i);

                    if (levelRowList[i][j] == 'S')
                    {
                        tileColor = Color.Blue;
                        levelTiles[j, i] = new Tile(tileTexture, tilePosition, tileColor, true);
                    }
                    else
                    {
                        levelTiles[j, i] = new Tile(tileTexture, tilePosition, tileColor);
                    }

                    if (levelRowList[i][j] == '-')
                    {
                        Pellet pellet = new Pellet(spriteSheet, tilePosition);
                        pelletList.Add(pellet);
                    }
                    else if (levelRowList[i][j] == '+')
                    {
                        // add power-up food
                    }
                    else if (levelRowList[i][j] == 'P')
                    {
                        player = new Player(spriteSheet, tilePosition);
                    }
                    else if (levelRowList[i][j] == 'G')
                    {
                        Ghost ghost = new Ghost(spriteSheet, tilePosition);
                        ghostList.Add(ghost);
                    }
                }
            }
        }

        private void DrawTiles()
        {
            for (int i = 0; i < levelRowList.Count; i++)
            {
                for (int j = 0; j < levelRowList[0].Length; j++)
                {
                    levelTiles[j, i].Draw(spriteBatch);
                }
            }
        }
    }
}