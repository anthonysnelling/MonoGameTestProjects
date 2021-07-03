using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace ShootingGallery
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        private Texture2D targetSprite;
        private Texture2D crosshairSprite;
        private Texture2D backgroundSprite;
        private SpriteFont gameFont;

        private Vector2 targetPosition = new Vector2(915, 495);
        private const int targetRadius = 45;

        private MouseState mState;
        private bool mReleased = true;
        private int score = 0;

        private double timer = 10;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = false;
        }

        protected override void Initialize()
        {
            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            targetSprite = Content.Load<Texture2D>("gallery_assets/target");
            crosshairSprite = Content.Load<Texture2D>("gallery_assets/crosshairs");
            backgroundSprite = Content.Load<Texture2D>("gallery_assets/sky");
            gameFont = Content.Load<SpriteFont>("gallery_assets/galleryFont");
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed ||
                Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            if (timer > 0)
            {
                timer -= gameTime.ElapsedGameTime.TotalSeconds;
                if (timer < 0)
                {
                    timer = 0;
                }
            }


            mState = Mouse.GetState();

            if (mState.LeftButton == ButtonState.Pressed && mReleased == true)
            {
                float mouseTargetDist = Vector2.Distance(targetPosition, mState.Position.ToVector2());

                if (mouseTargetDist < targetRadius && timer > 0)
                {
                    score++;
                    Random rand = new Random();
                    targetPosition.X = rand.Next(10, _graphics.GraphicsDevice.Viewport.Width);
                    targetPosition.Y = rand.Next(10, _graphics.GraphicsDevice.Viewport.Height - 90);
                }

                mReleased = false;
            }

            if (mState.LeftButton == ButtonState.Released)
            {
                mReleased = true;
            }

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            _spriteBatch.Begin();
            _spriteBatch.Draw(backgroundSprite, new Vector2(0, 0), Color.White);
            if (timer > 0)
            {
                _spriteBatch.Draw(targetSprite,
                    new Vector2(targetPosition.X - targetRadius, targetPosition.Y - targetRadius), Color.White);
            }

            _spriteBatch.DrawString(gameFont, "Score: " + score.ToString(), new Vector2(3, 3), Color.White);
            _spriteBatch.DrawString(gameFont, "Time: " + Math.Ceiling(timer).ToString(), new Vector2(3, 40),
                Color.White);
            if (timer == 0)
            {
                _spriteBatch.DrawString(gameFont, "GAMEOVER! " , new Vector2(_graphics.GraphicsDevice.Viewport.Width / 2,  _graphics.GraphicsDevice.Viewport.Height / 2), Color.White);
            }
            _spriteBatch.Draw(crosshairSprite, new Vector2(mState.X - 25, mState.Y - 25), Color.White);

            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}