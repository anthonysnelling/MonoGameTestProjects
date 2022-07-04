using System;
using System.Globalization;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace ShootingGallery
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        private Texture2D _targetSprite;
        private Texture2D _crosshairsSprite;
        private Texture2D _backgroundSprite;
        private SpriteFont _gameFont;

        private Vector2 _targetPosition = new Vector2(300, 300);
        private const int TargetRadius = 45;

        private MouseState _mState;
        private bool _mReleased = true;
        private int _score = 0;

        private double _timer = 10;


        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = false;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here


            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
            _targetSprite = Content.Load<Texture2D>("target");
            _crosshairsSprite = Content.Load<Texture2D>("crosshairs");
            _backgroundSprite = Content.Load<Texture2D>("sky");
            _gameFont = Content.Load<SpriteFont>("galleryFont");
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed ||
                Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            if (_timer > 0)
            {
                _timer -= gameTime.ElapsedGameTime.TotalSeconds;
            }

            if (_timer < 0)
            {
                _timer = 0;
            }

            _mState = Mouse.GetState();

            if (_mState.LeftButton == ButtonState.Pressed && _mReleased == true)
            {
                float mouseTargetDist = Vector2.Distance(_targetPosition, _mState.Position.ToVector2());
                if (mouseTargetDist < TargetRadius && _timer > 0)
                {
                    _score++;

                    Random ran = new Random();

                    _targetPosition.X = ran.Next(45, _graphics.GraphicsDevice.Viewport.Width - TargetRadius);
                    _targetPosition.Y = ran.Next(45, _graphics.GraphicsDevice.Viewport.Height - TargetRadius * 2);
                }

                _mReleased = false;
            }

            if (_mState.LeftButton == ButtonState.Released)
            {
                _mReleased = true;
            }

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            int halfScreenHeight = _graphics.GraphicsDevice.Viewport.Height / 2;
            int halfScreenWidth = _graphics.GraphicsDevice.Viewport.Width / 2;

            _spriteBatch.Begin();
            _spriteBatch.Draw(_backgroundSprite, new Vector2(0, 0), Color.White);
            if (_timer > 0)
            {
                _spriteBatch.Draw(_targetSprite,
                    new Vector2(_targetPosition.X - TargetRadius, _targetPosition.Y - TargetRadius), Color.White);
            }
            else
            {
                _spriteBatch.DrawString(_gameFont, "Game Over!",
                    new Vector2(halfScreenWidth - 90, halfScreenHeight - 90), Color.Red);
            }

            _spriteBatch.DrawString(_gameFont, "Score: " + _score.ToString(), new Vector2(3, 3), Color.White);
            _spriteBatch.DrawString(_gameFont, "Time: " + Math.Ceiling(_timer).ToString(CultureInfo.InvariantCulture),
                new Vector2(3, 40), Color.White);
            _spriteBatch.Draw(_crosshairsSprite, new Vector2(_mState.X - 25, _mState.Y - 25), Color.SeaGreen);
            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}