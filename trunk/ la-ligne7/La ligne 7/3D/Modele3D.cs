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

        protected int a2;
        protected int b2;
        protected int c2;

        protected BoundingBox box2;
        protected BoundingBox box3;

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

            a2 = 3;
            b2 = 100;
            c2 = 3;

            LoadContent(screenManager.Content3D);
            box = GenerateBoundingBox(position);
        }

        public Modele3D(Map map, ScreenManager screenManager, Vector3 position, string assetName, int a2, int b2, int c2)
        {
            this.assetName = assetName;
            this.position = position;
            rotation = 0;
            taille = 0.3f;

            this.a2 = a2;
            this.b2 = b2;
            this.c2 = c2;

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

            if (assetName == "FBX/train")
            {
                box = GenerateBoundingBoxspecial(position, 20, 15, 50);
                box2 = GenerateBoundingBoxspecial(position + new Vector3(0, 0, -40), 7, 15, 9);
                box3 = GenerateBoundingBoxspecial(position + new Vector3(0, 0, 40), 7, 15, 9);
            }
            else
            {
                box = GenerateBoundingBox(position);
            }
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
            /*
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
            */

            return new BoundingBox(position - new Vector3(a2, 0, c2), position + new Vector3(a2, b2, c2));
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

        public BoundingBox GenerateBoundingBoxspecial(Vector3 position, int a, int b, int c)
        {
            return new BoundingBox(position - new Vector3(a, 0, c), position + new Vector3(a, b, c));
        }

        public BoundingBox Box
        {
            get
            {
                return box;
            }
        }

        public BoundingBox Box2
        {
            get
            {
                return box2;
            }
        }
        public BoundingBox Box3
        {
            get
            {
                return box3;
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

    class Joueur2 : Modele3D
    {
        public Joueur2(Map map, ScreenManager screenManager, Vector3 position)
            : base(map, screenManager, position, "FBX/player")
        {
            box = GenerateBoundingBox(position);
        }

        public void Update()
        {
            box = GenerateBoundingBox(position);
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

        protected new bool IsCollision(BoundingBox nextBox, IEnumerable<Modele3D> list)
        {
            foreach (Modele3D modele in list)
            {
                if (this != modele && (nextBox.Intersects(modele.Box) || (modele.Box2 != new BoundingBox(Vector3.Zero, Vector3.Zero) && nextBox.Intersects(modele.Box2)) || (modele.Box3 != new BoundingBox(Vector3.Zero, Vector3.Zero) && nextBox.Intersects(modele.Box3))))
                    return true;
            }

            return false;
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

        public bool IsCollision(Modele3D cible)
        {
            return box.Intersects(cible.Box);
        }

        public new BoundingBox GenerateBoundingBox(Vector3 nextPosition)
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

        public new BoundingBox GenerateBoundingBox(Vector3 nextPosition)
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
    }

    class Terrain : Modele3D
    {
        protected int a;
        protected int b;
        protected int c;

        public Terrain(Map map, ScreenManager screenManager, Vector3 position, string s, int i, int j, int k)
            : base (map)
        {
            this.assetName = s;
            a = i;
            b = j;
            c = k;

            this.position = position;
            rotation = 0;
            taille = 0.3f;

            LoadContent(screenManager.Content3D);
            box = GenerateBoundingBox();
        }

        public BoundingBox GenerateBoundingBox()
        {
            return new BoundingBox(position - new Vector3(a, 0, c), position + new Vector3(a, b, c));
        }
    }

    class Box : Modele3D
    {
        public Box(Map map, ScreenManager screenManager, Vector3 position)
            : base(map, screenManager, position, "FBX/box", 1, 10, 1)
        { 
        
        }
    }

    class Gravier : Modele3D
    {
        public Gravier(Map map, ScreenManager screenManager, Vector3 position)
            : base(map, screenManager, position, "FBX/gravier", 2, 10, 2)
        {

        }
    }

    class Wall : Modele3D
    {
        public Wall(Map map, ScreenManager screenManager, Vector3 position)
            : base(map, screenManager, position, "FBX/wall", 2, 10, 1)
        {

        }
    }

    class Pillier2 : Modele3D
    { 
        public Pillier2(Map map, ScreenManager screenManager, Vector3 position)
            : base(map, screenManager, position, "FBX/pillier2", 1, 100, 1)
        {

        }
    }

    /*
    class Train3D : Modele3D
    {
        protected BoundingBox box2;
        protected BoundingBox box3;

        public Train3D(Map map, ScreenManager screenManager, Vector3 position)
            : base(map, screenManager, position, "FBX/train")
        {

        }

        public new void LoadContent(RessourceManager<Model> ressourceManager)
        {
            // On charge le Model
            model = ressourceManager.GetElement(assetName);

            box = GenerateBoundingBoxspecial(position, 7, 15, 10);
            box2 = GenerateBoundingBoxspecial(position + new Vector3(0, 0, -40), 7, 15, 9);
            box3 = GenerateBoundingBoxspecial(position + new Vector3(0, 0, 40), 7, 15, 9);
        }

        public BoundingBox GenerateBoundingBoxspecial(Vector3 position, int a, int b, int c)
        {
            return new BoundingBox(position - new Vector3(a, 0, c), position + new Vector3(a, b, c));
        }

        public BoundingBox Box2
        {
            get
            {
                return box2;
            }
        }
        public BoundingBox Box3
        {
            get
            {
                return box3;
            }
        }
    }
    */
}
