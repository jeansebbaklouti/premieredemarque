using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace premieredemarque2
{
    class Level
    {
        public int[,] _map;
        public Vector2 _start;
        public Vector2 _bonus;

        private int TileSize = 50;

        public Level(int[,] map, int[] start, int[] bonus)
        {
            this._map = map;
            this._start = new Vector2(start[0] * TileSize, start[1] * TileSize);
            this._bonus = new Vector2(bonus[0] * TileSize, bonus[1] * TileSize);
        }

        public Vector2 getStart()
        {
            return this._start;
        }

        public Vector2 getBonus()
        {
            return this._bonus;
        }

        internal int[,] getMap()
        {
            return this._map;
        }
    }
}
