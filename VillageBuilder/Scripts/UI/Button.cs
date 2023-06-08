using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace VillageBuilder
{
    public class Button
    {
        private Rectangle _clickableButtonCollider;
        private Rectangle _drawButtonCollider;
        private Color _color;
        private bool _isPressed;
        private double _pressTime;
        private bool _haveClick = false;
        
        private readonly Texture2D _texture;
        private readonly Action<object> _click;

        private const double MaxSecondDelay = 0.2;

        public Button(Texture2D sprite, Rectangle buttonCollider, Action<object> onClick)
        {
            _texture = sprite;
            _clickableButtonCollider = buttonCollider;
            _drawButtonCollider = buttonCollider;
            _click = onClick;

            _color = Color.White;
            _pressTime = -1;
        }

        public bool EnterButton()
        {
            return Mouse.GetState().X <= _clickableButtonCollider.Location.X + _clickableButtonCollider.Width
                && Mouse.GetState().Y <= _clickableButtonCollider.Location.Y + _clickableButtonCollider.Height
                && Mouse.GetState().Y >= _clickableButtonCollider.Location.Y
                && Mouse.GetState().X >= _clickableButtonCollider.Location.X;
        }

        public void Update(GameTime gameTime)
        {
            if (EnterButton() && Mouse.GetState().LeftButton == ButtonState.Pressed)
            {
                _isPressed = true;
            }
            else if (_isPressed && !_haveClick 
                && Mouse.GetState().LeftButton == ButtonState.Released && EnterButton())
            {
                _color = Color.Gray;
                _pressTime = gameTime.TotalGameTime.TotalSeconds;
                _haveClick = true;
                _click(null);
            }
            else if (_isPressed && !EnterButton())
            {
                _pressTime = gameTime.TotalGameTime.TotalSeconds;
                _isPressed = false;
            }
            else if (_haveClick && gameTime.TotalGameTime.TotalSeconds - _pressTime >= MaxSecondDelay)
            {
                _isPressed = false;
                _haveClick = false;
                _color = Color.White;
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_texture, _drawButtonCollider, _color);
        }

        public void UpdateCollider(Rectangle newCollider)
        {
            _drawButtonCollider = newCollider;
            _clickableButtonCollider = newCollider;
        }

        public void UpdateClickableCollider(Rectangle newCollider)
            => _clickableButtonCollider = newCollider;
    }
}