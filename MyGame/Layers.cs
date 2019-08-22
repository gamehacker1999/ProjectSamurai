using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MonoGame.Utilities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace MyGame
{
    //Class for parallax scroller for layers
    class Layers: GameObject
    {

        public Layers(ContentManager content,float level)
        {

            texture = content.Load<Texture2D>("layer" + level);

        }


        


        


    }
}
