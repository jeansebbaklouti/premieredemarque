using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace premieredemarque2.vetement
{
    class Coupon:Vetement
    {
        public Vector2 _Position;

        public Coupon(Game1 jeu, int time, int createTime, Vector2 position):base(jeu, 1, time, createTime)
        {
            active = true;
            this._Position = position;
        }

        public void Initialize()
        {
            textureV = this._jeu.Content.Load<Texture2D>("bonus-fin");
            hitbox = new Rectangle(-10 + (int)this._Position.X, (int)this._Position.Y, 100, 50);
        }
    }
}
