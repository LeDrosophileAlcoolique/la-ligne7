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
#endregion

namespace ligne7
{
    class Enemy : ModelDeplacement
    {
        protected new const string asset_name = "FBX/zombie";

        protected int timerAttack = 2000;

        public Enemy(Map map, ScreenManager screenManager)
            : base (map)
        {
            assetName = asset_name;
            rotation = 0;
            taille = 0.25f;

            moveSpeed = 0.005f;
            rotationSpeed = 0.00035f;

            LoadContent(screenManager.Content3D);

            Random rand = new Random();

            do
            {
                int signeX = CalculSigne(rand.Next(2));
                int signeY = CalculSigne(rand.Next(2));

                this.position = new Vector3(signeX * rand.Next((int)map.Terrain.Box.Max.X), 0, signeY * rand.Next((int)map.Terrain.Box.Max.Z));
                box = GenerateBoundingBox(position);
            } while (IsCollision(box, map.ListModelDeplacement()) || IsCollisionJoueur(box, map.Joueur) || IsCollision(box, map.ListDecor()));
        }

        public Enemy(Map map, ScreenManager screenManager, Vector3 position)
            : base(map)
        {
            assetName = asset_name;
            rotation = 0;
            taille = 0.25f;
            this.position = position;

            moveSpeed = 0.005f;
            rotationSpeed = 0.00035f;

            LoadContent(screenManager.Content3D);
            box = GenerateBoundingBox(position);
        }

        public int CalculSigne(int rand)
        {
            if (rand == 0)
                return -1;

            return 1;
        }

        public new BoundingBox GenerateBoundingBox(Vector3 nextPosition)
        {
            /*
            Matrix[] transforms = new Matrix[model.Bones.Count];
            model.CopyAbsoluteBoneTransformsTo(transforms);

            BoundingBox genereBox = new BoundingBox();
            BoundingSphere sphere;

            foreach (ModelMesh mesh in model.Meshes)
            {
                sphere = mesh.BoundingSphere.Transform(transforms[mesh.ParentBone.Index] * Matrix.CreateRotationY(rotation) * Matrix.CreateScale(taille));
                genereBox = BoundingBox.CreateMerged(genereBox, BoundingBox.CreateFromSphere(sphere));
            }

            genereBox.Min += nextPosition;
            genereBox.Max += nextPosition;

            return genereBox;
            */

            return new BoundingBox(nextPosition - new Vector3(5, 20, 5), nextPosition + new Vector3(5, 20, 5));
        }

        public void IA(GameTime gameTime, Joueur joueur)
        {
            timerAttack += gameTime.ElapsedGameTime.Milliseconds;
            Suivre(gameTime, joueur);
        }

        protected bool IsCollisionJoueur(BoundingBox nextBox, Joueur joueur)
        {
            return nextBox.Intersects(joueur.Box);
        }

        protected void Avancement(Vector3 nextPosition, Joueur joueur, Vector3 avance)
        {
            BoundingBox nextBox = GenerateBoundingBox(nextPosition);
            bool isCollisionJoueur = IsCollisionJoueur(nextBox, joueur);

            if (IsCollision(nextBox, map.ListDecor()))
            {
                Vector3 newVector = new Vector3(avance.Z, 0, avance.X);
                Vector3 inverseNewVector = -newVector;

                Vector3 deltaNewVector = new Vector3(position.X + newVector.X - joueur.Position.X, 0, position.Z + newVector.Z - joueur.Position.Z);
                Vector3 deltaInverseNewVector = new Vector3(position.X + inverseNewVector.X - joueur.Position.X, 0, position.Z + inverseNewVector.Z - joueur.Position.Z);

                if (deltaNewVector.LengthSquared() <= deltaInverseNewVector.LengthSquared())
                {
                    nextPosition = position + newVector;
                }
                else
                {
                    nextPosition = position + inverseNewVector;
                }

                nextBox = GenerateBoundingBox(nextPosition);
            }
            
            if (!IsCollision(nextBox, map.ListModelDeplacement()) && !isCollisionJoueur)
            {
                position = nextPosition;
                box = nextBox;
            }

            if (isCollisionJoueur && timerAttack >= 2000)
            {
                joueur.Vie--;
                timerAttack = 0;
            }
        }

        protected float DeltaRadian(Joueur joueur, float deltaZ)
        {
            if (deltaZ >= 0)
                deltaZ = -1;
            else
                deltaZ = 1;

            Vector2 vector = new Vector2(joueur.Position.Z - position.Z, joueur.Position.X - position.X), reference = new Vector2(0, -1);
            float scalaire = vector.X * reference.X + vector.Y * reference.Y;
            return deltaZ * (float)Math.Acos(scalaire / (vector.Length() * reference.Length()));
        }

        protected void Suivre(GameTime gameTime, Joueur joueur)
        {
            Vector3 nextPosition = position, delta = position - joueur.Position;
            float speedParUpdate = moveSpeed * gameTime.ElapsedGameTime.Milliseconds;
            float radian = DeltaRadian(joueur, delta.Z);
            rotation = radian + MathHelper.PiOver2;

            if (delta.X <= 40 && delta.X >= -40 && delta.Z <= 40 && delta.Z >= -40)
            {
                delta.Normalize();

                Vector3 avance = new Vector3(delta.X * speedParUpdate, 0, delta.Z * speedParUpdate);
                nextPosition = position - avance;
                Avancement(nextPosition, joueur, avance);
            }
        }

        public float Rotation123
        {
            get
            {
                return rotation;
            }
        }
    }
}