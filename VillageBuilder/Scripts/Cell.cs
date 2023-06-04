using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VillageBuilder
{
    public class Cell
    {
        public Rectangle Rect { get; }
        public Building Building { get; set; }

        private Texture2D _buildTexture;
        private Texture2D _texture;

        public Cell(Vector2 size, Vector2 position)
        {
            Rect = new Rectangle(position.ToPoint(), size.ToPoint());
        }

        public Cell(Rectangle rect)
        {
            Rect = rect;
        }

        public void Initialize()
        {
            _texture = Game1.ContentManage.Load<Texture2D>(@"Sprites/Tile");
            _buildTexture = Game1.ContentManage.Load<Texture2D>(@"Sprites/TileForBuild");
        }

        public void Update(GameTime gameTime)
        {
            if (Building != null)
                Building.Update(gameTime);

            if (!Game1.OnBuildMode)
                return;
            var mouseState = Mouse.GetState();

            var mouseRect = new Rectangle(
                        new Point(mouseState.Position.X, mouseState.Position.Y),
                        new Point(1, 1));

            if (mouseState.X <= Game1.Graphics.PreferredBackBufferWidth * (1 - Game1.ShareInWidthUI)
                && Building == null && Store.ChoosenBuilding != null
                && mouseState.LeftButton == ButtonState.Pressed
                && Rect.Intersects(mouseRect)
                && Game1.Resources.All(
                    x => !Store.ChoosenBuilding.Building.Cost.ContainsKey(x.Type)
                        || Store.ChoosenBuilding.Building.Cost.ContainsKey(x.Type) && Store.ChoosenBuilding.Building.Cost[x.Type] <= x.Count))
            {
                foreach (var res in Game1.Resources)
                {
                    if (Store.ChoosenBuilding.Building.Cost.ContainsKey(res.Type))
                        res.Count -= Store.ChoosenBuilding.Building.Cost[res.Type];
                }

                Building = Store.ChoosenBuilding.Building.Clone();
                Building.ChangeRect(Rect);
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (Building is null)
            {
                if(Game1.OnBuildMode)
                    spriteBatch.Draw(_buildTexture, Rect, Color.White);
                else 
                    spriteBatch.Draw(_texture, Rect, Color.White);
            }
            else
            {
                Building.Draw(spriteBatch);
            }
        }
    }
}
