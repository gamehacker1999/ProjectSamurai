using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.IO;
using System.Collections.Generic;



namespace MyGame
{
    delegate List<GameObject> CollisionDelegate(Rectangle rect);

    enum GameState
    {
        MainMenu,
        Game,
        Loss
    }

    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {

        //Fields for the base game
        RenderTarget2D renderTarget;


        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Map map;
        ScalingViewportAdapter viewportAdapter;
        MouseState mouseState;
        MouseState prevMouseState;
        Vector2 mousePosition;
        List<GameObject> gameObjects;
        List<GameObject> collidableObjects;

        Texture2D background;

        Player player;
        List<Enemy> enemies;

        QuadTree quad;

        Camera camera;
        



        public Game1()
        {
            
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            this.IsMouseVisible = true;
            graphics.PreferredBackBufferHeight = 1440;
            graphics.PreferredBackBufferWidth = 2560;
            graphics.GraphicsProfile = GraphicsProfile.HiDef;
            Enemy.Content = Content;
            


        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            

            gameObjects = new List<GameObject>();
            collidableObjects = new List<GameObject>();

            map = new Map();
            quad = new QuadTree(0, 0, GraphicsDevice.Viewport.Width, GraphicsDevice.Viewport.Height);
            Enemy.enemyCollision += quad.Retrieve;
            player = new Player(Content);
            enemies = new List<Enemy>();

            background = Content.Load<Texture2D>("Background1");
            
            player.playerCollision += quad.Retrieve;

           
            
            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            viewportAdapter = new ScalingViewportAdapter(GraphicsDevice, 1280,720);
            
            mousePosition= new Vector2(mouseState.X, mouseState.Y);
            

            spriteBatch = new SpriteBatch(GraphicsDevice);
            Tiles.Content = Content;
            int[,] levelMap = new int[20,11];
            StreamReader reader=null;

            //reading the first level from the file
            try
            {
                int yPosition = 0;
                reader = new StreamReader("../../../../LevelMaps/Level1.txt");
                string line = null;
                while ((line = reader.ReadLine()) != null)
                {
                    line = line.Trim();
                    string[] values = line.Split(',');
                    for(int i = 0; i < values.Length; i++)
                    {
                        
                        levelMap[i, yPosition] = int.Parse(values[i]);
                        if (values[i] == "4")
                        {
                            player.Position = new Vector2(i*128,yPosition*128);
                        }

                        if (int.Parse(values[i]) == 9)
                        {
                            enemies.Add(new Enemy());
                            enemies[enemies.Count - 1].Position = new Vector2(i * 128, yPosition * 128);
                        }
                    }

                    
                    

                    yPosition++;

                }



            }

            catch
            {
                System.Console.WriteLine("File could not be read");
            }

            if (reader != null)
            {
                reader.Close();
            }


            map.GenerateMap(levelMap, 128);
            camera = new Camera(new Viewport(0,0,viewportAdapter.VirtualWidth,viewportAdapter.VirtualHeight));

            player.XOffset = map.Width;
            player.YOffset = map.Height;

          gameObjects.AddRange(map.Tiles);
            
          gameObjects.AddRange(enemies);
             
            // TODO: use this.Content to load your game content here
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();


         
            quad.Clear();
            quad.AddObject(player);

            // gameObjects.Add(player);



            //adding all game objects to the quad tree
            for (int i = 0; i < gameObjects.Count; i++)
            {
                quad.AddObject(gameObjects[i]);
            }

            // TODO: Add your update logic here
            prevMouseState = mouseState;
            mouseState = Mouse.GetState();

            foreach(Enemy enemy in enemies)
            {
                enemy.Update(gameTime);
            }

            player.Update(gameTime);
            camera.Update(player.Position, map.Width, map.Height);
            //map.CheckCollision(player);
            var scaledMousePosition = Vector2.Transform(mousePosition, Matrix.Invert(viewportAdapter.GetScaleMatrix()));

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            Matrix transform = camera.Transform*viewportAdapter.GetScaleMatrix();

            spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend,
                null, null, null, null,
               transformMatrix: transform);

            spriteBatch.Draw(background,new Rectangle(0,0,background.Width,background.Height),Color.White);
            

            

            foreach(Enemy enemy in enemies)
            {
                enemy.Draw(spriteBatch);
            }
           
            map.Draw(spriteBatch);

            player.Draw(spriteBatch);



            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
