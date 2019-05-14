
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Game2
{
    class Enemy
    {
        //BÅDA KLASSERNA HAR SAMMA FUNKTION MED OLIKA OBJEKT! DESSA KOMMENTARER KAN TILLÄMPAS TILL DEN ANDRA OCKSÅ
        Vector2 position;
        Texture2D texture;
        Vector2 center;
        Rectangle hitbox;


        public Enemy(Texture2D texture, Vector2 position)
        {
            this.texture = texture; //sätter texturet till det som skickas in
            this.center = position; // -II- fast med positionen
            hitbox = new Rectangle((int)this.center.X, (int)this.center.Y, 70, 70); //skapar dess hitbox 


        }

        public Rectangle Gethitbox() { return hitbox; } //returnerar hitboxen och uppdateras när update funktionen används.

        public void Update()
        {
            this.center.X -= 5; // sätter hastigheten för den visuella fienden
            hitbox.X -= 5;  //sätter hastigheten för hitboxen

        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, this.center, Color.Red); //Renderar Fienden
            spriteBatch.Draw(texture, new Rectangle(position.ToPoint(), new Point(15, 15)), Color.White);

        }



    }

}
