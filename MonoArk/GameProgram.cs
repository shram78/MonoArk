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
        private SpriteBatch _spriteBatch;
        private GraphicsDeviceManager _graphicsManager;

        GameOption GameOptions = new GameOption(1920, 1080);
        private Rectangle _viewPortRectangle;

        private SpriteFont _fontInGame;
        private Texture2D _menuBackground, _gameBackground, _optionsBackground;

        private Button _exitButton, _startButton, _menuOptionButton, _backButton, _fullScreenButtun, _windowsButtun;

        private Racket _racket;
        private Ball _ball;
        private Brick[,] _bricks;

        private static int _brickInLenght = 10;
        private static int _brickInHeight = 5;
        private int _brickCount = _brickInLenght * _brickInHeight;

        MouseState mouse;
        ProgramStates programState;

        //счетчик FPS
        int total_frames = 0;
        double elapsed_time = 0;
        int fps = 0;

        public GameProgram()
        {
            _graphicsManager = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            programState = ProgramStates.MAIN_MENU;
        }

        protected override void Initialize()
        {
            GameOptions.SetResolution(_graphicsManager);
            GameOptions.SetFullScreenMode(_graphicsManager);
            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            // viewPortRectangle = new Rectangle(0, 0, graphicsManager.GraphicsDevice.Viewport.Width, graphicsManager.GraphicsDevice.Viewport.Height);
            _viewPortRectangle = new Rectangle(0, 0, 1920, 1080);
            _menuBackground = Content.Load<Texture2D>("menuBackground");
            _gameBackground = Content.Load<Texture2D>("gameBackground");
            _optionsBackground = Content.Load<Texture2D>("optionsBackground");
            _fontInGame = Content.Load<SpriteFont>("fontInGame");

            _exitButton = new Button(100, 800, 200, 100, Content.Load<Texture2D>("ExitNoPress"));
            _startButton = new Button(100, 400, 200, 100, Content.Load<Texture2D>("StartNoPress"));
            _menuOptionButton = new Button(100, 600, 200, 100, Content.Load<Texture2D>("menuOptionButton"));
            _backButton = new Button(850, 800, 200, 100, Content.Load<Texture2D>("backButton"));
            _fullScreenButtun = new Button(750, 400, 200, 100, Content.Load<Texture2D>("FullScreenButtun"));
            _windowsButtun = new Button(950, 400, 200, 100, Content.Load<Texture2D>("WindowsButtun"));

            _racket = new Racket(Content.Load<Texture2D>("Racket"), new Vector2(_viewPortRectangle.Width / 2 - 50, _viewPortRectangle.Height - 50), 10);

            _bricks = new Brick[_brickInLenght, _brickInHeight];

            for (int i = 0; i < _brickInLenght; i++)
            {

                for (int j = 0; j < _brickInHeight; j++)
                {
                    _bricks[i, j] = new Brick(Content.Load<Texture2D>("Brick"), new Vector2(i * 150 + 220, j * 50 + 200), 0);
                }
            }
            _ball = new Ball(Content.Load<Texture2D>("Ball"), new Vector2(_viewPortRectangle.Width / 2 - 15, _viewPortRectangle.Height - 80), new Vector2(7, 7));
        }

        protected override void UnloadContent()
        {
            _spriteBatch.Dispose();
        }

        protected override void Update(GameTime gameTime)
        {
            mouse = Mouse.GetState();

            switch (programState)
            {
                case ProgramStates.MAIN_MENU:
                    {
                        IsMouseVisible = true;
                        if (mouse.LeftButton == ButtonState.Pressed && _exitButton.ContainsButton(mouse.X, mouse.Y))
                        {
                            Exit();
                        }

                        if (mouse.LeftButton == ButtonState.Pressed && _startButton.ContainsButton(mouse.X, mouse.Y))
                        {
                            programState = ProgramStates.GAME_PLAY;
                        }

                        if (mouse.LeftButton == ButtonState.Pressed && _menuOptionButton.ContainsButton(mouse.X, mouse.Y))
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

                        if (mouse.LeftButton == ButtonState.Pressed && _backButton.ContainsButton(mouse.X, mouse.Y))
                        {
                            programState = ProgramStates.MAIN_MENU;
                        }

                        if (mouse.LeftButton == ButtonState.Pressed && _fullScreenButtun.ContainsButton(mouse.X, mouse.Y))
                        {
                            GameOptions.SetFullScreenMode(_graphicsManager);
                        }

                        if (mouse.LeftButton == ButtonState.Pressed && _windowsButtun.ContainsButton(mouse.X, mouse.Y))
                        {
                            GameOptions.SetWindowsMode(_graphicsManager);
                        }
                        //Проверка кнопок настроек игры и вызов методов из класса GameOption для применения новых настроек игры
                        break;
                    }

                case ProgramStates.GAME_PLAY:
                    {
                        IsMouseVisible = false;

                        if (Keyboard.GetState().IsKeyDown(Keys.Escape))
                        {
                            programState = ProgramStates.MAIN_MENU;
                            // Exit();
                        }

                        foreach (var brick in _bricks)
                        {

                            if (brick.is_alive() && _ball.check_bricks_collision(brick))
                            {
                                _ball.changeDirection_Y();
                                brick.kill();
                                _brickCount--;
                                if (_brickCount == 0) Exit();
                            }
                        }

                        _racket.move(Keyboard.GetState(), _viewPortRectangle);
                        _ball.move();
                        _ball.check_wall_collision(_viewPortRectangle);
                        _ball.check_racket_collision(_racket);

                        if (_ball.GetLive() == false)
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

            _spriteBatch.Begin();


            switch (programState)
            {

                case ProgramStates.MAIN_MENU:
                    {
                        _spriteBatch.Draw(_menuBackground, _viewPortRectangle, Color.White);
                        _exitButton.DrawButton(mouse.X, mouse.Y, _spriteBatch);
                        _startButton.DrawButton(mouse.X, mouse.Y, _spriteBatch);
                        _menuOptionButton.DrawButton(mouse.X, mouse.Y, _spriteBatch);
                        break;
                    }

                case ProgramStates.OPTIONS:
                    {
                        _spriteBatch.Draw(_optionsBackground, _viewPortRectangle, Color.White);
                        _backButton.DrawButton(mouse.X, mouse.Y, _spriteBatch);
                        _fullScreenButtun.DrawButton(mouse.X, mouse.Y, _spriteBatch);
                        _windowsButtun.DrawButton(mouse.X, mouse.Y, _spriteBatch);
                        //Проверка кнопок настроек игры и вызов методов из класса GameOption для применения новых настроек игры
                        break;
                    }

                case ProgramStates.GAME_MENU:
                    {
                        break;
                    }

                case ProgramStates.GAME_PLAY:
                    {
                        _spriteBatch.Draw(_gameBackground, _viewPortRectangle, Color.White);
                        _spriteBatch.DrawString(_fontInGame, $"FPS = {fps}.   Game is playing. Mouse is disabled. To return- Esc", new Vector2(0, 0), Color.Red);
                        _racket.draw(_spriteBatch);
                        _ball.draw(_spriteBatch);

                        foreach (var brick in _bricks)
                        {
                            if (brick.is_alive())
                                brick.draw(_spriteBatch);

                        }
                        break;
                    }

                case ProgramStates.EXIT:
                    {
                        break;
                    }
            }
            _spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}