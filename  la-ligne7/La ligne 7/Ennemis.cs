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
    class Ennemis : ModelDeplacement
    {
        public Ennemis(ContentManager content)
            : base()
        {
            speed = 0.03f;
            position = Vector3.Zero;

            // On charge le modèle
            model = content.Load<Model>("Balle");
        }

        // Ennemis ne nous suivent pas en Y car les zombies ne sautent pas 

        public void Suivre(Joueur joueur, GameTime gameTime)
        {
            int direction_x, direction_z;
            float speed = gameTime.ElapsedGameTime.Milliseconds * this.speed;
            Vector3 direction = position - joueur.Position;
            Vector3 deplacement;

            direction_x = Direction(direction.X, speed);
            direction_z = Direction(direction.Z, speed);

            deplacement = new Vector3(direction_x * speed, 0, direction_z * speed);

            Update(deplacement);
        }

        protected int Direction(float direction, float speed)
        {
            int retourne;

            // speed / 2 permet a l'ennemi de ne pas se deplacer lorsqu'il arrive sur le joueur
            if (direction < - speed /2)
                retourne = 1;
            else if (direction > speed /2)
                retourne = -1;
            else
                retourne = 0;

            return retourne;
        }
    }
}