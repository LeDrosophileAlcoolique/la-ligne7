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

        protected Vector3 direction;

        public Tir(Map map, ScreenManager screenManager, Joueur joueur)
            : base(map)
        {
            assetName = asset_name;
            position = new Vector3(joueur.Position.X, joueur.Position.Y - 0.15f, joueur.Position.Z);
            direction = Vector3.Transform(Vector3.Backward, joueur.CameraRotation());
            rotation = 0;
            taille = 0.10f;
            moveSpeed = 0.1f;

            LoadContent(screenManager.Content3D);
            box = GenerateBoundingBox(position);
        }

        protected bool IsKillEnemy(Map map)
        {
            foreach (MyList<Enemy>.Element enemy in map.ListEnemy.Enum())
            {
                if (box.Intersects(enemy.Value.Box))
                {
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
    }
}