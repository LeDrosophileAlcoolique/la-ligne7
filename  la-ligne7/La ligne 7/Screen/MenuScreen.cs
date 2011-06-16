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
    abstract class MenuScreen : GameScreen
    {
        protected GraphicsDeviceManager graphics;
        protected Curseur curseur;

        public MenuScreen(ScreenManager screenManager)
            : base(screenManager)
        {
            graphics = screenManager.Graphics;
            curseur = new Curseur("Image/curseur");
        }

        public override void LoadContent()
        {
            curseur.LoadContent(screenManager.ContentImage, graphics);
        }

        public override void Update(GameTime gameTime)
        {
            curseur.Update(screenManager.Game1.Souris.CurrentMouseState.X, screenManager.Game1.Souris.CurrentMouseState.Y);
        }

        public override void Draw(GameTime gameTime)
        {
            curseur.Draw(screenManager.SpriteBatch);
        }
    }

    class MainMenuScreen : MenuScreen
    {
        protected Menu menu;
        protected Ecrire author;
        protected Ecrire team;
        protected Texture2D fond;

        public MainMenuScreen(ScreenManager screenManager)
            : base (screenManager)
        {
            menu = new MainMenu(new Bouton[] { new Bouton("Jouer !", 30, 100), new Bouton("Instructions", 130, 200), new Bouton("Options", 230, 300), new Bouton("Quitter, bye bye", 330, 400) });
            author = new Ecrire("Copyright - RETP - Arnaud, Jacques, Remi, Thibault", screenManager.Game.GraphicsDevice.Viewport.Height - 100);
            team = new Ecrire("Les Rats Envahissent Tout Paris", screenManager.Game.GraphicsDevice.Viewport.Height - 65);
        }

        public override void LoadContent()
        {
            base.LoadContent();

            menu.Load(screenManager);
            author.LoadFont(screenManager.ContentFont);
            team.LoadFont(screenManager.ContentFont);
            fond = screenManager.ContentImage.GetElement("Image/bloody");
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            menu.Update(screenManager);
        }

        public override void Draw(GameTime gameTime)
        {
            // On met l'écran en blanc
            screenManager.Game.GraphicsDevice.Clear(Color.White);

            screenManager.SpriteBatch.Begin();
            screenManager.SpriteBatch.Draw(fond, new Vector2((screenManager.Game.GraphicsDevice.Viewport.Width - fond.Width) / 2 + 135, (screenManager.Game.GraphicsDevice.Viewport.Height - fond.Height) / 2 - 75), Color.White);
            author.Draw(screenManager.SpriteBatch);
            team.Draw(screenManager.SpriteBatch);
            menu.Draw(screenManager.SpriteBatch);

            // Pour afficher le curseur
            base.Draw(gameTime);
            screenManager.SpriteBatch.End();
        }
    }

    class OptionsScreen : MenuScreen
    { 
        protected OptionsMenu menu;

        public OptionsScreen(ScreenManager screenManager, bool returnMenu)
            : base (screenManager)
        {
            menu = new OptionsMenu(new Bouton[] { new Bouton("Volume", 75, 100), new Bouton("Niveau", 75, 200), new Bouton("Vie", 75, 300), new Bouton("Retour", 75, 400) }, screenManager.Options.NbrOptions, returnMenu); 
        }

        public override void LoadContent()
        {
            base.LoadContent();

            menu.Load(screenManager);
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            menu.Update(screenManager);
        }

        public override void Draw(GameTime gameTime)
        {
            // On met l'écran en blanc
            screenManager.Game.GraphicsDevice.Clear(Color.White);

            screenManager.SpriteBatch.Begin();
            menu.Draw(screenManager.SpriteBatch, screenManager);

            // Pour afficher le curseur
            base.Draw(gameTime);
            screenManager.SpriteBatch.End();
        }
    }

    class PlayScreen : MenuScreen
    {
        protected Menu menu;

        public PlayScreen(ScreenManager screenManager)
            : base(screenManager)
        {
            menu = new PlayMenu(new Bouton[] { new Bouton("Campagne solo", 75, 200), new Bouton("Multijoueur", 75, 300) });
        }

        public override void LoadContent()
        {
            base.LoadContent();

            menu.Load(screenManager);
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            menu.Update(screenManager);
        }

        public override void Draw(GameTime gameTime)
        {
            // On met l'écran en blanc
            screenManager.Game.GraphicsDevice.Clear(Color.White);

            screenManager.SpriteBatch.Begin();
            menu.Draw(screenManager.SpriteBatch);

            // Pour afficher le curseur
            base.Draw(gameTime);
            screenManager.SpriteBatch.End();
        }
    }

    class PauseScreen : MenuScreen
    {
        protected Menu menu;

        public PauseScreen(ScreenManager screenManager)
            : base(screenManager)
        {
            menu = new PauseMenu(new Bouton[] { new Bouton("Reprendre le jeu", 75, 200), new Bouton("Options", 75, 300) });
        }

        public override void LoadContent()
        {
            base.LoadContent();

            menu.Load(screenManager);
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            menu.Update(screenManager);
        }

        public override void Draw(GameTime gameTime)
        {
            // On met l'écran en blanc
            screenManager.Game.GraphicsDevice.Clear(Color.White);

            screenManager.SpriteBatch.Begin();
            menu.Draw(screenManager.SpriteBatch);

            // Pour afficher le curseur
            base.Draw(gameTime);
            screenManager.SpriteBatch.End();
        }
    }
}
