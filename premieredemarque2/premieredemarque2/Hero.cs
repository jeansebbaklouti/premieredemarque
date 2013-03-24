
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace premieredemarque2
{
    public class Hero
    {
        public static float _acceleration = 0.6f;
        public static float _decceleration = 0.8f;

        public Game1 _jeu;
        public Vector2 _position;
        public float _vitesse;
        public List<Texture2D> animationup, animationdown, animationleft, animationright;
        //LISTE ANIMATIONS FUMEE
        public List<Texture2D> fumeup, fumedown, fumeleft, fumeright;
        //
        public Texture2D animstun;
        public Rectangle hitbox;
        public int _direction; //0 = haut; 1 = bas; 2 = droite; 3 = gauche 4 = rien
        public float _noanimation;
        public Boolean marche;
        public Boolean taper;
        public Boolean toucher;
        public Boolean tuer;
        public int stress;
        public int tpsinvincible;
        public Boolean invincible;
        Gauje stressGauje;
        public Boolean isFury;
        public int stop;
        //AJOUT DE FUMEE LORSQUE ON PEUT TUER LES PNG
        public Boolean Fume;
        //

        public int nbVetementBought = 0;

        public Hero(Game1 jeu, Vector2 pos)
        {
            Fume = false;
            tpsinvincible = 0;
            invincible = false;
            tuer = false;
            taper = false;
            toucher = false;
            stress = 0;
            _noanimation = 0;
            _jeu = jeu;
            _direction = 0;
            stop = 0;
            _position = pos;
            hitbox = new Rectangle((int)pos.X, (int)pos.Y, 50, 50);
            _vitesse = 0;

            animationup = new List<Texture2D>();
            animationdown = new List<Texture2D>();
            animationright = new List<Texture2D>();
            animationleft = new List<Texture2D>();

            //INSTANCIE LES LISTE ANIM FUMEE
            fumeup = new List<Texture2D>();
            fumedown = new List<Texture2D>();
            fumeright = new List<Texture2D>();
            fumeleft = new List<Texture2D>();
            //

            marche = false;

            stressGauje = new Gauje(jeu, "stress", new Vector2(1000, 25));
        }

        public void charger()
        {


            animationup.Add(_jeu.Content.Load<Texture2D>("heros-up1"));
            animationup.Add(_jeu.Content.Load<Texture2D>("heros-up2"));

            animationdown.Add(_jeu.Content.Load<Texture2D>("heros-down1"));
            animationdown.Add(_jeu.Content.Load<Texture2D>("heros-down2"));

            animationleft.Add(_jeu.Content.Load<Texture2D>("heros-left1"));
            animationleft.Add(_jeu.Content.Load<Texture2D>("heros-left2"));

            animationright.Add(_jeu.Content.Load<Texture2D>("heros-right1"));
            animationright.Add(_jeu.Content.Load<Texture2D>("heros-right2"));

            //IMPORT IMAGE FUMEE
            fumeup.Add(_jeu.Content.Load<Texture2D>("fumee-haut1"));
            fumeup.Add(_jeu.Content.Load<Texture2D>("fumee-haut2"));

            fumedown.Add(_jeu.Content.Load<Texture2D>("fumee-bas1"));
            fumedown.Add(_jeu.Content.Load<Texture2D>("fumee-bas2"));

            fumeleft.Add(_jeu.Content.Load<Texture2D>("fumee-gauche1"));
            fumeleft.Add(_jeu.Content.Load<Texture2D>("fumee-gauche2"));

            fumeright.Add(_jeu.Content.Load<Texture2D>("fumee-droite1"));
            fumeright.Add(_jeu.Content.Load<Texture2D>("fumee-droite2"));
            //

            animstun = _jeu.Content.Load<Texture2D>("ecrase");
            stressGauje.Initialize();
        }

        internal void Maj(GameTime time, List<Perso> lperso, List<Rectangle> mur)
        {
            int i = istoucher(lperso);
            if (i == -1 || invincible)
            {
                if (new Rectangle(49, 49, 1182, 702).Intersects(this.hitbox) && touchemur(mur) == false)
                {
                    taper = false;
                    tuer = false;
                    toucher = false;
                    if (stop == 0)
                    {
                        tpsinvincible++;
                        if (tpsinvincible > 100)
                        {
                            tpsinvincible = 0;
                            invincible = false;
                        }
                        KeyboardState currentKeys = Keyboard.GetState();

                        if (currentKeys.IsKeyDown(Keys.Up) && (_direction != 1 || _vitesse == 0))
                        {
                            _noanimation = (_noanimation + 1 * 0.25f) % 2;
                            if (_direction != 0)
                            {
                                _vitesse = 0;
                            }
                            if (_vitesse <= 20)
                            {
                                _vitesse += _acceleration;
                            }
                            _direction = 0;
                        }
                        else if (currentKeys.IsKeyDown(Keys.Down) && (_direction != 0 || _vitesse == 0))
                        {
                            _noanimation = (_noanimation + 1 * 0.25f) % 2;
                            if (_direction != 1)
                            {
                                _vitesse = 0;
                            }
                            if (_vitesse <= 20)
                            {
                                _vitesse += _acceleration;
                            }
                            _direction = 1;
                        }
                        else if (currentKeys.IsKeyDown(Keys.Right) && (_direction != 3 || _vitesse == 0))
                        {
                            _noanimation = (_noanimation + 1 * 0.25f) % 2;
                            if (_direction != 2)
                            {
                                _vitesse = 0;
                            }
                            if (_vitesse <= 20)
                            {
                                _vitesse += _acceleration;
                            }
                            _direction = 2;
                        }
                        else if (currentKeys.IsKeyDown(Keys.Left) && (_direction != 2 || _vitesse == 0))
                        {
                            _noanimation = (_noanimation + 1 * 0.25f) % 2;
                            if (_direction != 3)
                            {
                                _vitesse = 0;
                            }
                            if (_vitesse <= 20)
                            {
                                _vitesse += _acceleration;
                            }
                            _direction = 3;
                        }
                        else
                        {
                            _vitesse -= _decceleration;
                            _noanimation = 0;
                            _vitesse = (_vitesse <= 0) ? 0 : _vitesse;

                        }

                        //DEFINI LE BOOLEAN FUME
                        if (_vitesse >= 19.9)
                        {
                            Fume = true;
                        }
                        else
                        {
                            Fume = false;
                        }
                        //

                        if (_direction == 0)
                        {
                            _position.Y -= _vitesse;
                            hitbox.Y = (int)_position.Y;
                        }
                        else if (_direction == 1)
                        {
                            _position.Y += _vitesse;
                            hitbox.Y = (int)_position.Y;
                        }
                        else if (_direction == 2)
                        {
                            _position.X += _vitesse;
                            hitbox.X = (int)_position.X;
                        }
                        else if (_direction == 3)
                        {
                            _position.X -= _vitesse;
                            hitbox.X = (int)_position.X;
                        }
                    }
                    else if (stop > 0)
                    {
                        stop--;
                    }
                }
                else
                {
                    if (_direction == 0)
                    {
                        _position.Y += _vitesse + 1;
                        hitbox.Y = (int)_position.Y;
                    }
                    else if (_direction == 1)
                    {
                        _position.Y -= _vitesse + 1;
                        hitbox.Y = (int)_position.Y;
                    }
                    else if (_direction == 2)
                    {
                        _position.X -= _vitesse + 1;
                        hitbox.X = (int)_position.X;
                    }
                    else if (_direction == 3)
                    {
                        _position.X += _vitesse + 1;
                        hitbox.X = (int)_position.X;
                    }

                    _vitesse = 0;
                }
                marche = _vitesse > 0;

            }
            else if (i != -1 && _vitesse >= 19.9)
            {
                //lperso.RemoveAt(i);
                //tuer = true;
                lperso[i].hitbox = new Rectangle(-1, -1, 0, 0);
                lperso[i].vitesse = 0;
                lperso[i].dead = _direction + 1;

                stress--;
                if (stress < 0)
                {
                    stress = 0;
                }
                tuer = true;
                taper = true;
            }
            else if (i != -1)
            {
                if (isFury == true)
                {
                    stop = 70;
                }
                stress++;
                toucher = true;
                invincible = true;
                _vitesse = 0;
                //nbVetementBought--;
            }
            stressGauje.Value = stress;

        }

        internal int istoucher(List<Perso> lpers)
        {
            int i;
            for (i = 0; i < lpers.Count; i++)
            {
                if (lpers[i].hitbox.Intersects(this.hitbox))
                {
                    return i;
                }
            }
            return -1;
        }

        internal bool touchemur(List<Rectangle> lrect)
        {
            foreach (Rectangle r in lrect)
            {
                if (r.Intersects(this.hitbox))
                {
                    return true;
                }
            }
            return false;
        }

        public void afficher(SpriteBatch _sb)
        {
            //AJOUT CONDITION POUR SAVOIR SI ON PEUT TUER LES CLIENTS
            if (stop == 0 && Fume == false)
            {
                switch (_direction)
                {
                    case 0:

                        _sb.Draw(animationup[(int)_noanimation], this._position, Color.White);
                        break;
                    case 1:

                        _sb.Draw(animationdown[(int)_noanimation], this._position, Color.White);

                        break;

                    case 2:

                        _sb.Draw(animationright[(int)_noanimation], this._position, Color.White);
                        break;

                    case 3:

                        _sb.Draw(animationleft[(int)_noanimation], this._position, Color.White);
                        break;
                }
            }
            else if (stop > 0)
            {
                _sb.Draw(animstun, this._position, Color.White);
            }
            else if (Fume == true)
            {
                switch (_direction)
                {
                    case 0:
                        _sb.Draw(fumeup[(int)_noanimation], new Vector2(this._position.X, this._position.Y + 50), Color.White);
                        _sb.Draw(animationup[(int)_noanimation], this._position, Color.White);
                        break;
                    case 1:
                        _sb.Draw(fumedown[(int)_noanimation], new Vector2(this._position.X, this._position.Y - 20), Color.White);
                        _sb.Draw(animationdown[(int)_noanimation], this._position, Color.White);
                        break;

                    case 2:
                        _sb.Draw(fumeright[(int)_noanimation], new Vector2(this._position.X - 70, this._position.Y + 25), Color.White);
                        _sb.Draw(animationright[(int)_noanimation], this._position, Color.White);
                        break;

                    case 3:
                        _sb.Draw(fumeleft[(int)_noanimation], new Vector2(this._position.X + 45, this._position.Y + 25), Color.White);
                        _sb.Draw(animationleft[(int)_noanimation], this._position, Color.White);
                        break;
                }
            }
            //
            stressGauje.Draw(_sb);
        }
    }
}