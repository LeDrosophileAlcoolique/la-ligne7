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
    class Modele
    {

        Model model;
        Vector3 modelePosition;
        //Random rand;
        

        
        

        public Modele(ContentManager Content, List<Modele> liste_modele, float i)
        {
            Vector3 modeleRef = new Vector3(i, 0.0f, i);

            // On charge le model
            model = Content.Load<Model>("Zombie");

            //modelePosition = new Vector3(rand.Next(10), -1.0f, rand.Next(10));
            //modelePosition = new Vector3((-3.0f + i), (-3.0f + i), -650.0f);
            modelePosition = modeleRef;
            //modelePosition = new Vector3((0.0f + i), (0.0f + i), 300.0f);
           
        }

        public Vector3 Position
        {
            get { return modelePosition; } 
        }


        public void Update(Viewport vp)
        {
        }

        public void Draw(Matrix Projection, Vector3 position, Vector3 cible)
        {
            //On dessine le mur
            Matrix[] transforms = new Matrix[model.Bones.Count];
            model.CopyAbsoluteBoneTransformsTo(transforms);

            foreach (ModelMesh mesh in model.Meshes)
            {
                foreach (BasicEffect effect in mesh.Effects)
                {
                    effect.Projection = Projection;
                    effect.View = Matrix.CreateLookAt(position, cible, Vector3.Up);
                    effect.World = transforms[mesh.ParentBone.Index] * Matrix.CreateTranslation(modelePosition);
                }
                mesh.Draw();
            }
        }
        
    }
}

