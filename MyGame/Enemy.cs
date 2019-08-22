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
    /// Class representing the enemy
    /// </summary>
    class Enemy: GameObject
    {
        //fields for the enemy class
        Vector2 position;
        Vector2 velocity;

        static ContentManager content;

        public static event CollisionDelegate enemyCollision;

        //properties for the enemy
        public Vector2 Position
        {
            set { position = value; }
        }

        protected Vector2 Velocity
        {
            get { return velocity; }
            set { velocity = value; }
        }

        public static ContentManager Content
        {
            set { content = value; }
        }

        //constructor for the enemy class
        public Enemy()
        {
            texture = content.Load<Texture2D>("Enemy");
        }


        //method to update the enemy
        public void Update(GameTime gameTime)
        {
            rect = new Rectangle((int)position.X, (int)position.Y, texture.Width, texture.Height);
            
        }



    }
}
