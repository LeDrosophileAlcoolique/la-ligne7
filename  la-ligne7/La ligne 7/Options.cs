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
                volume = value;
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
                niveau = value;
            }
            get
            {
                return niveau;
            }
        }
    }
}
