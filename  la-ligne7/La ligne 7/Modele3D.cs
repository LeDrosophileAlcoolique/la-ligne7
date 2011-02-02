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
    class Modele3D
    {
        protected Model model;
        protected Vector3 position;
        protected BoundingBox box;
        protected string assetName;

        public Modele3D()
        {
            
        }

        public void LoadContent(ContentManager Content)
        {
            // On charge le model
            model = Content.Load<Model>(assetName);
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

        public BoundingBox Box
        { 
            get
            {
                return box;
            }
        }
    }

    class ModelDeplacement : Modele3D
    {
        protected float speed;
        protected BoundingBox nextBox;

        public ModelDeplacement()
            : base()
        {

        }

        public bool IsCollision(BoundingBox cible, Vector3 translation)
        {
            Vector3 prevision = position + translation;

            nextBox = new BoundingBox(prevision - new Vector3(50, 50, 50), prevision + new Vector3(50, 50, 50));

            return nextBox.Intersects(cible);
        }

        protected void Update(Vector3 translation)
        {
            position += translation;

            box = nextBox;
        }
    }

    class Terrain : Modele3D
    {
        public Terrain()
            : base()
        {
            position = new Vector3(0, 0, 0);
            assetName = "Field";
        }
    }
}
