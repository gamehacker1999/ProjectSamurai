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
    /// Represents all objects in the game
    /// </summary>
    abstract class GameObject
    {
        //fields for the game object class
        protected Texture2D texture;
        protected Rectangle rect;




        //properties for the fields of the game object class

            /// <summary>
            /// Sets the value of the texture of the object
            /// </summary>
        public Texture2D Texture
        {
            
            get{ return texture; }
        }

        /// <summary>
        /// Gets and sets the value of the rectangle of the game object
        /// </summary>
        public Rectangle Rectangle
        {
            get { return rect; }
            set { rect = value; }
        }

        /// <summary>
        /// Gets the X position of the tile
        /// </summary>
        public int X
        {
            get { return rect.X; }
            set {  rect.X=value; }
        }

        /// <summary>
        /// Gets the Y value of the tileC:\Users\Shubham Sachdeva\source\repos\MyGame\MyGame\GameObject.cs
        /// </summary>
        public int Y
        {
            get { return rect.Y; }
            set {  rect.Y=value; }
        }

        //constructor for the game object class
      //public GameObject(Texture2D texture,int x,int y,int width,int height)
      //{
      //    this.texture = texture;
      //    rect = new Rectangle(x, y, width, height);
      //}

        /// <summary>
        /// Method that draws the current object on the screen
        /// </summary>
        /// <param name="spriteBatch">spritebatch object</param>
        public virtual void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, Rectangle, Color.White);       
        }

    }
}
