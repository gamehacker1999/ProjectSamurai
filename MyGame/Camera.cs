using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyGame
{
    /// <summary>
    /// Class that represnts the camera that would follow the player
    /// </summary>
    class Camera
    {
        //fields for the camera class
        Matrix transform;
        Vector2 center;
        Viewport viewport;
        
        //properties for the camera class

            /// <summary>
            /// gets the transform matrix
            /// </summary>
        public Matrix Transform
        {
            get { return transform;}
        }

        /// <summary>
        /// Constructs the camera object
        /// </summary>
        /// <param name="viewport"></param>
        public Camera(Viewport viewport)
        {

            this.viewport = viewport;



        }

        /// <summary>
        /// Update method of the camera class
        /// </summary>
        /// <param name="position">Postion of the center of the camera</param>
        /// <param name="xOffset">x offset of the screen</param>
        /// <param name="yOffset">y offset of the screen</param>
        public void Update(Vector2 position, float xOffset, float yOffset)
        {

            //if the player is on the left edge of the screen then
           
            if (position.X < viewport.Width / 2)
            {
                center.X = viewport.Width / 2;
            }


            //if the player moves past the mid point of the screen 
            else if(position.X>xOffset-(viewport.Width/2))
            {
                center.X = xOffset - (viewport.Width /2);
            }

            else
            {
                center.X = position.X;
            }


            //if the player is close to the top edge of the screen
            if (position.Y < viewport.Height / 2)
            {
                center.Y = viewport.Height / 2;
            }

            //if the player is below the middle of the screen 
            else if (position.Y > yOffset - (viewport.Height / 2))
            {
                center.Y = yOffset - (viewport.Height /2);
            }

            else
            {
                center.Y = position.Y;
            }

            //transformation matrix that would move the world according to the position of the player
            transform = Matrix.CreateTranslation(new Vector3(-center.X + (viewport.Width / 2), -center.Y + (viewport.Height / 2), 0));
        }


    }
}
