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
    Vector2 spelareHitboxPos = new Vector2(100, 100);
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
    //sätter massa variablar som används i koden. Alla dessa förklarar sig själva med namnen.




    public Game1()
    {
        graphics = new GraphicsDeviceManager(this);
        graphics.PreferredBackBufferWidth = 1800;  // Sätter  bredden på skärmen
        graphics.PreferredBackBufferHeight = 700; // Sätter  höjden på skärmen
        Content.RootDirectory = "Content";

    }

   
    protected override void Initialize()
    {
      

        base.Initialize();
    }

  
    protected override void LoadContent()
    {
       // Laddar in alla texturer och texten
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

       
    }

    
    protected override void UnloadContent()
    {
        
    }

   
    protected override void Update(GameTime gameTime)
    {
        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            Exit();
        KeyboardState kstate = Keyboard.GetState();

        spelareHitbox = new Rectangle((int)spelareHitboxPos.X, (int)spelareHitboxPos.Y, 80, 90); //sätter spelarens hitbox på spelaren
        sköldhitbox = new Rectangle((int)sköldPos.X, (int)sköldPos.Y, 200, 200); // sätter spelarens sköld på spelaren

        if (healthLevel < 1) // om spelarens hälsonivå är 0 eller mindre så sätts spelet av och nivå till minus tio.
        {
            levelCount = -10;
            gamerunning = false;
        }

        if (kstate.IsKeyDown(Keys.D)) // sätter accelerationen när spelaren håller in D
        {
            spelareHastighet.X += 0.15f;

        }
        if (kstate.IsKeyDown(Keys.A)) // sätter acceleration när spelaren håller in A
        {
            spelareHastighet.X += -0.15f;
        }
        SpelarePosition.X += spelareHastighet.X;

        if (kstate.IsKeyDown(Keys.W)) // stänger av gravitionen när man håller in W men också flyttar spelaren uppåt.
        {
            gravity = false;
            SpelarePosition.Y -= 5;
        }
        else // om inte W är intryckt så sätts gravitionen på.
            gravity = true;


        if (kstate.IsKeyDown(Keys.S)) // om spelaren håller in S så förflyttas karaktären neråt
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
        if (kstate.IsKeyDown(Keys.P)) { gamerunning = false; } //Sätter P som Pausknapp
        if (levelCount > 0) { if (kstate.IsKeyDown(Keys.C)) { gamerunning = true; } }// sätter C som startknapp efter paus om inte nivån är under 0.

        if (SpelarePosition.X < 0) { SpelarePosition.X = 0; } //alla dessa begränsar spelaren så den inte kan åka utanför skärmen.
        if (SpelarePosition.X > 1700) { SpelarePosition.X = 1700; }
        if (SpelarePosition.Y < 0) { SpelarePosition.Y = 0; }
        if (SpelarePosition.Y > 600) { SpelarePosition.Y = 600; }
        if (SpelarePosition.X > 0 && SpelarePosition.X < 1700 && SpelarePosition.Y > -10 && SpelarePosition.Y < 600) { inbound = true; } // ställer in så att gravitationen inte fungerar utanför skärmen.
        else { inbound = false; }

        foreach (var item in enemyLista)
        {
            item.Update(); // uppdaterar varje objekt i enemylistan
        }
        foreach (var item in healthLista)
        {
            item.Update(); // uppdaterar varje objekt i healthlistan
        }
        if (gamerunning == true)
        {
            if (random.Next(1, enemyspawnchance) == 1)
            {
                enemyLista.Add(new Enemy(EnemyTexture, new Vector2(1900, random.Next(1, 700)))); // spawnar in fiender beroende på variabeln enemyspawnchance som ändras i nivåerna.

            }

            if (random.Next(1, 500) == 1)
            {
                healthLista.Add(new Health(HealthTexture, new Vector2(1900, random.Next(1, 700)))); // spawnar in hälsokloten 1 gång på varje 500 uppdateringar i genomsnitt.

            }


        }
        if (inbound == true)
        {
            if (gravity == true) // om spelaren är inom skärmgränserna så sätts gravitationen på och accelererar spelaren neråt med 0.15 enheter per sekund per sekund.
            {
                spelareHastighet.Y += 0.15f;
                SpelarePosition.Y += spelareHastighet.Y;
            }
            if (gravity == false)
            {
                spelareHastighet.Y = 0f; // om gravitationen är av så sätts hastigheten i Y led till 0.
            }
        }

        if (points > 200) // dessa definerar poängantalet för de olika nivåerna. Och ändrar där med 
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

        for (int i = 0; i < enemyLista.Count; i++) //kollar om fienderna skär poänglinjen som befinnersig i vänstra kanten av skärmen. Om de skärs så lägger denna funktion till poäng.
        {
            if (enemyLista[i].Gethitbox().Intersects(Pointline)) { points++; }
        }

        if (gamerunning == true) // om spelet körs och spelarens hitbox skär en fiendes hitbox så förlorar man poäng, liv och fienden tas bort.
        {
            for (int i = 0; i < enemyLista.Count; i++)
            {
                if (enemyLista[i].Gethitbox().Intersects(spelareHitbox))
                {
                    points--;
                    healthLevel -= 10;
                    enemyLista.RemoveAt(i);


                }

                if (points > 0)
                {
                    if (enemyLista[i].Gethitbox().Intersects(sköldhitbox)) // kollar om spelarens poäng är högre än 0. Är dem det och skölden är aktiv(genom att hålla in mellanslag) så försvinner fienderna i sköldens hitbox
                    {
                        if (sköld == true)
                        {
                            enemyLista.RemoveAt(i);

                        }

                    }
                }
            }
        }
        if (gamerunning == true)
        {
            for (int i = 0; i < healthLista.Count; i++) // om spelet körs och spelarens hitbox skärs med en hälsoglobs hitbox så får spelaren liv och hälsogloben tas bort.
            {
                if (healthLista[i].Gethitbox().Intersects(spelareHitbox))
                {
                    if (healthLevel < 101) { healthLevel += 10; }


                    healthLista.RemoveAt(i);


                }
            }
        }

        if (healthLevel < 1)
        { gamerunning = false; } // om hälsonivån är 0 så bryts spelet.

        if (kstate.IsKeyDown(Keys.Space)) // Om spelarens poäng är högre än 0 och space klickas ner aktiveras skölden.
        {
            if (points > 0)
                sköld = true;
        }
        if (kstate.IsKeyUp(Keys.Space)) // om space inte är nere så är skölden inte aktiverad.
        {
            sköld = false;

        }
        if (sköld == true) // om skölden är på försvinner poäng.
        {
            points--;
        }

        if (points < 0) points = 0; // sätter de minsta poängvärde till 0.


        if (spelareHitboxPos.X != SpelarePosition.X) // alla dessa sätter hitboxarnas positioner till de visuella objekten.
        { spelareHitboxPos.X = SpelarePosition.X; }
        if (spelareHitboxPos.Y != SpelarePosition.Y)
        {
            spelareHitboxPos.Y = SpelarePosition.Y;
        }
        if (spelareHitboxPos.X != sköldPos.X)
        { sköldPos.X = spelareHitboxPos.X; }
        if (spelareHitboxPos.Y != sköldPos.Y)
        { sköldPos.Y = spelareHitboxPos.Y; } //

        base.Update(gameTime);
    }


    /// <param name="gameTime">Provides a snapshot of timing values.</param>
    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.CornflowerBlue);
        spriteBatch.Begin(); 
        if (levelCount == 1) { spriteBatch.Draw(Bakgrund, BakgrundPos, Color.White); }
        if (levelCount == 2) { spriteBatch.Draw(Bakgrund2, BakgrundPos, Color.White); }
        if (levelCount == 3) { spriteBatch.Draw(Bakgrund3, BakgrundPos, Color.White); }
        if (levelCount == 4) { spriteBatch.Draw(Bakgrund4, BakgrundPos, Color.White); }
        if (levelCount < 0) { spriteBatch.Draw(Bakgrund5, BakgrundPos, Color.White); } // sätter alla bakgrunder beroende på nivån.

        if (gamerunning == true) spriteBatch.Draw(Spelare, SpelarePosition, Color.White); // om spelet körs så skall spelaren visas, annars inte

        if (gamerunning == true) { if (sköld == true) { spriteBatch.Draw(SpelareSköld, SpelarePosition, Color.White); } } // om spelet körs skall skölden visas om skölden är på. annars inte.
        spriteBatch.DrawString(text, "Points: " + (points).ToString(), new Vector2(300, 200), Color.White);
        spriteBatch.DrawString(text, "Level: " + levelCount.ToString(), LeverTextPos, Color.White);
        spriteBatch.DrawString(text, "Health: " + healthLevel.ToString(), new Vector2(300, 100), Color.White); // lägger ut text på olika ställen med poäng, nivån och hälsan.
        foreach (var item in enemyLista)
        {
            item.Draw(spriteBatch);
        }
        foreach (var item in healthLista)
        {
            item.Draw(spriteBatch);
        } // uppdaterar alla objekt i fiende och hälsolistan. 
        if (debugmenu == true)
        {
            spriteBatch.DrawString(text, "Enemycount: " + enemyLista.Count.ToString(), new Vector2(1200, 100), Color.White);
            spriteBatch.DrawString(text, "Enemycount: " + sköld, new Vector2(1200, 300), Color.White);
            spriteBatch.DrawString(text, "Enemy spawnchance: 1/" + enemyspawnchance.ToString(), new Vector2(1200, 130), Color.White);
            spriteBatch.DrawString(text, "Click on P to stop the enemies, and then C to continue  ", new Vector2(1200, 200), Color.White);


        } // om debugmenyn är på (Caps lock) så visar den information som antal fiender, om skölden är på, fiendepawnchans och hur man pausar och sätter på spelet.
        spriteBatch.End();
        base.Draw(gameTime);
    }
}

