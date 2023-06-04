using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace VillageBuilder
{
    public static class Store
    {
        public static BuildingToBuy ChoosenBuilding { get; set; }

        private static BuildingToBuy[] _buildingsToBuy;

        public static void Initialize(Rectangle newRect)
        {
            var rects = new Rectangle[9];

            for (int i = 0; i < rects.Length; i++)
            {
                rects[i] = new Rectangle(newRect.Location, new Point(newRect.Size.X / 3, newRect.Size.Y / 3));
                newRect = new Rectangle(
                    new Point(newRect.Location.X, newRect.Location.Y + newRect.Size.Y / 3),
                    newRect.Size);

                if ((rects.Length - 1) / 3 == i || (rects.Length - 1) * 2 / 3 == i)
                {
                    newRect.Location += new Point(newRect.Size.X / 3, -newRect.Size.Y);
                }
            }

            var forest = new Forest(
                    Game1.ContentManage.Load<Texture2D>(@"Sprites/Tiles/tileForest"), rects[0]);

            var mine = new Mine(
                    Game1.ContentManage.Load<Texture2D>(@"Sprites/Tiles/tileMine"), rects[1]);

            var ironMine = new IronMining(
                    Game1.ContentManage.Load<Texture2D>(@"Sprites/Tiles/tileIronMine"), rects[2]);

            _buildingsToBuy = new[]
            {
                new BuildingToBuy(
                    Game1.ContentManage.Load<Texture2D>(@"Sprites/Tiles/tileForestBuy"),
                    rects[0], forest),

                new BuildingToBuy(
                    Game1.ContentManage.Load<Texture2D>(@"Sprites/Tiles/tileMineBuy"),
                    rects[1], mine),

                new BuildingToBuy(
                    Game1.ContentManage.Load<Texture2D>(@"Sprites/Tiles/tileIronMineBuy"),
                    rects[2], ironMine)
            };

            foreach (var obj in _buildingsToBuy)
            {
                obj.Initialize();
            }
        }

        public static void Update(GameTime gameTime)
        {
            if (Game1.OnBuildMode)
            {
                foreach (var obj in _buildingsToBuy)
                {
                    obj.Update(gameTime);
                }
            }
        }

        public static void Draw(SpriteBatch spriteBatch)
        {
            if (Game1.OnBuildMode)
            {
                foreach (var obj in _buildingsToBuy)
                {
                    obj.Draw(spriteBatch);
                }
            }
        }
    }
}
