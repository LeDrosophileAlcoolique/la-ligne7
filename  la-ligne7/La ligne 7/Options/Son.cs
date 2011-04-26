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
        protected ScreenManager screenManager;

        protected Song son;
        protected string assetName;

        public Son(ScreenManager screenManager)
        {
            this.screenManager = screenManager;
            assetName = "Musique/18";
        }

        public void LoadContentAndPlay()
        {
            MediaPlayer.Volume = screenManager.Options.GetVolume();
            son = screenManager.ContentSong.GetElement(assetName);
            MediaPlayer.Play(son);
        }

        public void ChangeVolume(float volume)
        {
            MediaPlayer.Volume = volume;
        }
    }
}
