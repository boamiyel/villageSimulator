using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace VillageBuilder
{
    public class Button
    {
        public Rectangle ButtonCollider { get; set; }

        private Color _color;
        private bool _isPressed;
        private Texture2D _texture;
        private Action<object> click;
        private double _pressTime;

        private const double MaxSecondDelay = 0.3;

        public Button(Texture2D sprite, Rectangle buttonCollider, Action<object> onClick)
        {
            _texture = sprite;
            ButtonCollider = buttonCollider;
            click = onClick;

            _color = Color.White;
            _pressTime = -1;
        }

        public bool EnterButton()
        {
            return Mouse.GetState().X <= ButtonCollider.Location.X + ButtonCollider.Width
                && Mouse.GetState().Y <= ButtonCollider.Location.Y + ButtonCollider.Height
                && Mouse.GetState().Y >= ButtonCollider.Location.Y
                && Mouse.GetState().X >= ButtonCollider.Location.X;
        }

        public void Update(GameTime gameTime)
        {
            if (EnterButton() && Mouse.GetState().LeftButton == ButtonState.Pressed && !_isPressed)
            {
                _isPressed = true;
                _color = Color.Black;
                _pressTime = gameTime.TotalGameTime.TotalSeconds;

                click(null);
            }

            if(gameTime.TotalGameTime.TotalSeconds - _pressTime >= MaxSecondDelay)
            {
                _isPressed = false;
                _color = Color.White;
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_texture, ButtonCollider, _color);
        }
    }
}