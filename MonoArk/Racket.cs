using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace MonoArk
{
    class Racket : GameEssence, IMovable
    {
        public Racket(Texture2D texture, Vector2 position, Vector2 size) : base(texture, position, size) { }
       
        public float Move(float position, int direction)
        {
            position += direction;
            if (position < 0) position = 0;
            if (position > 1820) position = 1820;

            return position;
        }

        public void Move() 
        {
            throw new NotImplementedException();
        }
    }
}
