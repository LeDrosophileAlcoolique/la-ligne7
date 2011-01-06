using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ligne7
{
    class debuge
    {

        public string debug(Joueur joueur, Ennemis ennemi)
        {
            string str;
            str = joueur.Position
                + "\n" + joueur.Target
                + "\n" + joueur.ang1
                + "\n" + joueur.ang2
                + "\n" + ennemi.position;
            return str;
        }
    }
}
