using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ligne7
{
    class Objectif
    {
        public int Nombre { get; set; }
        protected string objectif;
        public bool isVictoire { get; set; }

        protected Joueur joueur;

        public Objectif(string objectif)
        {
            Nombre = 0;
            this.objectif = objectif;
        }

        public Objectif(string objectif, Joueur joueur)
        {
            Nombre = 0;
            this.objectif = objectif;

            this.joueur = joueur;
        }

        public bool IsVictoire()
        {
            bool result = false;

            switch (objectif)
            { 
                case "Zombie":
                    result = Nombre <= 0;
                    break;
                case "Time":
                    result = Nombre <= 0;
                    break;
                case "Position":
                    result = joueur.Position.Z >= Nombre;
                    break;
            }

            return isVictoire || result;
        }

        public void Decrement(int value)
        {
            Nombre -= value;
        }

        public string Mission
        {
            get
            {
                return objectif;
            }
        }
    }
}
