﻿using System;
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

        private int _nbVetementBought =0;
        private List<Vetement> _currentVetements;

        private List<Vetement> _boughtVetements;

        private List<Vetement> _failBoughtVetements;

        private Score _score;

        private Game1 _jeu;

        private Map _playground;

        private int _time;

        private int _money;

        private Gauje timegauje;

        private Gauje moneygauje;

        private Hero _joueur;

        private int _status = 0;

        public Gestion(Game1 jeu, int maxSoldes, Map playground, Hero joueur)
        {
            this._joueur = joueur;
            this._jeu = jeu;
            _playground = playground;

            this._maxSoldes = maxSoldes;
            this._score = new Score();
            _currentVetements = new List<Vetement>();
            _boughtVetements = new List<Vetement>();
            _failBoughtVetements = new List<Vetement>();
            timegauje = new Gauje(jeu, "temps", new Vector2(50, 50));

            moneygauje = new Gauje(jeu, "sous", new Vector2(500, 50));

            _money = 10;
            _time = 90;
            
        }

        public void  charger(){

            timegauje.Initialize();
            moneygauje.Initialize();
        }

        public void Update( GameTime gameTime)
        {
           
            if (soldeTimeIsterminated())
            {
                _failBoughtVetements.AddRange(_currentVetements);
                _currentVetements.RemoveRange(0, _currentVetements.Count );

            }
            if (_currentVetements.Count == 0)
            {
                loadSoldes(gameTime.TotalGameTime.Seconds);
            }
            // TODO : ajouter la logique de mise à jour ici
            foreach (Vetement vetement in _currentVetements)
            {
                vetement.Update(gameTime);
            }
          
            _time = 90 - gameTime.TotalGameTime.Seconds + (gameTime.TotalGameTime.Minutes * 60);
            timegauje.Value = (_time * 10) /90;

            moneygauje.Value = _money;

        }

        public Boolean endLevel()
        {
            if (_time <= 0)
            {
                _status = 1;
                return true;
            }
            if(_money <= 0){
                _status = 1;
                return true;
            }

            if (_joueur.stress >= 100)
            {
                _status = -1;
                return true;
            }

            if (_joueur.nbVetementBought < 0)
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
        public void loadSoldes(int currentTime){

            Random rnd = new Random();
            for (var i = 0; i < this._maxSoldes; i++ )
            {
                Vetement vetement = new Vetement(this._jeu, rnd.Next(1, 100), 2, currentTime);
                Vector2 vetementPosition = findEmptyCase(rnd);
                    vetement.Initialize(vetementPosition);
                    _currentVetements.Add(vetement);
            }
    
        }

        private Vector2 findEmptyCase(Random rnd)
        {

            int poxX = rnd.Next(1, _playground.getLengthX() -1);
            int poxY = rnd.Next(1, _playground.getLengthY() -1);
            while(_playground.isEmpty(poxX,poxY)){
                poxX = rnd.Next(1, _playground.getLengthX() - 1);
                poxY = rnd.Next(1, _playground.getLengthY() - 1);
            }

            return new Vector2(poxX*50 + 10, poxY*50);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            // Draw the Player
            foreach (Vetement vetement in _currentVetements)
            {
                vetement.Draw(spriteBatch);
            }
            timegauje.Draw(spriteBatch);
            moneygauje.Draw(spriteBatch);
        }

        public void instersectVetement(Rectangle player, Boolean enemy){

            Vetement vetementTemp = null;
            foreach(Vetement vetement in _currentVetements ){
                if (vetement.instersect(player))
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

        public void takeSolde(Vetement vetement){
            _money-= vetement.prix?1:2;
            _joueur.nbVetementBought+= vetement.prix ? 1 : -1;
            _currentVetements.Remove(vetement);
            _boughtVetements.Add(vetement);
        }

        public int getScore(){
            return _score.getValue();

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
