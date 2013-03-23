
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using System.Timers;

namespace premieredemarque2
{
    class Perso
    {
        public static float acceleration = 0.6f;
        public static float decceleration = 0.9f;
        public Game1 G;
        public List<Texture2D> Left_Moove, Right_Moove, Up_Moove, Down_Moove;
        public float vitesse = 0;
        public float Posx, Posy;
        public Random rand;
        public Vector2 position;
        public Rectangle hitbox;
        public int direction = 0; //0==haut;  1==bas;  2==droite;  3==gauche
        public float NoAnimation = 0.0f;
        public int alea = 0;
        public int total = 0;
        public int temps = 5;
        public float espaceaction;
        public int Vit_Limite = 10;

        public Perso(Game1 jeu, Vector2 pos)
        {
            G = jeu;
            Left_Moove = new List<Texture2D>();
            Right_Moove = new List<Texture2D>();
            Up_Moove = new List<Texture2D>();
            Down_Moove = new List<Texture2D>();
            hitbox = new Rectangle((int)pos.X +1, (int)pos.Y +1, 50, 50);
            rand = new Random();
            position = pos;
            espaceaction = 0;
        }

        public void Charger()
        {
            rand = new Random();
            Left_Moove.Add(G.Content.Load<Texture2D>("left1"));
            Left_Moove.Add(G.Content.Load<Texture2D>("left2"));

            Right_Moove.Add(G.Content.Load<Texture2D>("right1"));
            Right_Moove.Add(G.Content.Load<Texture2D>("right2"));

            Up_Moove.Add(G.Content.Load<Texture2D>("up1"));
            Up_Moove.Add(G.Content.Load<Texture2D>("up2"));

            Down_Moove.Add(G.Content.Load<Texture2D>("down1"));
            Down_Moove.Add(G.Content.Load<Texture2D>("down2"));
        }

        public void afficher(SpriteBatch _sb)
        {
            if (direction == 0)
            {
                _sb.Draw(Up_Moove[(int)NoAnimation], this.position, Color.White);
            }
            else if (direction == 1)
            {
                _sb.Draw(Down_Moove[(int)NoAnimation], this.position, Color.White);
            }
            else if (direction == 2)
            {
                _sb.Draw(Right_Moove[(int)NoAnimation], this.position, Color.White);
            }
            else if (direction == 3)
            {
                _sb.Draw(Left_Moove[(int)NoAnimation], this.position, Color.White);
            }
        }

        public void Maj(GameTime time, List<Rectangle> Rec)
        {
            Inter(Rec);
            if (new Rectangle(90, 50, 1280, 700).Intersects(this.hitbox))
            {
                this.Aleatoire();
                if (direction == 0)
                {
                    NoAnimation = (NoAnimation + 1 * 0.3f) % 2;
                    if (direction != 0)
                    {
                        vitesse = 0;
                    }
                    if (vitesse <= Vit_Limite)
                    {
                        vitesse += acceleration;
                    }
                    direction = 0;
                }
                else if (direction == 1)
                {
                    NoAnimation = (NoAnimation + 1 * 0.3f) % 2;
                    if (direction != 1)
                    {
                        vitesse = 0;
                    }
                    if (vitesse <= Vit_Limite)
                    {
                        vitesse += acceleration;
                    }
                    direction = 1;
                }
                else if (direction == 2)
                {
                    NoAnimation = (NoAnimation + 1 * 0.3f) % 2;
                    if (direction != 2)
                    {
                        vitesse = 0;
                    }
                    if (vitesse <= Vit_Limite)
                    {
                        vitesse += acceleration;
                    }
                    direction = 2;
                }
                else if (direction == 3)
                {
                    NoAnimation = (NoAnimation + 1 * 0.3f) % 2;
                    if (direction != 3)
                    {
                        vitesse = 0;
                    }
                    if (vitesse <= Vit_Limite)
                    {
                        vitesse += acceleration;
                    }
                    direction = 3;
                }
                else
                {
                    vitesse -= decceleration;
                    NoAnimation = 0;
                    if (vitesse <= 0)
                    {
                        vitesse = 0;
                    }
                }
                if (direction == 0)
                {
                    position.Y -= vitesse;
                    hitbox.Y = (int)position.Y;
                }
                else if (direction == 1)
                {
                    position.Y += vitesse;
                    hitbox.Y = (int)position.Y;
                }
                else if (direction == 2)
                {
                    position.X += vitesse;
                    hitbox.X = (int)position.X;
                }
                else if (direction == 3)
                {
                    position.X -= vitesse;
                    hitbox.X = (int)position.X;
                }
            }
            else
            {
                if (direction == 0)
                {
                    position.Y += vitesse + 1;
                    hitbox.Y = (int)position.Y;
                }
                else if (direction == 1)
                {
                    position.Y -= vitesse + 1;
                    hitbox.Y = (int)position.Y;
                }
                else if (direction == 2)
                {
                    position.X -= vitesse + 1;
                    hitbox.X = (int)position.X;
                }
                else if (direction == 3)
                {
                    position.X += vitesse + 1;
                    hitbox.X = (int)position.X;
                }
                vitesse = 0;
            }
        }

        public void Aleatoire()
        {
            rand = new Random();
            int anciendir, nouveaudir;
            int tmp = (int)espaceaction;
            espaceaction = espaceaction + 0.03f;
            if (tmp != (int)espaceaction)
            {
                anciendir = direction;
                do
                {
                    nouveaudir = rand.Next() % 4;
                } while (anciendir == nouveaudir);

                direction = nouveaudir;
            }
        }

        public void Inter(List<Rectangle> Rec)
        {
            foreach (Rectangle R in Rec)
            {
                if (R.Intersects(hitbox))
                {
                    if (direction == 1)
                    {
                        direction = 0;
                        position.Y -= vitesse + 1;
                        hitbox.Y = (int)position.Y;
                        vitesse = 0;
                        //return false;
                    }
                    else if ( direction == 0)
                    {
                        direction = 1;
                        position.Y += vitesse + 1;
                        hitbox.Y = (int)position.Y;
                        vitesse = 0;
                        //return false;
                    }
                    else if ( direction == 2)
                    {
                        direction = 3;
                        position.X -= vitesse + 1;
                        hitbox.X = (int)position.X;
                        vitesse = 0;
                        //return false;
                    }
                    else if (direction == 3)
                    {
                        direction = 2;
                        position.X += vitesse + 1;
                        hitbox.X = (int)position.X;
                        vitesse = 0;
                        //return false;
                    }
                }
            }
            //return true;
        }
    }
}