using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
// внес изменения, хочу запушить в састер
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
    
    interface GameObject {
        
        public Texture2D texture;
        public Vector2 position;
        public Vector2 size;
                
    }
    
    interface MoveableGameObject {
        
        public int speed;
        
        void move();
                
    }
    
    class RocketObject : GameObject, MoveableGameObject {
        
        RocketObject(Texture2D ro_texture, ro_position, ro_size, ro_speed) {
            texture = ro_texture;
            position = ro_position;
            size = ro_size;
            speed = ro_speed;       
        }
        
        void move() {
            position.X = position.X + speed;
        }
        
        void changeDirection() {
            speed = -speed;
        }
        
    }
}
