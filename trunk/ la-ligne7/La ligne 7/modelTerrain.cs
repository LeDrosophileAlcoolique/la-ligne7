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
    class modelTerrain : ModelDeplacement
    {
        public BoundingBox boxModel;

        // methode pour cree chaque objet du decor ainsi que sa boundingbox
        public modelTerrain(ContentManager content,Vector3 pos, int a2, int b2, int c2, string str)
            : base()
        {
            position = pos;

            model = content.Load<Model>(str);
            boxModel = new BoundingBox(position - new Vector3(a2, 0, c2), position + new Vector3(a2, b2, c2));
        }

    }
}
