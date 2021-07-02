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
        OPTIONS,
        GAME_PLAY,
        EXIT
    }

    public class GameProgram : Game
    {
        private SpriteBatch spriteBatch;
        public GraphicsDeviceManager graphicsManager; //убрать

        GameOption GameOptions = new GameOption(1920, 1080);

        private Rectangle viewPortRectangle;

        private int widthClip = 1920;// убрать
        private int heightClip = 1080; // убрать

        private SpriteFont fontInGame;
        private Texture2D menuBackground, gameBackground, optionsBackground;

        private Button exitButton, startButton, menuOptionButton, backButton, FHDButtun;

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
            //GameOption gameOption = new GameOption(1920, 1080);
            //gameOption.ChangeResolution(1920, 1080);
            graphicsManager = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            programState = ProgramStates.MAIN_MENU;
        }

        protected override void Initialize()
        {
            graphicsManager.PreferredBackBufferWidth = widthClip;
            graphicsManager.PreferredBackBufferHeight = heightClip;
            GameOptions.SetFullScreenOreWindows(graphicsManager);
            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            viewPortRectangle = new Rectangle(0, 0, graphicsManager.GraphicsDevice.Viewport.Width, graphicsManager.GraphicsDevice.Viewport.Height);
            menuBackground = Content.Load<Texture2D>("menuBackground");
            gameBackground = Content.Load<Texture2D>("gameBackground");
            optionsBackground = Content.Load<Texture2D>("optionsBackground");
            fontInGame = Content.Load<SpriteFont>("fontInGame");

            exitButton = new Button(100, 800, 200, 100, Content.Load<Texture2D>("ExitNoPress"));
            startButton = new Button(100, 400, 200, 100, Content.Load<Texture2D>("StartNoPress"));
            menuOptionButton = new Button(100, 600, 200, 100, Content.Load<Texture2D>("menuOptionButton"));
            backButton = new Button(850, 800, 200, 100, Content.Load<Texture2D>("backButton"));
            FHDButtun = new Button(850, 400, 200, 100, Content.Load<Texture2D>("FHDButtun"));


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
                        if (mouse.LeftButton == ButtonState.Pressed && menuOptionButton.ContainsButton(mouse.X, mouse.Y))
                        {
                            programState = ProgramStates.OPTIONS;
                        }
                        break;
                    }

                case ProgramStates.GAME_MENU:
                    {
                        IsMouseVisible = true;
                        break;
                    }
                case ProgramStates.OPTIONS:
                    {
                        IsMouseVisible = true;

                        if (mouse.LeftButton == ButtonState.Pressed && backButton.ContainsButton(mouse.X, mouse.Y))
                        {
                            programState = ProgramStates.MAIN_MENU;
                        }

                        if (mouse.LeftButton == ButtonState.Pressed && FHDButtun.ContainsButton(mouse.X, mouse.Y))
                        {
                            GameOptions.SetFullScreenOreWindows(graphicsManager);
                        }

                        //Проверка кнопок настроек игры и вызов методов из класса GameOption для применения новых настроек игры
                        break;
                    }
                case ProgramStates.GAME_PLAY:
                    {
                        IsMouseVisible = false;
                        if (Keyboard.GetState().IsKeyDown(Keys.Escape))
                        {
                            //programState = ProgramStates.MAIN_MENU;
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
                        menuOptionButton.DrawButton(mouse.X, mouse.Y, spriteBatch);
                        break;
                    }

                case ProgramStates.OPTIONS:
                    {
                        spriteBatch.Draw(optionsBackground, viewPortRectangle, Color.White);
                        backButton.DrawButton(mouse.X, mouse.Y, spriteBatch);
                        FHDButtun.DrawButton(mouse.X, mouse.Y, spriteBatch);
                        //Проверка кнопок настроек игры и вызов методов из класса GameOption для применения новых настроек игры
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