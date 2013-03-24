using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace premieredemarque2
{
    class Score
    {
    
        private int _value;

        private SpriteFont police;

        public Texture2D     textureBonus10;

        public Texture2D textureBonus100;

        public Texture2D textureMalus10;

        public Texture2D textureMalus100;



        private Game1 _game;

        private Vector2 Position;

        private static double TIME_TO_DISPLAY = 0.5;

        private List<Bonus> _currentBonus;

        public double _currentTime;

        public enum BonusType
        {
            kill = 1,
            takeObject = 2,
            loseObject = 3,
            fall = 4
        }

        public Score(Game1 jeu)
        {
            _game = jeu;
            _currentBonus = new List<Bonus>();
            _currentTime = 0;
        }

       

        public Score(){
           _value = 0;

        }

        public int calculFinalScore(int stress, int time, int nbObject)
        {
            return _value + nbObject * 100 + time * 100 - stress * 200;
        }

        public int getValue()
        {
            return _value;
        }

        public void addBonus(BonusType bonusType, Vector2 position)
        {
            int typeBonus = 0;
            
            switch (bonusType)
            {

                case BonusType.kill:
                    typeBonus = 1;
                    _value+= 100;
                    
                    
                    break;
                case BonusType.takeObject:
                    typeBonus = 2;
                    _value+= 10;
            
                    break;
                case BonusType.loseObject:
                    typeBonus = 3;
                    _value-= 100;

                    break;
                case BonusType.fall:
                    typeBonus = 4;
                    _value-= 10;
      
                    break; 

            }

            _currentBonus.Add(new Bonus("kill", _currentTime, typeBonus, position));

        }




        public void Initialize()
        {

            police = _game.Content.Load<SpriteFont>("SpriteFont1");

        
            textureBonus10 = this._game.Content.Load<Texture2D>("bonus10");
            textureBonus100 = this._game.Content.Load<Texture2D>("bonus100");
            textureMalus100 = this._game.Content.Load<Texture2D>("malus100");
            textureMalus10 = this._game.Content.Load<Texture2D>("malus10");
  

        }

        public void Update(GameTime gameTime)
        {

            _currentTime = gameTime.TotalGameTime.TotalSeconds;
            _currentBonus.RemoveAll(o => TIME_TO_DISPLAY < gameTime.TotalGameTime.TotalSeconds - o.createTime);
          
        }

        public void Draw(SpriteBatch spriteBatch)
        {

            foreach (Bonus bonus in _currentBonus)
            {
                // spriteBatch.DrawString(police, bonus.texToDisplay, bonus.position, Color.Green);
                if (bonus.type == 4)
                {
                    spriteBatch.Draw(textureMalus10, bonus.position, null, Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);
                }
                {
                    if (bonus.type == 3)
                    {
                        spriteBatch.Draw(textureMalus100, bonus.position, null, Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);
                    }
                    else
                    {
                        if (bonus.type == 1)
                        {
                            spriteBatch.Draw(textureBonus100, bonus.position, null, Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);
                        }
                        else
                        {

                            if (bonus.type == 2)
                            {
                                spriteBatch.Draw(textureBonus10, bonus.position, null, Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);
                            }
                        }

                    }
                }

            }


               
        }

       
    }
}
