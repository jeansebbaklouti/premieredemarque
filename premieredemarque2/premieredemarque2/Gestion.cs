using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using premieredemarque2.vetement;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace premieredemarque2
{
    class Gestion
    {
        private int _maxSoldes;

        private int _nbVetementBought = 0;
        public List<Vetement> _currentVetements;

        private List<Vetement> _boughtVetements;

        private List<Vetement> _failBoughtVetements;

        private Score _score;

        private Game1 _jeu;

        private Map _playground;

        public int _time;

        private Gauje timegauje;

        private Hero _joueur;

        private int _status = 0;

        public Boolean isFury = false;

        private static int PARTY_TIME = 90;

        public Gestion(Game1 jeu, int maxSoldes, Map playground, Hero joueur)
        {
            this._joueur = joueur;
            this._jeu = jeu;
            _playground = playground;

            this._maxSoldes = maxSoldes;
            this._score = new Score(jeu);
            _currentVetements = new List<Vetement>();
            _boughtVetements = new List<Vetement>();
            _failBoughtVetements = new List<Vetement>();
            timegauje = new Gauje(jeu, "temps", new Vector2(160, 25));

            _time = PARTY_TIME;
            _time = 41;

        }

        public void charger()
        {
            timegauje.Initialize();
            _score.Initialize();
          }

        public void Update(GameTime gameTime, int GameState)
        {

            if (soldeTimeIsterminated())
            {
                _failBoughtVetements.AddRange(_currentVetements);
                _currentVetements.RemoveRange(0, _currentVetements.Count);

            }
            if (_currentVetements.Count == 0)
            {
                loadSoldes(gameTime.TotalGameTime.Seconds);
            }
            foreach (Vetement vetement in _currentVetements)
            {
                vetement.Update(gameTime);
            }


            if (GameState == 1)
            {
                _time = 41 - ((int)gameTime.TotalGameTime.TotalSeconds - _jeu.startTime) % 41;
                timegauje.Value = ((_time * 11)-1) / 41;
                if (_time <= 15 && _time > 0)
                {
                    isFury = true;
                }
                else if (_time == 41)
                {
                    isFury = false;
                }
            }
            _score.Update(gameTime);
        }

        public void updateAfterPlayer()
        {

            if(_joueur.tuer){
                _score.addBonus( Score.BonusType.kill, _joueur._position);
            }
            if (_joueur.toucher)
            {
                _score.addBonus(Score.BonusType.fall, _joueur._position);
            }
        }




        public Boolean endLevel()
        {
            if (isFury && _joueur.hitbox.Intersects(_playground.sortie))
            {
                _status = 1;
                return true;
            }

            if (_time == 1)
            {
                _status = -1;
                return true;
            }

            if (_joueur.stress >= 10)
            {
                _status = -1;
                return true;
            }

            if (_joueur.nbVetementBought < -2)
            {
                _status = -2;
                return true;
            }

            return false;

        }

        private Boolean soldeTimeIsterminated()
        {
            foreach (Vetement vetement in _currentVetements)
            {
                if (vetement.isActive()) return false;
            }
            return true;
        }
        /**
         * when there are not more solde, load solde
         */
        public void loadSoldes(int currentTime)
        {

            Random rnd = new Random();
            for (var i = 0; i < this._maxSoldes; i++)
            {
                Vetement vetement = new Vetement(this._jeu, rnd.Next(1, 100), 2, currentTime);
                Vector2 vetementPosition = findEmptyCase(rnd);
                vetement.Initialize(vetementPosition);
                _currentVetements.Add(vetement);
            }

        }

        private Vector2 findEmptyCase(Random rnd)
        {

            int poxX = rnd.Next(1, _playground.getLengthX() - 1);
            int poxY = rnd.Next(1, _playground.getLengthY() - 1);
            while (_playground.isEmpty(poxX, poxY))
            {
                poxX = rnd.Next(1, _playground.getLengthX() - 1);
                poxY = rnd.Next(1, _playground.getLengthY() - 1);
            }

            return new Vector2(-10 + poxX * 50, poxY * 50);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            // Draw the Player
            foreach (Vetement vetement in _currentVetements)
            {
                vetement.Draw(spriteBatch);
            }
            timegauje.Draw(spriteBatch);

            _score.Draw(spriteBatch);
        }

        public void instersectVetement(Rectangle player, Boolean enemy)
        {

            Vetement vetementTemp = null;
            foreach (Vetement vetement in _currentVetements)
            {
                if (vetement.instersect(player, enemy))
                {
                    vetementTemp = vetement;
                }
            }
            if (vetementTemp != null)
            {
                if (enemy)
                {
                    takeSoldeEnemy(vetementTemp);
                }
                else
                {
                    takeSolde(vetementTemp);
                }

            }

        }

        public void takeSoldeEnemy(Vetement vetement)
        {
            _currentVetements.Remove(vetement);
            _failBoughtVetements.Add(vetement);
        }

        public void takeSolde(Vetement vetement)
        {
            _joueur.nbVetementBought += vetement.prix ? 1 : -1;
            _currentVetements.Remove(vetement);
            _boughtVetements.Add(vetement);

            _score.addBonus(vetement.prix ? Score.BonusType.takeObject : Score.BonusType.loseObject, _joueur._position);

        }

        public int getScore()
        {
            return _score.getValue();

        }

        public int getStatus()
        {
            return _status;

        }


        public int time
        {

            get
            {
                return _time;
            }
        }

    }



}
