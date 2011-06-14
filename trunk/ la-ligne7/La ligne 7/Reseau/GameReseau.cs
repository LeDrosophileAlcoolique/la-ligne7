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
    class GameReseau : GameScreen
    {
        protected Curseur curseur;
        protected Map map;

        public GameReseau(ScreenManager screenManager)
            : base(screenManager)
        {
            curseur = new Curseur();
            map = new Map(screenManager);
        }

        public override void LoadContent()
        {
            curseur.LoadContent(screenManager.ContentImage, screenManager.Graphics);
            map.LoadContent();
        }

        public override void Update(GameTime gameTime)
        {
            map.Update(gameTime);
            screenManager.Game1.Souris.AuCentre(screenManager.Graphics);
        }

        public override void Draw(GameTime gameTime)
        {
            screenManager.Game.GraphicsDevice.Clear(Color.White);

            map.Draw(gameTime);

            screenManager.SpriteBatch.Begin();
            curseur.Draw(screenManager.SpriteBatch);
            screenManager.SpriteBatch.End();
        }

        public Map Map
        {
            get
            {
                return map;
            }
        }
    }
}
