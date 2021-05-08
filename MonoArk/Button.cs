using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;


namespace MonoArk
{
    class Button
    {
        private Rectangle rectangle;
        private Texture2D texture2D;

        public Button(int x, int y, int width, int height, Texture2D texture)
        {
            rectangle = new Rectangle(x, y, width, height);

            texture2D = texture;
        }

        public void DrawButton(int mouseX, int mouseY, SpriteBatch spriteBatch)
        {
            if (rectangle.Contains(mouseX, mouseY))
            {
                spriteBatch.Draw(texture2D, rectangle, Color.Blue);
            }
            else
            {
                spriteBatch.Draw(texture2D, rectangle, Color.White);
            }
        }

        public bool ContainsButton(int mouseX, int mouseY)
        {
            return rectangle.Contains(mouseX, mouseY);
        }
    }
}
