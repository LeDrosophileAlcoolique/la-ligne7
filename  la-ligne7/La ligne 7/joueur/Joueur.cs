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
        public BoundingBox bbpos;

        protected int vie;
        protected ScreenManager screenManager;

        protected float rotspeed = 0.3f;
        public float sautmax;
        public bool estausol;

        public bool IsEnTrainDeSauter;

        public Joueur(float aspectRatio, ScreenManager screenManager)
        {
            vie = 10;
            this.screenManager = screenManager;

            cible = new Vector3(0, 0, 1);
            cameraTranslation = new Vector3(0.04f);
            cameraPosition = new Vector3(50, 25, -20);
            //cameraPosition = new Vector3(205, 25, 135);
            cameraTarget = cameraPosition + cible;
            estausol = true;
            
            // Matrice de l'effet de vue en perspective
            projection = Matrix.CreatePerspectiveFieldOfView(MathHelper.ToRadians(45), aspectRatio, 1.0f, 10000.0f);

            // effect view = vue de la camera
            view = Matrix.CreateLookAt(cameraPosition, cameraTarget, Vector3.Up);

            IsEnTrainDeSauter = false;
            directionMouv = previsionPosition - cameraPosition;
            sautmax = cameraPosition.Y + 15;
        }

        public bool IsCollisionEnnemis(List<Ennemis> listEnnemis)
        {
            bool isCollision = false;

            for (int i = 0; i < listEnnemis.Count; i++)
            {
                if (nextBoxcam.Intersects(listEnnemis[i].zombiebox))
                    isCollision = true;
            }

            return isCollision;
        }

        public void Deplacement(int x, int y, ContentManager Content, List<Ennemis> listEnnemis, List<ModelTerrain> listdecor, List<ModelTerrain> listdecorinvers, GameTime gametime)
        {
            float timeDifference = (float)gametime.ElapsedGameTime.TotalMilliseconds / 1000.0f;
            float updownRot = 0.0f;
            float leftrightRot = 0.0f;
            Vector3 axeHorizon = Vector3.Transform(cible, Matrix.CreateRotationY((float)(-Math.PI / 2)));

            bbpos = new BoundingBox(cameraPosition - new Vector3(5, 20, 5), cameraPosition + new Vector3(5, 20, 5));
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
                previsionPosition.X -= axeHorizon.X;
                previsionPosition.Z -= axeHorizon.Z;
            }

            //Permet d'aller a droite
            if (clavier.IsKeyDown(Keys.D))
            {
                previsionPosition.X += axeHorizon.X;
                previsionPosition.Z += axeHorizon.Z;
            }

            nextBoxcam = new BoundingBox(previsionPosition - new Vector3(5, 20, 5), previsionPosition + new Vector3(5, 20, 5));

            //bloque les mouvement si collision pour chaque objet du decor
            position = cameraPosition;
            if (!IsCollisionEnnemis(listEnnemis) && !IsCollisiondecor(listdecor, nextBoxcam) && IsCollisiondecor(listdecorinvers, nextBoxcam) ) 
                cameraPosition = previsionPosition;

            // Partie souris
            MouseState mouseste = Mouse.GetState();

            // fait pivoter la camera selon le deplacement de la souris   
            
            if (mouseste.Y != y && updownRot > -80  && updownRot < 80)
                updownRot -= rotspeed * (mouseste.Y-y) * timeDifference;

            if (mouseste.X != x)
                leftrightRot -= rotspeed * (mouseste.X-x) * timeDifference;

            Matrix Rotation = Matrix.CreateFromAxisAngle(axeHorizon, updownRot) *Matrix.CreateRotationY(leftrightRot);
            cible = Vector3.Transform(cible, Rotation);

            


            // Saut optimisé ais toujours a ameliorer lorsque l'on saut sur un objet en hauteur
            if (IsCollisionsol(listdecor, nextBoxcam))
                estausol = true;
            else
                if( IsCollisionsol(listdecorinvers, nextBoxcam))
                    estausol = true;


            if (estausol)
            {
                IsEnTrainDeSauter = clavier.IsKeyDown(Keys.Space);
                sautmax = cameraPosition.Y + 15;
            }
                

            if (IsEnTrainDeSauter)
            {
                estausol = false;
                cameraPosition.Y += 5f;
                IsEnTrainDeSauter = cameraPosition.Y <= sautmax;
            }
             // gravite si le personnage n'est pas encore sur un objet
            if ((cameraPosition.Y > (sautmax - 15)) && !IsEnTrainDeSauter && (!estausol || IsCollisionsol2(listdecorinvers, nextBoxcam)))
            {
                cameraPosition.Y--;
            }

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

        public int Vie
        {
            set
            {
                if (value >= 0 && screenManager.Options.Vie)
                {
                    vie = value;
                }
            }
            get
            {
                return vie;
            }
        }
    }
}