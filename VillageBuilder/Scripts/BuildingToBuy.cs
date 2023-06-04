using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;

namespace VillageBuilder
{
    public class BuildingToBuy
    {
        public Building Building { get; set; }

        private Button _buyButton;
        private Texture2D _textureInShop;
        private Rectangle _positionInShop;

        public BuildingToBuy(Texture2D textureInShop, Rectangle positionInShop,
            Building building)
        {
            _textureInShop = textureInShop;
            _positionInShop = positionInShop;
            Building = building;
        }

        public void Initialize()
        {
            _buyButton = new Button(_textureInShop, _positionInShop,
                _ =>
                {
                    if (Game1.Resources.All(
                        x => !Building.Cost.ContainsKey(x.Type) 
                            || Building.Cost.ContainsKey(x.Type) && Building.Cost[x.Type] <= x.Count))
                    {
                        Store.ChoosenBuilding = this;
                    }
                    else
                    {
                        Store.ChoosenBuilding = null;
                    }
                });
        }

        public void Update(GameTime gameTime)
        {
            _buyButton.Update(gameTime);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            _buyButton.Draw(spriteBatch);
        }
    }
}
