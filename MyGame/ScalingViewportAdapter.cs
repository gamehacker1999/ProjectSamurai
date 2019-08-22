using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended;
using MonoGame.Extended.ViewportAdapters;

namespace MyGame
{
    /// <summary>
    /// Class that scales the game according to the resolution of the screen
    /// </summary>
    public class ScalingViewportAdapter:ViewportAdapter
    {

        //fields for the viewport adapter
        int virtualWidth, virtualHeight;
       
        //constructor for the viewport adapter
        public ScalingViewportAdapter(GraphicsDevice graphicsDevice,int virtualWidth,int virtualHeight):base(graphicsDevice)
        {
            this.virtualWidth = virtualWidth;
            this.virtualHeight = virtualHeight;
        }

        /// <summary>
        /// gets the virtual height 
        /// </summary>
        public override int VirtualHeight
        {
            get { return virtualHeight; }
        }


        /// <summary>
        /// gets the virtual width
        /// </summary>
        public override int VirtualWidth
        {
            get { return virtualWidth; }
        }


        /// <summary>
        /// gets the height of the viewport
        /// </summary>
        public override int ViewportHeight
        {
            get { return GraphicsDevice.Viewport.Height; }
        }

        /// <summary>
        /// Gets the width of the viewport
        /// </summary>
        public override int ViewportWidth
        {
            get { return GraphicsDevice.Viewport.Width; }
        }

        public override Matrix GetScaleMatrix()
        {
            var scaleX = (float)ViewportWidth / VirtualWidth;
            var scaleY = (float)ViewportHeight / VirtualHeight;
            return Matrix.CreateScale(scaleX, scaleY, 1.0f);
        }



    }
}
