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

        public void SetBoutonSelected(int selected, int operation)
        {
            boutons[selected].IsSelected = false;
            int ieme = selected + operation;
            
            if (ieme >= nbrBouton)
            {
                ieme = 0;
            }

            if (ieme < 0)
            {
                ieme = nbrBouton - 1;
            }

            this.selected = ieme;
            boutons[ieme].IsSelected = true;
        }

        public abstract void pressEnter(ScreenManager screenManager);

        public int Selected
        { 
            get { return selected; }
        }

        public Bouton[] Boutons
        {
            get { return boutons; }
        }
    }
}
