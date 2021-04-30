using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace MonoArk
{

    public class GameProgram : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        MouseState mouse;

        //Texture2D gameBackground;
        Texture2D menuBackground;
        Texture2D exitNoPress;  

        Rectangle exitButton = new Rectangle(100, 800, 200, 100);// такие же координаты, как у кнопки выход

        int BackBufferWidth = 1920;
        int BackBufferHeight = 1080;

        public GameProgram()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            graphics.PreferredBackBufferWidth = BackBufferWidth;
            graphics.PreferredBackBufferHeight = BackBufferHeight;
            graphics.IsFullScreen = true;
            graphics.ApplyChanges();
            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            menuBackground = Content.Load<Texture2D>("menuBackground");
            //gameBackground = Content.Load<Texture2D>("gameBackground");
            exitNoPress = Content.Load<Texture2D>("ExitNoPress");
        }

        protected override void UnloadContent()
        {

        }

        protected override void Update(GameTime gameTime)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.E))
                Exit();
            mouse = Mouse.GetState();

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            spriteBatch.Begin();

            spriteBatch.Draw(menuBackground, new Rectangle(0, 0, BackBufferWidth, BackBufferHeight), Color.White);
            spriteBatch.Draw(exitNoPress, new Rectangle(100, 800, 200, 100), Color.White); // переписать с масшатабом разрешения

            if (exitButton.Contains(mouse.X, mouse.Y))
            {
                spriteBatch.Draw(exitNoPress, new Rectangle(100, 800, 200, 100), Color.Red);
            }

            if (mouse.LeftButton == ButtonState.Pressed && exitButton.Contains(mouse.X, mouse.Y))
            {
                Exit();
            }

            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}