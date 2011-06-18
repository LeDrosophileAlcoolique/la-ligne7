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
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        protected GraphicsDeviceManager graphics;
        protected SpriteBatch spriteBatch;
        private ScreenManager screenManager;

        public NetworkSession Session { get; set; }
        public AvailableNetworkSessionCollection AvailableSessions { get; set; }

        public PacketReader PacketReader { get; set; }
        public PacketWriter PacketWriter { get; set; }

        protected Clavier clavier;
        protected Souris souris;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            Components.Add(new GamerServicesComponent(this));

            // graphics.ToggleFullScreen();
            Window.Title = "La ligne 7";

            clavier = new Clavier(this);
            souris = new Souris(this);
        }

        protected override void Initialize()
        {
            screenManager = new ScreenManager(this, graphics);

            PacketReader = new PacketReader();
            PacketWriter = new PacketWriter();

            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            screenManager.ChargeContent(spriteBatch);
        }

        protected override void Update(GameTime gameTime)
        {
            if (clavier.IsNewKeyPress(Keys.I) && !Guide.IsVisible)
                Guide.ShowSignIn(1, false);

            if (!Guide.IsVisible)
            {
                screenManager.GameTime = gameTime;
                screenManager.MiseJour();
            }

            if (Session != null)
                Session.Update();

            // On update le clavier et la souris
            clavier.Update();
            souris.Update();

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.RenderState.DepthBufferEnable = true;

            screenManager.GameTime = gameTime;
            screenManager.Dessin();

            base.Draw(gameTime);
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
    }
}