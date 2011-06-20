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
        protected string name;
        protected Objectif objectif;

        public Niveau(string objectif, string name)
        {
            this.name = name;
            this.objectif = new Objectif(objectif);
        }

        public Niveau(string objectif, string name, Joueur joueur)
        {
            this.name = name;
            this.objectif = new Objectif(objectif, joueur);
        }

        public abstract void LoadMap(Map map, ScreenManager screenManager);

        public Objectif Objectif
        {
            get
            {
                return objectif;
            }
        }

        public string Name
        {
            get
            {
                return name;
            }
        }
    }

    class Nivo1 : Niveau
    {
        public Nivo1()
            : base("Zombie", "nivo1")
        {
            this.objectif.Nombre = 20;
        }

        public override void LoadMap(Map map, ScreenManager screenManager)
        {
            map.Terrain = new Terrain(map, screenManager, Vector3.Zero, "FBX/terrain", 40, 150, 70);

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

    class Nivo2 : Niveau
    {
        public Nivo2(Joueur joueur, Joueur2 joueur2)
            : base("Position", "nivo2", joueur)
        {
            this.objectif.Nombre = 550;
            joueur.Position = new Vector3(0, Joueur.hauteur_des_yeux, -580);
            joueur2.Position = new Vector3(0, Joueur.hauteur_des_yeux, -570);
        }

        public override void LoadMap(Map map, ScreenManager screenManager)
        {
            map.Terrain = new Terrain(map, screenManager, Vector3.Zero, "FBX/nivo2", 5, 30, 590);

            map.ListDecorGS.Add(new Box(map, screenManager, new Vector3(5, 0, -520)));
            map.ListDecorGS.Add(new Box(map, screenManager, new Vector3(-5.7f, 0, -420)));
            map.ListDecorGS.Add(new Box(map, screenManager, new Vector3(4.3f, 0, -500)));
            map.ListDecorGS.Add(new Box(map, screenManager, new Vector3(-7.9f, 0, 520)));
            map.ListDecorGS.Add(new Box(map, screenManager, new Vector3(-2.4f, 0, -470)));
            map.ListDecorGS.Add(new Box(map, screenManager, new Vector3(2.2f, 0, -490)));
            map.ListDecorGS.Add(new Box(map, screenManager, new Vector3(1.3f, 0, -320)));
            map.ListDecorGS.Add(new Box(map, screenManager, new Vector3(-1.8f, 0, -400)));
            map.ListDecorGS.Add(new Box(map, screenManager, new Vector3(-7.6f, 0, 220)));
            map.ListDecorGS.Add(new Box(map, screenManager, new Vector3(2, 0, 520)));
            map.ListDecorGS.Add(new Box(map, screenManager, new Vector3(1, 0, 420)));
            map.ListDecorGS.Add(new Box(map, screenManager, new Vector3(-5.7f, 0, 320)));
            map.ListDecorGS.Add(new Box(map, screenManager, new Vector3(5.3f, 0, 500)));

            map.ListDecorGS.Add(new Gravier(map, screenManager, new Vector3(6.7f, 0, 490)));
            map.ListDecorGS.Add(new Gravier(map, screenManager, new Vector3(-5.7f, 0, -400)));
            map.ListDecorGS.Add(new Gravier(map, screenManager, new Vector3(7.3f, 0, 220)));
            map.ListDecorGS.Add(new Gravier(map, screenManager, new Vector3(-4.9f, 0, -320)));
            map.ListDecorGS.Add(new Gravier(map, screenManager, new Vector3(-2.4f, 0, 327)));
            map.ListDecorGS.Add(new Gravier(map, screenManager, new Vector3(2.2f, 0, -456)));
            map.ListDecorGS.Add(new Gravier(map, screenManager, new Vector3(1.3f, 0, 559)));
            map.ListDecorGS.Add(new Gravier(map, screenManager, new Vector3(-1.8f, 0, -110)));
            map.ListDecorGS.Add(new Gravier(map, screenManager, new Vector3(-5.6f, 0, 49)));
            map.ListDecorGS.Add(new Gravier(map, screenManager, new Vector3(0, 0, -20)));
            map.ListDecorGS.Add(new Gravier(map, screenManager, new Vector3(4.3f, 0, 11)));

            map.ListDecorGS.Add(new Wall(map, screenManager, new Vector3(-4.4f, 0, -410)));
            map.ListDecorGS.Add(new Wall(map, screenManager, new Vector3(5.9f, 0, 540)));
            
            map.ListDecorGS.Add(new Modele3D(map, screenManager, new Vector3(-9.9f, 0, 420), "FBX/train"));

            map.ListDecorGS.Add(new Pillier2(map, screenManager, new Vector3(-4.4f, 0, 520)));
            map.ListDecorGS.Add(new Pillier2(map, screenManager, new Vector3(4.9f, 0, -470)));
            map.ListDecorGS.Add(new Pillier2(map, screenManager, new Vector3(-8.3f, 0, 500)));
            map.ListDecorGS.Add(new Pillier2(map, screenManager, new Vector3(-1.4f, 0, 120)));
            map.ListDecorGS.Add(new Pillier2(map, screenManager, new Vector3(8.4f, 0, 180)));
            map.ListDecorGS.Add(new Pillier2(map, screenManager, new Vector3(3.9f, 0, -250)));
            map.ListDecorGS.Add(new Pillier2(map, screenManager, new Vector3(-3.2f, 0, 330)));
            map.ListDecorGS.Add(new Pillier2(map, screenManager, new Vector3(-4.1f, 0, 140)));
            map.ListDecorGS.Add(new Pillier2(map, screenManager, new Vector3(-7.5f, 0, 190)));
        }
    }

    class Nivo3 : Niveau
    {
        public Nivo3()
            : base("Time", "nivo3")
        {
            this.objectif.Nombre = 5 * 1000 * 60;
        }

        public override void LoadMap(Map map, ScreenManager screenManager)
        {
            map.Terrain = new Terrain(map, screenManager, Vector3.Zero, "FBX/nivo3", 22, 150, 60);
            map.ListDecorGS.Add(new Modele3D(map, screenManager, new Vector3(0, 0, -20), "FBX/train"));
        }
    }
}
