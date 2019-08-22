using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyGame
{

    /// <summary>
    /// Enum that represents the state of the player
    /// </summary>
    enum PlayerState
    {
        standingLeft,standingRight,walkingLeft,walkingRight, jumping, attacking, falling, dying, block
    }


    /// <summary>
    /// Class that represents the main character
    /// </summary>
    class Player : GameObject
    {
        //Fields for the main character
        Vector2 position;
        Vector2 velocity;
        const float gravity=1f;
        PlayerState playerState = PlayerState.standingLeft;
        PlayerState prevPlayerState;
        bool hasJumped;

        float xOffset;
        float yOffset;
        

        KeyboardState keyboardState;
        KeyboardState previousKeyboardState;

        MouseState mouseState;
        MouseState previousMouseState;

        bool attacking;
        bool blocking;

        public event CollisionDelegate playerCollision;

        //time 
        int totalTime;

        //current frame the current animation is at
        int currentFrame;


        //properties for the player

        public Vector2 Position
        {
            get { return position; }
            set { position=value;}
        }

        /// <summary>
        /// Sets the value for the X component of the velocity
        /// </summary>
        protected float VelocityX
        {
            get { return velocity.Y; }
            set { velocity.X = value; }
        }

        /// <summary>
        /// Sets the value for the Y component of velocity
        /// </summary>
        protected float VelocityY
        {
            get { return velocity.Y; }
            set { velocity.Y = value; }
        }

        public float XOffset
        {

            set { xOffset = value; }

        }

        public float YOffset
        {
            set { yOffset = value; }
        }

        /// <summary>
        /// Constructor for he player 
        /// </summary>
        /// <param name="content"></param>
        public Player(ContentManager content)
        {
            texture = content.Load<Texture2D>("IdlePlayer");

            totalTime = 0;
            currentFrame = 0;
            
        }

        /// <summary>
        /// Update method for the player
        /// </summary>
        /// <param name="gameTime"></param>
        public void Update(GameTime gameTime)
        {
            
            
         
            previousKeyboardState = keyboardState;
            previousMouseState = mouseState;
            keyboardState = Keyboard.GetState();
            position += velocity;
            //rect = new Rectangle((int)position.X,(int)position.Y,texture.Width/20,texture.Height);
            rect = new Rectangle((int)position.X, (int)position.Y, texture.Width / 10, texture.Height);

            switch (playerState)
            {
                
                case PlayerState.standingLeft:

                    VelocityX = 0;
                    prevPlayerState = PlayerState.standingLeft;

                   if (keyboardState.IsKeyDown(Keys.Right) && previousKeyboardState.IsKeyUp(Keys.Right))
                   {
                        playerState = PlayerState.standingRight;
                   }

                    if (keyboardState.IsKeyDown(Keys.Left))
                    {
                        playerState = PlayerState.walkingLeft;
                    }

                  //if (keyboardState.IsKeyDown(Keys.Up) && previousKeyboardState.IsKeyUp(Keys.Up) && hasJumped == false)
                  //{
                  //    position.Y -= 1f;
                  //    playerState = PlayerState.jumping;
                  //}

                    break;

                case PlayerState.standingRight:

                    VelocityX = 0;
                    prevPlayerState = PlayerState.standingRight;
                    if (keyboardState.IsKeyDown(Keys.Left) && previousKeyboardState.IsKeyUp(Keys.Left))
                    {
                        playerState = PlayerState.standingLeft;

                    }

                    if (keyboardState.IsKeyDown(Keys.Right))
                    {
                        playerState = PlayerState.walkingRight;
                    }

                  //if (keyboardState.IsKeyDown(Keys.Up) && previousKeyboardState.IsKeyUp(Keys.Up) && hasJumped == false)
                  //{
                  //    position.Y -= 1f;
                  //    playerState = PlayerState.jumping;
                  //}

                    break;

                case PlayerState.walkingLeft:

                    prevPlayerState = PlayerState.walkingLeft;
                    velocity.X = -5;
                    

                    if (keyboardState.IsKeyUp(Keys.Left) && previousKeyboardState.IsKeyDown(Keys.Left))
                    {
                        playerState = PlayerState.standingLeft;
                    }

                    if (keyboardState.IsKeyDown(Keys.Right) && previousKeyboardState.IsKeyUp(Keys.Right))
                    {
                        playerState = PlayerState.standingRight;
                    }

                 //if (keyboardState.IsKeyDown(Keys.Up) && previousKeyboardState.IsKeyUp(Keys.Up) && hasJumped == false)
                 //{
                 //
                 //    position.Y -= 1f;
                 //    playerState = PlayerState.jumping;
                 //}

                    break;

                case PlayerState.walkingRight:


                    velocity.X = 5;
                    prevPlayerState = PlayerState.walkingRight;

                    if (keyboardState.IsKeyUp(Keys.Right) && previousKeyboardState.IsKeyDown(Keys.Right))
                    {
                        playerState = PlayerState.standingRight;
                    }

                    if (keyboardState.IsKeyDown(Keys.Left) && previousKeyboardState.IsKeyUp(Keys.Left))
                    {
                        playerState = PlayerState.standingLeft;

                    }

                 //if (keyboardState.IsKeyDown(Keys.Up) && previousKeyboardState.IsKeyUp(Keys.Up) && hasJumped == false)
                 //{
                 //    position.Y -= 1f;
                 //    playerState = PlayerState.jumping;
                 //}

                    break;

                case PlayerState.jumping:



                       if (keyboardState.IsKeyDown(Keys.Right))
                       {
                               playerState = PlayerState.walkingRight;
                       }

                       if (keyboardState.IsKeyDown(Keys.Left))
                       {
                           playerState = PlayerState.walkingLeft;
                       }

                    hasJumped = true;

                   
                    VelocityY = -20f;


                    playerState = prevPlayerState ;
                   
                    
                    break;
                    
                   

                

            }

            if (keyboardState.IsKeyDown(Keys.Up)&&hasJumped == false)
            {
                position.Y -= 2f;
                VelocityY = -20f;
                
                hasJumped = true;
               
            }

            if(mouseState.LeftButton==ButtonState.Pressed&&previousMouseState.LeftButton==ButtonState.Released)
            {
                //playAttackAnimation();
                attacking = true;

            }

            if (mouseState.RightButton == ButtonState.Pressed && previousMouseState.RightButton == ButtonState.Released)
            {
                if (!hasJumped)
                {
                    VelocityX = 0;
                    blocking = true;
                }

            }



            if (VelocityY <= 10)
                VelocityY += gravity;

            CheckCollision();

            UpdateAnimation(gameTime);


        }

        /// <summary>
        /// Checking the collision between the player and tile
        /// </summary>
        /// <param name="tile">Tile that the player is colliding with</param>
      

        //Method to check collision between playr and other game objects
        protected void CheckCollision()
        {
            //getting all the game objects that the player can collide with
            //These objects are in the same quad as the player
            List<GameObject> collidableObjects=playerCollision(this.rect);

            //looping through all the collidable game objects
            foreach(GameObject gameObject in collidableObjects)
            {
                if (!(gameObject is Player))
                {

                    if (gameObject is Tiles)
                    {
                        Tiles tile = (Tiles)gameObject;


                        if (tile.TileID == 1)
                        {

                            //Checking to see if the player's bottom is colliding with a game object
                            if (rect.BottomCollision(tile.Rectangle))
                            {

                                //changing the Y velocity to zero to stop jumping and changing the player state
                                rect.Y = gameObject.Rectangle.Y - rect.Height - 1;

                                VelocityY = 0f;

                                hasJumped = false;

                            }

                            //Check to see if the right side of the player is colliding
                            if (rect.RightCollision(tile.Rectangle))
                            {
                                position.X = gameObject.Rectangle.Left - rect.Width;
                                //VelocityX = 0;

                            }

                            //check to see if the left of the player is colliding with a gameobject
                            if (rect.LeftCollision(tile.Rectangle))
                            {
                                //try with rect too
                                position.X = gameObject.Rectangle.X + gameObject.Rectangle.Width;
                                //VelocityX = 0;

                            }

                            //check to see if the top of the player is touchng  game object
                            if (rect.TopCollision(tile.Rectangle))
                            {

                                VelocityY = 1f;

                            }
                        }
                    }

                }
                  
            }

            if(position.X<=5)
            {
                position.X = 5;
            }

            if(position.X>=xOffset-rect.Width-5)
            {

                position.X = xOffset - rect.Width-5;

            }


        }

        /// <summary>
        /// Function to update the animation
        /// </summary>
        /// <param name="gameTime"></param>
        void UpdateAnimation(GameTime gameTime)
        {
            //addin onto the total time
            totalTime += (int)(gameTime.ElapsedGameTime.TotalMilliseconds);
            
            //if delta time is greater than the time per frame
            if(totalTime>1000)
            {
                //reset the total time
                totalTime = 0;

                //incrementing the current frame
                currentFrame++;

                //if current frame is the number of maximum frame then reset the frame
                if (currentFrame == 10)
                {
                    currentFrame = 0;
                }


            }

        }


        //overriding the draw method
        public override void Draw(SpriteBatch spriteBatch)
        {
            //drawing the current frame of the animation

            spriteBatch.Draw(texture, //using the spritesheet
                rect, //destination rectangle, it stores the location of the player as well as its width and hand
                new Rectangle((texture.Width/10*currentFrame)+25,0, texture.Width / 10, texture.Height), //position of the texture in the texture
                Color.White); //default color
        }

    }
}
