#region Using
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Net;
using Microsoft.Xna.Framework.Storage;
#endregion

namespace ligne7
{
    class ScreenManager : DrawableGameComponent
    {
        // XNA
        protected Game1 game1;
        protected SpriteBatch spriteBatch;
        protected GraphicsDeviceManager graphics;
        public GameTime GameTime { get; set; }
        public RessourceManager<Texture2D> ContentImage { get; set; }
        public RessourceManager<Model> Content3D { get; set; }
        public RessourceManager<SpriteFont> ContentFont { get; set; }
        public RessourceManager<Song> ContentSong { get; set; }

        protected Son son;

        // Ecran de jeu (screen)
        protected GameScreen gameScreen;
        protected MainScreen mainScreen;

        // Config du jeu
        protected Options options;

        // Multi
        protected NetworkSession session;
        protected GameReseau gameReseau;

        public ScreenManager(Game1 game, GraphicsDeviceManager graphics, NetworkSession session)
            : base(game)
        {
            game1 = game;
            this.graphics = graphics;
            ContentImage = new RessourceManager<Texture2D>(this.Game.Content);
            Content3D = new RessourceManager<Model>(this.Game.Content);
            ContentFont = new RessourceManager<SpriteFont>(this.Game.Content);
            ContentSong = new RessourceManager<Song>(this.Game.Content);

            son = new Son(this);
            
            // On initialise l'écran du jeu
            gameScreen = new MainMenuScreen(this);
            gameReseau = new GameReseau(this);
            mainScreen = new MainScreen(this);
            
            // On initialise le jeu
            options = new Options(son);

            this.session = session;
        }

        public void ChargeContent(SpriteBatch spriteBatch)
        {
            this.spriteBatch = spriteBatch;

            son.LoadContentAndPlay();

            // On charge l'écran de jeu
            gameScreen.LoadContent();
            mainScreen.LoadContent();
        }

        public void MiseJour()
        {
            gameScreen.Update(GameTime);
        }

        public void Dessin()
        {
            gameScreen.Draw(GameTime);
        }

        public void ChargeMainScreen()
        {
            gameScreen = mainScreen;
        }

        public void ChangeGameScreen(GameScreen gameScreen)
        {
            this.gameScreen = gameScreen;
            gameScreen.LoadContent();
        }

        public void ChargeGameReseau()
        {
            this.gameScreen = gameReseau;
            gameReseau.LoadContent();
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

        public Options Options
        {
            get
            {
                return options;
            }
        }

        public Game1 Game1
        {
            get
            {
                return game1;
            }
        }

        public NetworkSession Session
        {
            get
            {
                return session;
            }
            set
            {
                session = value;
            }
        }

        public GameReseau GameReseau
        {
            get
            {
                return gameReseau;
            }
        }
    }
}
