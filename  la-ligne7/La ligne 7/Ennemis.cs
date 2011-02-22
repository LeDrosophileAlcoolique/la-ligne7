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
        Vector3 zombieView;   // la composante sur Y ne servira que de test pour que le zombie n'attaque pas le joueur en altitude.
        public bool iAmHungry;
        public BoundingBox zombiebox;
        public bool supr;

        protected bool isReadyAttack;
        protected Thread threadAttack;

        public Ennemis(ContentManager content)
            : base()
        {
            speed = 0.03f;
            position = new Vector3(70,0,70);
            zombieView = new Vector3(0,0, -1);
            iAmHungry = false;
            //zombieJoueur = new Vector3 (position.X + 10.0f, position.Y, position.Z);

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

        public void RotationZombie(Vector3 dep, float produitScalaire)  
        {
            //this.angle += 0.0005f;
            this.angle = (float)Math.Cos(dep.X);
            //this.angle = (float)Math.Acos(produitScalaire);
            zombieView.X = -(float)Math.Sin(Math.Cos(dep.X));
            zombieView.Z = (float)Math.Cos(Math.Cos(dep.X));
            zombieView.Y = 0;
            zombieView.Normalize();
        }

        // I.A.

        // Ennemis ne nous suivent pas en Y car les zombies ne sautent pas 

        public void Suivre(Joueur joueur, GameTime gameTime, List<Ennemis> listEnnemis, List<ModelTerrain> listdecor)
        {
            zombiebox = new BoundingBox(position - new Vector3(5, 20, 5), position + new Vector3(5, 20, 5));
            float produitScalaire;
            int direction_x, direction_z;
            float speed = gameTime.ElapsedGameTime.Milliseconds * this.speed;
            Vector3 direction = position - joueur.Positioncam;
            Vector3 deplacement;

            direction_x = Direction(direction.X, speed);
            direction_z = Direction(direction.Z, speed);

            deplacement = new Vector3(direction_x * speed, 0, direction_z * speed);

            Vector3 dep = joueur.Position - this.Position;
            dep.Normalize();
            //RotationZombie(dep);
            
            produitScalaire = dep.Z * zombieView.Z + dep.X * zombieView.X;

            if ((produitScalaire > 0.0f) || (iAmHungry))
            {
                if (!joueur.bbpos.Intersects(this.zombiebox))
                {
                    if (!IsCollisionEnnemis(listEnnemis) && !IsCollisiondecor(listdecor, this.zombiebox))
                    {
                        RotationZombie(dep, produitScalaire);
                        Update(deplacement);
                        iAmHungry = true;
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
