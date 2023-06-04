using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

namespace VillageBuilder
{
    public abstract class Building
    {
        public Texture2D Texture { get; }
        public Rectangle Rect { get; private set; }
        public abstract Dictionary<ResourceType, int> Gives { get; }
        public abstract Dictionary<ResourceType, int> Cost { get; }

        private int _timeToStartWork = 6;
        private int _timeToEndWork = 22;
        private int _lastTimeUpd = -1;

        public Building(Texture2D texture, Rectangle rect)
        {
            Rect = rect;
            Texture = texture;
        }

        public virtual void Update(GameTime gameTime)
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

        public abstract Building Clone();
    }
}
