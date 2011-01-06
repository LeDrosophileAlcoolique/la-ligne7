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

        public Ecrire(string name, float x, float y)
        {
            this.name = name;
            position = new Vector2(10, y);
        }

        public void LoadFont(ContentManager content, string font)
        {
            this.font = content.Load<SpriteFont>(font);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            Color couleur = Color.Black;

            spriteBatch.DrawString(font, name, position, couleur);
        }
    }
}
