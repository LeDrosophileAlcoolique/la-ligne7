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
using Microsoft.Xna.Framework.Net;
using Microsoft.Xna.Framework.Storage;
#endregion

namespace ligne7
{
    class MyVideo
    {
        protected Video vid;
        protected VideoPlayer player;

        public MyVideo()
        { 

        }

        public void LoadVideo(string s, ContentManager content)
        {
            vid = content.Load<Video>(s);
            player = new VideoPlayer();
        }

        public void Update()
        {
            player.Play(vid);

            if (player.PlayPosition >= vid.Duration)
                player.Stop();
        }

        public void Draw(SpriteBatch spritebatch)
        {
            Texture2D videotext = null;

            if (player.State != MediaState.Stopped)
                videotext = player.GetTexture();

            if (videotext != null)
            {
                //spriteBatch.Begin();
                spritebatch.Draw(videotext, new Rectangle(300, 130, 390, 280), Color.White);
                //spriteBatch.End();
            }
        }
    }
}
