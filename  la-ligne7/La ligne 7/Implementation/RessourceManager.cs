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
    // RessourceManager permet d'optimiser le code car si un élément est déjà chargé dans le jeu, il ne le sera pas rechargé
    class RessourceManager<T>
    {
        private Dictionary<string, T> content = new Dictionary<string, T>();
        private ContentManager contentManager;

        public RessourceManager(ContentManager contentManager)
        {
            this.contentManager = contentManager;
        }

        public T GetElement(string assetName)
        {
            // Si l'élément n'a pas été chargé, on l'ajoute
            if (!content.ContainsKey(assetName))
            { 
                content.Add(assetName, contentManager.Load<T>(assetName));
            }

            return content[assetName];
        }
    }
}
