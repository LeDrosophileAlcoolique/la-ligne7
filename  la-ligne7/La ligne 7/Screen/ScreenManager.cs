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

        // Ecran de jeu (screen)
        protected GameScreen gameScreen;
        protected MainScreen mainScreen;

        public Son Son { get; set; }

        // Config du jeu
        protected Options options;

        // Multi
        protected NetworkSession session;

        public ScreenManager(Game1 game, GraphicsDeviceManager graphics, NetworkSession session)
            : base(game)
        {
            game1 = game;
            this.graphics = graphics;
            ContentImage = new RessourceManager<Texture2D>(this.Game.Content);
            Content3D = new RessourceManager<Model>(this.Game.Content);
            ContentFont = new RessourceManager<SpriteFont>(this.Game.Content);
            ContentSong = new RessourceManager<Song>(this.Game.Content);

            Son = new Son();

            // On initialise l'écran du jeu
            gameScreen = new MainMenuScreen(this);
            mainScreen = new MainScreen(this);
            
            // On initialise le jeu
            options = new Options(Son);

            this.session = session;
        }

        public void ChargeContent(SpriteBatch spriteBatch)
        {
            this.spriteBatch = spriteBatch;

            Son.LoadContentAndPlay("18");

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

        public void InitMainScreen()
        {
            mainScreen = new MainScreen(this);
            mainScreen.LoadContent();
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

        public MainScreen MainScreen
        {
            get
            {
                return mainScreen;
            }
        }
    }
}
