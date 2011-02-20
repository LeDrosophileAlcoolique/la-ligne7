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
        public float angle; //pour la rotation
        protected BoundingBox box;
        protected string assetName;
        protected Texture2D texture;

        public Modele3D()
        {
            
        }

        public void LoadContent(ContentManager Content)
        {
            // On charge le model
            model = Content.Load<Model>(assetName);
            texture = Content.Load<Texture2D>("Stucco Wall");
        }


        public void Draw(Joueur joueur)
        {
            // Matrice squelette ? Surement lolaz\
            Matrix[] transforms = new Matrix[model.Bones.Count];
            model.CopyAbsoluteBoneTransformsTo(transforms);

            // Dessine le modèle
            foreach (ModelMesh mesh in model.Meshes)
            {
                foreach (BasicEffect effect in mesh.Effects)
                {
                    effect.TextureEnabled = false;
                    effect.Texture = texture;
                    // Lumiere
                    effect.EnableDefaultLighting();

                    // Effect world = possibilite de faire rotation, translation...
                    effect.Parameters["World"].SetValue(true);
                    effect.World = transforms[mesh.ParentBone.Index] * Matrix.CreateRotationY(3 * angle) * Matrix.CreateTranslation(position);

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
        public Vector3 Position
        {
            get
            {
                return position;
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

            nextBox = new BoundingBox(prevision - new Vector3(5, 20, 5), prevision + new Vector3(5, 20, 5));

            return nextBox.Intersects(cible);
        }

        protected void Update(Vector3 translation)
        {
            position += translation;

            box = nextBox;
        }

        // verifie la colision horizontal avec chaque objet du decor
        public bool IsCollisiondecor(List<ModelTerrain> listdecor, BoundingBox nextBox)
        {
            bool isCollision = false;

            foreach (ModelTerrain model in listdecor)
            {
                if (nextBox.Intersects(model.boxModel))
                    isCollision = true;
            }

            return isCollision;
        }

        // verifie si la box se situe au dessus d'un objet du decor 
        public bool IsCollisionsol(List<ModelTerrain> list, BoundingBox box)
        {
            bool sol = false;
            foreach (ModelTerrain decors in list)
            {
                if (box.Intersects(decors.boxModel))
                {
                    if (box.Min.Y > decors.boxModel.Min.Y - 2)
                        sol = true;
                }
            }
            return sol;
        }

    }

}
