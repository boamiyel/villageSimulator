﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace VillageBuilder
{
    public class Game1 : Game
    {
        public static GraphicsDeviceManager Graphics { get; private set; }
        public static  SpriteBatch SpriteBatch { get; private set; }
        public static ContentManager ContentManage { get; private set; }

        public static Vector2 CellRect { get; private set; }
        public static Resource[] Resources { get; private set; }
        public static bool OnBuildMode { get; private set; } = false;

        private const int CellsInWidth = 20;
        private const int CellsInHeight = 16;
        public const float ShareInWidthUI = 0.25f;

        private SpriteFont _font;
        private Cell[,] _cells; // width, height
        private UI _ui;

        public Game1()
        {
            Graphics = new GraphicsDeviceManager(this);
            Graphics.PreferredBackBufferWidth = 1920;
            Graphics.PreferredBackBufferHeight = 1080;
            Graphics.ToggleFullScreen();
            Content.RootDirectory = "Content";
            IsMouseVisible = true;

            ContentManage = Content;
            
            CellRect = new Vector2(
                Graphics.PreferredBackBufferWidth * (1 - ShareInWidthUI) / CellsInWidth,
                Graphics.PreferredBackBufferHeight / CellsInHeight);
        }

        protected override void LoadContent()
        {
            SpriteBatch = new SpriteBatch(GraphicsDevice);
            _font = Game1.ContentManage.Load<SpriteFont>(@"Fonts/VlaShu");

            _cells = new Cell[CellsInWidth, CellsInHeight];
            for (int i = 0; i < _cells.GetLength(0); i++)
            {
                for (int j = 0; j < _cells.GetLength(1); j++)
                {
                    _cells[i, j] = new Cell(CellRect, new Vector2(i * CellRect.X, j * CellRect.Y));
                    _cells[i, j].Initialize();
                }
            }

            var widthRes = Game1.Graphics.PreferredBackBufferWidth * (1 - ShareInWidthUI);

            var woodTexture = Game1.ContentManage.Load<Texture2D>(@"Sprites/wood");
            var ironTexture = Game1.ContentManage.Load<Texture2D>(@"Sprites/iron");
            var stoneTexture = Game1.ContentManage.Load<Texture2D>(@"Sprites/stone");

            Resources = new[]
            {
                new Resource(ResourceType.Wood, 20, woodTexture, _font),
                new Resource(ResourceType.Stone, 20, stoneTexture, _font),
                new Resource(ResourceType.Iron, 0, ironTexture, _font)
            };

            _ui = new UI(new Rectangle(new Point((int)widthRes, 0),
                new Point(
                    (int)(Game1.Graphics.PreferredBackBufferWidth * ShareInWidthUI),
                    Game1.Graphics.PreferredBackBufferHeight)), Resources);

            _ui.Initialize();
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            _ui.Update(gameTime);


            for (int i = 0; i < _cells.GetLength(0); i++)
            {
                for (int j = 0; j < _cells.GetLength(1); j++)
                {
                    _cells[i, j].Update(gameTime);
                }
            }

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            Game1.Graphics.GraphicsDevice.Clear(Color.ForestGreen);

            SpriteBatch.Begin();

            for (int i = 0; i < _cells.GetLength(0); i++)
            {
                for (int j = 0; j < _cells.GetLength(1); j++)
                {
                    _cells[i, j].Draw(SpriteBatch);
                }
            }

            _ui.Draw(SpriteBatch);

            SpriteBatch.End();

            base.Draw(gameTime);
        }

        public static void SwitchBuildMode()
        {
            OnBuildMode = !OnBuildMode;
        }
    }
}