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
        public Rectangle Rect;
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

            if (mouseState.X <= Game1.Graphics.PreferredBackBufferWidth * (1 - Game1.ShareInWidthUI)
                && Building == null && Game1.obj != null
                && mouseState.LeftButton == ButtonState.Pressed
                && Rect.Intersects(
                    new Rectangle(
                    new Point(
                        (int)((mouseState.Position.X)),
                        (int)((mouseState.Position.Y))),
                    new Point(0, 0)))
                && Game1.Resources.All(
                    x => !Game1.obj._costs.ContainsKey(x.Type)
                        || Game1.obj._costs.ContainsKey(x.Type) && Game1.obj._costs[x.Type] <= x.Count))
            {
                foreach (var res in Game1.Resources)
                {
                    if (Game1.obj._costs.ContainsKey(res.Type))
                        res.Count -= Game1.obj._costs[res.Type];
                }

                Building = new Building(Game1.obj.Building.Texture, Rect, Game1.obj.Building.Gives);
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
