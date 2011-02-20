using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ligne7
{
    class Options
    {
        protected int volume;
        protected int niveau;

        public Options()
        {
            volume = 1;
            niveau = 1;
        }

        public int Volume
        {
            set
            {
                volume = value % 3;
            }
            get
            {
                return volume;
            }
        }

        public int Niveau
        {
            set
            {
                niveau = value % 3;
            }
            get
            {
                return niveau;
            }
        }

        public string getNiveau()
        {
            string stringNiveau = "";

            switch (niveau)
            {
                case 0:
                    stringNiveau = "facile";
                    break;
                case 1:
                    stringNiveau = "normal";
                    break;
                case 2:
                    stringNiveau = "difficil";
                    break;
            }

            return stringNiveau;

        }
    }
}
