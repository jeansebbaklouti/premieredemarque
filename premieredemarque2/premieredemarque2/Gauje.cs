using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;


namespace premieredemarque2
{
    class Gauje
    {

        public List<Texture2D> textures;
        public Vector2 Position;
        private int _value;

        private String _type; 
        private Game1 _jeu;

        public Gauje(Game1 jeu,string type, Vector2 vect)
        {
            this._jeu = jeu;
            this._value = 10;
            this._type = type;
            this.Position = vect;
            textures = new List<Texture2D>();
        }
        public void decrementValue()
        {
            _value-= 10;
        }

        public int Value
        {
            set
            {
                _value = value;
            }
            get
            {
                return _value;
            }
        }

        public void Initialize()
        {
            textures.Add(this._jeu.Content.Load<Texture2D>("jauge0"));
            for (var i = 1; i <= 10;i++ )
            {
                textures.Add(this._jeu.Content.Load<Texture2D>("jauge-" + _type + i + "0"));
            }
            // Set the starting position of the player around the middle of the screen and to the back
               
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if(_value >= 0 &&  _value<=10){
            spriteBatch.Draw(textures[_value], Position, null, Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);
            }
                
        }

    }

    
}
