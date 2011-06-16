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
    class MainScreen : GameScreen
    {
        protected Curseur curseur;
        protected Ptdevie ptDeVie;
        protected Map map;

        protected Debug debug;

        public MainScreen(ScreenManager screenManager)
            : base(screenManager)
        {
            curseur = new Curseur();
            ptDeVie = new Ptdevie();
            map = new Map(screenManager);

            debug = new Debug("Debug", 0, 0);
        }

        public override void LoadContent()
        {
            curseur.LoadContent(screenManager.ContentImage, screenManager.Graphics);
            ptDeVie.LoadContent(screenManager.ContentImage);
            map.LoadContent();

            debug.LoadFont(screenManager.ContentFont);
        }

        public override void Update(GameTime gameTime)
        {
            if (screenManager.Game1.Clavier.IsNewKeyPress(Keys.P))
            {
                screenManager.ChangeGameScreen(new PauseScreen(screenManager));
            }
            else
            {
                map.Update(gameTime);

                if (screenManager.Options.Debug == 1)
                    debug.Update(map.Joueur.Vie.ToString() + "; " + map.Joueur.CameraYawX.ToString() + "; " + map.Joueur.CameraYawY.ToString() + "; " + map.Joueur.Position.ToString());

                screenManager.Game1.Souris.AuCentre(screenManager.Graphics);
            }
        }

        public override void Draw(GameTime gameTime)
        {
            screenManager.Game.GraphicsDevice.Clear(Color.White);

            map.Draw(gameTime);

            screenManager.SpriteBatch.Begin();
            ptDeVie.Draw(screenManager.SpriteBatch, map.Joueur);
            curseur.Draw(screenManager.SpriteBatch, Color.RoyalBlue, 0.6f);

            if (screenManager.Options.Debug == 1)
                debug.Draw(screenManager.SpriteBatch);
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
