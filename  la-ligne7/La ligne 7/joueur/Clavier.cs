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
    class Clavier : GameComponent
    {
        protected KeyboardState currentKeyboardState;
        protected KeyboardState lastKeyboardState;

        public Clavier(Game game)
            : base(game)
        {      
        }

        public void Update()
        {
            lastKeyboardState = currentKeyboardState;

            currentKeyboardState = Keyboard.GetState();

            // Permet de quitter quand on appuie sur echap
            if (currentKeyboardState.IsKeyDown(Keys.Escape))
                Game.Exit();
        }

        public bool IsNewKeyPress(Keys key)
        {
            return (currentKeyboardState.IsKeyDown(key) && lastKeyboardState.IsKeyUp(key));
        }

        public KeyboardState CurrentKeyboardState
        {
            get { return currentKeyboardState; }
        }
    }
}
