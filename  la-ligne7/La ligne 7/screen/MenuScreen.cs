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
    class MenuScreen : GameScreen
    {
        protected GraphicsDeviceManager graphics;
        protected Menu menu;
        protected Ecrire author;
        protected Ecrire team;
        protected Texture2D fond;
        protected Curseur curseur;

        public MenuScreen(ScreenManager screenManager)
            : base(screenManager)
        {
            graphics = screenManager.Graphics;
            menu = new MainMenu(new Bouton[] { new Bouton("Jouer !", 30, 100), new Bouton("Instructions", 130, 200), new Bouton("Options", 230, 300), new Bouton("Quitter, bye bye", 330, 400) });
            author = new Ecrire("Copyright - RETP - Arnaud, Jacques, Remi, Thibault", 0, screenManager.Game.GraphicsDevice.Viewport.Height - 100);
            team = new Ecrire("Les Rats Envahissent Tout Paris", 0, screenManager.Game.GraphicsDevice.Viewport.Height - 65);
            curseur = new Curseur();
        }

        public override void LoadContent()
        {
            foreach (Bouton bouton in menu.Boutons)
                bouton.LoadFont(screenManager.Game.Content);

            author.LoadFont(screenManager.Game.Content);
            team.LoadFont(screenManager.Game.Content);
            fond = screenManager.Game.Content.Load<Texture2D>("Bloody");
            curseur.LoadContent(screenManager.Game.Content, graphics);
        }

        public override void Update(GameTime gameTime)
        {
            foreach (Bouton bouton in menu.Boutons)
                bouton.Translation(gameTime);

            if (screenManager.Clavier.IsNewKeyPress(Keys.Down))
                menu.SetBoutonSelected(menu.Selected, 1);

            if (screenManager.Clavier.IsNewKeyPress(Keys.Up))
                menu.SetBoutonSelected(menu.Selected, -1);

            if (screenManager.Clavier.IsNewKeyPress(Keys.Enter))
                menu.PressEnter(screenManager);

            if (screenManager.Souris.IsChangeState())
                menu.Focus(screenManager.Souris.CurrentMouseState.X, screenManager.Souris.CurrentMouseState.Y);

            if (screenManager.Souris.IsNewClickPress())
                menu.Click(screenManager, screenManager.Souris.CurrentMouseState.X, screenManager.Souris.CurrentMouseState.Y);

            curseur.Update(screenManager.Souris.CurrentMouseState.X, screenManager.Souris.CurrentMouseState.Y);
        }

        public override void Draw(GameTime gameTime)
        {
            screenManager.Game.GraphicsDevice.Clear(Color.White);

            SpriteBatch spriteBatch = screenManager.SpriteBatch;
            
            spriteBatch.Begin();
            spriteBatch.Draw(fond, new Vector2((screenManager.Game.GraphicsDevice.Viewport.Width - fond.Width) / 2 + 135, (screenManager.Game.GraphicsDevice.Viewport.Height - fond.Height) / 2 - 75), Color.White);
            foreach (Bouton bouton in menu.Boutons)
                bouton.Draw(spriteBatch);

            author.Draw(spriteBatch);
            team.Draw(spriteBatch);
            curseur.Draw(spriteBatch);
            spriteBatch.End();
        }
    }

    class OptionsScreen : GameScreen
    {
        protected GraphicsDeviceManager graphics;
        protected OptionsMenu menu;
        protected Texture2D fond;
        protected Curseur curseur;

        public OptionsScreen(ScreenManager screenManager)
            : base(screenManager)
        {
            graphics = screenManager.Graphics;
            menu = new OptionsMenu(new Bouton[] { new Bouton("Retour", 75, 300) }, new Option[] { new Option("Volume", 75, 100), new Option("Niveau", 75, 200) });
            curseur = new Curseur();
        }

        public override void LoadContent()
        {
            fond = screenManager.Game.Content.Load<Texture2D>("Bloody");

            foreach (Option option in menu.TabOptions)
                option.LoadFont(screenManager.Game.Content);
            foreach (Bouton bouton in menu.Boutons)
                bouton.LoadFont(screenManager.Game.Content);

            curseur.LoadContent(screenManager.Game.Content, graphics);
        }

        public override void Update(GameTime gameTime)
        {
            menu.TabOptions[0].Valeur = screenManager.Options.Volume;
            menu.TabOptions[1].Valeur = screenManager.Options.Niveau;

            foreach (Option option in menu.TabOptions)
                option.Translation(gameTime);
            foreach (Bouton bouton in menu.Boutons)
                bouton.Translation(gameTime);

            if (screenManager.Clavier.IsNewKeyPress(Keys.Down))
                menu.SetBoutonSelected(menu.Selected, 1);

            if (screenManager.Clavier.IsNewKeyPress(Keys.Up))
                menu.SetBoutonSelected(menu.Selected, -1);

            if (screenManager.Clavier.IsNewKeyPress(Keys.Enter))
                menu.PressEnter(screenManager);

            if (screenManager.Souris.IsChangeState())
                menu.Focus(screenManager.Souris.CurrentMouseState.X, screenManager.Souris.CurrentMouseState.Y);

            if (screenManager.Souris.IsNewClickPress())
                menu.Click(screenManager, screenManager.Souris.CurrentMouseState.X, screenManager.Souris.CurrentMouseState.Y);

            curseur.Update(screenManager.Souris.CurrentMouseState.X, screenManager.Souris.CurrentMouseState.Y);
        }

        public override void Draw(GameTime gameTime)
        {
            screenManager.Game.GraphicsDevice.Clear(Color.White);

            SpriteBatch spriteBatch = screenManager.SpriteBatch;

            spriteBatch.Begin();
            spriteBatch.Draw(fond, new Vector2((screenManager.Game.GraphicsDevice.Viewport.Width - fond.Width) / 2 + 100, (screenManager.Game.GraphicsDevice.Viewport.Height - fond.Height) / 2 + 150), Color.White);
            foreach (Option option in menu.TabOptions)
                option.Draw(spriteBatch);
            foreach (Bouton bouton in menu.Boutons)
                bouton.Draw(spriteBatch);
            curseur.Draw(spriteBatch);
            spriteBatch.End();
        }
    }
}
