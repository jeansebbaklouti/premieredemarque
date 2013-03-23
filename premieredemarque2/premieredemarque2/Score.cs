using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace premieredemarque2
{
    class Score
    {

        private int _value;

        public Score(){
           _value = 0;
        }

        public int calculFinalScore(int stress, int time, int money)
        {
            return money * 100 + time * 100 - stress * 200;
        }

        public int getValue()
        {
            return _value;
        }
    }
}
