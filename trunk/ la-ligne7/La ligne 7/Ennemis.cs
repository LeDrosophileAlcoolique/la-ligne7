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
    class Ennemis
    {
        protected Model model;
        protected Vector3 position;
        protected float speed;

        public Ennemis()
        {
            speed = 0.03f;
            position = Vector3.Zero;
        }

        // Méthode pour charger le modèle

        public void LoadContent(ContentManager content)
        {
            model = content.Load<Model>("cube");
        }

        protected void Update(Vector3 translation)
        {
            position += translation;
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

            deplacement = new Vector3(direction_x * speed / 3, 0, direction_z * speed);

            Update(deplacement);
        }

        protected int Direction(float direction, float speed)
        {
            int retourne;

            if (direction < -speed / 2)
                retourne = 1;
            else if (direction > speed / 2)
                retourne = -1;
            else
                retourne = 0;

            return retourne;
        }

        public void Draw(Joueur joueur)
        {
            // Matrice squelette ? Surement lol
            Matrix[] transforms = new Matrix[model.Bones.Count];
            model.CopyAbsoluteBoneTransformsTo(transforms);

            // Dessine le modèle
            foreach (ModelMesh mesh in model.Meshes)
            {
                foreach (BasicEffect effect in mesh.Effects)
                {
                    // Lumiere
                    effect.EnableDefaultLighting();

                    // Effect world = possibilite de faire rotation, translation...
                    effect.World = transforms[mesh.ParentBone.Index] * Matrix.CreateTranslation(position);

                    // Vue du modèle en perspective
                    effect.Parameters["Projection"].SetValue(joueur.Projection);

                    // Camera
                    effect.Parameters["View"].SetValue(joueur.View);
                }

                mesh.Draw();
            }
        }
    }
}
