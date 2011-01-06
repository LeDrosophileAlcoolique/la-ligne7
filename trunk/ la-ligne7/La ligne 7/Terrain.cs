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
    class Terrain
    {
        protected Model field;
        protected Vector3 position;

        public Terrain()
        {
            position = Vector3.Zero - new Vector3(0,50.0f,0);
        }

        public void LoadContent(ContentManager Content)
        {
            // On charge le model
            field = Content.Load<Model>("field");
        }

        public void Draw(Joueur joueur)
        {
            // Matrice squelette ? Surement lol
            Matrix[] transforms = new Matrix[field.Bones.Count];
            field.CopyAbsoluteBoneTransformsTo(transforms);

            // Dessine le modèle
            foreach (ModelMesh mesh in field.Meshes)
            {
                foreach (BasicEffect effect in mesh.Effects)
                {
                    // Lumiere
                    effect.EnableDefaultLighting();

                    // Effect world = possibilite de faire rotation, translation...
                    effect.Parameters["World"].SetValue(true);
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
