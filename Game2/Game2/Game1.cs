using Game2;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;

namespace Game2
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    /// 




}



public class Game1 : Game
{
    GraphicsDeviceManager graphics;
    SpriteBatch spriteBatch;
    SpriteFont text;
    Texture2D Spelare;
    Texture2D SpelareSköld;
    Texture2D Bakgrund;
    Texture2D Bakgrund2;
    Texture2D Bakgrund3;
    Texture2D Bakgrund4;
    Texture2D Bakgrund5;
    Texture2D EnemyTexture;
    Texture2D HealthTexture;
    Vector2 SpelarePosition = new Vector2(100, 100);
    Vector2 BakgrundPos = new Vector2(0, 0);
    Vector2 LeverTextPos = new Vector2(100, 100);
    Vector2 spelareHastighet = new Vector2(1, 1);
    Vector2 spelareHitboxPos = new Vector2(100,100);
    Vector2 sköldPos = new Vector2(100, 100);
    Rectangle Pointline = new Rectangle(0, 0, 1, 2000);
    Rectangle spelareHitbox;
    Rectangle sköldhitbox;
    List<Enemy> enemyLista = new List<Enemy>();
    List<Health> healthLista = new List<Health>();
    Random random = new Random();
    int enemyspawnchance = 30;
    int levelCount = 1;
    int healthLevel = 100;
    float points = 0;
    bool inbound = true;
    bool gamerunning = true;
    bool debugmenu = false;
    bool gravity = true;
    bool sköld = false;





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
        SpelareSköld = Content.Load<Texture2D>("playershield");
        EnemyTexture = Content.Load<Texture2D>("enemy");
        HealthTexture = Content.Load<Texture2D>("redcrosspng");
        Bakgrund = Content.Load<Texture2D>("bakgrund");
        Bakgrund2 = Content.Load<Texture2D>("bakgrund2");
        Bakgrund3 = Content.Load<Texture2D>("bakgrund3");
        Bakgrund4 = Content.Load<Texture2D>("bakgrund4");
        Bakgrund5 = Content.Load<Texture2D>("endscreen");

        text = Content.Load<SpriteFont>("Ubuntu32");

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

        spelareHitbox = new Rectangle((int)spelareHitboxPos.X, (int)spelareHitboxPos.Y, 80, 90);
        sköldhitbox = new Rectangle((int)sköldPos.X, (int)sköldPos.Y, 200, 200);

        if(healthLevel < 1)
        {
            levelCount = -10;
            gamerunning = false;
        }

        if (kstate.IsKeyDown(Keys.D))
        {
            spelareHastighet.X += 0.15f;

        }
        if (kstate.IsKeyDown(Keys.A))
        {
            spelareHastighet.X += -0.15f;
        }
        SpelarePosition.X += spelareHastighet.X;

        if (kstate.IsKeyDown(Keys.W))
        {
            gravity = false;
            SpelarePosition.Y -= 5;
        }
        else
            gravity = true;
        if (kstate.IsKeyDown(Keys.S))
        {
            SpelarePosition.Y += 5;
        }
        if (kstate.IsKeyDown(Keys.CapsLock)) //Sätter capslock som knapp för att öppna debugmenyn
        {
            debugmenu = true;
        }
        if (kstate.IsKeyDown(Keys.LeftShift)) // sätter vänstra shift som knapp för att stänga debugmenyn
        {
            debugmenu = false;
        }
        if (kstate.IsKeyDown(Keys.P)) { gamerunning = false; }
        if (kstate.IsKeyDown(Keys.C)) { gamerunning = true; }

        if (SpelarePosition.X < 0) { SpelarePosition.X = 0; }
        if (SpelarePosition.X > 1700) { SpelarePosition.X = 1700; }
        if (SpelarePosition.Y < 0) { SpelarePosition.Y = 0; }
        if (SpelarePosition.Y > 600) { SpelarePosition.Y = 600; }

        if (SpelarePosition.X > 0 && SpelarePosition.X < 1700 && SpelarePosition.Y > -10 && SpelarePosition.Y < 600) { inbound = true; }
        else { inbound = false; }

        foreach (var item in enemyLista)
        {
            item.Update();
        }
        foreach (var item in healthLista)
        {
            item.Update();
        }
        if (gamerunning == true)
        {
            if (random.Next(1, enemyspawnchance) == 1)
            {
                enemyLista.Add(new Enemy(EnemyTexture, new Vector2(1900, random.Next(1, 700))));
                
            }

            if (random.Next(1, 500) == 1)
            {
                healthLista.Add(new Health(HealthTexture, new Vector2(1900, random.Next(1, 700))));
                
            }


        }
        if (inbound == true) {
            if (gravity == true)
            {
                spelareHastighet.Y += 0.15f;
                SpelarePosition.Y += spelareHastighet.Y;
            }
            if (gravity == false)
            {
                spelareHastighet.Y = 0f;
            }
        }

        if (points > 200)
        {
            enemyspawnchance = 20; // Nivå 2
            levelCount = 2;
        }
        if (points > 500)
        {
            enemyspawnchance = 15; // Nivå 3
            levelCount = 3;
        }
        if (points > 1000)
        {
            enemyspawnchance = 10; // Nivå 4
            levelCount = 4;
        }

        for (int i = 0; i < enemyLista.Count; i++)
        {
            if (enemyLista[i].Gethitbox().Intersects(Pointline)) { points++; }
        }

if(gamerunning == true) { 
        for (int i = 0; i < enemyLista.Count; i++)
        {
            if (enemyLista[i].Gethitbox().Intersects(spelareHitbox))
            {
                points--;
                healthLevel-=10;
                enemyLista.RemoveAt(i);


            }

            if (points > 0) { 
            if (enemyLista[i].Gethitbox().Intersects(sköldhitbox))
            {
                if (sköld == true)
                { enemyLista.RemoveAt(i);
                        
                }

            }
            }
            }
        }
        for (int i = 0; i < healthLista.Count; i++)
        {
            if (healthLista[i].Gethitbox().Intersects(spelareHitbox))
            {
                if(healthLevel < 101) { healthLevel += 10; }
                

                healthLista.RemoveAt(i);
                

            }
        }



        if (kstate.IsKeyDown(Keys.Space))
        {
            if(points > 0)
            sköld = true; }
        if (kstate.IsKeyUp(Keys.Space))
        {
            sköld = false;

        }
        if(sköld == true)
        {
            points--;
        }

        if (points < 0) points = 0;
        for (int i = 0; i < healthLista.Count; i++)
            {
            

          


            }
        
        if (spelareHitboxPos.X != SpelarePosition.X)
        { spelareHitboxPos.X = SpelarePosition.X; }
        if (spelareHitboxPos.Y != SpelarePosition.Y)
        { spelareHitboxPos.Y = SpelarePosition.Y;
        }
        if(spelareHitboxPos.X != sköldPos.X)
        { sköldPos.X = spelareHitboxPos.X; }
        if (spelareHitboxPos.Y != sköldPos.Y)
        { sköldPos.Y = spelareHitboxPos.Y; }

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
        if (levelCount == 1) { spriteBatch.Draw(Bakgrund, BakgrundPos, Color.White); }
        if (levelCount == 2) { spriteBatch.Draw(Bakgrund2, BakgrundPos, Color.White); }
        if (levelCount == 3) { spriteBatch.Draw(Bakgrund3, BakgrundPos, Color.White); }
        if (levelCount == 4) { spriteBatch.Draw(Bakgrund4, BakgrundPos, Color.White); }
        if (levelCount < 0) { spriteBatch.Draw(Bakgrund5, BakgrundPos, Color.White); }

       if(gamerunning == true) spriteBatch.Draw(Spelare, SpelarePosition, Color.White);

       if(sköld == true) { spriteBatch.Draw(SpelareSköld, SpelarePosition, Color.White); }
        spriteBatch.DrawString(text, "Points: " + (points).ToString(), new Vector2(300, 200), Color.White);
        spriteBatch.DrawString(text, "Level: " + levelCount.ToString(), LeverTextPos, Color.White);
        spriteBatch.DrawString(text, "Health: " + healthLevel.ToString(), new Vector2(300, 100), Color.White);
        foreach (var item in enemyLista)
        {
            item.Draw(spriteBatch);
        }
        foreach (var item in healthLista)
        {
            item.Draw(spriteBatch);
        }
        if (debugmenu == true)
        {
            spriteBatch.DrawString(text, "Enemycount: " + enemyLista.Count.ToString(), new Vector2(1200, 100), Color.White);
            spriteBatch.DrawString(text, "Enemycount: " + sköld, new Vector2(1200, 300), Color.White);
            spriteBatch.DrawString(text, "Enemy spawnchance: 1/" + enemyspawnchance.ToString(), new Vector2(1200, 130), Color.White);
            spriteBatch.DrawString(text, "Click on P to stop the enemies, and then C to continue  ", new Vector2(1200, 200), Color.White);


        }
        spriteBatch.End();
        base.Draw(gameTime);
    }
}

