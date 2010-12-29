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
    class Joueur
    {
        protected Vector3 cible;
        protected Vector3 cameraTranslation;
        protected Vector3 cameraPosition;
        protected Vector3 cameraTarget;
        protected Matrix projection;
        protected Matrix view;
        bool sol;
        float ang1;
        float ang2;

        public Joueur(float aspectRatio)
        {
            cible = new Vector3(1, 0, 0);
            cameraTranslation = new Vector3(20.0f);
            cameraPosition = new Vector3(-650, 0, 0);
            cameraTarget = Vector3.Zero;

            // Matrice de l'effet de vue en perspective
            projection = Matrix.CreatePerspectiveFieldOfView(MathHelper.ToRadians(45), aspectRatio, 1.0f, 10000.0f);

            // effect view = vue de la camera
            view = Matrix.CreateLookAt(cameraPosition, cameraTarget, Vector3.Up);
        }

        public void Deplacement(int x, int y)
        {
            // Partie clavier
            KeyboardState clavier = Keyboard.GetState();

            // Permet d'avancer
            if (clavier.IsKeyDown(Keys.W))
            {
                cameraPosition.X += cible.X;
                cameraPosition.Z += cible.Z;
            }

            //Permet de reculer
            if (clavier.IsKeyDown(Keys.S))
            {
                cameraPosition.X -= cible.X;
                cameraPosition.Z -= cible.Z;
            }

            //Permet d'aller a gauche
            if (clavier.IsKeyDown(Keys.A))
            {
                cameraPosition.X += (float)(Math.Cos(ang2 - 90));
                cameraPosition.Z += (float)(Math.Sin(ang2 - 90));
            }

            //Permet d'aller a droite
            if (clavier.IsKeyDown(Keys.D))
            {
                cameraPosition.X += (float)(Math.Cos(ang2 + 90));
                cameraPosition.Z += (float)(Math.Sin(ang2 + 90));
            }

            // Partie souris
            MouseState mouseste = Mouse.GetState();

            // fait pivoter la camera selon le deplacement de la souris
            if (mouseste.Y > y + 10)
            {
                ang1 -= 0.05f;
                cible.X = (float)(cible.X * Math.Cos(ang1));
                cible.Z = (float)(cible.Z * Math.Cos(ang1));
                cible.Y = (float)(1 * Math.Sin(ang1));
            }

            if (mouseste.Y < y - 10)
            {
                ang1 += 0.05f;
                cible.X = (float)(cible.X * Math.Cos(ang1));
                cible.Z = (float)(cible.Z * Math.Cos(ang1));
                cible.Y = (float)(1 * Math.Sin(ang1));
            }

            if (mouseste.X > x + 10)
            {
                ang2 += 0.05f;
                cible.X = (float)(1 * Math.Cos(ang2));
                cible.Z = (float)(1 * Math.Sin(ang2));
            }

            if (mouseste.X < x - 10)
            {
                ang2 -= 0.05f;
                cible.X = (float)(1 * Math.Cos(ang2));
                cible.Z = (float)(1 * Math.Sin(ang2));
            }

            if (sol == true && clavier.IsKeyDown(Keys.Space))
            {
                cameraPosition.Y += 10f;
                if (cameraPosition.Y < 50)
                {
                    sol = true;
                }
                else
                {
                    sol = false;
                }
            }

            //positionne la cible de la camera en face de sa position
            cameraTarget = cameraPosition + cible;

            // effect view = vue de la camera
            view = Matrix.CreateLookAt(cameraPosition, cameraTarget, Vector3.Up);

            if (cameraPosition.Y != 0)
            {
                cameraPosition.Y -= 0.5f;
            }
            else
            {
                sol = true;
            }
        }

        public Vector3 Position
        {
            get { return cameraPosition; }
        }

        public Matrix Projection
        {
            get { return projection; }
        }

        public Matrix View
        {
            get { return view; }
        }

        public Vector3 Target
        {
            get { return cameraTarget; }
        }

    }
}