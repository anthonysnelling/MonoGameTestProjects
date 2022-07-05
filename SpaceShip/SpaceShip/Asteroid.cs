using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace SpaceShip
{
    public class Asteroid
    {
        public Vector2 Position;
        public int Speed;
        public int Radius = 59;
        private Random rand = new Random();

        public Asteroid(int newSpeed)
        {
            Speed = newSpeed;
            Position = new Vector2(1380, rand.Next(0, 721));
        }

        public void AsteroidUpdate(GameTime gameTime)
        {
            float dt = (float)gameTime.ElapsedGameTime.TotalSeconds;
            Position.X -= Speed * dt;
        }
        

    }
}