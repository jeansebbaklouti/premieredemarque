
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
        public Texture2D Left_Moove, Right_Moove, Up_Moove, Down_Moove;
        public float vitesse = 0;
        public float Posx, Posy;
        public static Random rand;
        public Vector2 position;
        public Rectangle hitbox;
        public int direction = 0; //0==haut;  1==bas;  2==droite;  3==gauche
        public float NoAnimation = 0.0f;
        public int alea = 0;
        public int total = 0;
        public int temps = 5;
        public float espaceaction;
        public int Vit_Limite = 1;
        public List<Texture2D> Fumee;
        public bool isFurie = false;
        public int idPerso;
        public Texture2D SpriteOthers, sd, sg, sb, sh;
        public int dead, attente;

        public Perso(Game1 jeu, Vector2 pos)
        {
            attente = 200;
            dead = 0;
            G = jeu;
            hitbox = new Rectangle((int)pos.X + 1, (int)pos.Y + 1, 50, 50);
            rand = new Random();
            position = pos;
            Fumee = new List<Texture2D>();
            espaceaction = 0;
        }

        public void Charger()
        {
            this.idPerso = rand.Next(12);
            if (!isFurie)
            {
                Vit_Limite = 1;
                Left_Moove = G.Content.Load<Texture2D>("left1");

                Right_Moove = G.Content.Load<Texture2D>("right1");

                Up_Moove = G.Content.Load<Texture2D>("up1");

                Down_Moove = G.Content.Load<Texture2D>("down1");

                sh = G.Content.Load<Texture2D>("sang-haut");
                sb = G.Content.Load<Texture2D>("sang-bas");
                sd = G.Content.Load<Texture2D>("sang-droite");
                sg = G.Content.Load<Texture2D>("sang-gauche");

                //Fumee.Add(G.Content.Load<Texture2D>("Fumee1"));
                //Fumee.Add(G.Content.Load<Texture2D>("Fumee2"));
            }
            else
            {
                Vit_Limite = 12;
            }
        }

        public void afficher(SpriteBatch _sb, Texture2D SpriteOthers, Boolean fury)
        {
            if (dead == 0)
            {
                if (!fury)
                {
                    _sb.Draw(SpriteOthers, this.position,
                                new Rectangle(this.idPerso * 50, 0, 50, 50),
                                Color.White);

                }
                else
                {
                    if (direction == 0)
                    {
                        _sb.Draw(SpriteOthers, this.position,
                                new Rectangle(this.idPerso * 50, 0, 50, 50),
                                Color.White);
                        //_sb.Draw(Fumee[(int)NoAnimation], new Vector2(position.X, position.Y + 50), Color.White);
                    }
                    else if (direction == 1)
                    {
                        _sb.Draw(SpriteOthers, this.position,
                                new Rectangle(this.idPerso * 50, 0, 50, 50),
                                Color.White);
                        //_sb.Draw(Fumee[(int)NoAnimation], new Vector2(position.X, position.Y - 50), Color.White);
                    }
                    else if (direction == 2)
                    {
                        _sb.Draw(SpriteOthers, this.position,
                                new Rectangle(this.idPerso * 50, 0, 50, 50),
                                Color.White);
                        //_sb.Draw(Fumee[(int)NoAnimation], new Vector2(position.X + 50, position.Y), Color.White);
                    }
                    else if (direction == 3)
                    {
                        _sb.Draw(SpriteOthers, this.position,
                                new Rectangle(this.idPerso * 50, 0, 50, 50),
                                Color.White);
                        //_sb.Draw(Fumee[(int)NoAnimation], new Vector2(position.X - 50, position.Y), Color.White);
                    }
                }
            }
            else if (dead == 1)
            {
                _sb.Draw(sh, this.position, Color.White);
            }
            else if (dead == 2)
            {
                _sb.Draw(sb, this.position, Color.White);
            }
            else if (dead == 3)
            {
                _sb.Draw(sd, this.position, Color.White);
            }
            else if (dead == 4)
            {
                _sb.Draw(sg, this.position, Color.White);
            }
        }

        public void Maj(GameTime time, List<Rectangle> Rec, Boolean B)
        {
            if (B)
            {
                Vit_Limite = 5;
            }
            else
            {
                Vit_Limite = 1;
            }
            if (dead == 0)
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
        }

        public void Aleatoire()
        {
            //rand = new Random();
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
                    else if (direction == 0)
                    {
                        direction = 1;
                        position.Y += vitesse + 1;
                        hitbox.Y = (int)position.Y;
                        vitesse = 0;
                        //return false;
                    }
                    else if (direction == 2)
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