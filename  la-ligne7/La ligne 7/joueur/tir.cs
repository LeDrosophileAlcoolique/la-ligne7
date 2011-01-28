﻿#region Using
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
    class Tir : ModelDeplacement
    {
        Vector3 epaule = new Vector3(0, 0.0f, 0.0f);
        Vector3 direction = Vector3.Zero;
        
        public Tir(ContentManager Content, Joueur joueur)
        {
            speed = 1.0f;

            position = joueur.Position - new Vector3(0.0f, 50.0f, 0.0f);
            direction = joueur.Target - joueur.Position;

            // On charge le model
            model = Content.Load<Model>("balle");
        }

        public void PartirTresLoin(GameTime gameTime)
        {
            float speed = gameTime.ElapsedGameTime.Milliseconds * this.speed;
            Vector3 deplacement;

            deplacement = new Vector3(direction.X * speed, direction.Y * speed, direction.Z * speed);

            Update(deplacement);
        }
    }
}
