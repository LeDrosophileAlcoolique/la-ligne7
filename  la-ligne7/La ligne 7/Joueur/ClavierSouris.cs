#region Using
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
#endregion

namespace ligne7
{
    public class Clavier : GameComponent
    {
        public KeyboardState CurrentKeyboardState { get; set; }
        protected KeyboardState lastKeyboardState;

        public Clavier(Game game)
            : base(game)
        {
        }

        public Clavier(Game game, KeyboardState keyBoardState)
            : base(game)
        {
            CurrentKeyboardState = keyBoardState;
        }

        public void Update()
        {
            lastKeyboardState = CurrentKeyboardState;

            CurrentKeyboardState = Keyboard.GetState();

            // Permet de quitter quand on appuie sur echap
            if (CurrentKeyboardState.IsKeyDown(Keys.Escape))
                Game.Exit();
        }

        public bool IsNewKeyPress(Keys key)
        {
            return CurrentKeyboardState.IsKeyDown(key) && lastKeyboardState.IsKeyUp(key);
        }
    }

    public class Souris : GameComponent
    {
        public MouseState CurrentMouseState { get; set; }
        protected MouseState lastMouseState;

        public Souris(Game game)
            : base(game)
        {
        }

        public Souris(Game game, MouseState mouseState)
            : base(game)
        {
            CurrentMouseState = mouseState;
        }

        public void Update()
        {
            lastMouseState = CurrentMouseState;

            CurrentMouseState = Mouse.GetState();
        }

        public void AuCentre(GraphicsDeviceManager graphics)
        {
            Mouse.SetPosition(graphics.PreferredBackBufferWidth / 2, graphics.PreferredBackBufferHeight / 2);
        }

        public bool IsChangeState()
        {
            return CurrentMouseState.X != lastMouseState.X || CurrentMouseState.Y != lastMouseState.Y;
        }

        public bool IsNewClickPress()
        {
            return CurrentMouseState.LeftButton == ButtonState.Pressed && lastMouseState.LeftButton == ButtonState.Released;
        }

        // Les fonctions DeltaX et DeltaY retourne la différence entre le centre et la position X ou Y de la souris
        public int DeltaX(GraphicsDeviceManager graphics)
        {
            return graphics.PreferredBackBufferWidth / 2 - CurrentMouseState.X;
        }

        public int DeltaY(GraphicsDeviceManager graphics)
        {
            return graphics.PreferredBackBufferHeight / 2 - CurrentMouseState.Y;
    }
    }
}
