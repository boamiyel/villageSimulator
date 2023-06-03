using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace VillageBuilder
{
    public class Building 
    {
        public Texture2D Texture;
        public Rectangle Rect;
        public Dictionary<ResourceType, int> Gives;

        private int _timeToStartWork = 6;
        private int _timeToEndWork = 22;

        private int _lastTimeUpd = -1;

        public Building(Texture2D texture, Rectangle rect, Dictionary<ResourceType, int> gives)
        {
            Rect = rect;
            Texture = texture;
            Gives = gives;
        }

        public void Update(GameTime gameTime)
        {
            if (Clock.Time >= _timeToStartWork && Clock.Time <= _timeToEndWork && _lastTimeUpd != Clock.Time)
            {
                foreach (var res in Game1.Resources)
                {
                    if (Gives.ContainsKey(res.Type))
                        res.Count += Gives[res.Type];
                }

                _lastTimeUpd = Clock.Time;
            }

        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Texture, Rect, Color.White);
        }

        public void ChangeRect(Rectangle rect)
        {
            Rect = rect;
        }
    }
}
