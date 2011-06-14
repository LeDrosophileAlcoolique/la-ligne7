#region Using
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
#endregion

namespace ligne7
{
    class Curseur
    {
        protected Vector2 position;
        protected Texture2D texture;
        protected string assetName;

        public Curseur() 
        {
            assetName = "Image/curseur";
        }

        public Curseur(string assetName, Vector2 position)
        {
            this.assetName = assetName;
            this.position = position;
        }

        public void LoadContent(RessourceManager<Texture2D> ressourceManager, GraphicsDeviceManager graphics)
        {
            texture = ressourceManager.GetElement(assetName);
            position = new Vector2((graphics.GraphicsDevice.Viewport.Width - texture.Width) / 2, (graphics.GraphicsDevice.Viewport.Height - texture.Height) / 2);
        }

        public void LoadContentModel(RessourceManager<Texture2D> ressourceManager, GraphicsDeviceManager graphics)
        {
            texture = ressourceManager.GetElement(assetName);
        }

        public void Update(int x, int y)
        {
            position = new Vector2(x - texture.Width / 2, y - texture.Height / 2);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, position, new Color(Color.RoyalBlue, 0.1f));
        }

        public void DrawModel(SpriteBatch spriteBatch, Joueur joueur)
        {
            spriteBatch.Draw(texture, position, new Color(Color.Black, 0.1f * (Joueur.nbr_vie - joueur.Vie)));
        }
    }
}
