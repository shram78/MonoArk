using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace MonoArk
{
    class Racket
    {
        private Texture2D texture2D;

        public Racket(Texture2D texture)
        {
            texture2D = texture;
        }

        public void DrawRacket(SpriteBatch spriteBatch, Vector2 _position)
        {
            spriteBatch.Draw(texture2D, _position, Color.White);
        }

        public float MoveRocket(float _position, int direction)
        {
            _position += direction;
            if (_position < 0) _position = 0;
            if (_position > 1820) _position = 1820;

            return _position;
        }
    }
}
