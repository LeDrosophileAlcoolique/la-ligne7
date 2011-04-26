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
    class Map
    {
        protected ScreenManager screenManager;

        protected Joueur joueur;
        protected MyList<Enemy> listEnemy;
        protected MyList<Tir> listTir;
        protected MyList<Declancheur> listDeclancheur;
        protected MyList<Munition> listMunition;
        protected MyList<Modele3D> listDecor;
        protected Terrain terrain;

        public Map(ScreenManager screenManager)
        {
            this.screenManager = screenManager;

            joueur = new Joueur(this, screenManager);
            listEnemy = new MyList<Enemy>();
            listTir = new MyList<Tir>();
            listDeclancheur = new MyList<Declancheur>();
            listMunition = new MyList<Munition>();
            listDecor = new MyList<Modele3D>();
        }

        public void LoadContent()
        {
            terrain = new Terrain(this, screenManager, Vector3.Zero);
            Enemy.FirstLoadContent(screenManager.Content3D);
            Tir.FirstLoadContent(screenManager.Content3D);
            Munition.FirstLoadContent(screenManager.Content3D);
            Modele3D.FirstLoadContent(screenManager.Content3D);

            listDecor.Add(new Modele3D(this, screenManager, new Vector3(30, 0, 60), "FBX/pillier"));
            listDecor.Add(new Modele3D(this, screenManager, new Vector3(30, 0, 30), "FBX/pillier"));
            listDecor.Add(new Modele3D(this, screenManager, new Vector3(30, 0, -30), "FBX/pillier"));
            listDecor.Add(new Modele3D(this, screenManager, new Vector3(30, 0, -60), "FBX/pillier"));

            listDecor.Add(new Modele3D(this, screenManager, new Vector3(0, 0, 60), "FBX/pillier"));
            listDecor.Add(new Modele3D(this, screenManager, new Vector3(0, 0, 30), "FBX/pillier"));
            listDecor.Add(new Modele3D(this, screenManager, new Vector3(0, 0, -30), "FBX/pillier"));
            listDecor.Add(new Modele3D(this, screenManager, new Vector3(0, 0, -60), "FBX/pillier"));

            listDecor.Add(new Modele3D(this, screenManager, new Vector3(-30, 0, 60), "FBX/pillier"));
            listDecor.Add(new Modele3D(this, screenManager, new Vector3(-30, 0, 30), "FBX/pillier"));
            listDecor.Add(new Modele3D(this, screenManager, new Vector3(-30, 0, -30), "FBX/pillier"));
            listDecor.Add(new Modele3D(this, screenManager, new Vector3(-30, 0, -60), "FBX/pillier"));
            listDecor.Add(new Modele3D(this, screenManager, new Vector3(-40, 0, -60), "FBX/pillier"));

            listDeclancheur.Add(new Declancheur(this, screenManager));
        }

        public void Update(GameTime gameTime)
        {
            joueur.Update(gameTime);

            if (screenManager.Game1.Clavier.IsNewKeyPress(Keys.H))
                listDeclancheur.Add(new Declancheur(this, screenManager));

            // Pour les tests
            if (listEnemy.Longueur <= screenManager.Options.GetNiveauNbrZombie())
                listEnemy.Add(new Enemy(this, screenManager));

            if (listMunition.Longueur <= 10)
                listMunition.Add(new Munition(this, screenManager));

            foreach (Enemy enemy in listEnemy.EnumValue())
                enemy.IA(gameTime, joueur);

            foreach (MyList<Tir>.Element tir in listTir.Enum())
            {
                if (tir.Value.ParirTresLoin(gameTime, this))
                    listTir.Delete(tir);
            }

            foreach (MyList<Declancheur>.Element declancheur in listDeclancheur.Enum())
            {
                if (declancheur.Value.IsCollision(joueur))
                {
                    listEnemy.Add(new Enemy(this, screenManager, new Vector3(0, 0, -75)));
                    listEnemy.Add(new Enemy(this, screenManager, new Vector3(15, 0, -75)));
                    listEnemy.Add(new Enemy(this, screenManager, new Vector3(-15, 0, -75)));
                    listEnemy.Add(new Enemy(this, screenManager, new Vector3(30, 0, -75)));
                    listEnemy.Add(new Enemy(this, screenManager, new Vector3(-30, 0, -75)));
                    listEnemy.Add(new Enemy(this, screenManager, new Vector3(45, 0, -75)));
                    listEnemy.Add(new Enemy(this, screenManager, new Vector3(-45, 0, -75)));
                    listEnemy.Add(new Enemy(this, screenManager, new Vector3(0, 0, -55)));
                    listEnemy.Add(new Enemy(this, screenManager, new Vector3(15, 0, -55)));
                    listEnemy.Add(new Enemy(this, screenManager, new Vector3(-15, 0, -55)));
                    listEnemy.Add(new Enemy(this, screenManager, new Vector3(30, 0, -55)));
                    listEnemy.Add(new Enemy(this, screenManager, new Vector3(-30, 0, -55)));
                    listEnemy.Add(new Enemy(this, screenManager, new Vector3(45, 0, -55)));
                    listEnemy.Add(new Enemy(this, screenManager, new Vector3(-45, 0, -55)));

                    listDeclancheur.Delete(declancheur);
                }
            }

            foreach (MyList<Munition>.Element munition in listMunition.Enum())
            {
                if (munition.Value.IsCollision(joueur))
                {
                    listMunition.Delete(munition);
                    joueur.NbrMunition++;
                }
            }
        }

        public void Draw(GameTime gameTime)
        {
            foreach (Modele3D modele in ListModel())
                modele.Draw(joueur);
        }

        // Pour la collision donc on met pas le tir
        public IEnumerable<Modele3D> ListModelDeplacement()
        {
            foreach (Enemy enemy in listEnemy.EnumValue())
                yield return enemy;
        }

        public IEnumerable<Modele3D> ListModel()
        {
            yield return terrain;

            foreach (Enemy enemy in listEnemy.EnumValue())
                yield return enemy;

            foreach (Tir tir in listTir.EnumValue())
                yield return tir;

            /*
            foreach (Declancheur declancheur in listDeclancheur.EnumValue())
                yield return declancheur;
            */

            foreach (Munition munition in listMunition.EnumValue())
                yield return munition;

            foreach (Modele3D decor in listDecor.EnumValue())
                yield return decor;
        }

        public IEnumerable<Modele3D> ListDecor()
        {
            foreach (Modele3D decor in listDecor.EnumValue())
                yield return decor;
        }

        public MyList<Tir> ListTir
        {
            get
            {
                return listTir;
            }
        }

        public MyList<Enemy> ListEnemy
        {
            get
            {
                return listEnemy;
            }
            set
            {
                listEnemy = value;
            }
        }

        public Joueur Joueur
        {
            get
            {
                return joueur;
            }
        }

        public Terrain Terrain
        {
            get
            {
                return terrain;
            }
        }
    }
}
