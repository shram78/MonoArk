using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using GameClasses;


namespace MonoArk
{
    enum ProgramStates
    {
        MAIN_MENU,
        GAME_MENU,
        GAME_PLAY,
        EXIT
    }

    public class GameProgram : Game
    {
        private GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;

        private Rectangle viewPortRectangle;

        private int widthClip = 1920;
        private int heightClip = 1080;

        private SpriteFont fontInGame;
        private Texture2D menuBackground, gameBackground;

        private Button exitButton, startButton;

        private Racket racket;
        private Ball ball;

        private Brick[,] bricks;

        private static int brickInLenght = 10;
        private static int brickInHeight = 5;
        private int brickCount = brickInLenght * brickInHeight;

        MouseState mouse;
        ProgramStates programState;

        //счетчик FPS
        int total_frames = 0;
        double elapsed_time = 0;
        int fps = 0;

        public GameProgram()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            //  programState = ProgramStates.MAIN_MENU;
            programState = ProgramStates.GAME_PLAY;
           
        }

        protected override void Initialize()
        {
            graphics.PreferredBackBufferWidth = widthClip;
            graphics.PreferredBackBufferHeight = heightClip;
            graphics.IsFullScreen = false;
            graphics.ApplyChanges();
            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            viewPortRectangle = new Rectangle(0, 0, graphics.GraphicsDevice.Viewport.Width, graphics.GraphicsDevice.Viewport.Height);
            menuBackground = Content.Load<Texture2D>("menuBackground");
            gameBackground = Content.Load<Texture2D>("gameBackground");
            fontInGame = Content.Load<SpriteFont>("fontInGame");

            exitButton = new Button(100, 800, 200, 100, Content.Load<Texture2D>("ExitNoPress"));
            startButton = new Button(100, 600, 200, 100, Content.Load<Texture2D>("StartNoPress"));
            racket = new Racket(Content.Load<Texture2D>("Racket"), new Vector2(viewPortRectangle.Width / 2 - 50, viewPortRectangle.Height - 50), 10);

            bricks = new Brick[brickInLenght, brickInHeight];

            for (int i = 0; i < brickInLenght; i++)
            {
                for (int j = 0; j < brickInHeight; j++)
                {
                    bricks[i, j] = new Brick(Content.Load<Texture2D>("Brick"), new Vector2(i * 150 + 220, j * 50 + 200), 0);
                }
            }
            ball = new Ball(Content.Load<Texture2D>("Ball"), new Vector2(viewPortRectangle.Width / 2 - 15, viewPortRectangle.Height - 80), new Vector2(7, 7));
        }

        protected override void UnloadContent()
        {
            spriteBatch.Dispose();
        }

        protected override void Update(GameTime gameTime)
        {
            mouse = Mouse.GetState();
            switch (programState)
            {
                case ProgramStates.MAIN_MENU:
                    {
                        IsMouseVisible = true;
                        if (mouse.LeftButton == ButtonState.Pressed && exitButton.ContainsButton(mouse.X, mouse.Y))
                        {
                            Exit();
                        }
                        if (mouse.LeftButton == ButtonState.Pressed && startButton.ContainsButton(mouse.X, mouse.Y))
                        {
                            programState = ProgramStates.GAME_PLAY;
                        }
                        break;
                    }
                case ProgramStates.GAME_MENU:
                    {
                        IsMouseVisible = true;
                        break;
                    }
                case ProgramStates.GAME_PLAY:
                    {
                        IsMouseVisible = false;
                        if (Keyboard.GetState().IsKeyDown(Keys.Escape))
                        {
                            // programState = ProgramStates.MAIN_MENU;
                            Exit();
                        }
                        foreach (var brick in bricks)
                        {
                            if (brick.is_alive() && ball.check_bricks_collision(brick))
                            {
                                ball.changeDirection_Y();
                                brick.kill();
                                brickCount--;
                                if (brickCount == 0) Exit();
                            }
                        }

                        racket.move(Keyboard.GetState(), viewPortRectangle);

                        ball.move();
                        ball.check_wall_collision(viewPortRectangle);
                        ball.check_racket_collision(racket);
                        if (ball.GetLive() == false)
                        {
                            Exit();
                        }
                        break;
                    }
                case ProgramStates.EXIT:
                    {
                        break;
                    }
            }
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            //for fps
            total_frames++;
            elapsed_time = elapsed_time + gameTime.ElapsedGameTime.TotalMilliseconds;
            if (elapsed_time >= 1000)
            {
                fps = total_frames;
                total_frames = 0;
                elapsed_time = 0;
            }

            spriteBatch.Begin();

            switch (programState)
            {
                case ProgramStates.MAIN_MENU:
                    {
                        spriteBatch.Draw(menuBackground, viewPortRectangle, Color.White);
                        exitButton.DrawButton(mouse.X, mouse.Y, spriteBatch);
                        startButton.DrawButton(mouse.X, mouse.Y, spriteBatch);
                        break;
                    }
                case ProgramStates.GAME_MENU:
                    {
                        break;
                    }
                case ProgramStates.GAME_PLAY:
                    {
                        spriteBatch.Draw(gameBackground, viewPortRectangle, Color.White);
                        spriteBatch.DrawString(fontInGame, $"FPS = {fps}.   Game is playing. Mouse is disabled. To return- Esc", new Vector2(0, 0), Color.Red);
                        racket.draw(spriteBatch);
                        ball.draw(spriteBatch);

                        foreach (var brick in bricks)
                        {
                            if (brick.is_alive())
                                brick.draw(spriteBatch);
                        }
                        break;
                    }
                case ProgramStates.EXIT:
                    {
                        break;
                    }
            }
            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}