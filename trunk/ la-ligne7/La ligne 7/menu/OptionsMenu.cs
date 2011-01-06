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
    class OptionsMenu : Menu
    {
        public OptionsMenu(Bouton[] boutons) 
            : base(boutons)
        {
        }

        public override void pressEnter(ScreenManager screenManager)
        {
            switch (selected)
            {
                case 2:
                    screenManager.ChangeGameScreen(new MenuScreen(screenManager));
                    break;
            }
        }
    }
}
