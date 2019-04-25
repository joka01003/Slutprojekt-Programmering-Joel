﻿
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
        Vector2 position;
        Texture2D texture;
        Vector2 center;
        Rectangle hitbox;


        public Enemy(Texture2D texture, Vector2 position)
        {
            this.texture = texture;
            this.center = position;
            hitbox = new Rectangle((int)this.center.X, (int)this.center.Y, 65, 65);


        }

        public Rectangle Gethitbox() { return hitbox; }

        public void Update()
        {
            this.center.X -= 5;
            hitbox.X -= 5;

        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, this.center, Color.White);
            spriteBatch.Draw(texture, new Rectangle(position.ToPoint(), new Point(15, 15)), Color.White);

        }



    }

}
