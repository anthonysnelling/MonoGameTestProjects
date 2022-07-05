using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace SpaceShip
{
    public class Ship
    {
        static public Vector2 DefaultPosition = new Vector2(640, 360);
        public Vector2 Position = DefaultPosition;
        public int Speed = 180;
        public int radius = 30;

        public void ShipUpdate(GameTime gameTime)
        {
            KeyboardState kState = Keyboard.GetState();
            float dt = (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (kState.IsKeyDown(Keys.Right) || kState.IsKeyDown(Keys.D) && Position.X < 1280)
            {
                Position.X += Speed * dt;
            }

            if (kState.IsKeyDown(Keys.Left) || kState.IsKeyDown(Keys.A) && Position.X > 0 + radius)
            {
                Position.X -= Speed * dt;
            }

            if (kState.IsKeyDown(Keys.Up) || kState.IsKeyDown(Keys.W) && Position.Y > 0 + radius)
            {
                Position.Y -= Speed * dt;
            }

            if (kState.IsKeyDown(Keys.Down) || kState.IsKeyDown(Keys.S) && Position.Y < 720 - radius)
            {
                Position.Y += Speed * dt;
            }
        }
    }
}