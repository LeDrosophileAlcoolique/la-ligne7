﻿#region Using
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

        protected Modele3D joueur2;

        public Map(ScreenManager screenManager)
        {
            this.screenManager = screenManager;
            
            joueur = new Joueur(this, screenManager, 0, 0);

            listEnemy = new MyList<Enemy>();
            listTir = new MyList<Tir>();
            listDeclancheur = new MyList<Declancheur>();
            listMunition = new MyList<Munition>();
            listDecor = new MyList<Modele3D>();

            joueur2 = new Modele3D(this, screenManager, Vector3.Zero, "FBX/zombie");
        }

        public void LoadContent()
        {
            Enemy.FirstLoadContent(screenManager.Content3D);
            Tir.FirstLoadContent(screenManager.Content3D);
            Munition.FirstLoadContent(screenManager.Content3D);
            Modele3D.FirstLoadContent(screenManager.Content3D);

            terrain = new Terrain(this, screenManager, Vector3.Zero);

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

            listDeclancheur.Add(new Declancheur(this, screenManager));
        }

        public void Update(GameTime gameTime)
        {
            joueur.Update(gameTime);

            if (screenManager.Game1.Session != null)
            {
                screenManager.Game1.PacketWriter.Write(Joueur.PositionReseau);
                screenManager.Game1.PacketWriter.Write((double)Joueur.CameraYawX + Math.PI);

                if (screenManager.Game1.Session.IsHost)
                {
                    screenManager.Game1.PacketWriter.Write(listEnemy.Longueur);

                    foreach (Enemy enemy in listEnemy.EnumValue())
                    {
                        screenManager.Game1.PacketWriter.Write(enemy.Position);
                        screenManager.Game1.PacketWriter.Write((double)enemy.Rotation);
                    }
                }

                screenManager.Game1.Session.LocalGamers[0].SendData(screenManager.Game1.PacketWriter, SendDataOptions.ReliableInOrder);

                LocalNetworkGamer gamer = screenManager.Game1.Session.LocalGamers[0];

                if (gamer.IsDataAvailable)
                {
                    NetworkGamer sender;
                    PacketReader packetReader = new PacketReader();
                    gamer.ReceiveData(packetReader, out sender);

                    if (gamer != sender)
                    {
                        Joueur2.Position = packetReader.ReadVector3();
                        joueur2.Rotation = (float)packetReader.ReadDouble();

                        if (!screenManager.Game1.Session.IsHost)
                        {
                            int nbrZombie = packetReader.ReadInt32();
                            listEnemy = new MyList<Enemy>();

                            for (int i = 0; i < nbrZombie; ++i)
                            {
                                listEnemy.Add(new Enemy(this, screenManager, packetReader.ReadVector3()));
                            }
                        }
                    }
                }
            }

            if (screenManager.Game1.Session == null || screenManager.Game1.Session.IsHost)
            {
                // Pop arme et zombie
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
                    /*
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
                    */
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
            else
            {
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
            
            if (screenManager.Game1.Session != null)
                yield return joueur2;

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

        public Modele3D Joueur2
        {
            get
            {
                return joueur2;
            }
            set
            {
                joueur2 = value;
            }
        }
    }
}
