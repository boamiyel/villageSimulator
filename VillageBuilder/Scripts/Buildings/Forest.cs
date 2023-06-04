using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VillageBuilder
{
    public class Forest : Building
    {
        public override Dictionary<ResourceType, int> Cost { get; } 
            = new Dictionary<ResourceType, int> { [ResourceType.Wood] = 20 };

        public override Dictionary<ResourceType, int> Gives { get; } 
            = new Dictionary<ResourceType, int> { [ResourceType.Wood] = 5 };

        public Forest(Texture2D texture, Rectangle rect) : base(texture, rect) { }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }

        public override Building Clone() => new Forest(Texture, Rect);
    }
}
