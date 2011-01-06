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
    class Bouton : Ecrire
    {
        protected float translation;
        protected bool isSelected;

        public Bouton(string name, float x, float y)
            : base(name, x, y)
        {
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
            set { isSelected = value; }
        }
    }
}
