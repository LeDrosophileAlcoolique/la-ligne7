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
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Joueur joueur;
        Ennemis ennemis;
        Son son;
        Curseur curseur;
        public float aspectRatio;
        //modele
        List<Modele> list_modele;
        Modele modele;
        float i = 0.0f;
        KeyboardState oldState;
        Vector3 vecteurTir = Vector3.Zero;
        
        Terrain field;
        debuge debug;
        SpriteFont font;

        //tir
        Tir tir;
        List<Tir> list_tir;

        public Game1()
        {
            // Appel de la classe GraphicsDeviceManager
            graphics = new GraphicsDeviceManager(this);

            // Assignation plus simple de Content.RootDirectory
            Content.RootDirectory = "Content";

            // Nom de la fenêtre
            Window.Title = "La ligne 7";

            // Plein écran
            // graphics.ToggleFullScreen();
        }

        protected override void Initialize()
        {
            // Souris visible uniquement pour les test
            // base.IsMouseVisible = true;

            // Ratio Hauteur/Largeur pour la vue en perspective du modèle
            aspectRatio = (float)graphics.GraphicsDevice.Viewport.Width / (float)graphics.GraphicsDevice.Viewport.Height;

            // Appel de la classe Son, Joueur et ennemis
            son = new Son();
            debug = new debuge();
            joueur = new Joueur(aspectRatio);
            ennemis = new Ennemis();
            curseur = new Curseur();
            field = new Terrain();

            //test
            modele = new Modele(Content, list_modele, i);
            list_modele = new List<Modele>();

            // tir
            //tir initialize
            tir = new Tir(Content, list_tir, joueur.Position, vecteurTir);
            list_tir = new List<Tir>();

            base.Initialize();
        }

        protected override void LoadContent()
        {
            son.LoadContentAndPlay(Content);
            ennemis.LoadContent(Content);

            // Permet d'afficher des images 2D
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // Load l'image du curseur
            curseur.LoadContent(Content, "curseur", graphics);

            field.LoadContent(Content);

            font = Content.Load<SpriteFont>("Spritefont1");


        }

        protected override void UnloadContent()
        {

        }

        protected override void Update(GameTime gameTime)
        {
            

            // Permet de quitter quand on appuie sur échap
            if (Keyboard.GetState().IsKeyDown(Keys.Escape))
                this.Exit(); 

            // Appel de la méthode Update dans la classe Joueur
            joueur.Deplacement(graphics.GraphicsDevice.Viewport.Width / 2, graphics.GraphicsDevice.Viewport.Height / 2, Content);

            // Les ennemis qui nous suivent
            ennemis.Suivre(joueur, gameTime);

            // Remet la souris au centre de l'ecran
            Mouse.SetPosition(graphics.PreferredBackBufferWidth / 2, graphics.PreferredBackBufferHeight / 2);

            

            //test
            // On créé un modèle 3d si le joueur appuie sur M
            if ((Keyboard.GetState().IsKeyDown(Keys.M)) && (oldState.IsKeyUp(Keys.M)))
            {
                i += 30;
                list_modele.Add(new Modele(Content, list_modele, i));
            }

            //Tir
            // On tire lorsque le joueur appuie sur clic gauche.
            if ((Mouse.GetState().LeftButton == ButtonState.Pressed))
            //if (Keyboard.GetState().IsKeyDown(Keys.K))
            {
                vecteurTir = joueur.Target - joueur.Position;
                list_tir.Add(new Tir(Content, list_tir, joueur.Position- new Vector3(0.0f,50.0f,0.0f), vecteurTir));
            }
            //tir update
            //tir
            foreach (Tir tir in list_tir)
                tir.PartirTresLoin(gameTime);



            // Effect view = vue de la camera
            Matrix view = Matrix.CreateLookAt(joueur.Position, joueur.Target, Vector3.Up);
            oldState = Keyboard.GetState();
            base.Update(gameTime);
        }
         
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.RenderState.DepthBufferEnable = true ;
            // Ecran blanc au démarrage
            GraphicsDevice.Clear(Color.Blue);

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
            spriteBatch.DrawString(font, debug.debug(joueur, ennemis), new Vector2(250, 0), Color.Black);
            spriteBatch.End();

            base.Draw(gameTime);
         }
     }
}

    


