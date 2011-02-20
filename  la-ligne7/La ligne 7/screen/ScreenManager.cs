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
    class ScreenManager : DrawableGameComponent
    {
        protected GameScreen gameScreen;
        protected SpriteBatch spriteBatch;
        protected GraphicsDeviceManager graphics;
        protected GameTime gameTime;
        protected Clavier clavier;
        protected Souris souris;
        protected Options options;

        public ScreenManager(Game game, GraphicsDeviceManager graphics)
            : base(game)
        {
            clavier = new Clavier(Game);
            souris = new Souris(Game);
            this.graphics = graphics;
            gameScreen = new MenuScreen(this);
            options = new Options();
        }

        public void ChargeContent(SpriteBatch spriteBatch)
        {
            this.spriteBatch = spriteBatch;
            gameScreen.LoadContent();
        }

        public void MiseJour()
        {
            clavier.Update();
            souris.Update();
            gameScreen.Update(gameTime);
        }

        public void Dessin()
        {
            gameScreen.Draw(gameTime);
        }

        public void ChangeGameScreen(GameScreen gameScreen)
        {
            this.gameScreen = gameScreen;
            gameScreen.LoadContent();
        }

        public GameTime GameTime
        {
            set 
            { 
                gameTime = value; 
            }
            get 
            { 
                return gameTime; 
            }
        }

        public SpriteBatch SpriteBatch
        {
            get 
            { 
                return spriteBatch; 
            }
        }

        public GraphicsDeviceManager Graphics
        {
            get 
            { 
                return graphics; 
            }
        }

        public Clavier Clavier
        {
            get 
            { 
                return clavier; 
            }
        }

        public Souris Souris
        {
            get
            {
                return souris;
            }
        }

        public Options Options
        {
            get
            {
                return options;
            }
        }
    }
}
