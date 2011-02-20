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
        protected Bouton[] boutons;
        protected int nbrBouton;
        protected int selected;

        public Menu(Bouton[] boutons)
        {
            this.boutons = boutons;
            nbrBouton = boutons.Length;
            selected = 0;
            this.boutons[0].IsSelected = true;
        }

        protected void Unselected(int i)
        {
            boutons[selected].IsSelected = false;

            this.selected = i;
            boutons[i].IsSelected = true;
        }

        // La fonction SetBoutonSelected change le bouton qui est sélectionné

        public void SetBoutonSelected(int selected, int operation)
        {
            int ieme = selected + operation;
            
            if (ieme >= nbrBouton)
            {
                ieme = 0;
            }

            if (ieme < 0)
            {
                ieme = nbrBouton - 1;
            }

            Unselected(ieme);
        }

        public void PressEnter(ScreenManager screenManager)
        {
            Action(screenManager, selected);
        }

        public void Click(ScreenManager screenManager, int x, int y)
        {
            for (int i = 0; i < boutons.Length; i++)
            {
                if (boutons[i].Box.Contains(x, y))
                {
                    Action(screenManager, i);
                }
            }
        }

        public void Focus(int x, int y)
        {
            for (int i = 0; i < boutons.Length; i++)
            {
                if (boutons[i].Box.Contains(x, y))
                {
                    Unselected(i);
                }
            }
        }

        protected abstract void Action(ScreenManager screenManager, int selected);

        public int Selected
        { 
            get 
            { 
                return selected; 
            }
        }

        public Bouton[] Boutons
        {
            get 
            { 
                return boutons; 
            }
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
                    screenManager.ChangeGameScreen(new MainScreen(screenManager));
                    break;
                case 2:
                    screenManager.ChangeGameScreen(new OptionsScreen(screenManager));
                    break;
                case 3:
                    screenManager.Game.Exit();
                    break;
            }
        }
    }

    class OptionsMenu : Menu
    {
        protected Option[] tabOptions;

        public OptionsMenu(Bouton[] boutons, Option[] tabOptions)
            : base(boutons)
        {
            this.tabOptions = tabOptions;
        }

        protected override void Action(ScreenManager screenManager, int selected)
        {
            switch (selected)
            {
                case 0:
                    screenManager.Options.Volume = screenManager.Options.Volume + 1;
                    break;
                case 1:
                    screenManager.Options.Niveau = screenManager.Options.Niveau + 1;
                    break;
                case 2:
                    screenManager.ChangeGameScreen(new MenuScreen(screenManager));
                    break;
            }
        }

        public new void Click(ScreenManager screenManager, int x, int y)
        {
            int j = 0;

            for (int i = 0; i < tabOptions.Length; i++)
            {
                if (tabOptions[i].Box.Contains(x, y))
                {
                    Action(screenManager, j);
                }

                j++;
            }

            for (int i = 0; i < boutons.Length; i++)
            {
                if (boutons[i].Box.Contains(x, y))
                {
                    Action(screenManager, j);
                }

                j++;
            }
        }

        public new void Focus(int x, int y)
        {
            if (selected < 2)
            {
                tabOptions[selected].IsSelected = false;
            }
            else 
            {
                boutons[selected - 2].IsSelected = false;
            }

            int j = 0;

            for (int i = 0; i < tabOptions.Length; i++)
            {
                if (tabOptions[i].Box.Contains(x, y))
                {
                    this.selected = j;
                    tabOptions[i].IsSelected = true;
                }

                j++;
            }

            for (int i = 0; i < boutons.Length; i++)
            {
                if (boutons[i].Box.Contains(x, y))
                {
                    this.selected = j;
                    boutons[i].IsSelected = true;
                }

                j++;
            }
        }

        public Option[] TabOptions
        {
            get
            {
                return tabOptions;
            }
        }
    }
}
