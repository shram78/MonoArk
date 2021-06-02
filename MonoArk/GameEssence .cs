using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace MonoArk
{
    abstract class GameEssence 
    {
        public Texture2D Texture;
        public Vector2 Position;
        public Vector2 Size;

        public GameEssence(Texture2D texture, Vector2 position, Vector2 size)
        {
            Texture = texture;
            Position = position;
            Size = size;
        }
    }
}