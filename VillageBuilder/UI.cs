using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace VillageBuilder
{
    public class UI
    {
        private Rectangle _rectUI;
        private Resource[] _resources;
        private Texture2D _whiteMask;
        private Clock _clock;
        private Button _openShop;

        private Store _store;

        private const float ResourceRect = 0.07f;

        private int Indent = (int)(Game1.Graphics.PreferredBackBufferHeight / 50);

        public UI(Rectangle rect, Resource[] resources)
        {
            _rectUI = rect;
            _resources = resources;
        }

        public void Initialize()
        {
            _whiteMask = Game1.ContentManage.Load<Texture2D>(@"Sprites\white_pixel");

            var rect = new Rectangle(_rectUI.Location + new Point(0, Indent), 
                new Point(_rectUI.Width, (int)(Game1.Graphics.PreferredBackBufferHeight * ResourceRect)));

            _clock = new Clock(rect, Game1.ContentManage.Load<SpriteFont>(@"Fonts/VlaShu"));

            foreach (var res in _resources)
            {
                rect.Location += new Point(0, Indent + rect.Size.Y);

                res.AddRect(rect);
            }

            var shopTexture = Game1.ContentManage.Load<Texture2D>(@"Sprites/shop");

            rect.Location += new Point(0, Indent + rect.Size.Y);
            rect.Size = new Point(rect.Width, rect.Width / 3);

            _openShop = new Button(shopTexture, rect, 
                _ =>
                {
                    Game1.OnBuildMode = !Game1.OnBuildMode;
                    Game1.Selected = null;
                });
            rect.Location += new Point(0, rect.Size.Y);
            rect.Size = new Point(
                Game1.Graphics.PreferredBackBufferWidth - rect.Location.X,
                Game1.Graphics.PreferredBackBufferHeight - rect.Location.Y);

            _store = new Store(rect);
            _store.Initialize();
        }

        public void Update(GameTime gameTime)
        {
            _clock.Update(gameTime);
            _openShop.Update(gameTime);

            _store.Update(gameTime);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_whiteMask, _rectUI, Color.White);

            _clock.Draw(spriteBatch);
            _openShop.Draw(spriteBatch);

            foreach (var res in _resources)
                res.Draw(spriteBatch);

            _store.Draw(spriteBatch);
        }
    }
}
