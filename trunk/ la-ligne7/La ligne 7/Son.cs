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
    class Son
    {
        public Son()
        {
        }

        // Charge et joue une musique

        public void LoadContentAndPlay(ContentManager content)
        {
            MediaPlayer.Play(content.Load<Song>("psycraft"));
        }
    }
}
