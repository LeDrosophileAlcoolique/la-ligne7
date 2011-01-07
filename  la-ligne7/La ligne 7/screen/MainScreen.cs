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
    class MainScreen : GameScreen
    {
        protected GraphicsDeviceManager graphics;
        protected Clavier clavier;
        protected Joueur joueur;
        protected Ennemis ennemis;
        protected Son son;
        protected Curseur curseur;
        protected float aspectRatio;
        //modele
        protected List<Modele> list_modele;
        protected Modele modele;
        protected float i = 0.0f;
        protected KeyboardState oldState;
        protected Vector3 vecteurTir = Vector3.Zero;

        protected Terrain field;
        protected Debuge debug;
        protected SpriteFont font;

        //tir
        protected Tir tir;
        protected List<Tir> list_tir;

        public MainScreen(ScreenManager screenManager)
            : base(screenManager)
        {
            graphics = screenManager.Graphics;

            clavier = new Clavier(screenManager.Game);

            // Souris visible uniquement pour les test
            // base.IsMouseVisible = true;

            // Ratio Hauteur/Largeur pour la vue en perspective du modèle
            aspectRatio = (float)graphics.GraphicsDevice.Viewport.Width / (float)graphics.GraphicsDevice.Viewport.Height;

            // Appel de la classe Son, Joueur et ennemis
            son = new Son();
            debug = new Debuge();
            joueur = new Joueur(aspectRatio);
            ennemis = new Ennemis();
            curseur = new Curseur();
            field = new Terrain();

            //test
            modele = new Modele(screenManager.Game.Content, list_modele, i);
            list_modele = new List<Modele>();

            // tir
            //tir initialize
            tir = new Tir(screenManager.Game.Content, list_tir, joueur.Position, vecteurTir);
            list_tir = new List<Tir>();
        }

        public override void LoadContent()
        {
            son.LoadContentAndPlay(screenManager.Game.Content);
            ennemis.LoadContent(screenManager.Game.Content);

            // Load l'image du curseur
            curseur.LoadContent(screenManager.Game.Content, "curseur", graphics);

            field.LoadContent(screenManager.Game.Content);

            font = screenManager.Game.Content.Load<SpriteFont>("Spritefont1");
        }

        public override void Update(GameTime gameTime)
        {
            clavier.Update();

            // Permet de quitter quand on appuie sur échap
            if (Keyboard.GetState().IsKeyDown(Keys.Escape))
                screenManager.Game.Exit();

            // Appel de la méthode Update dans la classe Joueur
            joueur.Deplacement(graphics.GraphicsDevice.Viewport.Width / 2, graphics.GraphicsDevice.Viewport.Height / 2, screenManager.Game.Content);

            // Les ennemis qui nous suivent
            ennemis.Suivre(joueur, gameTime);

            // Remet la souris au centre de l'ecran
            Mouse.SetPosition(graphics.PreferredBackBufferWidth / 2, graphics.PreferredBackBufferHeight / 2);



            //test
            // On créé un modèle 3d si le joueur appuie sur M
            if ((Keyboard.GetState().IsKeyDown(Keys.M)) && (oldState.IsKeyUp(Keys.M)))
            {
                i += 30;
                list_modele.Add(new Modele(screenManager.Game.Content, list_modele, i));
            }

            //Tir
            // On tire lorsque le joueur appuie sur clic gauche.
            if ((Mouse.GetState().LeftButton == ButtonState.Pressed))
            //if (Keyboard.GetState().IsKeyDown(Keys.K))
            {
                vecteurTir = joueur.Target - joueur.Position;
                list_tir.Add(new Tir(screenManager.Game.Content, list_tir, joueur.Position - new Vector3(0.0f, 50.0f, 0.0f), vecteurTir));
            }
            //tir update
            //tir
            foreach (Tir tir in list_tir)
                tir.PartirTresLoin(gameTime);



            // Effect view = vue de la camera
            Matrix view = Matrix.CreateLookAt(joueur.Position, joueur.Target, Vector3.Up);
            oldState = Keyboard.GetState();

        }

        public override void Draw(GameTime gameTime)
        {
            screenManager.Game.GraphicsDevice.Clear(Color.Black);

            SpriteBatch spriteBatch = screenManager.SpriteBatch;
            // Appel de la méthode Draw dans la classe ennemis
            ennemis.Draw(joueur);
            //appel des tir
            //Tir.Draw2(joueur.Projection, joueur.View, joueur.Target,joueur.List_tir, joueur.Shoot);

            //tir
            foreach (Tir tir in list_tir)
                tir.Draw(joueur.Projection, joueur.View, joueur.Target);
            field.Draw(joueur);
            //test
            foreach (Modele modele in list_modele)
                modele.Draw(joueur.Projection, joueur.Position, joueur.Target);

            spriteBatch.Begin();
            curseur.Draw(spriteBatch);
            // spriteBatch.DrawString(font, debug.debug(joueur, ennemis), new Vector2(250, 0), Color.Black);
            spriteBatch.End();
        }
    }
}