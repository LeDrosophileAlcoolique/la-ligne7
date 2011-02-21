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

                switch (volume)
                { 
                    case 0:
                        MediaPlayer.Volume = 0;
                        break;
                    case 1:
                        MediaPlayer.Volume = 0.1f;
                        break;
                    case 2:
                        MediaPlayer.Volume = 1;
                        break;
                }
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
