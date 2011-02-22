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
        Vector3 epaule = Vector3.Zero;
        Vector3 direction = Vector3.Zero;
        public BoundingBox tirbox;
        
        public Tir(ContentManager Content, Joueur joueur)
        {
            speed = 1.0f;

            position = joueur.Positioncam - new Vector3(0.0f, 5.0f, 0.0f);
            direction = joueur.Target - joueur.Positioncam;

            // On charge le model
            model = Content.Load<Model>("balle");
        }

        public void PartirTresLoin(GameTime gameTime, List<Ennemis> listEnnemis)
        {
            tirbox = new BoundingBox(position - new Vector3(1, 1, 1), position + new Vector3(1, 1, 1));
            float speed = gameTime.ElapsedGameTime.Milliseconds * this.speed;
            Vector3 deplacement;

            deplacement = new Vector3(direction.X * speed, direction.Y * speed, direction.Z * speed);

            Update(deplacement);

            foreach (Ennemis ennemis in listEnnemis)
            {
                if (ennemis.zombiebox.Intersects(tirbox))
                    ennemis.supr = true;
            }
            
            // Gestion Tir, gros bourrin à améliorer 

            for (int i = 0; i < listEnnemis.Count; i++)
            {
                if (listEnnemis[i].supr)
                    listEnnemis.Remove(listEnnemis[i]);
            }
        }
    }
}

