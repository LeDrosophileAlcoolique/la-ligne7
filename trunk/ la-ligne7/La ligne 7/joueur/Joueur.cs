#region Using
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
#endregion

namespace ligne7
{
    class Joueur : ModelDeplacement
    {
        // Constante
        protected const float hauteur_des_yeux = 5.75f;
        protected const int nbr_par_munition = 25;

        protected ScreenManager screenManager;

        // Caméra
        protected Matrix projection;
        protected Vector3 cameraTarget;
        protected float cameraYawX;
        protected float cameraYawY;
        protected Matrix view;

        // Tir
        protected int nbrBalle;
        public int NbrMunition { get; set; }

        protected int vie = 10;
   
        public Joueur(Map map, ScreenManager screenManager)
            : base (map)
        {
            assetName = asset_name;
            this.screenManager = screenManager;
            projection = Matrix.CreatePerspectiveFieldOfView(MathHelper.PiOver4, screenManager.Graphics.GraphicsDevice.Viewport.Width / screenManager.Graphics.GraphicsDevice.Viewport.Height, 1, 500); // 180° se rapproche de l'oeil humain
            position = new Vector3(0, hauteur_des_yeux, 0);
            cameraTarget = new Vector3();
            cameraYawX = 0;
            cameraYawY = 0;

            moveSpeed = 0.01f;
            rotationSpeed = 0.00005f;

            taille = 0.25f;
            rotation = 0;

            LoadContent(screenManager.Content3D);
            box = GenerateBoundingBox(position - new Vector3(0, hauteur_des_yeux, 0));

            nbrBalle = nbr_par_munition;
            NbrMunition = 2;
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
                genereBox = BoundingBox.CreateMerged(genereBox, BoundingBox.CreateFromSphere(sphere));
            }

            genereBox.Min += nextPosition;
            genereBox.Max += nextPosition;

            return genereBox;
        }

        public Matrix CameraRotation()
        {
            return Matrix.CreateRotationX(cameraYawY) * Matrix.CreateRotationY(cameraYawX);
        }

        protected void UpdateView()
        {
            Matrix cameraRotation = CameraRotation();

            Vector3 cameraRotatedTarget = Vector3.Transform(Vector3.Backward, cameraRotation);
            cameraTarget = position + cameraRotatedTarget;

            Vector3 cameraRotatedUpVector = Vector3.Transform(Vector3.Up, cameraRotation);

            view = Matrix.CreateLookAt(position, cameraTarget, cameraRotatedUpVector);
        }

        protected new void Avancement(Vector3 nextPosition)
        {
            BoundingBox nextBox = GenerateBoundingBox(nextPosition - new Vector3(0, hauteur_des_yeux, 0));

            if (!IsCollision(nextBox, map.ListModelDeplacement()) && !IsCollision(nextBox, map.ListDecor()) && nextBox.Intersects(map.Terrain.Box))
            {
                position = nextPosition;
                box = nextBox;
                UpdateView();
            }
        }

        protected void Deplacement(GameTime gameTime)
        {
            Clavier clavier = new Clavier(screenManager.Game, Keyboard.GetState());
            Vector3 vector = Vector3.Zero;

            if (clavier.CurrentKeyboardState.IsKeyDown(Keys.Z) || clavier.CurrentKeyboardState.IsKeyDown(Keys.A) || clavier.CurrentKeyboardState.IsKeyDown(Keys.E))
                vector.Z = moveSpeed * gameTime.ElapsedGameTime.Milliseconds;

            if (clavier.CurrentKeyboardState.IsKeyDown(Keys.S))
                vector.Z = -moveSpeed * gameTime.ElapsedGameTime.Milliseconds;

            if (clavier.CurrentKeyboardState.IsKeyDown(Keys.Q) || clavier.CurrentKeyboardState.IsKeyDown(Keys.A))
                vector.X = moveSpeed * gameTime.ElapsedGameTime.Milliseconds;

            if (clavier.CurrentKeyboardState.IsKeyDown(Keys.D) || clavier.CurrentKeyboardState.IsKeyDown(Keys.E))
                vector.X = -moveSpeed * gameTime.ElapsedGameTime.Milliseconds;

            Vector3 rotatedVector = Vector3.Transform(vector, CameraRotation());

            rotatedVector.X += position.X;
            rotatedVector.Y = position.Y;
            rotatedVector.Z += position.Z;

            Avancement(rotatedVector);
        }

        protected void Rotation(GameTime gameTime)
        {
            Souris souris = new Souris(screenManager.Game, Mouse.GetState());

            cameraYawX += rotationSpeed * souris.DeltaX(screenManager.Graphics) * gameTime.ElapsedGameTime.Milliseconds;

            int deltaY = souris.DeltaY(screenManager.Graphics);

            // La caméra ne peut pas faire un tour complet en regardant en haut ou en bas ce qui est normal
            if ((cameraYawY >= -MathHelper.PiOver2 || deltaY <= 0) && (cameraYawY <= MathHelper.PiOver2 || deltaY >= 0))
                cameraYawY -= rotationSpeed * souris.DeltaY(screenManager.Graphics) * gameTime.ElapsedGameTime.Milliseconds;

            UpdateView();
        }

        public void Tir()
        {
            if (screenManager.Game1.Souris.IsNewClickPress() && nbrBalle >= 1)
            {
                map.ListTir.Add(new Tir(map, screenManager, this));
                nbrBalle--;
            }

            if (screenManager.Game1.Clavier.IsNewKeyPress(Keys.R) && NbrMunition >= 1)
            {
                NbrMunition--;
                nbrBalle = nbr_par_munition;
            }

            if (screenManager.Game1.Clavier.IsNewKeyPress(Keys.T))
                nbrBalle = nbr_par_munition;

            if (screenManager.Game1.Clavier.IsNewKeyPress(Keys.Y))
                map.ListEnemy = new MyList<Enemy>();
        }

        public void Update(GameTime gameTime)
        {
            Deplacement(gameTime);
            Rotation(gameTime);
            Tir();
        }

        public Matrix Projection
        {
            get
            {
                return projection;
            }
        }

        public Matrix View
        {
            get
            {
                return view;
            }
        }

        public Vector3 CameraTarget
        {
            get
            {
                return cameraTarget;
            }
        }

        public int Vie
        {
            set
            {
                if (value >= 0 && screenManager.Options.Vie == 1)
                {
                    vie = value;
                }
            }
            get
            {
                return vie;
            }
        }

        public float CameraYawX
        {
            get
            {
                return cameraYawX;
            }
        }

        public float CameraYawY
        {
            get
            {
                return cameraYawY;
            }
        }
    }
}
