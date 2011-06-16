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
        protected Modele2D fond;
        protected Train train;

        protected Menu menu;

        public MenuScreen(ScreenManager screenManager)
            : base(screenManager)
        {
            graphics = screenManager.Graphics;
            curseur = new Curseur();
            fond = new Modele2D("Image/menu", Vector2.Zero);
            train = new Train(new Vector2(-3200, 0));

            menu = new Menu(screenManager);
        }

        public override void LoadContent()
        {
            curseur.LoadContent(screenManager.ContentImage, graphics);
            fond.LoadContent(screenManager.ContentImage);
            train.LoadContent(screenManager.ContentImage);

            menu.LoadContent(screenManager);
        }

        public override void Update(GameTime gameTime)
        {
            train.Update(gameTime);
            curseur.Update(screenManager.Game1.Souris.CurrentMouseState.X, screenManager.Game1.Souris.CurrentMouseState.Y);

            menu.Update(screenManager);
        }

        public override void Draw(GameTime gameTime)
        {
            // On met l'écran en blanc
            screenManager.Game.GraphicsDevice.Clear(Color.White);

            screenManager.SpriteBatch.Begin();
            fond.Draw(screenManager.SpriteBatch, Color.White);
            menu.Draw(screenManager.SpriteBatch);
            train.Draw(screenManager.SpriteBatch, Color.White);

            // Pour afficher le curseur
            curseur.Draw(screenManager.SpriteBatch, Color.RoyalBlue, 0.75f);
            screenManager.SpriteBatch.End();
        }
    }

    class MainMenuScreen : MenuScreen
    {
        public MainMenuScreen(ScreenManager screenManager)
            : base (screenManager)
        {
            menu.Boutons = new Bouton[] { new Bouton("Exit", "Image/exit", new Vector2(720, 89)), new Bouton("Jouer", "Image/jeu", new Vector2(370, 215)) };
            menu.Liens = new Lien[] { new Lien("Coopération", "Jouer", 30, 150), new Lien("Instructions", "Instructions", 30, 250), new Lien("Options", "Options main menu", 30, 350) };
        }
    }

    class PauseScreen : MenuScreen
    {
        public PauseScreen(ScreenManager screenManager)
            : base(screenManager)
        {
            menu.Boutons = new Bouton[] { new Bouton("Exit", "Image/exit", new Vector2(720, 89)), new Bouton("Jouer", "Image/jeu", new Vector2(370, 215)) };
            menu.Liens = new Lien[] { new Lien("Options", "Options pause menu", 30, 150), new Lien("Abondonner", "Abondonner", 30, 250) };
        }
    }

    class OptionsScreen : MenuScreen
    {
        public OptionsScreen(ScreenManager screenManager, string retour)
            : base(screenManager)
        {
            menu.IsOptions = true;

            menu.Boutons = new Bouton[] { new Bouton("Exit", "Image/exit", new Vector2(720, 89)), new Bouton("Jouer", "Image/jeu", new Vector2(370, 215)) };
            menu.Liens = new Lien[4];

            menu.Liens[0] = new Lien("Volume : " + screenManager.Options.Volume.ToString(), "Modif volume", 30, 150);
            menu.Liens[1] = new Lien("Niveau : " + screenManager.Options.GetNiveau(), "Modif niveau", 30, 225);
            menu.Liens[2] = new Lien("Vie : " + screenManager.Options.GetVie(), "Modif vie", 30, 300);

            switch (retour)
            {
                case "main":
                    menu.Liens[3] = new Lien("Retour", "Main menu", 30, 375);
                    break;
                case "pause":
                    menu.Liens[3] = new Lien("Retour", "Pause", 30, 375);
                    break;
            }
        }
    }
}
