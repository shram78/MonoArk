using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace MonoArk
{
    class Racket
    {
        private Rectangle rectangle;
        private Texture2D texture2D;

        public Racket(int x, int y, int width, int height, Texture2D texture)
        {
            rectangle = new Rectangle(x, y, width, height);

            texture2D = texture;
        }

        public void DrawRacket(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture2D, rectangle, Color.White);
        }
    }
}
