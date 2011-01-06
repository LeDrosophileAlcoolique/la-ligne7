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

// nouvelle version

namespace ligne7
{
    class Tir
    {

        Model balle;
        Vector3 tirPosition;
        Vector3 epaule = new Vector3(0, 0.0f, 0.0f);
        Vector3 direction = Vector3.Zero;
        float speed = 1.0f;
        
        public Tir(ContentManager Content, List<Tir> liste_tir, Vector3 spawnBalle, Vector3 vecteurTir)
        {
            // On charge le model
            balle = Content.Load<Model>("balle");

            //modelePosition = new Vector3(rand.Next(10), -1.0f, rand.Next(10));
            tirPosition = spawnBalle;// -epaule;

            direction = vecteurTir;

        }




        protected void Update(Vector3 translation)
        {
            tirPosition += translation;
        }

        // Ennemis ne nous suivent pas en Y car les zombies ne sautent pas 

        public void PartirTresLoin(GameTime gameTime)
        {
            float speed = gameTime.ElapsedGameTime.Milliseconds * this.speed;
            Vector3 deplacement;

            deplacement = new Vector3(direction.X * speed, direction.Y * speed, direction.Z * speed);

            Update(deplacement);
        }

        /*public void Draw2(Matrix Projection, Matrix view, Vector3 cible, List<Tir> list_tir, Tir shoot)
        {
           foreach (Tir tir in list_tir)
                shoot.Draw(Projection, view, cible);
        }*/

        public void Draw(Matrix Projection, Matrix view, Vector3 cible)
        {
            //On dessine le mur
            Matrix[] transforms = new Matrix[balle.Bones.Count];
            balle.CopyAbsoluteBoneTransformsTo(transforms);

            foreach (ModelMesh mesh in balle.Meshes)
            {
                foreach (BasicEffect effect in mesh.Effects)
                {
                    effect.Projection = Projection;
                    effect.View = view;
                    effect.World = transforms[mesh.ParentBone.Index] * Matrix.CreateTranslation(tirPosition);
                }
                mesh.Draw();
            }
        }

    }
}

