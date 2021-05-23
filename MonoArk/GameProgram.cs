﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

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
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Button exitButton, startButton;
        Racket racket;
        Texture2D menuBackground, gameBackground;
        SpriteFont fontInGame;
        int widthClip = 1920;
        int heightClip = 1080;
        MouseState mouse;

        private Vector2 _positionRacket;

        ProgramStates programState;
        public GameProgram()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            programState = ProgramStates.MAIN_MENU;
        }

        protected override void Initialize()
        {
            graphics.PreferredBackBufferWidth = widthClip;
            graphics.PreferredBackBufferHeight = heightClip;
            graphics.IsFullScreen = true;
            graphics.ApplyChanges();
            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);

            menuBackground = Content.Load<Texture2D>("menuBackground");
            gameBackground = Content.Load<Texture2D>("gameBackground");
            fontInGame = Content.Load<SpriteFont>("fontInGame");
            _positionRacket = new Vector2(860, 1000);
            exitButton = new Button(100, 800, 200, 100, Content.Load<Texture2D>("ExitNoPress"));
            startButton = new Button(100, 600, 200, 100, Content.Load<Texture2D>("StartNoPress"));
            racket = new Racket(Content.Load<Texture2D>("Racket"));
        }

        protected override void UnloadContent() { }

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
                            programState = ProgramStates.MAIN_MENU;
                        }

                        if (Keyboard.GetState().IsKeyDown(Keys.A))
                        {
                            _positionRacket.X = racket.MoveRocket(_positionRacket.X, -10);
                        }

                        if (Keyboard.GetState().IsKeyDown(Keys.D))
                        {
                            _positionRacket.X = racket.MoveRocket(_positionRacket.X, 10);
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
            spriteBatch.Begin();

            switch (programState)
            {
                case ProgramStates.MAIN_MENU:
                    {
                        spriteBatch.Draw(menuBackground, new Rectangle(0, 0, widthClip, heightClip), Color.White);
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
                        spriteBatch.Draw(gameBackground, new Rectangle(0, 0, widthClip, heightClip), Color.White);
                        spriteBatch.DrawString(fontInGame, "Game is playing. Mouse is disabled. To return- Esc", new Vector2(0, 0), Color.Red);

                        racket.DrawRacket(spriteBatch, _positionRacket);   /////////


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