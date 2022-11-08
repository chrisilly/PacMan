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

        Tile[,] levelTiles;
        List<string> levelRowList;

        Player player;

        Texture2D tileTexture;
        Texture2D spriteSheet;

        int frame;
        double frameTimer, frameInterval;

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

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            spriteBatch.Begin();

            DrawTiles();
            player.Draw(spriteBatch);

            spriteBatch.End();

            base.Draw(gameTime);
        }

        public void UpdateFrameTimer(GameTime gameTime)
        {
            frameTimer -= gameTime.ElapsedGameTime.TotalMilliseconds;
            if (frameTimer <= 0)
            {
                frameTimer = frameInterval; frame++;
                player.SetSourceRectangleX((frame % 3) * Tile.tileSize);
            }
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
                    else if (levelRowList[i][j] == '0')
                    {
                        levelTiles[j, i] = new Tile(tileTexture, tilePosition, tileColor, true);
                    }
                    else
                    {
                        levelTiles[j, i] = new Tile(tileTexture, tilePosition, tileColor);
                    }

                    if (levelRowList[i][j] == '-')
                    {
                        // add new food objects
                    }
                    else if (levelRowList[i][j] == '+')
                    {
                        // add power-up food
                    }
                    else if (levelRowList[i][j] == 'P')
                    {
                        player = new Player(spriteSheet, tilePosition, new Rectangle(0, 0, Tile.tileSize, Tile.tileSize));
                    }
                    else if (levelRowList[i][j] == '1')
                    {
                        // add ghost object 1
                    }
                    else if (levelRowList[i][j] == '2')
                    {
                        // add ghost object 2
                    }
                    else if (levelRowList[i][j] == '3')
                    {
                        // add ghost object 3
                    }
                    else if (levelRowList[i][j] == '4')
                    {
                        // add ghost object 4
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