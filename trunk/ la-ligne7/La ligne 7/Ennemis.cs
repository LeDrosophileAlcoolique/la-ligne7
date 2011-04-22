#region Using
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
using System.Threading;
#endregion

namespace ligne7
{
    class Ennemis : ModelDeplacement
    {
        
        public BoundingBox zombiebox;
        public bool supr;
        protected bool isReadyAttack;
        protected Thread threadAttack;
        protected float turnSpeed = 1.0f;  // 100.0f



        public Ennemis(ContentManager content, Joueur joueur)
            : base()
        {
            speed = 0.03f;
            //position = new Vector3(225,0,225);
            position = new Vector3(70, 0, 70);
            
            //zombieJoueur = new Vector3 (position.X + 10.0f, position.Y, position.Z);
            float dX = joueur.Position.X - this.Position.X;
            float dZ = joueur.Position.Z - this.Position.Z;
            angle = (float)Math.Atan(dX/dZ);
            
            //On charge le modèle
            model = content.Load<Model>("Zombie");

            isReadyAttack = true;
            threadAttack = new Thread(new ThreadStart(funThreadAttack));
            threadAttack.Start();
            supr = false;
        }
        public bool IsCollisionEnnemis(List<Ennemis> listEnnemis)
        {
            bool isCollision = false;

            for (int i = 0; i < listEnnemis.Count; i++)
            {
                if (this != listEnnemis[i] && nextBox.Intersects(listEnnemis[i].zombiebox))
                    isCollision = true;
            }
            return isCollision;
        }
        // I.A.

        public float TuVasTournerConnard/*,ou pas?*/(Vector3 direction,
            float currentAngle, float turnSpeed)  //Vector3 dep, float produitScalaire
        {
            
            float x = direction.X;
            float y = direction.Z;

            float desiredAngle = (float)Math.Atan(x / y) /*+ MathHelper.PiOver2)*/;
            //float difference = WrapAngle(desiredAngle - currentAngle);                      // reactive pour rotation elaborée
            //difference = MathHelper.Clamp(difference, -turnSpeed, turnSpeed);               // reactive pour rotation elaborée
            //return WrapAngle(currentAngle + difference);
            //return droitDevant((direction, desiredAngle)+ MathHelper.Pi);  :fonctionnel mais inversée:

            //desiredAngle = WrapAngle(currentAngle + difference);  // ajouté pour rotation elaborée

            desiredAngle = droitDevant(direction, desiredAngle);
            desiredAngle += MathHelper.Pi;
            return desiredAngle;

        }
        private static float WrapAngle(float radians)
        {
            while (radians < -MathHelper.Pi)
            {
                radians += MathHelper.TwoPi;
            }
            while (radians > MathHelper.Pi)
            {
                radians -= MathHelper.TwoPi;
            }
            return radians;
        }
        private static float droitDevant(Vector3 direction, float angle)
        {
            float z = direction.Z;

            if (z < 0)
                angle += MathHelper.Pi;
            return angle;
        }

        
        public void Suivre(Joueur joueur, GameTime gameTime, List<Ennemis> listEnnemis, List<ModelTerrain> listdecor)
        {
            zombiebox = new BoundingBox(position - new Vector3(5, 20, 5), position + new Vector3(5, 20, 5));

            int direction_x, direction_z;
            float speed = gameTime.ElapsedGameTime.Milliseconds * this.speed;
            Vector3 direction = position - joueur.Positioncam;
            Vector3 deplacement;

            direction_x = Direction(direction.X, speed);
            direction_z = Direction(direction.Z, speed);
            

            deplacement = new Vector3(direction_x * speed, 0, direction_z * speed);

            Vector3 dep = joueur.Position - this.Position;

           
                if (!joueur.bbpos.Intersects(this.zombiebox))
                {
                    if (!IsCollisionEnnemis(listEnnemis) && !IsCollisiondecor(listdecor, this.zombiebox))
                    {
                        angle = TuVasTournerConnard(dep, angle, turnSpeed);
                        Update(deplacement);                                   // 15/04 test
                        
                    }
                }
                else
                {
                    if (isReadyAttack)
                    {
                        //Perdre des points de vie
                        joueur.Vie = joueur.Vie - 1;
                        isReadyAttack = false;
                    }
                }
            

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

        public void funThreadAttack()
        {
            // Tant que le thread n'est pas tué, on travaille
            while (Thread.CurrentThread.IsAlive)
            {
                if (!isReadyAttack)
                {
                    // Attente de 500 ms
                    Thread.Sleep(500);

                    isReadyAttack = true;
                }
            }
        }
    }
}
