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
    class Joueur
    {
        protected Vector3 cible;
        protected Vector3 cameraTranslation;
        protected Vector3 cameraPosition;
        protected Vector3 cameraTarget;
        protected Matrix projection;
        protected Matrix view;

        protected double ang1;
        protected double ang2;

        protected bool IsEnTrainDeSauter;

        public Joueur(float aspectRatio)
        {
            cible = new Vector3(0, 0, 1);
            cameraTranslation = new Vector3(0.04f);
            cameraPosition = new Vector3(0, 35, -150.0f);
            cameraTarget = cameraPosition + cible;
            
            // Matrice de l'effet de vue en perspective
            projection = Matrix.CreatePerspectiveFieldOfView(MathHelper.ToRadians(45), aspectRatio, 1.0f, 10000.0f);

            // effect view = vue de la camera
            view = Matrix.CreateLookAt(cameraPosition, cameraTarget, Vector3.Up);

            IsEnTrainDeSauter = false;
        }

        
        public void Deplacement(int x, int y, ContentManager Content)
        {
            // Partie clavier
            KeyboardState clavier = Keyboard.GetState();

            // Permet d'avancer
            if (clavier.IsKeyDown(Keys.Z))
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
            if (clavier.IsKeyDown(Keys.Q))
            {
                cameraPosition.X += (float)(Math.Sin(ang2 + (Math.PI / 2)));
                cameraPosition.Z += (float)(Math.Cos(ang2 + (Math.PI / 2)));
            }

            //Permet d'aller a droite
            if (clavier.IsKeyDown(Keys.D))
            {
                cameraPosition.X += (float)(Math.Sin(ang2 - (Math.PI / 2)));
                cameraPosition.Z += (float)(Math.Cos(ang2 - (Math.PI / 2)));
            }

            // Partie souris
            MouseState mouseste = Mouse.GetState();

            // fait pivoter la camera selon le deplacement de la souris
            if (mouseste.Y > y + 10)
            {
                if (ang1 > -(Math.PI / 2))
                    ang1 -= (Math.PI / 90);
                cible.X = (float)(1 * Math.Sin(ang2));
                cible.Z = (float)(1 * Math.Cos(ang2));
                cible.Y = (float)(1 * Math.Sin(ang1));
            }

            if (mouseste.Y < y - 10)
            {
                if (ang1 < (Math.PI / 2))
                    ang1 += (Math.PI / 90);
                cible.X = (float)(1 * Math.Sin(ang2));
                cible.Z = (float)(1 * Math.Cos(ang2));
                cible.Y = (float)(1 * Math.Sin(ang1));
            }

            if (mouseste.X > x + 10)
            {
                ang2 -= (Math.PI / 90);
                cible.Z = (float)(1 * Math.Cos(ang2));
                cible.X = (float)(1 * Math.Sin(ang2));
            }

            if (mouseste.X < x - 10)
            {
                ang2 += (Math.PI / 90);
                cible.Z = (float)(1 * Math.Cos(ang2));
                cible.X = (float)(1 * Math.Sin(ang2));
            }

            // Saut optimisé mais à modifier si le jeu possede plusieurs étages

            if (cameraPosition.Y <= 35)
                IsEnTrainDeSauter = clavier.IsKeyDown(Keys.Space);

            if (IsEnTrainDeSauter)
            {
                cameraPosition.Y += 5f;
                IsEnTrainDeSauter = cameraPosition.Y < 65;
            }

            if (!IsEnTrainDeSauter && cameraPosition.Y > 35)
                cameraPosition.Y -= 0.5f;

            //positionne la cible de la camera en face de sa position
            cameraTarget = cameraPosition + cible;

            // effect view = vue de la camera
            view = Matrix.CreateLookAt(cameraPosition, cameraTarget, Vector3.Up);
        }

        public Vector3 Position
        {
            get 
            { 
                return cameraPosition; 
            }
        }

        public Matrix Projection
        {
            get 
            { 
                return projection; 
            }
        }

        public Matrix View
        {
            get 
            { 
                return view; 
            }
        }

        public Vector3 Target
        {
            get 
            { 
                return cameraTarget; 
            }
        }

    }
}