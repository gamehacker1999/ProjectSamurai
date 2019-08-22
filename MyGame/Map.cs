using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace MyGame
{
    /// <summary>
    /// class that represents the map of the tiles on the screen
    /// </summary>
    class Map
    {
        //fields for the map
        private List<Tiles> tiles = new List<Tiles>();
        private float width, height;

        //properties for the fields

        /// <summary>
        /// returns the tiles list
        /// </summary>
        public List<Tiles> Tiles
        {
            get { return tiles; }
        }

        /// <summary>
        /// Returns the width of the screen
        /// </summary>
        public float Width
        {
            get { return width; }
        }

        /// <summary>
        /// Returns the height of the screen
        /// </summary>
        public float Height
        {
            get { return height; }
        }

        /// <summary>
        /// Method that generates the map 
        /// </summary>
        /// <param name="tiles">2D array that represents the map</param>
        /// <param name="size">size of a single tile</param>
        public void GenerateMap(int[,] map, int size)
        {
            for(int y = 0; y < map.GetLength(1); y++)
            {
                for(int x = 0; x < map.GetLength(0); x++)
                {
                    int number = map[x, y];

                    if (number > 0 && number != 4 && number != 9)
                    tiles.Add(new Tiles(number, new Rectangle(x * size, y * size, size, size)));

                //if (number > 4)
                //{
                //    tiles[tiles.Count - 1].IsCollidable = true;
                //}

                    width = (x+1) * size;
                    height = (y + 1) * size;

                }
            }
        }

        /// <summary>
        /// Method that draws the map on the screen
        /// </summary>
        /// <param name="spriteBatch"></param>
        public void Draw(SpriteBatch spriteBatch)
        {
            foreach(Tiles t in tiles)
            {
                t.Draw(spriteBatch);
            }
        }


      
    }
}
