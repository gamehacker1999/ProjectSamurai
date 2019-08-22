using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyGame
{

    /// <summary>
    /// Class that represents the tiles drawn on the screen
    /// </summary>
    class Tiles:GameObject
    {
        //fields for the tile class
        //bool isCollidable;
        private int tileID;

        private static ContentManager content;

        /// <summary>
        /// Sets the content manger for the tiles
        /// </summary>
        public static ContentManager Content
        {
            set { content = value; }
        }

        /// <summary>
        /// Sets the isCollidable field for the tile
        /// </summary>
        public bool IsCollidable
        {
            get { if (tileID > 0) { return true; } else return false; }

        }

        public int TileID
        {
            get { return tileID; }
        }


        /// <summary>
        /// Constructs the tile object
        /// </summary>
        /// <param name="i">parameter that specifies the kind of tile</param>
        /// <param name="rect">location of the tile</param>
        public Tiles(int i,Rectangle rect)
        {

            texture = content.Load<Texture2D>("Tile" + i);
            tileID = i;
            this.rect = rect;

            

        }
        

    }
}
