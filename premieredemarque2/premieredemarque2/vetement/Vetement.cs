
using System;
using Microsoft.Xna.Framework;

using Microsoft.Xna.Framework.Graphics;



namespace premieredemarque2.vetement
{


    class Vetement
    {

        public Texture2D textureV;
        public Texture2D textureP;
        public Vector2 Position;
        public Rectangle hitbox;
        private Boolean _prix;
        //time in second
        private int time;
        private int _createTime;
        SpriteFont police;
        public Game1 _jeu;
        public Boolean active;

        private int _type;
        private static Random rnd = new Random();

        public Circle priceArea;
        public Boolean _displayPriceArea;

        public Vetement(Game1 jeu, float prix, int time, int createTime)
        {
            this._jeu = jeu;
            // Set the player to be active
            this._prix = prix % 2 == 0;
            // Set the player health
            this.time = time;
            _createTime = createTime;
            active = false;
            _displayPriceArea = false;



            police = _jeu.Content.Load<SpriteFont>("SpriteFont1");
        }

        public void Initialize(Vector2 position)
        {
            _type = rnd.Next(1, 5);
            textureV = this._jeu.Content.Load<Texture2D>("promo" + _type);
            String ok = this.prix ? "-ok" : "-ko";
            textureP = this._jeu.Content.Load<Texture2D>("promo" + ok);

            // Set the starting position of the player around the middle of the screen and to the back
            Position = position;
            hitbox = new Rectangle((int)Position.X, (int)Position.Y, 50, 50);

            priceArea = new Circle(new Vector2((Position.X + 50 / 2), (Position.Y + 50 / 2)), 200);
        }

        public void Update(GameTime gameTime)
        {
            if (gameTime.TotalGameTime.Seconds >= _createTime && gameTime.TotalGameTime.Seconds <= _createTime + time)
            {
                active = true;
            }
            else
            {
                active = false;
            }
            // -= 1;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (active == true)
            {
                spriteBatch.Draw(textureV, Position, null, Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);
                if (this._displayPriceArea)
                {
                    spriteBatch.Draw(textureP, Position, null, Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);

                }

            }
        }
        public void displayPriceArea(Rectangle player)
        {
            if (active && this.priceArea.Intersects(player))
            {
                this._displayPriceArea = true;
            }
        }
        public Boolean instersect(Rectangle player, Boolean isEnemy)
        {
            if (!isEnemy)
            {
                this.displayPriceArea(player);
            }
            return active && player.Intersects(this.hitbox);

        }

        public int Width
        {
            get { return textureV.Width; }
        }

        // Get the height of the player ship
        public int Height
        {
            get { return textureV.Height; }
        }

        public Boolean Prix()
        {
            return prix;
        }

        public Boolean isActive()
        {
            return active;
        }

        public Boolean prix
        {

            get
            {
                return _prix;
            }
        }
    }
}