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
using Microsoft.Xna.Framework.Net;
using Microsoft.Xna.Framework.Storage;
#endregion

namespace ligne7
{
    abstract class Niveau
    {
        protected Objectif objectif;

        public Niveau(string objectif)
        {
            this.objectif = new Objectif(objectif);
        }

        public abstract void LoadMap(Map map, ScreenManager screenManager);

        public Objectif Objectif
        {
            get
            {
                return objectif;
            }
        }
    }

    class Nivo1 : Niveau
    {
        public Nivo1()
            : base("Zombie")
        {
            this.objectif.NbrZombieTue = 20;
        }

        public override void LoadMap(Map map, ScreenManager screenManager)
        {
            map.Terrain = new Terrain(map, screenManager, Vector3.Zero);

            map.ListDecorGS.Add(new Modele3D(map, screenManager, new Vector3(30, 0, 60), "FBX/pillier"));
            map.ListDecorGS.Add(new Modele3D(map, screenManager, new Vector3(30, 0, 30), "FBX/pillier"));
            map.ListDecorGS.Add(new Modele3D(map, screenManager, new Vector3(30, 0, -30), "FBX/pillier"));
            map.ListDecorGS.Add(new Modele3D(map, screenManager, new Vector3(30, 0, -60), "FBX/pillier"));

            map.ListDecorGS.Add(new Modele3D(map, screenManager, new Vector3(0, 0, 60), "FBX/pillier"));
            map.ListDecorGS.Add(new Modele3D(map, screenManager, new Vector3(0, 0, 30), "FBX/pillier"));
            map.ListDecorGS.Add(new Modele3D(map, screenManager, new Vector3(0, 0, -30), "FBX/pillier"));
            map.ListDecorGS.Add(new Modele3D(map, screenManager, new Vector3(0, 0, -60), "FBX/pillier"));

            map.ListDecorGS.Add(new Modele3D(map, screenManager, new Vector3(-30, 0, 60), "FBX/pillier"));
            map.ListDecorGS.Add(new Modele3D(map, screenManager, new Vector3(-30, 0, 30), "FBX/pillier"));
            map.ListDecorGS.Add(new Modele3D(map, screenManager, new Vector3(-30, 0, -30), "FBX/pillier"));
            map.ListDecorGS.Add(new Modele3D(map, screenManager, new Vector3(-30, 0, -60), "FBX/pillier"));

            map.ListDeclancheur.Add(new Declancheur(map, screenManager));
        }
    }
}
