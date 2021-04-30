using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace MonoArk
{

    public class GameProgram : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Texture2D mainMenuBackground;
        Texture2D avia;
        Texture2D level1Back;
        int BackBufferWidth = 1920;
        int BackBufferHeight = 1080;

        public GameProgram()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
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
            mainMenuBackground = Content.Load <Texture2D>("SHRAM_GAMES-20");
            avia = Content.Load<Texture2D>("avia");
            level1Back = Content.Load<Texture2D>("level1Back");

        }

    
        protected override void UnloadContent()
        {
            
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.E))
                Exit();


            base.Update(gameTime);
        }


        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);


            spriteBatch.Begin();

            spriteBatch.Draw(mainMenuBackground, new Rectangle(0, 0, BackBufferWidth, BackBufferHeight), Color.White);

            //spriteBatch.Draw(avia, new Vector2((Window.ClientBounds.Width / 2),
            //                                 (Window.ClientBounds.Height / 2)),
            //    null, Color.White, 0, Vector2.Zero, 0.2f, SpriteEffects.None, 0);

            spriteBatch.Draw(avia, new Rectangle((Window.ClientBounds.Width / 8), (Window.ClientBounds.Height / 8),
                            BackBufferWidth / 2, BackBufferHeight / 2), Color.White);


            spriteBatch.End();
             

            base.Draw(gameTime);
        }
    }
}
