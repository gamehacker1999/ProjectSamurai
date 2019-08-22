using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyGame
{
    /// <summary>
    /// Class for the collision of two game objects
    /// </summary>
    static class CollisionHelper
    {
        /// <summary>
        /// Check if this rectangle is colliding with the top of another rectangle
        /// </summary>
        /// <param name="r1"></param>
        /// <param name="r2"></param>
        /// <returns></returns>
        public static bool BottomCollision(this Rectangle r1, Rectangle r2)
        {
            return (r1.Bottom >= r2.Top-1&&r1.Bottom<=r2.Top+(r2.Height/2) && r1.Top <= r2.Top
                && r1.Right >= r2.Left+(r2.Width/5)&& r1.Left <= r2.Right-(r2.Width/5));
        }

        /// <summary>
        /// Check if this rectnagle is colliding with the bottom of another rectangle
        /// </summary>
        /// <param name="r1"></param>
        /// <param name="r2"></param>
        /// <returns></returns>
        public static bool TopCollision(this Rectangle r1, Rectangle r2)
        {
            return (r1.Top <= r2.Bottom  && r1.Bottom >= r2.Top && r1.Top>=r2.Bottom-(r2.Height/2)&&
                r1.Right >= r2.Left+(r2.Width/5) && r1.Left <= r2.Right-(r2.Width/5));
        }

        /// <summary>
        /// Check if this rectangle is colliding with the right of another
        /// </summary>
        /// <param name="r1"></param>
        /// <param name="r2"></param>
        /// <returns></returns>

        public static bool LeftCollision(this Rectangle r1,Rectangle r2)
        {
            return (r1.Left <= r2.Right  && r1.Right >= r2.Right &&
                r1.Bottom >= r2.Top+(r2.Height/4) && r1.Top <= r2.Bottom-(r2.Height/4));
        }

        /// <summary>
        /// Check if this rectangle is colliding with the left of another
        /// </summary>
        /// <param name="r1"></param>
        /// <param name="r2"></param>
        /// <returns></returns>
        public static bool RightCollision(this Rectangle r1, Rectangle r2)
        {
            return (r1.Right >= r2.Left && r1.Left <= r2.Left &&
                r1.Bottom >= r2.Top+(r2.Height/4) && r1.Top <= r2.Bottom-(r2.Height/4));    
        }



    }
}
