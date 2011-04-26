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

        protected void Init(string name)
        {
            this.name = name;
            assetNameFont = "Police/author";
        }

        public Ecrire(string name, float y)
        {
            Init(name);
            position = new Vector2(10, y);
            couleur = Color.Black;
        }

        public Ecrire(string name, float x, float y)
        {
            Init(name);
            position = new Vector2(x, y);
        }

        public void LoadFont(RessourceManager<SpriteFont> ressourceManager)
        {
            this.font = ressourceManager.GetElement(assetNameFont);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.DrawString(font, name, position, couleur);
        }
    }

    class Bouton : Ecrire
    {
        protected float translation;
        public bool IsSelected { get; set; }
        public Rectangle Box { get; set; }

        public Bouton(string name, float x, float y)
            : base(name, y)
        {
            assetNameFont = "Police/menufont";
            translation = x;
            IsSelected = false;
            Box = new Rectangle((int)x - 200, (int)y, 500, 60);
        }

        public void Translation(GameTime gameTime)
        {
            if (position.X < translation)
            {
                position += new Vector2(gameTime.ElapsedGameTime.Milliseconds * 0.2f, 0);
            }
        }

        protected Color fontColor()
        {
            Color couleur = Color.Black;

            if (IsSelected)
                couleur = Color.Red;

            return couleur;
        }

        public new void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.DrawString(font, name, position, fontColor());
        }

        public void Draw(SpriteBatch spriteBatch, string value)
        {
            spriteBatch.DrawString(font, name + " : " + value, position, fontColor());
        }
    }

    class Debug : Ecrire
    {
        public Debug(string name, float x, float y)
            : base(name, x, y)
        {
            assetNameFont = "Police/debug";
            couleur = Color.White;
        }

        public void Update(string name)
        {
            this.name = name;
        }
    }
}
