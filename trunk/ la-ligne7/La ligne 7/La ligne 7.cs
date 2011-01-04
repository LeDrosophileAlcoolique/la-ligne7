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
        float aspectRatio;
        List<Modele> list_modele;
        Modele modele;
        float i = 0.0f;

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
            joueur = new Joueur(aspectRatio);
            ennemis = new Ennemis();
            curseur = new Curseur();

            //initialisation liste de modele
            modele = new Modele(Content, list_modele, i, ennemis.Position);
            list_modele = new List<Modele>();

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
            joueur.Deplacement(graphics.GraphicsDevice.Viewport.Width / 2, graphics.GraphicsDevice.Viewport.Height / 2);

            // Les ennemis qui nous suivent
            ennemis.Suivre(joueur, gameTime);

            // Remet la souris au centre de l'ecran
            Mouse.SetPosition(graphics.PreferredBackBufferWidth / 2, graphics.PreferredBackBufferHeight / 2);

            // Effect view = vue de la camera
            Matrix view = Matrix.CreateLookAt(joueur.Position, joueur.Target, Vector3.Up);

            // On dessine un mur si le joueur appui sur M
            if (Keyboard.GetState().IsKeyDown(Keys.M))
            {
                i += 20;
                list_modele.Add(new Modele(Content, list_modele, i, ennemis.Position));

            }

            base.Update(gameTime);
        }
         
        protected override void Draw(GameTime gameTime)
        {
            // Ecran blanc au démarrage
            GraphicsDevice.Clear(Color.RoyalBlue);

            foreach (Modele modele in list_modele)
            {
                //modele.Draw(joueur.Projection, Vector3.Zero, joueur.Target);
                modele.Draw(joueur.Projection, modele.Position, joueur.Target);
            }

            // Appel de la méthode Draw dans la classe ennemis
            ennemis.Draw(joueur);

            spriteBatch.Begin();
            curseur.Draw(spriteBatch);
            spriteBatch.End();

            base.Draw(gameTime);
         }
     }
}
    


