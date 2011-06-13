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
    abstract class Menu
    {
        public Bouton[] Boutons { get; set; }
        public int Selected { get; set; }

        public Menu(Bouton[] boutons)
        {
            Boutons = boutons;
            Selected = 0;
            Boutons[Selected].IsSelected = true;
        }

        protected void Unselected(int i)
        {
            Boutons[Selected].IsSelected = false;

            Selected = i;
            Boutons[i].IsSelected = true;
        }

        // La fonction SetBoutonSelected change le bouton qui est sélectionné
        public void SetBoutonSelected(int selected, int operation)
        {
            int ieme = (selected + operation) % Boutons.Length;

            Unselected(ieme);
        }

        // La fonction Contains retourne -1 si les rectangles ne contiennent pas sinon retourne la position de la souris dans le tableau
        protected int Contains(int x, int y)
        {
            for (int i = 0; i < Boutons.Length; i++)
            {
                if (Boutons[i].Box.Contains(x, y))
                {
                    return i;
                }
            }

            return -1;
        }

        public void Focus(int x, int y)
        {
            int i = Contains(x, y);

            if (i >= 0)
            {
                Unselected(i);
            }
        }

        public void PressEnter(ScreenManager screenManager)
        {
            Action(screenManager, Selected);
        }

        public void Click(ScreenManager screenManager, int x, int y)
        {
            int i = Contains(x, y);

            if (i >= 0)
            {
                Action(screenManager, i);
            }
        }

        protected abstract void Action(ScreenManager screenManager, int selected);

        public void Load(ScreenManager screenManager)
        {
            foreach (Bouton bouton in Boutons)
                bouton.LoadFont(screenManager.ContentFont);
        }

        public void Update(ScreenManager screenManager)
        {
            foreach (Bouton bouton in Boutons)
                bouton.Translation(screenManager.GameTime);

            if (screenManager.Game1.Clavier.IsNewKeyPress(Keys.Down))
                SetBoutonSelected(Selected, 1);

            if (screenManager.Game1.Clavier.IsNewKeyPress(Keys.Up))
                SetBoutonSelected(Selected, -1);

            if (screenManager.Game1.Clavier.IsNewKeyPress(Keys.Enter))
                PressEnter(screenManager);

            if (screenManager.Game1.Souris.IsChangeState())
                Focus(screenManager.Game1.Souris.CurrentMouseState.X, screenManager.Game1.Souris.CurrentMouseState.Y);

            if (screenManager.Game1.Souris.IsNewClickPress())
                Click(screenManager, screenManager.Game1.Souris.CurrentMouseState.X, screenManager.Game1.Souris.CurrentMouseState.Y);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (Bouton bouton in Boutons)
                bouton.Draw(spriteBatch);
        }
    }

    // Le comportement du menu pricipal affiché au lancement du jeu
    class MainMenu : Menu
    {
        public MainMenu(Bouton[] boutons)
            : base(boutons)
        {
        }

        protected override void Action(ScreenManager screenManager, int selected)
        {
            switch (selected)
            {
                case 0:
                    screenManager.ChangeGameScreen(new PlayScreen(screenManager));
                    break;
                case 2:
                    screenManager.ChangeGameScreen(new OptionsScreen(screenManager, true));
                    break;
                case 3:
                    screenManager.Game.Exit();
                    break;
            }
        }
    }

    class PlayMenu : Menu
    {
        public PlayMenu(Bouton[] boutons)
            : base(boutons)
        {
        }

        protected override void Action(ScreenManager screenManager, int selected)
        {
            switch (selected)
            {
                case 0:
                    screenManager.ChargeMainScreen();
                    break;
                case 1:
                    if (screenManager.Session != null)
                    {
                        if (screenManager.Session.IsHost)
                        {
                            screenManager.ChargeMainScreen();
                        }
                        else
                        {
                            screenManager.ChargeGameReseau(new GameReseau(screenManager));
                        }
                    }
                    break;
            }
        }
    }

    class PauseMenu : Menu
    {
        public PauseMenu(Bouton[] boutons)
            : base(boutons)
        {
        }

        protected override void Action(ScreenManager screenManager, int selected)
        {
            switch (selected)
            {
                case 0:
                    screenManager.ChargeMainScreen();
                    break;
                case 1:
                    screenManager.ChangeGameScreen(new OptionsScreen(screenManager, false));
                    break;
            }
        }
    }

    class OptionsMenu : Menu
    {
        protected bool returnMenu;

        public OptionsMenu(Bouton[] boutons, int nbr, bool returnMenu)
            : base(boutons)
        {
            this.returnMenu = returnMenu;
        }

        protected override void Action(ScreenManager screenManager, int selected)
        {
            switch (selected)
            {
                case 0:
                    screenManager.Options.ChangeOptionIncrement(selected);
                    break;
                case 1:
                    screenManager.Options.ChangeOptionIncrement(selected);
                    break;
                case 2:
                    screenManager.Options.ChangeOptionIncrement(selected);
                    break;
                case 3:
                    if (returnMenu)
                        screenManager.ChangeGameScreen(new MainMenuScreen(screenManager));
                    else
                        screenManager.ChangeGameScreen(new PauseScreen(screenManager));
                    break;
            }
        }

        public void Draw(SpriteBatch spriteBatch, ScreenManager screenManager)
        {
            for (int i = 0; i < Boutons.Length && i < screenManager.Options.NbrOptions; i++)
            { 
                Boutons[i].Draw(spriteBatch, screenManager.Options.GetOption(i));
            }

            for (int i = screenManager.Options.NbrOptions; i < Boutons.Length; i++)
            {
                Boutons[i].Draw(spriteBatch);
            }
        }
    }
}
