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

        public MenuScreen(ScreenManager screenManager)
            : base(screenManager)
        {
            graphics = screenManager.Graphics;
            menu = new MainMenu(new Bouton[] { new Bouton("Jouer !", 30, 100), new Bouton("Instructions", 130, 200), new Bouton("Options", 230, 300), new Bouton("Quitter, bye bye", 330, 400) });
            author = new Ecrire("Copyright - RETP - Arnaud, Jacques, Remi, Thibault", 0, screenManager.Game.GraphicsDevice.Viewport.Height - 100);
            team = new Ecrire("Les Rats Envahissent Tout Paris", 0, screenManager.Game.GraphicsDevice.Viewport.Height - 65);
        }

        public override void LoadContent()
        {
            foreach (Bouton bouton in menu.Boutons)
                bouton.LoadFont(screenManager.Game.Content);

            author.LoadFont(screenManager.Game.Content);
            team.LoadFont(screenManager.Game.Content);
            fond = screenManager.Game.Content.Load<Texture2D>("Bloody");
        }

        public override void Update(GameTime gameTime)
        {
            screenManager.Clavier.Update();

            foreach (Bouton bouton in menu.Boutons)
                bouton.Translation(gameTime);

            if (screenManager.Clavier.IsNewKeyPress(Keys.Down))
                menu.SetBoutonSelected(menu.Selected, 1);

            if (screenManager.Clavier.IsNewKeyPress(Keys.Up))
                menu.SetBoutonSelected(menu.Selected, -1);

            if (screenManager.Clavier.IsNewKeyPress(Keys.Enter))
                menu.pressEnter(screenManager);
            Mouse.SetPosition(graphics.PreferredBackBufferWidth / 2, graphics.PreferredBackBufferHeight / 2);
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
            spriteBatch.End();
        }
    }

    class OptionsScreen : GameScreen
    {
        protected Menu menu;
        protected Texture2D fond;

        public OptionsScreen(ScreenManager screenManager)
            : base(screenManager)
        {
            menu = new OptionsMenu(new Bouton[] { new Bouton("Vitesse du jeu", 75, 100), new Bouton("Sensibilite de la souris", 75, 200), new Bouton("Retour", 75, 300) });
        }

        public override void LoadContent()
        {
            fond = screenManager.Game.Content.Load<Texture2D>("Bloody");

            foreach (Bouton bouton in menu.Boutons)
                bouton.LoadFont(screenManager.Game.Content);
        }

        public override void Update(GameTime gameTime)
        {
            screenManager.Clavier.Update();

            foreach (Bouton bouton in menu.Boutons)
                bouton.Translation(gameTime);

            if (screenManager.Clavier.IsNewKeyPress(Keys.Down))
                menu.SetBoutonSelected(menu.Selected, 1);

            if (screenManager.Clavier.IsNewKeyPress(Keys.Up))
                menu.SetBoutonSelected(menu.Selected, -1);

            if (screenManager.Clavier.IsNewKeyPress(Keys.Enter))
                menu.pressEnter(screenManager);
        }

        public override void Draw(GameTime gameTime)
        {
            screenManager.Game.GraphicsDevice.Clear(Color.White);

            SpriteBatch spriteBatch = screenManager.SpriteBatch;

            spriteBatch.Begin();
            spriteBatch.Draw(fond, new Vector2((screenManager.Game.GraphicsDevice.Viewport.Width - fond.Width) / 2 + 100, (screenManager.Game.GraphicsDevice.Viewport.Height - fond.Height) / 2 + 150), Color.White);
            foreach (Bouton bouton in menu.Boutons)
                bouton.Draw(spriteBatch);

            spriteBatch.End();
        }
    }
}
