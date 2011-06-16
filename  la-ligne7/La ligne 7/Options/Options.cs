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
using Microsoft.Xna.Framework.Storage;
#endregion

namespace ligne7
{
    class Options
    {
        protected Son son;

        protected int volume;
        protected int niveau;
        protected int vie;
        protected int nbrOptions;
        protected int debug;

        public Options(Son son)
        {
            volume = 1;
            niveau = 0;
            vie = 1;
            debug = 0;
            nbrOptions = 3;

            this.son = son;
        }

        public void ChangeOptionIncrement(string modif)
        {
            switch (modif)
            { 
                case "Modif volume":
                    Volume++;
                    son.ChangeVolume(GetVolume());
                    break;
                case "Modif niveau":
                    Niveau++;
                    break;
                case "Modif vie":
                    Vie++;
                    break;
            }
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

        public float GetVolume()
        {
            float result = 0;

            switch (volume)
            {
                case 1:
                    result = 0.1f;
                    break;
                case 2:
                    result = 1;
                    break;
            }

            return result;
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

        public int Vie
        {
            set
            {
                vie = value % 2;
            }
            get
            {
                return vie;
            }
        }

        public string GetNiveau()
        {
            string result = "";

            switch (niveau)
            {
                case 0:
                    result = "facile";
                    break;
                case 1:
                    result = "normal";
                    break;
                case 2:
                    result = "difficile";
                    break;
            }

            return result;
        }

        public int GetNiveauNbrZombie()
        {
            int result = 1;

            switch (niveau)
            {
                case 1:
                    result = 20;
                    break;
                case 2:
                    result = 40;
                    break;
            }

            return result;
        }

        public string GetVie()
        {
            string result = "";

            switch (vie)
            {
                case 0:
                    result = "non";
                    break;
                case 1:
                    result = "oui";
                    break;
            }

            return result;
        }

        public int Debug
        {
            set
            {
                debug = value % 2;
            }
            get
            {
                return debug;
            }
        }

        public int NbrOptions
        {
            get
            {
                return nbrOptions;
            }
        }
    }
}
