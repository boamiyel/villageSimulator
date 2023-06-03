using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

namespace VillageBuilder
{
    public class Store
    {

        private Rectangle _rect;
        private ObjectToBuy[] objs;

        public Store(Rectangle rect)
        {
            _rect = rect;
        }

        public void Initialize()
        {
            var rects = new Rectangle[9];
            var newRect = _rect;

            for (int i = 0; i < rects.Length; i++)
            {
                rects[i] = new Rectangle(newRect.Location, new Point(newRect.Size.X / 3, newRect.Size.X / 3));
                newRect = new Rectangle(
                    new Point(newRect.Location.X, newRect.Location.Y + newRect.Size.X / 3),
                    newRect.Size);

                if ((rects.Length - 1) / 3 == i || (rects.Length - 1) * 2 / 3 == i)
                {
                    newRect.Location += new Point(newRect.Size.X / 3, -newRect.Size.X / 3 * 3);
                }
            }

            objs = new[]
            {
                new ObjectToBuy(
                    Game1.ContentManage.Load<Texture2D>(@"Sprites/Tiles/tileForestBuy"),
                    Game1.ContentManage.Load<Texture2D>(@"Sprites/Tiles/tileForest"),
                    new Dictionary<ResourceType, int> { [ResourceType.Wood] = 20 },
                    new Dictionary<ResourceType, int> { [ResourceType.Wood] = 5 },
                    rects[0]),

                new ObjectToBuy(
                    Game1.ContentManage.Load<Texture2D>(@"Sprites/Tiles/tileMineBuy"),
                    Game1.ContentManage.Load<Texture2D>(@"Sprites/Tiles/tileMine"),
                    new Dictionary<ResourceType, int> { [ResourceType.Wood] = 100, [ResourceType.Stone] = 20 },
                    new Dictionary<ResourceType, int> { [ResourceType.Stone] = 5 },
                    rects[1])
            };

            foreach (var obj in objs)
            {
                obj.Initialize();
            }
        }

        public void Update(GameTime gameTime)
        {
            if (Game1.OnBuildMode)
            {
                foreach (var obj in objs)
                {
                    obj.Update(gameTime);
                }
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (Game1.OnBuildMode)
            {
                foreach (var obj in objs)
                {
                    obj.Draw(spriteBatch);
                }
            }
        }
    }
}
