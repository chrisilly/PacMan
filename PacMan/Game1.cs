using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.IO;

namespace PacMan
{
    public enum GameState { Play, Win, Lose }

    public class Game1 : Game
    {
        private GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;

        static GameState gameState { get; set; } = GameState.Play;

        static Tile[,] levelTiles;
        List<string> levelRowList;

        Texture2D tileTexture;
        Texture2D spriteSheet;

        int frame;
        double frameTimer, frameInterval;

        ActorManager actorManager = new ActorManager();

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
                    actorManager.Update(gameTime);
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
            actorManager.Draw(spriteBatch);

            spriteBatch.End();

            base.Draw(gameTime);
        }

        // Animate sprites
        public void UpdateFrameTimer(GameTime gameTime)
        {
            frameTimer -= gameTime.ElapsedGameTime.TotalMilliseconds;
            if (frameTimer <= 0)
            {
                frameTimer = frameInterval; frame++;
                actorManager.player.AdvanceFrame(frame, 3);
                foreach (Ghost ghost in actorManager.ghostList)
                    ghost.AdvanceFrame(frame, 8);
            }
        }

        public void CheckGameState()
        {
            if (actorManager.pelletList.Count <= 0)
                gameState = GameState.Win;
            if (actorManager.playerLives <= 0)
                gameState = GameState.Lose;
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
                        actorManager.pelletList.Add(pellet);
                    }
                    else if (levelRowList[i][j] == 'P')
                    {
                        actorManager.player = new Player(spriteSheet, tilePosition);
                    }
                    else if (levelRowList[i][j] == 'G')
                    {
                        Ghost ghost = new Ghost(spriteSheet, tilePosition);
                        actorManager.ghostList.Add(ghost);
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