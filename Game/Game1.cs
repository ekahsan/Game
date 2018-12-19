using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Squared.Tiled;
using System.IO;

namespace GameTest
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    /// 

    public class Game1 : Game
    {
      
        Map map;
        Layer collision;

        Vector2 viewportPosition;
        int tilepixel;
        Texture2D cartexture;
        Car car;
        AICar aicar;
        AICar aicar2;

        Vector2[] path;

        
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;


        public Game1()
        {
       
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            graphics.PreferredBackBufferWidth = 1280;
            graphics.PreferredBackBufferHeight = 720;
            graphics.ApplyChanges();
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
            map = Map.Load(Path.Combine(Content.RootDirectory, "Map 1.tmx"), Content);
            collision = map.Layers["Tile Layer 1"];
            tilepixel = map.TileWidth;
            

            cartexture = Content.Load<Texture2D>("car");
           Texture2D aicartexture = Content.Load<Texture2D>("aicar");
            Vector2 carPos = new Vector2((graphics.PreferredBackBufferWidth / 2) - (cartexture.Width / 2),
                (graphics.PreferredBackBufferHeight / 2) - (cartexture.Height / 2));
           
            car = new Car();
            car.Init(cartexture, carPos);
            car.Position = new Vector2(map.ObjectGroups["Player"].Objects["Player"].X - 640, map.ObjectGroups["Player"].Objects["Player"].Y - 360);
            // TODO: use this.Content to load your game content here
            path = map.ObjectGroups["Player"].Objects["Path 1"].PointsList.ToArray();
            aicar = new AICar();
            aicar.Init(aicartexture, path);
            Vector2[] path2 = map.ObjectGroups["ai2"].Objects["Path 2"].PointsList.ToArray();
            aicar2 = new AICar();
            aicar2.Init(aicartexture, path2);


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

            // TODO: Add your update logic here

            car.Update(gameTime);
            aicar.Update(gameTime);
            aicar2.Update(gameTime);
            map.ObjectGroups["Player"].Objects["Player"].X = (int)car.Position.X;
            map.ObjectGroups["Player"].Objects["Player"].Y = (int)car.Position.Y;
            viewportPosition = new Vector2(map.ObjectGroups["Player"].Objects["Player"].X - 640, map.ObjectGroups["Player"].Objects["Player"].Y - 360);

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
            spriteBatch.Begin();

            map.Draw(spriteBatch, new Rectangle(0, 0, graphics.PreferredBackBufferWidth, graphics.PreferredBackBufferHeight), car.Position);
            car.Draw(spriteBatch, viewportPosition);
            aicar.Draw(spriteBatch, viewportPosition);
            aicar2.Draw(spriteBatch, viewportPosition);

            spriteBatch.End();



            base.Draw(gameTime);
        }

    }
}
