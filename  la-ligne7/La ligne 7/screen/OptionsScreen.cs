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
                bouton.LoadFont(screenManager.Game.Content, "MenuFont");
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
            SpriteBatch spriteBatch = screenManager.SpriteBatch;
            
            spriteBatch.Begin();
            spriteBatch.Draw(fond, new Vector2((screenManager.Game.GraphicsDevice.Viewport.Width - fond.Width) / 2 + 100, (screenManager.Game.GraphicsDevice.Viewport.Height - fond.Height) / 2 + 150), Color.White);
            foreach (Bouton bouton in menu.Boutons)
                bouton.Draw(spriteBatch);

            spriteBatch.End();
        }
    }
}
