using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace GameClasses
{
    class GameObject
    {
        protected Texture2D texture;
        protected Vector2 position;
        protected bool live = true;

        public GameObject(Texture2D _texture, Vector2 _position)
        {
            texture = _texture;
            position = _position;
        }

        public bool GetLive()
        {
            return live;
        }

        public void draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, position, Color.White);
        }

        public Vector2 get_position()
        {
            return position;
        }

        public Texture2D get_texture()
        {
            return texture;
        }
    }

    interface Moveable
    {
        void move(KeyboardState keyboard, Rectangle _vewportRectangle);
    }

    class Brick : GameObject
    {
        public Brick(Texture2D _texture, Vector2 _position, int _speed) : base(_texture, _position) { }

        public void kill()
        {
            live = false;
        }

        public bool is_alive()
        {
            return live;
        }
    }

    class Ball : GameObject
    {
        private Vector2 speed;
        public Ball(Texture2D _texture, Vector2 _position, Vector2 _speed) : base(_texture, _position)
        {
            speed = _speed;
        }

        public void move()
        {
            position.X += speed.X;
            position.Y += -speed.Y;
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
            if ((position.X + texture.Width) > _viewport_rectangle.Width || position.X < 0)
            {
                changeDirection_X();
            }
            else if (position.Y <= 0)
            {
                changeDirection_Y();

            }
            else if (position.Y > _viewport_rectangle.Height)
            {
                live = false;
            }

        }
        public void check_racket_collision(GameObject gameObject)
        {
            if ((position.Y + texture.Height) > gameObject.get_position().Y &&
                (position.X + texture.Width) > gameObject.get_position().X &&
                 position.X < (gameObject.get_position().X + gameObject.get_texture().Width))
            {
                changeDirection_Y();
            }
        }
        public bool check_bricks_collision(GameObject gameObject)
        {
            if (position.Y < (gameObject.get_position().Y + gameObject.get_texture().Height) &&
               (position.Y + texture.Height) > gameObject.get_position().Y &&
               (position.X + texture.Width) > gameObject.get_position().X &&
                position.X < (gameObject.get_position().X + gameObject.get_texture().Width))
            {
                return true;
            }
            return false;
        }
    }

    class Racket : GameObject, Moveable
    {
        private int speed;
        public Racket(Texture2D _texture, Vector2 _position, int _speed) : base(_texture, _position)
        {
            speed = _speed;
        }

        public void move(KeyboardState keyboard, Rectangle _vewportRectangle)
        {
            if (keyboard.IsKeyDown(Keys.A))
            {
                position.X -= speed;
            }
            if (keyboard.IsKeyDown(Keys.D))
            {
                position.X += speed;
            }
            position.X = MathHelper.Clamp(position.X, 0, _vewportRectangle.Width - texture.Width);
        }
    }
}
