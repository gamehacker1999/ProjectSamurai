using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MyGame;
using System;
using System.Collections.Generic;
using System.IO;

namespace ExternalTool
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    /// 

    enum ExternalToolState
    {
        MainTool, Instrctions
    }

    enum TileState
    {
        Player,Tile1,Tile2,Tile3,Enemy
    }

    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        int currLevel;

        SaveMenu saveMenu;

        ExternalToolState toolState;
        TileState tileState;

        Texture2D playerTexture;
        Texture2D tile1Texture;
        Texture2D enemyTexture;

        Vector2 playerPosition;
        List<Vector2> tile1Positions;
        List<Vector2> enemyPosition;

        Rectangle texturePosition;
        

        Dictionary<string, Texture2D> texture;
        
        MouseState ms;
        MouseState previousMs;
        KeyboardState ks;
        KeyboardState previousKS;

        Vector2 mousePosition;
        Vector2 scalingMousePosition;

        public static string[,] mapGrid;

        bool playerAdded;

        ScalingViewportAdapter viewportAdapter;

        int saveCounter;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            graphics.PreferredBackBufferHeight = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height;
            graphics.PreferredBackBufferWidth = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width;
            //graphics.IsFullScreen = true;
           
            graphics.GraphicsProfile = GraphicsProfile.HiDef;
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            tileState = TileState.Tile1;
            toolState = ExternalToolState.MainTool;
            
           
            this.IsMouseVisible = true;
            texturePosition = new Rectangle();
            //Mouse.WindowHandle = Window.Handle;
           //Mouse.SetPosition(GraphicsDevice.Viewport.Width / 2, GraphicsDevice.Viewport.Height/2);
            viewportAdapter = new ScalingViewportAdapter(GraphicsDevice, 2560, 1440);
            mousePosition = ms.Position.ToVector2();
            mapGrid = new string[20, 11];

            for(int i = 0; i < mapGrid.GetLength(0); i++)
            {
                for(int j = 0; j < mapGrid.GetLength(1); j++)
                {
                    mapGrid[i, j] = "0";
                }
            }

            tile1Positions = new List<Vector2>();
            texture = new Dictionary<string, Texture2D>();

            saveCounter = 1;

            

            base.Initialize();

        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
           // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            mousePosition =  new Vector2(ms.X, ms.Y);
           // playerTexture = Content.Load<Texture2D>("Player");
            texture.Add("player", Content.Load<Texture2D>("Player"));
            //tile1Texture = Content.Load<Texture2D>("Tile1");
            texture.Add("tile1", Content.Load<Texture2D>("Tile1"));
            texture.Add("tile2", Content.Load<Texture2D>("Tile2"));
            texture.Add("tile3", Content.Load<Texture2D>("Tile3"));
            //texture.Add("tile1", Content.Load<Texture2D>("Tile1"));
            //texture.Add("Enemy", Content.Load<Texture2D>("Enemy"));


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
            if (IsActive)
            {

                if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                    Exit();

                //ensuring that both the keyboard and the mousebuttons are only pressed once per frame
                previousMs = ms;
                ms = Mouse.GetState();

                previousKS = ks;
                ks = Keyboard.GetState();

                //creating the saving form and also hooking its closed event to the 
                //savelevel method in the the game class
                saveMenu = new SaveMenu();
                saveMenu.FormClosed += SaveLevel;

                mousePosition = new Vector2(ms.X, ms.Y);

                //converting the mouse's coordinates to world coordinates
                scalingMousePosition = Vector2.Transform(mousePosition, Matrix.Invert(viewportAdapter.GetScaleMatrix()));
                //SingleButtonPress();
                // TODO: Add your update logic here
                if (tileState == TileState.Player)
                {
                    if (ks.IsKeyDown(Keys.D1) && previousKS.IsKeyUp(Keys.D1))
                    {
                        tileState = TileState.Tile1;
                    }

                    if (ks.IsKeyDown(Keys.D2) && previousKS.IsKeyUp(Keys.D2))
                    {
                        tileState = TileState.Tile2;
                    }

                    if (ks.IsKeyDown(Keys.D3) && previousKS.IsKeyUp(Keys.D3))
                    {
                        tileState = TileState.Tile3;
                    }

                    //if the user clicks the left ouse button then
                    //add the player character at that location
                    if (ms.LeftButton == ButtonState.Pressed && previousMs.LeftButton == ButtonState.Released)
                    {

                        for (int y = 0; y < mapGrid.GetLength(1); y++)
                        {
                            for (int x = 0; x < mapGrid.GetLength(0); x++)
                            {
                                if (mapGrid[x, y] == "4")
                                    mapGrid[x, y] = "0";
                            }
                        }

                        mapGrid[(int)scalingMousePosition.X / texture["player"].Width, (int)scalingMousePosition.Y / texture["player"].Height] = "4";
                    }

                }

                //if the user wants to add a tile
                if (tileState == TileState.Tile1)
                {
                    if (ks.IsKeyDown(Keys.P) && previousKS.IsKeyUp(Keys.P))
                    {
                        tileState = TileState.Player;
                    }

                    if (ks.IsKeyDown(Keys.D2) && previousKS.IsKeyUp(Keys.D2))
                    {
                        tileState = TileState.Tile2;
                    }

                    if (ks.IsKeyDown(Keys.D3) && previousKS.IsKeyUp(Keys.D3))
                    {
                        tileState = TileState.Tile3;
                    }

                    if (ms.LeftButton == ButtonState.Pressed && previousMs.LeftButton == ButtonState.Released)
                    {
                        int xPos = (int)Math.Floor((double)ms.X / texture["tile1"].Width) * texture["tile1"].Width;
                        int yPos = (int)Math.Floor((double)ms.Y / texture["tile1"].Height) * texture["tile1"].Height;

                        tile1Positions.Add(new Vector2(xPos, yPos));
                        mapGrid[(int)scalingMousePosition.X / texture["tile1"].Width, (int)scalingMousePosition.Y / texture["tile1"].Height] = "1";

                    }
                }

                if (tileState == TileState.Tile2)
                {
                    if (ks.IsKeyDown(Keys.P) && previousKS.IsKeyUp(Keys.P))
                    {
                        tileState = TileState.Player;
                    }

                    if (ks.IsKeyDown(Keys.D1) && previousKS.IsKeyUp(Keys.D1))
                    {
                        tileState = TileState.Tile1;
                    }

                    if (ks.IsKeyDown(Keys.D3) && previousKS.IsKeyUp(Keys.D3))
                    {
                        tileState = TileState.Tile3;
                    }

                    if (ms.LeftButton == ButtonState.Pressed && previousMs.LeftButton == ButtonState.Released)
                    {
                        int xPos = (int)Math.Floor((double)ms.X / texture["tile2"].Width) * texture["tile2"].Width;
                        int yPos = (int)Math.Floor((double)ms.Y / texture["tile2"].Height) * texture["tile2"].Height;

                        tile1Positions.Add(new Vector2(xPos, yPos));
                        mapGrid[(int)scalingMousePosition.X / texture["tile2"].Width , (int)scalingMousePosition.Y / texture["tile2"].Height] = "2";

                    }
                }

                if (tileState == TileState.Tile3)
                {
                    if (ks.IsKeyDown(Keys.P) && previousKS.IsKeyUp(Keys.P))
                    {
                        tileState = TileState.Player;
                    }

                    if (ks.IsKeyDown(Keys.D2) && previousKS.IsKeyUp(Keys.D2))
                    {
                        tileState = TileState.Tile2;
                    }

                    if (ks.IsKeyDown(Keys.D1) && previousKS.IsKeyUp(Keys.D1))
                    {
                        tileState = TileState.Tile1;
                    }

                    if (ms.LeftButton == ButtonState.Pressed && previousMs.LeftButton == ButtonState.Released)
                    {
                        int xPos = (int)Math.Floor((double)ms.X / texture["tile1"].Width) * texture["tile1"].Width;
                        int yPos = (int)Math.Floor((double)ms.Y / texture["tile1"].Height) * texture["tile1"].Height;

                        tile1Positions.Add(new Vector2(xPos, yPos));
                        mapGrid[(int)scalingMousePosition.X / texture["tile1"].Width, (int)scalingMousePosition.Y / texture["tile1"].Height] = "3";

                    }
                }

                if (this.IsActive)
                {
                    //user wants to save the map
                    if (ks.IsKeyDown(Keys.S) && previousKS.IsKeyUp(Keys.S))
                    {
                       
                        saveMenu.Show();
                        saveMenu.TopMost = true;

                    }
                }
            }
            base.Update(gameTime);
        }

        
       

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend,
                null,null,null,null, transformMatrix: viewportAdapter.GetScaleMatrix());


            //drawing the map 
            for(int y = 0; y < mapGrid.GetLength(1); y++)
            {
                for(int x = 0; x<mapGrid.GetLength(0); x++)
                {
                    if (mapGrid[x, y] == "4")
                    {
                        spriteBatch.Draw(texture["player"],
                            new Vector2(x * texture["player"].Width, y * texture["player"].Height),
                            Color.White);
                    }

                    if (mapGrid[x, y] == "1")
                    {
                        spriteBatch.Draw(texture["tile1"],
                            new Vector2(x * texture["tile1"].Width, y * texture["tile1"].Height),
                            Color.White);
                    }

                    if (mapGrid[x, y] == "2")
                    {
                        spriteBatch.Draw(texture["tile2"],
                            new Vector2(x * texture["tile2"].Width, y * texture["tile2"].Height),
                            Color.White);
                    }

                    if (mapGrid[x, y] == "3")
                    {
                        spriteBatch.Draw(texture["tile3"],
                            new Vector2(x * texture["tile3"].Width, y * texture["tile3"].Height),
                            Color.White);
                    }
                }
            }


           
            spriteBatch.End();
            base.Draw(gameTime);
        }


        protected void SaveLevel(object sender, EventArgs e)
        {


            StreamWriter writer = null;

            if (saveMenu.Save == true)
            {

                //try to save the current level
                try
                {
                    writer = new StreamWriter("../../../../../MyGame/LevelMaps/Level"+saveCounter.ToString()+".txt");
                    for (int i = 0; i < mapGrid.GetLength(1); i++)
                    {
                        for (int j = 0; j < mapGrid.GetLength(0) - 1; j++)
                        {
                            writer.Write(mapGrid[j, i] + ",");

                        }
                        writer.WriteLine(mapGrid[mapGrid.GetLength(0)-1,i]);
                    }

                }

                catch
                {
                    Console.WriteLine("File coud not be opened");
                }

                if (writer != null)
                {
                    writer.Close();
                }
            }

        }
    }
}
