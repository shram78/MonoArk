using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace GameClasses
{
    class GameObject
    {
        protected Texture2D Texture;
        protected Vector2 Position;
        protected bool Live = true;

        public GameObject(Texture2D texture, Vector2 position)
        {
            Texture = texture;
            Position = position;
        }

        public bool GetLive()
        {
            return Live;
        }

        public void draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Texture, Position, Color.White);
        }

        public Vector2 get_position()
        {
            return Position;
        }

        public Texture2D get_texture()
        {
            return Texture;
        }
    }

    interface Moveable
    {
        void move(KeyboardState keyboard, Rectangle _vewportRectangle);
    }

    class Brick : GameObject
    {
        public Brick(Texture2D texture, Vector2 position, int speed) : base(texture, position) { }

        public void kill()
        {
            Live = false;
        }

        public bool is_alive()
        {
            return Live;
        }
    }

    class Ball : GameObject
    {
        private Vector2 speed;
        public Ball(Texture2D texture, Vector2 position, Vector2 speed) : base(texture, position)
        {
            this.speed = speed;
        }

        public void move()
        {
            Position.X += speed.X;
            Position.Y += -speed.Y;
        }

        public void changeDirection_X()
        {
            speed.X = -speed.X;
        }

        public void changeDirection_Y()
        {
            speed.Y = -speed.Y;
        }

        public void check_wall_collision(Rectangle _viewport_rectangle)
        {
            if ((Position.X + Texture.Width) > _viewport_rectangle.Width || Position.X < 0)
            {
                changeDirection_X();
            }
            else if (Position.Y <= 0)
            {
                changeDirection_Y();

            }
            else if (Position.Y > _viewport_rectangle.Height)
            {
                Live = false;
            }
        }

        public void check_racket_collision(GameObject gameObject)
        {
            if ((Position.Y + Texture.Height) > gameObject.get_position().Y &&
                (Position.X + Texture.Width) > gameObject.get_position().X &&
                 Position.X < (gameObject.get_position().X + gameObject.get_texture().Width))
            {
                changeDirection_Y();
            }
        }

        public bool check_bricks_collision(GameObject gameObject)
        {
            if (Position.Y < (gameObject.get_position().Y + gameObject.get_texture().Height) &&
               (Position.Y + Texture.Height) > gameObject.get_position().Y &&
               (Position.X + Texture.Width) > gameObject.get_position().X &&
                Position.X < (gameObject.get_position().X + gameObject.get_texture().Width))
            {
                return true;
            }
            return false;
        }
    }

    class Racket : GameObject, Moveable
    {
        private int _speed;
        public Racket(Texture2D texture, Vector2 position, int speed) : base(texture, position)
        {
            _speed = speed;
        }

        public void move(KeyboardState keyboard, Rectangle _vewportRectangle)
        {
            if (keyboard.IsKeyDown(Keys.A))
            {
                Position.X -= _speed;
            }
            if (keyboard.IsKeyDown(Keys.D))
            {
                Position.X += _speed;
            }
            Position.X = MathHelper.Clamp(Position.X, 0, _vewportRectangle.Width - Texture.Width);
        }
    }
}
