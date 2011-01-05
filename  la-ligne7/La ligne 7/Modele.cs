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
    class Modele
    {

        Model model;
        Vector3 modelePosition;
        Random rand;

        public Modele(ContentManager Content, List<Modele> liste_modele, float i)
        {
            // On charge le model
            model = Content.Load<Model>("Station");

            // On définit la position aléatoire du mur
            rand = new Random();

            //modelePosition = new Vector3(rand.Next(10), -1.0f, rand.Next(10));
            modelePosition = new Vector3(i, 0, i);

        }

        public void Update(Viewport vp)
        {
            
        }

        public void Draw(Matrix Projection, Matrix view, Vector3 cible)
        {
            //On déssine le mur
            Matrix[] transforms = new Matrix[model.Bones.Count];
            model.CopyAbsoluteBoneTransformsTo(transforms);

            foreach (ModelMesh mesh in model.Meshes)
            {
                foreach (BasicEffect effect in mesh.Effects)
                {
                    effect.Projection = Projection;
                    effect.View = view;
                    effect.World = transforms[mesh.ParentBone.Index] * Matrix.CreateTranslation(modelePosition);
                }
                mesh.Draw();
            }
        }

    }
}

