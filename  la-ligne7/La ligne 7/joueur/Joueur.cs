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
    class Joueur : ModelDeplacement
    {
        protected Vector3 cible;
        protected Vector3 cameraTranslation;
        protected Vector3 cameraPosition;
        protected Vector3 cameraTarget;
        protected Vector3 directionMouv;
        protected Vector3 previsionPosition;
        protected Vector3 oldcameraPosition;
        protected Matrix projection;
        protected Matrix view;
        protected BoundingBox boxcam;
        protected BoundingBox nextBoxcam;

        protected double ang1;
        protected double ang2;
        protected float sautmax;

        protected bool IsEnTrainDeSauter;

        public Joueur(float aspectRatio)
        {
            cible = new Vector3(0, 0, 1);
            cameraTranslation = new Vector3(0.04f);
            cameraPosition = new Vector3(20, 25, -20);
            cameraTarget = cameraPosition + cible;
            
            // Matrice de l'effet de vue en perspective
            projection = Matrix.CreatePerspectiveFieldOfView(MathHelper.ToRadians(45), aspectRatio, 1.0f, 10000.0f);

            // effect view = vue de la camera
            view = Matrix.CreateLookAt(cameraPosition, cameraTarget, Vector3.Up);

            IsEnTrainDeSauter = false;
            directionMouv = previsionPosition - cameraPosition;
        }

        public bool IsCollisionEnnemis(List<Ennemis> listEnnemis)
        {
            bool isCollision = false;

            for (int i = 0; i < listEnnemis.Count; i++)
            {
                if (nextBoxcam.Intersects(listEnnemis[i].Box))
                    isCollision = true;
            }

            return isCollision;
        }

        public void Deplacement(int x, int y, ContentManager Content, List<Ennemis> listEnnemis, List<modelTerrain> listdecor, BoundingBox limit)
        {
            // Partie clavier
            KeyboardState clavier = Keyboard.GetState();

            previsionPosition = cameraPosition;

            // Permet d'avancer
            if (clavier.IsKeyDown(Keys.Z))
            {
                previsionPosition.X += cible.X;
                previsionPosition.Z += cible.Z;
            }

            //Permet de reculer
            if (clavier.IsKeyDown(Keys.S))
            {               
                previsionPosition.X -= cible.X;
                previsionPosition.Z -= cible.Z;
            }

            //Permet d'aller a gauche
            if (clavier.IsKeyDown(Keys.Q))
            {
                previsionPosition.X += (float)(Math.Sin(ang2 + (Math.PI / 2)));
                previsionPosition.Z += (float)(Math.Cos(ang2 + (Math.PI / 2)));
            }

            //Permet d'aller a droite
            if (clavier.IsKeyDown(Keys.D))
            {
                previsionPosition.X += (float)(Math.Sin(ang2 - (Math.PI / 2)));
                previsionPosition.Z += (float)(Math.Cos(ang2 - (Math.PI / 2)));
            }

            nextBoxcam = new BoundingBox(previsionPosition - new Vector3(5, 20, 5), previsionPosition + new Vector3(5, 20, 5));

            position = cameraPosition;
            if (!IsCollisionEnnemis(listEnnemis) && !IsCollisiondecor(listdecor, nextBoxcam) && IsCollision(limit, (previsionPosition-cameraPosition)))
                cameraPosition = previsionPosition;

            // Partie souris
            MouseState mouseste = Mouse.GetState();

            // fait pivoter la camera selon le deplacement de la souris
            

            if (mouseste.Y > y)
            {
                ang1 -= ((mouseste.Y -y)/10 *Math.PI / 80);
                if (!(ang1 > -(Math.PI / 2 + 0.1)))
                    cible.Y = (float)(1 * Math.Sin(ang1));
                else
                    ang1 += ((y - mouseste.Y) / 10 * Math.PI / 80);
            }

            if (mouseste.Y < y)
            {
                ang1 += ((y-mouseste.Y)/10* Math.PI / 80);
                if (!(ang1 < (Math.PI / 2) - 0.1))
                    cible.Y = (float)(1 * Math.Sin(ang1));
                else
                    ang1 -= ((mouseste.Y - y) / 10 * Math.PI / 80);
            }

            if (mouseste.X > x)
            {
                ang2 -= ((mouseste.X -x)/10 * Math.PI / 70);
                cible.Z = (float)(1 * Math.Cos(ang2) * Math.Cos(ang1));
                cible.X = (float)(1 * Math.Sin(ang2) * Math.Cos(ang1));
            }

            if (mouseste.X < x)
            {
                ang2 += ((x - mouseste.X)/ 10 * Math.PI / 70);
                cible.Z = (float)(1 * Math.Cos(ang2) * Math.Cos(ang1));
                cible.X = (float)(1 * Math.Sin(ang2) * Math.Cos(ang1));
            }


            // Saut optimisé mais à modifier si le jeu possede plusieurs étages

            if (cameraPosition.Y <= 25)
                IsEnTrainDeSauter = clavier.IsKeyDown(Keys.Space);

            if (!IsEnTrainDeSauter)
                sautmax = cameraPosition.Y + 15;

            if (IsEnTrainDeSauter)
            {
                cameraPosition.Y += 5f;
                IsEnTrainDeSauter = cameraPosition.Y <= sautmax;
            }

            if (!IsEnTrainDeSauter && IsCollisionsol(listdecor, nextBox))
                    cameraPosition.Y -= 1f;

            //positionne la cible de la camera en face de sa position
            cameraTarget = cameraPosition + cible;

            directionMouv = previsionPosition - cameraPosition;

            // effect view = vue de la camera
            view = Matrix.CreateLookAt(cameraPosition, cameraTarget, Vector3.Up);

            boxcam = nextBoxcam;

            oldcameraPosition = cameraPosition;
        }

        public Vector3 Positioncam
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

        public BoundingBox Boxcam
        {
            get
            {
                return boxcam;
            }
        }

        public double angl1
        {
            get
            {
                return ang1;
            }
        }
        public double angl2
        {
            get
            {
                return ang2;
            }
        }
    }
}