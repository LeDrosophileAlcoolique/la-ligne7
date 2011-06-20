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
    class Tir : ModelDeplacement
    {
        protected new const string asset_name = "FBX/balle";
        protected const int init_rotation = 0;
        protected const float init_taille = 0.10f;
        protected const float init_moveSpeed = 0.1f;

        protected Vector3 direction;

        public Tir(Map map, ScreenManager screenManager, Joueur joueur)
            : base(map)
        {
            assetName = asset_name;
            position = new Vector3(joueur.Position.X, joueur.Position.Y - 0.15f, joueur.Position.Z);
            direction = Vector3.Transform(Vector3.Backward, joueur.CameraRotation());
            rotation = init_rotation;
            taille = init_taille;
            moveSpeed = init_moveSpeed;

            LoadContent(screenManager.Content3D);
            box = GenerateBoundingBox(position);
        }

        public Tir(Map map, ScreenManager screenManager, Vector3 position)
            : base(map)
        {
            assetName = asset_name;
            this.position = position;
            rotation = init_rotation;
            taille = init_taille;
            moveSpeed = init_moveSpeed;

            LoadContent(screenManager.Content3D);
            box = GenerateBoundingBox(position);
        }

        public Tir(Map map, ScreenManager screenManager, Vector3 position, Vector3 direction)
            : base(map)
        {
            assetName = asset_name;
            this.position = position;
            rotation = init_rotation;
            taille = init_taille;
            moveSpeed = init_moveSpeed;

            this.direction = direction;

            LoadContent(screenManager.Content3D);
            box = GenerateBoundingBox(position);
        }

        protected bool IsKillEnemy(Map map)
        {
            foreach (MyList<Enemy>.Element enemy in map.ListEnemy.Enum())
            {
                if (box.Intersects(enemy.Value.Box))
                {
                    if (map.Nivo.Objectif.Mission == "Zombie")
                        map.Nivo.Objectif.Decrement(1);
                    map.ListEnemy.Delete(enemy);
                    return true;
                }
            }

            return false;
        }

        protected bool TropLoin(Joueur joueur)
        {
            const int distance = 5000;
            Vector3 vector = joueur.Position - position;

            if (vector.X >= distance || vector.X <= -distance || vector.Y >= distance || vector.Y <= -distance || vector.Z >= distance || vector.Z <= -distance)
                return true;
            else
                return false;
        }

        public bool ParirTresLoin(GameTime gameTime, Map map)
        {
            position = position + direction * moveSpeed * gameTime.ElapsedGameTime.Milliseconds;
            box = GenerateBoundingBox(position);

            return IsKillEnemy(map) || TropLoin(map.Joueur) || IsCollision(box, map.ListDecor());
        }

        public Vector3 Direction
        {
            get
            {
                return direction;
            }
        }
    }
}