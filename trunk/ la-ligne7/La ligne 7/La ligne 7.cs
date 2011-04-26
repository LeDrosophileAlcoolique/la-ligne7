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

        protected NetworkSession session;
        protected AvailableNetworkSessionCollection availableSessions;

        protected PacketReader packetReader;
        protected PacketWriter packetWriter;

        protected Clavier clavier;
        protected Souris souris;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            Components.Add(new GamerServicesComponent(this));

            // graphics.ToggleFullScreen();
            // Window.Title = "La ligne 7";

            clavier = new Clavier(this);
            souris = new Souris(this);
        }

        protected override void Initialize()
        {
            screenManager = new ScreenManager(this, graphics);

            base.Initialize();

            packetReader = new PacketReader();
            packetWriter = new PacketWriter();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            screenManager.ChargeContent(spriteBatch);
        }

        protected override void Update(GameTime gameTime)
        {
            if (clavier.IsNewKeyPress(Keys.I) && !Guide.IsVisible && session == null)
                Guide.ShowSignIn(1, false);

            /*
            // Création du serveur
            if (clavier.IsNewKeyPress(Keys.J) && session == null && SignedInGamer.SignedInGamers.Count >= 1)
                session = NetworkSession.Create(NetworkSessionType.SystemLink, 1, 2);

            // Gestion client
            if (clavier.IsNewKeyPress(Keys.K) && session == null && SignedInGamer.SignedInGamers.Count >= 1)
            {
                availableSessions = NetworkSession.Find(NetworkSessionType.SystemLink, 1, null);

                if (availableSessions != null && availableSessions.Count > 0)
                    session = NetworkSession.Join(availableSessions[0]); // On rejoint le premier
            }

            if (session != null)
            {
                if (session.IsHost)
                    Window.Title = "Serveur lancé";
                else
                    Window.Title = "Client connecté sur " + session.Host.ToString();

                session.Update();
            }
            */

            if (!Guide.IsVisible)
            {
                screenManager.GameTime = gameTime;
                screenManager.MiseJour();
            }

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