using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace MonoArk
{
    class Button
    {
        private Rectangle _rectangle;
        private Texture2D _texture2D;

        public Button(int x, int y, int width, int height, Texture2D texture)
        {
            _rectangle = new Rectangle(x, y, width, height);

            _texture2D = texture;
        }

        public void DrawButton(int mouseX, int mouseY, SpriteBatch spriteBatch)
        {
            if (_rectangle.Contains(mouseX, mouseY))
            {
                spriteBatch.Draw(_texture2D, _rectangle, Color.Blue);
            }
            else
            {
                spriteBatch.Draw(_texture2D, _rectangle, Color.White);
            }
        }

        public bool ContainsButton(int mouseX, int mouseY)
        {
            return _rectangle.Contains(mouseX, mouseY);
        }
    }
}
