using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Game2
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Texture2D Spelare;
        Texture2D Bakgrund;
        Vector2 SpelarePosition = new Vector2(100, 100);
        Vector2 BakgrundPos = new Vector2(0, 0);
        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            graphics.PreferredBackBufferWidth = 1800;  // set this value to the desired width of your window
            graphics.PreferredBackBufferHeight = 700;
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
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            Spelare = Content.Load<Texture2D>("player");
            Bakgrund = Content.Load<Texture2D>("bakgrund");
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
            KeyboardState kstate = Keyboard.GetState();

          
                if (kstate.IsKeyDown(Keys.D)) { SpelarePosition.X += 5; }
                if (kstate.IsKeyDown(Keys.A)) { SpelarePosition.X -= 5; }
                if (kstate.IsKeyDown(Keys.W))
                {
                    for (int i = 5; i > 0; i--)
                    {
                        SpelarePosition.Y -= i;
                    }
                }
                if (kstate.IsKeyDown(Keys.S))
                {
                    for (int i = 5; i > 0; i--)
                    {
                        SpelarePosition.Y += i;
                    }
                }
            if (SpelarePosition.X < 0) { SpelarePosition.X = 0; }
            if (SpelarePosition.X > 1700) { SpelarePosition.X = 1700; }
            if (SpelarePosition.Y < 0) { SpelarePosition.Y = 0; }
            if (SpelarePosition.Y > 600) { SpelarePosition.Y = 600; }





            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            spriteBatch.Begin();
            spriteBatch.Draw(Bakgrund, BakgrundPos, Color.White);
            spriteBatch.Draw(Spelare, SpelarePosition, Color.White);

            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
