using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;

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

        GameOption _optionsManager;
        private bool _isFpsOn = true;

        private Rectangle _viewPortRectangle;

        private SpriteFont _fontInGame;
        private Texture2D _menuBackground, _gameBackground, _optionsBackground;

        private Racket _racket;
        private Ball _ball;
        private Brick[,] _bricks;

        private GuiManager _guiManager;

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
            _optionsManager = new GameOption(new GraphicsDeviceManager(this), 1920, 1080);

            Content.RootDirectory = "Content";

            _guiManager = new GuiManager();

            _guiManager.AddButton("EXIT", new Button(100, 800, 200, 100, Content.Load<Texture2D>("ExitNoPress")));
            _guiManager.AddButton("START", new Button(100, 400, 200, 100, Content.Load<Texture2D>("StartNoPress")));
            _guiManager.AddButton("OPTIONS", new Button(100, 600, 200, 100, Content.Load<Texture2D>("menuOptionButton")));
            _guiManager.AddButton("BACK", new Button(850, 800, 200, 100, Content.Load<Texture2D>("backButton")));
            _guiManager.AddButton("FULLSCREEN", new Button(750, 400, 200, 100, Content.Load<Texture2D>("FullScreenButtun")));
            _guiManager.AddButton("WINDOW", new Button(950, 400, 200, 100, Content.Load<Texture2D>("WindowsButtun")));
            _guiManager.AddButton("FPSON", new Button(750, 500, 200, 100, Content.Load<Texture2D>("fpsOnButton")));
            _guiManager.AddButton("FPSOFF", new Button(950, 500, 200, 100, Content.Load<Texture2D>("fpsOffButton")));


            programState = ProgramStates.MAIN_MENU;
        }

        protected override void Initialize()
        {
            _optionsManager.SetFullScreenMode(true);
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
                        if (mouse.LeftButton == ButtonState.Pressed && _guiManager.GetButton("EXIT").ContainsButton(mouse.X, mouse.Y))
                        {
                            Exit();
                        }

                        if (mouse.LeftButton == ButtonState.Pressed && _guiManager.GetButton("START").ContainsButton(mouse.X, mouse.Y))
                        {
                            programState = ProgramStates.GAME_PLAY;
                        }

                        if (mouse.LeftButton == ButtonState.Pressed && _guiManager.GetButton("OPTIONS").ContainsButton(mouse.X, mouse.Y))
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

                        if (mouse.LeftButton == ButtonState.Pressed && _guiManager.GetButton("BACK").ContainsButton(mouse.X, mouse.Y))
                        {
                            programState = ProgramStates.MAIN_MENU;
                        }

                        if (mouse.LeftButton == ButtonState.Pressed && _guiManager.GetButton("FULLSCREEN").ContainsButton(mouse.X, mouse.Y))
                        {
                            _optionsManager.SetFullScreenMode(true);
                            //Такое преобразование нужно, чтобы вернуть к дефолту размеры кнопок перед очередным изменением разрешения
                            _guiManager.ResizeButtons(1 / _optionsManager.GetXscale(), 1 / _optionsManager.GetYscale());
                            //
                            _optionsManager.SetResolution(1920, 1080);
                            _guiManager.ResizeButtons(_optionsManager.GetXscale(), _optionsManager.GetYscale());
                        }

                        if (mouse.LeftButton == ButtonState.Pressed && _guiManager.GetButton("WINDOW").ContainsButton(mouse.X, mouse.Y))
                        {
                            _optionsManager.SetFullScreenMode(false);
                            //Такое преобразование нужно, чтобы вернуть к дефолту размеры кнопок перед очередным изменением разрешения
                            _guiManager.ResizeButtons(1 / _optionsManager.GetXscale(), 1 / _optionsManager.GetYscale());
                            //
                            _optionsManager.SetResolution(1280, 720);
                            _guiManager.ResizeButtons(_optionsManager.GetXscale(), _optionsManager.GetYscale());
                        }

                        if (mouse.LeftButton == ButtonState.Pressed && _guiManager.GetButton("FPSON").ContainsButton(mouse.X, mouse.Y))
                        {
                            _isFpsOn = true;
                            _guiManager.ResizeButtons(1 / _optionsManager.GetXscale(), 1 / _optionsManager.GetYscale());
                            _guiManager.ResizeButtons(_optionsManager.GetXscale(), _optionsManager.GetYscale());
                        }

                        if (mouse.LeftButton == ButtonState.Pressed && _guiManager.GetButton("FPSOFF").ContainsButton(mouse.X, mouse.Y))
                        {
                            _isFpsOn = false;
                            _guiManager.ResizeButtons(1 / _optionsManager.GetXscale(), 1 / _optionsManager.GetYscale());
                            _guiManager.ResizeButtons(_optionsManager.GetXscale(), _optionsManager.GetYscale());
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
                        _guiManager.GetButton("EXIT").DrawButton(mouse.X, mouse.Y, _spriteBatch);
                        _guiManager.GetButton("START").DrawButton(mouse.X, mouse.Y, _spriteBatch);
                        _guiManager.GetButton("OPTIONS").DrawButton(mouse.X, mouse.Y, _spriteBatch);
                        break;
                    }

                case ProgramStates.OPTIONS:
                    {
                        _spriteBatch.Draw(_optionsBackground, _viewPortRectangle, Color.White);
                        _guiManager.GetButton("BACK").DrawButton(mouse.X, mouse.Y, _spriteBatch);
                        _guiManager.GetButton("FULLSCREEN").DrawButton(mouse.X, mouse.Y, _spriteBatch);
                        _guiManager.GetButton("WINDOW").DrawButton(mouse.X, mouse.Y, _spriteBatch);
                        _guiManager.GetButton("FPSON").DrawButton(mouse.X, mouse.Y, _spriteBatch);
                        _guiManager.GetButton("FPSOFF").DrawButton(mouse.X, mouse.Y, _spriteBatch);
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

            if (_isFpsOn)
            {
                _spriteBatch.DrawString(_fontInGame, $"FPS = {fps}.   Game is playing. Mouse is disabled. To return- Esc", new Vector2(0, 0), Color.Red);

            }

            _spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}