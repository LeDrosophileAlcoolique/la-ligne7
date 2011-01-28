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

        public Ecrire(string name, float x, float y)
        {
            this.name = name;
            assetNameFont = "Author";
            position = new Vector2(10, y);
        }

        public void LoadFont(ContentManager content)
        {
            this.font = content.Load<SpriteFont>(assetNameFont);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            Color couleur = Color.Black;

            spriteBatch.DrawString(font, name, position, couleur);
        }
    }

    class Bouton : Ecrire
    {
        protected float translation;
        protected bool isSelected;

        public Bouton(string name, float x, float y)
            : base(name, x, y)
        {
            assetNameFont = "MenuFont";
            translation = x;
            isSelected = false;
        }

        public void Translation(GameTime gameTime)
        {
            if (position.X < translation)
            {
                position += new Vector2(gameTime.ElapsedGameTime.Milliseconds * 0.2f, 0);
            }
        }

        public new void Draw(SpriteBatch spriteBatch)
        {
            Color couleur = Color.Black;

            if (isSelected)
                couleur = Color.Red;

            spriteBatch.DrawString(font, name, position, couleur);
        }

        public bool IsSelected
        {
            set 
            { 
                isSelected = value; 
            }
        }
    }
}
