using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using PlatformerTesting.Objects;
using PlatformerTesting.Utils;

namespace PlatformerTesting
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        ObjectHandler objectHandler;


        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
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
            
            spriteBatch = new SpriteBatch(GraphicsDevice);
            objectHandler = new ObjectHandler();
            Globals.camera = new Camera(graphics.PreferredBackBufferWidth, graphics.PreferredBackBufferHeight);
            Globals.box = Content.Load<Texture2D>("InventoryIcon");
            Globals.font = Content.Load<SpriteFont>("Font");

            objectHandler.Add(new Player(new Vector2(100, -150), new Rectangle(0, 0, 16, 32)));

            for (int i = 0; i<15; i++)
                for (int j = 0; j < 25; j++)
                    if (i % 2 == 0)
                        if (j % 4 == 0)
                            objectHandler.Add(new DestructableTerrain(new Vector2(i*10, j*-16), new Rectangle(0, 0, 16, 16)));

            objectHandler.Add(new MovingPlatform(new Vector2(300, -16), new Vector2(1000,-16),10, new Rectangle(0, 0, 64, 16)));
            objectHandler.Add(new MovingPlatform(new Vector2(1000, -32), new Vector2(300, -32), 10, new Rectangle(0, 0, 64, 16)));

            objectHandler.Add(new Terrain(new Vector2(0, 0), new Rectangle(0, 0, 500, 16)));
            base.LoadContent();
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
            Globals.keyBoard.Update(gameTime);
            objectHandler.Update((float)gameTime.ElapsedGameTime.TotalSeconds);
            // TODO: Add your update logic here

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            spriteBatch.Begin(sortMode: SpriteSortMode.Deferred,samplerState:SamplerState.PointClamp,transformMatrix: Globals.camera.GetMatrix);
            objectHandler.Draw(spriteBatch);
            spriteBatch.End();
            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }
    }
}
