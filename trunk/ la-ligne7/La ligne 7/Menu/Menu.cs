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
    class Menu
    {
        public Bouton[] Boutons { get; set; }
        public Lien[] Liens { get; set; }
        protected Bouton precSelectedBouton;
        protected Lien precSelectedLien;

        protected ScreenManager screenManager;

        protected bool isOptions;

        public Menu(ScreenManager screenManager)
        {
            precSelectedBouton = null;
            precSelectedLien = null;

            this.screenManager = screenManager;
        }

        public void LoadContent(ScreenManager screenManager)
        { 
            foreach (Bouton bouton in Boutons)
                bouton.LoadContent(screenManager.ContentImage);

            foreach (Lien lien in Liens)
                lien.LoadFont(screenManager.ContentFont);
        }

        public void Update(ScreenManager screenManager)
        {
            Souris souris = screenManager.Game1.Souris;
            Bouton selectedBouton = null;
            Lien selectedLien = null;

            foreach (Bouton bouton in Boutons)
            {
                if (bouton.Rec.Contains(souris.CurrentMouseState.X, souris.CurrentMouseState.Y))
                    selectedBouton = bouton;
            }

            foreach (Lien lien in Liens)
            {
                if (lien.Rec.Contains(souris.CurrentMouseState.X, souris.CurrentMouseState.Y))
                    selectedLien = lien;
            }

            if (precSelectedBouton != null)
                precSelectedBouton.IsFocused = false;

            if (precSelectedLien != null)
                precSelectedLien.IsFocused = false;

            if (selectedBouton != null)
            {
                selectedBouton.IsFocused = true;
                precSelectedBouton = selectedBouton;
                precSelectedLien = null;

                if (souris.IsNewClickPress())
                    Action(selectedBouton.Fonction);
            }
            else if (selectedLien != null)
            {
                selectedLien.IsFocused = true;
                precSelectedLien = selectedLien;
                precSelectedBouton = null;

                if (souris.IsNewClickPress())
                    Action(selectedLien.Fonction);
            }
            else
            {
                precSelectedBouton = null;
                precSelectedLien = null;
            }

            if (isOptions)
            {
                Liens[0].Name = "Volume : " + screenManager.Options.Volume.ToString();
                Liens[1].Name = "Niveau : " + screenManager.Options.GetNiveau();
                Liens[2].Name = "Vie : " + screenManager.Options.GetVie();
            }
        }

        public void Action(string fonction)
        {
            switch (fonction)
            { 
                case "Jouer":
                    screenManager.ChargeMainScreen();
                    break;
                case "Main menu":
                    screenManager.ChangeGameScreen(new MainMenuScreen(screenManager));
                    break;
                case "Instructions":
                    break;
                case "Pause":
                    screenManager.ChangeGameScreen(new PauseScreen(screenManager));
                    break;
                case "Options main menu":
                    screenManager.ChangeGameScreen(new OptionsScreen(screenManager, "main"));
                    break;
                case "Options pause menu":
                    screenManager.ChangeGameScreen(new OptionsScreen(screenManager, "pause"));
                    break;
                case "Modif volume":
                    screenManager.Options.ChangeOptionIncrement(fonction);
                    break;
                case "Modif niveau":
                    screenManager.Options.ChangeOptionIncrement(fonction);
                    break;
                case "Modif vie":
                    screenManager.Options.ChangeOptionIncrement(fonction);
                    break;
                case "Abondonner":
                    screenManager.InitMainScreen();
                    screenManager.ChangeGameScreen(new MainMenuScreen(screenManager));
                    break;
                case "Exit":
                    screenManager.Game.Exit();
                    break;
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (Bouton bouton in Boutons)
                bouton.Draw(spriteBatch, Color.White);

            foreach (Lien lien in Liens)
                lien.Draw(spriteBatch);
        }

        public bool IsOptions
        {
            set
            {
                isOptions = value;
            }
        }
    }
}
