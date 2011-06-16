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
    class Modele2D
    {
        protected Vector2 position;
        protected Texture2D texture;
        protected string assetName;

        public Modele2D(string assetName) 
        {
            this.assetName = assetName;
        }

        public Modele2D(string assetName, Vector2 position)
        {
            this.assetName = assetName;
            this.position = position;
        }

        public void LoadContent(RessourceManager<Texture2D> ressourceManager)
        {
            texture = ressourceManager.GetElement(assetName);
        }

        public void Update(int x, int y)
        {
            position = new Vector2(x - texture.Width / 2, y - texture.Height / 2);
        }

        public void Draw(SpriteBatch spriteBatch, Color color)
        {
            spriteBatch.Draw(texture, position, color);
        }

        public void Draw(SpriteBatch spriteBatch, Color color, float alpha)
        {
            spriteBatch.Draw(texture, position, new Color(color, alpha));
        }
    }

    class Train : Modele2D
    {
        public Train(Vector2 position)
            : base("Image/train", position)
        {
        }

        public void Update(GameTime gameTime)
        {
            position.X += gameTime.ElapsedGameTime.Milliseconds * 2;
        }
    }

    class Ptdevie : Modele2D
    {
        public Ptdevie()
            : base("Image/ptdevie", Vector2.Zero)
        { 
        }

        public void Draw(SpriteBatch spriteBatch, Joueur joueur)
        {
            spriteBatch.Draw(texture, position, new Color(Color.Black, 0.1f * (Joueur.nbr_vie - joueur.Vie)));
        }
    }

    class Curseur : Modele2D
    {
        public Curseur()
            : base ("Image/curseur")
        {
        }

        public void LoadContent(RessourceManager<Texture2D> ressourceManager, GraphicsDeviceManager graphics)
        {
            texture = ressourceManager.GetElement(assetName);
            position = new Vector2((graphics.GraphicsDevice.Viewport.Width - texture.Width) / 2, (graphics.GraphicsDevice.Viewport.Height - texture.Height) / 2);
        }
    }

    class Bouton : Modele2D
    {
        protected Rectangle rec;
        protected string fonction;

        protected Texture2D imgFocused;

        public bool IsFocused { get; set; }
        
        public Bouton(string fonction, string assetName, Vector2 position)
            : base (assetName, position)
        {
            this.fonction = fonction;
            IsFocused = false;
        }

        public new void LoadContent(RessourceManager<Texture2D> ressourceManager)
        {
            base.LoadContent(ressourceManager);
            imgFocused = ressourceManager.GetElement(assetName + " - active");
            rec = new Rectangle((int)position.X, (int)position.Y, texture.Width, texture.Height);
        }

        public new void Draw(SpriteBatch spriteBatch, Color color)
        {
            if (IsFocused)
                spriteBatch.Draw(imgFocused, position, color);
            else
                spriteBatch.Draw(texture, position, color);
        }

        public Rectangle Rec
        {
            get
            {
                return rec;
            }
        }

        public string Fonction
        {
            get
            {
                return fonction;
            }
        }
    }
}
