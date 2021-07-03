using System;
using System.Data.SqlTypes;
using System.Threading;
using System.Xml;
using System.Xml.Linq;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace TicTacToe
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        private Texture2D gridSprite;
        private Texture2D xSprite;
        private Texture2D ySprite;

        private Vector2 gridPos = new Vector2(0, 0);
        private Vector2 gridScale = new Vector2(3, 3);
        private Vector2 gridOrigin = new Vector2(0, 0);
       Color defaultColor = Color.White; 
       
        private MouseState mState;

        private int playerTurn = 1;
        private int[,] gameBoard = new int[3,3];

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            
        }

        protected override void Initialize()
        {
            gridPos.X = (_graphics.GraphicsDevice.Viewport.Width) / 2;
            gridPos.Y = (_graphics.GraphicsDevice.Viewport.Height) / 2;
            
            //initialize gameboard to all empty
            foreach (int position in gameBoard)
            {
                gameBoard[position,position] = 0;
                Console.Write(gameBoard[position,position] + ", ");
            }
            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            gridSprite = Content.Load<Texture2D>("Grid-TicTacToe");
            xSprite = Content.Load<Texture2D>("X-TicTacToe");
            ySprite = Content.Load<Texture2D>("O-TicTacToe");
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed ||
                Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            mState = Mouse.GetState();
            
            // TODO: Place pieces when click on empty spot
            
            // TODO: check and see if board has three in a row

            //  alternate between pieces for player turns. 
            if (playerTurn == 1)
            {
                playerTurn++;
            }
            else
            {
                playerTurn = 1;
            } 
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            
            Vector2 gridOffset = new Vector2(gridPos.X - 540, gridPos.Y - 540);
            // TODO: Only Draw the X or O when you click a certain spot.

            GraphicsDevice.Clear(Color.MidnightBlue);

            _spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, SamplerState.PointClamp,
                DepthStencilState.Default, RasterizerState.CullNone, null, null);

            _spriteBatch.Draw(gridSprite, gridOffset,
                null, defaultColor, 0,
                gridOrigin, gridScale 
                , SpriteEffects.None, 0);

            _spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}