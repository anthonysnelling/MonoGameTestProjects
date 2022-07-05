using System;
using System.Globalization;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace SpaceShip
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        private Texture2D _shipSprite;
        private Texture2D _asteroidSprite;
        private Texture2D _spaceSprite;

        private SpriteFont _gameFont;
        private SpriteFont _timerFont;

        private Ship player = new Ship();
        private Controller gameController = new Controller();

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            _graphics.PreferredBackBufferWidth = 1280;
            _graphics.PreferredBackBufferHeight = 720;
            _graphics.ApplyChanges();

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
            _shipSprite = Content.Load<Texture2D>("ship");
            _asteroidSprite = Content.Load<Texture2D>("asteroid");
            _spaceSprite = Content.Load<Texture2D>("space");
            _gameFont = Content.Load<SpriteFont>("spaceFont");
            _timerFont = Content.Load<SpriteFont>("timerFont");
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed ||
                Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here
            if (gameController.inGame)
            {
                player.ShipUpdate(gameTime);
            }

            gameController.ConUpdate(gameTime);

            for (int i = 0; i < gameController.Asteroids.Count; i++)
            {
                gameController.Asteroids[i].AsteroidUpdate(gameTime);

                int sum = gameController.Asteroids[i].Radius + player.radius;
                if (Vector2.Distance(gameController.Asteroids[i].Position, player.Position) < sum)
                {
                    gameController.inGame = false;
                    player.Position = Ship.DefaultPosition;
                    gameController.Asteroids.Clear();
                }
            }

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            _spriteBatch.Begin();
            _spriteBatch.Draw(_spaceSprite, new Vector2(0, 0), Color.White);
            _spriteBatch.Draw(_shipSprite, new Vector2(player.Position.X - 34, player.Position.Y - 50), Color.White);

            for (int i = 0; i < gameController.Asteroids.Count; i++)
            {
                _spriteBatch.Draw(_asteroidSprite,
                    new Vector2(gameController.Asteroids[i].Position.X - gameController.Asteroids[i].Radius,
                        gameController.Asteroids[i].Position.Y - gameController.Asteroids[i].Radius), Color.White);
            }

            if (gameController.inGame == false)
            {
                string menuMessage = "Press Enter To Begin";
                Vector2 sizeOfText = _gameFont.MeasureString(menuMessage);
                int halfWidth = _graphics.PreferredBackBufferWidth / 2;
                _spriteBatch.DrawString(_gameFont, menuMessage, new Vector2(halfWidth - (sizeOfText.X / 2), 200),
                    Color.White);
            }

            _spriteBatch.DrawString(_timerFont,
                "Time: " + Math.Floor(gameController.totalTime).ToString(CultureInfo.InvariantCulture),
                new Vector2(3, 3), Color.White);

            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}