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
    class Ecrire
    {
        protected string name;
        protected Vector2 position;
        protected SpriteFont font;
        protected string assetNameFont;

        protected Color couleur;

        public Ecrire(string name, string assetNameFont, float y)
        {
            this.name = name;
            position = new Vector2(10, y);
            couleur = Color.Black;
            this.assetNameFont = assetNameFont;
        }

        public Ecrire(string name, string assetNameFont, float x, float y)
        {
            this.name = name;
            position = new Vector2(x, y);
            this.assetNameFont = assetNameFont;
        }

        public void LoadFont(RessourceManager<SpriteFont> ressourceManager)
        {
            this.font = ressourceManager.GetElement(assetNameFont);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.DrawString(font, name, position, couleur);
        }

        public string Name
        {
            set
            {
                name = value;
            }
        }
    }

    class Lien : Ecrire
    {
        protected string fonction;
        protected Rectangle rec;

        protected Color colorFocused;

        public bool IsFocused { get; set; }

        public Lien(string name, string fonction, float x, float y)
            : base(name, "Police/menu", x, y)
        {
            this.fonction = fonction;
            couleur = Color.Black;
            colorFocused = new Color(237, 8, 8);
            IsFocused = false;
            rec = new Rectangle(0, (int)y, 265, 35);
        }

        public new void Draw(SpriteBatch spriteBatch)
        {
            if (IsFocused)
                spriteBatch.DrawString(font, name, position, colorFocused);
            else
                spriteBatch.DrawString(font, name, position, couleur);
        }

        public string Fonction
        {
            get
            {
                return fonction;
            }
            set
            {
                fonction = value;
            }
        }

        public Rectangle Rec
        {
            get
            {
                return rec;
            }
        }
    }

    class Debug : Ecrire
    {
        public Debug(string name, float x, float y)
            : base(name, "Police/debug", x, y)
        {
            couleur = Color.White;
        }

        public void Update(string name)
        {
            this.name = name;
        }
    }
}
