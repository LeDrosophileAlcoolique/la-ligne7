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
        protected const string asset_name = "FBX/pillier";

        // La 3D
        protected string assetName;
        protected Model model;

        // Information sur le modèle
        protected Vector3 position;
        protected float rotation;
        protected float taille;

        // Pour la collision
        protected BoundingBox box;

        // La carte (la classe qui a accès à tous les ennemis, joueur, ... etc)
        protected Map map;

        public Modele3D(Map map)
        {
            this.map = map;
        }

        public Modele3D(Map map, ScreenManager screenManager, Vector3 position, string assetName)
        {
            this.assetName = assetName;
            this.position = position;
            rotation = 0;
            taille = 0.3f;

            LoadContent(screenManager.Content3D);
            box = GenerateBoundingBox(position);
        }

        public static void FirstLoadContent(RessourceManager<Model> ressourceManager)
        {
            // On charge le Model
            ressourceManager.GetElement(asset_name);
        }

        public void LoadContent(RessourceManager<Model> ressourceManager)
        {
            // On charge le Model
            model = ressourceManager.GetElement(assetName);
        }

        public static void FirstLoadContent(RessourceManager<Model> ressourceManager, string assetName)
        {
            // On charge le Model
            ressourceManager.GetElement(assetName);
        }

        // La fonction Draw prend le joueur comme argment pour avoir les informations sur la caméra
        public void Draw(Joueur joueur)
        {
            Matrix[] transforms = new Matrix[model.Bones.Count];
            model.CopyAbsoluteBoneTransformsTo(transforms);

            // Dessine le modèle
            foreach (ModelMesh mesh in model.Meshes)
            {
                foreach (BasicEffect effect in mesh.Effects)
                {
                    // Lumière
                    effect.EnableDefaultLighting();
                    effect.LightingEnabled = true;

                    if (this is Enemy)
                        effect.DiffuseColor = Color.WhiteSmoke.ToVector3();
                    else
                    {
                        effect.DirectionalLight0.DiffuseColor = Color.Black.ToVector3();
                        effect.DirectionalLight0.Direction = new Vector3(1, 0, 1);
                        effect.DirectionalLight0.SpecularColor = Color.Black.ToVector3();

                        effect.DirectionalLight1.DiffuseColor = Color.Gray.ToVector3();
                        effect.DirectionalLight1.Direction = new Vector3(-1, 0, -1);
                        effect.DirectionalLight1.SpecularColor = Color.DarkGray.ToVector3();
                    }

                    effect.World = transforms[mesh.ParentBone.Index] * Matrix.CreateRotationY(rotation) * Matrix.CreateScale(taille) * Matrix.CreateTranslation(position);
                    effect.View = joueur.View;
                    effect.Projection = joueur.Projection;
                    mesh.Draw();
                }
            }
        }

        public BoundingBox GenerateBoundingBox(Vector3 nextPosition)
        {
            Matrix[] transforms = new Matrix[model.Bones.Count];
            model.CopyAbsoluteBoneTransformsTo(transforms);

            BoundingBox genereBox = new BoundingBox();
            BoundingSphere sphere;

            foreach (ModelMesh mesh in model.Meshes)
            {
                sphere = mesh.BoundingSphere.Transform(transforms[mesh.ParentBone.Index] * Matrix.CreateRotationY(rotation) * Matrix.CreateScale(taille));
                sphere.Center = new Vector3(sphere.Center.X / 2, sphere.Center.Y / 2, sphere.Center.Z / 2);
                sphere.Radius = sphere.Radius / 2;
                genereBox = BoundingBox.CreateMerged(genereBox, BoundingBox.CreateFromSphere(sphere));
            }

            genereBox.Min += nextPosition;
            genereBox.Max += nextPosition;

            return genereBox;
        }

        protected bool IsCollision(BoundingBox nextBox, IEnumerable<Modele3D> list)
        {
            foreach (Modele3D modele in list)
            {
                if (this != modele && nextBox.Intersects(modele.Box))
                    return true;
            }

            return false;
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
            set
            {
                position = value;
            }
        }

        public float Rotation
        {
            get
            {
                return rotation;
            }
            set
            {
                rotation = value;
            }
        }
    }

    class ModelDeplacement : Modele3D
    {
        // Vitesse
        protected float moveSpeed;
        protected float rotationSpeed;

        public ModelDeplacement(Map map)
            : base (map)
        { 
        }

        public void Avancement(Vector3 nextPosition)
        {
            BoundingBox nextBox = GenerateBoundingBox(nextPosition);

            if (!IsCollision(nextBox, map.ListModelDeplacement()))
            {
                position = nextPosition;
                box = nextBox;
            }
        }
    }

    class Munition : Modele3D
    {
        protected new const string asset_name = "FBX/famas";
        public const int init_rotation = 0;
        public const float init_taille = 0.25f;

        public Munition(Map map, ScreenManager screenManager)
            : base (map)
        {
            this.map = map;
            this.assetName = asset_name;
            rotation = init_rotation;
            taille = init_taille;

            LoadContent(screenManager.Content3D);

            Random rand = new Random();

            do
            {
                int signeX = CalculSigne(rand.Next(2));
                int signeY = CalculSigne(rand.Next(2));

                this.position = new Vector3(signeX * rand.Next((int)map.Terrain.Box.Max.X), 0, signeY * rand.Next((int)map.Terrain.Box.Max.Z));
                box = GenerateBoundingBox(position);
            } while (IsCollision(box, map.ListDecor()) || IsCollision(map.Joueur));
        }

        public Munition(Map map, ScreenManager screenManager, Vector3 position)
            : base(map)
        {
            this.map = map;
            this.assetName = asset_name;
            rotation = init_rotation;
            taille = init_taille;

            LoadContent(screenManager.Content3D);

            this.position = position;
            box = GenerateBoundingBox(position);
        }

        public int CalculSigne(int rand)
        {
            if (rand == 0)
                return -1;

            return 1;
        }

        public bool IsCollision(Joueur joueur)
        {
            return box.Intersects(joueur.Box);
        }
    }

    class Declancheur : Modele3D
    {
        protected new const string asset_name = "FBX/quai";

        public bool IsDeclanche { get; set; }

        public Declancheur(Map map, ScreenManager screenManager)
            : base (map)
        {
            this.map = map;
            this.assetName = asset_name;
            rotation = 0;
            taille = 1f;

            position = new Vector3(0, 0, -40);

            IsDeclanche = false;

            LoadContent(screenManager.Content3D);
            box = GenerateBoundingBox(position);
        }

        public bool IsCollision(Joueur joueur)
        {
            return box.Intersects(joueur.Box);
        }
    }

    class Terrain : Modele3D
    {
        public Terrain(Map map, ScreenManager screenManager, Vector3 position)
            : base (map)
        {
            this.assetName = "FBX/terrain";
            this.position = position;
            rotation = 0;
            taille = 0.3f;

            LoadContent(screenManager.Content3D);
            box = GenerateBoundingBox();
        }

        public BoundingBox GenerateBoundingBox()
        {
            return new BoundingBox(position - new Vector3(40, 0, 70), position + new Vector3(40, 150, 70));
        }
    }
}
