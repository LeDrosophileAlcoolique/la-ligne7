﻿#region Using
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

        public Curseur() 
        {
        }

        public void LoadContent(ContentManager content, string assetName, GraphicsDeviceManager graphics)
        {
            texture = content.Load<Texture2D>(assetName);
            position = new Vector2((graphics.GraphicsDevice.Viewport.Width - texture.Width) / 2, (graphics.GraphicsDevice.Viewport.Height - texture.Height) / 2);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, position, Color.RoyalBlue);
        }
    }
}