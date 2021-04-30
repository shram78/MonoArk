using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
// этот текст для теста пуша из домашего компа
namespace MonoArk
{

    public class GameProgram : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Texture2D gameBackground;
        Texture2D menuBackground;
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

        }

    
        protected override void UnloadContent()
        {
            
        }

        protected override void Update(GameTime gameTime)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.E))
                Exit();


            base.Update(gameTime);
        }


        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);


            spriteBatch.Begin();

            spriteBatch.Draw(menuBackground, new Rectangle(0, 0, BackBufferWidth, BackBufferHeight), Color.White);


            spriteBatch.End();
             

            base.Draw(gameTime);
        }
    }
}
