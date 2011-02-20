﻿#region Using
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
    class MainScreen : GameScreen
    {
        protected GraphicsDeviceManager graphics;
        protected Souris souris;
        protected Clavier clavier;
        public float aspectRatio;

        protected Son son;

        protected Joueur joueur;
        protected Curseur curseur;
        protected List<Ennemis> listEnnemis;
        protected List<Tir> listTir;
        protected List<ModelTerrain> listdecor;
        protected List<ModelTerrain> listdecorinvers;

        protected Debug debug;

        public MainScreen(ScreenManager screenManager)
            : base(screenManager)
        {
            graphics = screenManager.Graphics; 
            clavier = new Clavier(screenManager.Game);
            souris = new Souris(screenManager.Game);

            // Ratio Hauteur/Largeur pour la vue en perspective du modèle
            aspectRatio = (float)graphics.GraphicsDevice.Viewport.Width / (float)graphics.GraphicsDevice.Viewport.Height;

            // Souris visible uniquement pour les test
            // base.IsMouseVisible = true;

            son = new Son();

            joueur = new Joueur(aspectRatio);
            curseur = new Curseur();
            listEnnemis = new List<Ennemis>();
            listTir = new List<Tir>();
            listdecor = new List<ModelTerrain>();
            listdecorinvers = new List<ModelTerrain>();

            debug = new Debug("Debug", 0, 0);
        }

        public override void LoadContent()
        {
            son.LoadContentAndPlay(screenManager.Game.Content);

            curseur.LoadContent(screenManager.Game.Content, graphics);
            
            listEnnemis.Add(new Ennemis(screenManager.Game.Content));
            
            // en attendant un autre moyen de generer la carte en la creant piece par piece
            listdecorinvers.Add(new ModelTerrain(screenManager.Game.Content,new Vector3(0,0,0),90,100,95,"terrain"));
            listdecorinvers.Add(new ModelTerrain(screenManager.Game.Content, new Vector3(0, 0, 200), 90, 100, 95, "terrain"));
            listdecor.Add(new ModelTerrain(screenManager.Game.Content,new Vector3(0,0,0),5,100,5,"pillier"));
            listdecor.Add(new ModelTerrain(screenManager.Game.Content, new Vector3(100, 0, 100),5,100,5, "pillier"));
            listdecor.Add(new ModelTerrain(screenManager.Game.Content, new Vector3(0, 0, 100),5,100,5, "pillier"));
            listdecor.Add(new ModelTerrain(screenManager.Game.Content, new Vector3(100, 0, 0),5,100,5, "pillier"));
            listdecor.Add(new ModelTerrain(screenManager.Game.Content, new Vector3(50, 0, 50), 5, 10, 10, "box"));

            debug.LoadFont(screenManager.Game.Content);
        }

        public override void Update(GameTime gameTime)
        {
            clavier.Update();
            souris.Update();

            // Appel de la méthode Update dans la classe Joueur
            joueur.Deplacement(graphics.GraphicsDevice.Viewport.Width / 2, graphics.GraphicsDevice.Viewport.Height / 2, screenManager.Game.Content, listEnnemis, listdecor, listdecorinvers);

            // On créé un modèle 3d si le joueur appuie sur M
            if (clavier.IsNewKeyPress(Keys.M))
                listEnnemis.Add(new Ennemis(screenManager.Game.Content));

            // Les ennemis qui nous suivent
            foreach (Ennemis ennemis in listEnnemis)
                //ennemis.Suivre(joueur, gameTime, listEnnemis);

            // On tire lorsque le joueur appuie sur clic gauche.
            if (souris.IsNewClickPress())
                listTir.Add(new Tir(screenManager.Game.Content, joueur));

            // Deplacement Tir
            foreach (Tir tir in listTir)
                tir.PartirTresLoin(gameTime, listEnnemis);

            // Remet la souris au centre de l'ecran
            Mouse.SetPosition(graphics.PreferredBackBufferWidth / 2, graphics.PreferredBackBufferHeight / 2);

            if (listEnnemis.Count > 0)
                debug.Update(joueur.Boxcam.Min + "," + joueur.Position + ", \n" + listEnnemis[0].Box.Min + "," + joueur.angl1 + "," + joueur.angl2 + ",\n" + joueur.Target );
        }

        public override void Draw(GameTime gameTime)
        {
            screenManager.Game.GraphicsDevice.Clear(Color.Blue);

            // Dessine modele 3D
            foreach (ModelTerrain decor in listdecor)
                decor.Draw(joueur);
            foreach (ModelTerrain decor in listdecorinvers)
                decor.Draw(joueur);

            foreach (Ennemis ennemis in listEnnemis)
                ennemis.Draw(joueur);

            foreach (Tir tir in listTir)
                tir.Draw(joueur);

            // Dessin en 2D
            SpriteBatch spriteBatch = screenManager.SpriteBatch;

            spriteBatch.Begin();
            curseur.Draw(spriteBatch);
            debug.Draw(spriteBatch);
            spriteBatch.End();
        }
    }
}