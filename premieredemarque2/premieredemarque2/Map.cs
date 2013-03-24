﻿
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

namespace premieredemarque2
{
    class Map
    {
        private List<Level> _levels;
        private int[,] _data;
        private Vector2 _start;
        private Vector2 _bonus;
        private List<Rectangle> _murs;
        private Texture2D _rect;
        private int TileSize = 50;

        int[,] map1 =
            {
                { 4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4, },
                { 4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4, },
                { 1,1,1,1,1,1,1,3,3,3,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1, },
                { 1,1,1,1,1,1,1,3,3,3,1,1,1,1,1,1,1,5,1,1,1,1,1,1,1,1, },
                { 4,4,4,4,4,1,1,1,3,1,1,1,4,4,4,1,1,5,1,1,1,1,1,4,4,4, },
                { 4,4,4,4,4,1,1,1,1,1,1,1,4,4,4,1,1,5,1,1,1,1,1,4,4,4, },
                { 1,1,1,1,1,1,1,1,1,1,1,1,4,4,4,1,1,5,1,1,1,1,1,1,1,61, },
                { 1,1,1,1,1,1,1,1,6,1,1,1,1,1,1,1,1,5,1,1,1,1,1,1,1,1, },
                { 4,4,4,4,4,1,1,6,1,6,1,1,1,1,1,1,1,1,1,1,2,1,1,4,4,4, },
                { 4,4,4,4,4,1,1,1,6,1,1,1,1,1,1,1,1,1,1,1,2,1,1,4,4,4, },
                { 1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,5,1,1,1,1,1,1,1,1, },
                { 1,1,1,1,1,1,1,1,1,1,1,1,4,4,4,1,1,5,1,1,1,1,1,1,1,1, },
                { 4,4,4,4,4,1,1,1,1,1,1,1,4,4,4,1,1,5,1,1,1,1,1,4,4,4, },
                { 4,4,4,4,4,1,1,1,1,1,1,1,4,4,4,1,1,5,1,1,1,1,1,4,4,4, },
                { 1,1,1,1,1,1,1,1,2,1,1,1,1,1,1,1,1,5,1,1,1,1,1,1,1,1, },
                { 52,1,1,1,1,1,1,2,2,2,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1 }
            };
        int[,] map2 = 
            {
                { 4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4, },
                { 4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4, },
                { 1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,4,4,1,1,1,1,1,1,1,1, },
                { 1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,4,4,1,1,1,1,1,1,1,1, },
                { 4,4,4,4,4,4,4,4,1,1,1,1,1,1,1,1,4,4,1,1,1,1,1,1,1,1, },
                { 4,4,4,4,4,4,4,4,1,1,1,1,1,1,1,1,4,4,1,1,1,1,1,1,1,1, },
                { 1,1,1,1,1,1,1,1,1,1,1,5,1,5,1,1,1,1,1,1,1,1,5,5,5,5, },
                { 1,1,1,1,1,1,1,1,1,1,1,5,1,5,1,1,1,1,3,3,1,1,1,1,1,1, },
                { 1,1,1,1,1,1,1,1,1,1,1,5,1,5,1,1,1,3,1,1,3,1,1,1,1,1, },
                { 1,1,4,4,1,1,1,1,1,1,1,5,1,5,1,1,1,3,1,1,3,1,1,1,1,1, },
                { 1,1,4,4,1,1,1,1,1,1,1,5,1,5,1,1,1,1,3,3,1,1,1,1,1,1, },
                { 1,1,4,4,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1, },
                { 1,1,4,4,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,6,6,6,6, },
                { 1,1,4,4,1,1,1,1,1,1,2,2,2,2,1,1,1,1,1,1,1,1,6,11,11,11, },
                { 1,1,4,4,1,1,1,1,1,1,1,1,2,2,1,1,1,1,1,1,1,1,6,11,11,11, },
                { 1,1,4,4,1,1,1,1,1,1,1,1,1,2,1,1,1,1,1,1,1,1,6,11,11,11 }
            };
        int[,] map3 = 
            {
                { 4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4, },
                { 4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4, },
                { 1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1, },
                { 1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1, },
                { 1,1,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,1,1, },
                { 1,1,4,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,4,1,1, },
                { 1,1,4,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,4,1,1, },
                { 1,1,1,1,1,2,2,1,1,5,5,5,5,5,5,5,5,1,1,2,2,1,1,1,1,1, },
                { 1,1,1,1,1,2,2,1,1,1,1,1,1,1,1,1,1,1,1,2,2,1,1,1,1,1, },
                { 1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1, },
                { 1,1,4,4,1,1,1,1,1,1,1,1,3,3,1,1,1,1,1,1,1,1,4,4,1,1, },
                { 1,1,4,4,1,1,1,5,5,1,1,3,1,1,3,1,1,5,5,1,1,1,4,4,1,1, },
                { 1,1,4,4,1,1,1,4,4,1,1,3,1,1,3,1,1,4,4,1,1,1,4,4,1,1, },
                { 1,1,4,4,1,1,1,4,4,1,1,1,3,3,1,1,1,4,4,1,1,1,4,4,1,1, },
                { 1,1,4,4,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,4,4,1,1, },
                { 1,1,4,4,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,4,4,1,1 }
            };
        int[,] map4 = 
            {
                { 4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4, },
                { 4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4, },
                { 1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1, },
                { 1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1, },
                { 1,1,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,1,1, },
                { 1,1,4,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,4,1,1, },
                { 1,1,4,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,4,1,1, },
                { 1,1,1,1,1,2,2,1,1,5,5,5,5,5,5,5,5,1,1,2,2,1,1,1,1,1, },
                { 1,1,1,1,1,2,2,1,1,1,1,1,1,1,1,1,1,1,1,2,2,1,1,1,1,1, },
                { 1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1, },
                { 1,1,4,4,1,1,1,1,1,1,1,1,3,3,1,1,1,1,1,1,1,1,4,4,1,1, },
                { 1,1,4,4,1,1,1,5,5,1,1,3,1,1,3,1,1,5,5,1,1,1,4,4,1,1, },
                { 1,1,4,4,1,1,1,4,4,1,1,3,1,1,3,1,1,4,4,1,1,1,4,4,1,1, },
                { 1,1,4,4,1,1,1,4,4,1,1,1,3,3,1,1,1,4,4,1,1,1,4,4,1,1, },
                { 1,1,4,4,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,4,4,1,1, },
                { 1,1,4,4,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,4,4,1,1 }
            };
        int[,] map5 = 
            {
                { 4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4, },
                { 4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4, },
                { 1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1, },
                { 1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1, },
                { 1,1,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,1,1, },
                { 1,1,4,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,4,1,1, },
                { 1,1,4,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,4,1,1, },
                { 1,1,1,1,1,2,2,1,1,5,5,5,5,5,5,5,5,1,1,2,2,1,1,1,1,1, },
                { 1,1,1,1,1,2,2,1,1,1,1,1,1,1,1,1,1,1,1,2,2,1,1,1,1,1, },
                { 1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1, },
                { 1,1,4,4,1,1,1,1,1,1,1,1,3,3,1,1,1,1,1,1,1,1,4,4,1,1, },
                { 1,1,4,4,1,1,1,5,5,1,1,3,1,1,3,1,1,5,5,1,1,1,4,4,1,1, },
                { 1,1,4,4,1,1,1,4,4,1,1,3,1,1,3,1,1,4,4,1,1,1,4,4,1,1, },
                { 1,1,4,4,1,1,1,4,4,1,1,1,3,3,1,1,1,4,4,1,1,1,4,4,1,1, },
                { 1,1,4,4,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,4,4,1,1, },
                { 1,1,4,4,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,4,4,1,1 }
            };

        public Map(GraphicsDeviceManager graphicsDevice, int level, List<Rectangle> murs)
        {

            this._levels = new List<Level>();
            _levels.Add(new Level(map1, new int[] {0, 15}, new int[] {1, 14}));
            _levels.Add(new Level(map2, new int[] {0, 15}, new int[] {0, 15}));
            _levels.Add(new Level(map3, new int[] {0, 15}, new int[] {0, 15}));
            _levels.Add(new Level(map4, new int[] {0, 15}, new int[] {0, 15}));
            _levels.Add(new Level(map5, new int[] {0, 15}, new int[] {0, 15}));

            this.setLevel(level);
            this._murs = murs;
            this._rect = new Texture2D(graphicsDevice.GraphicsDevice, 50, 50);
            Color[] data = new Color[50 * 50];
            for (int i = 0; i < data.Length; ++i) data[i] = Color.Black;
            this._rect.SetData(data);
        }

        public void setLevel(int level)
        {
            this._data = this._levels[level - 1].getMap();
            this._start = this._levels[level - 1].getStart();
            this._bonus = this._levels[level - 1].getBonus();
        }

        public Vector2 getStart()
        {
            return this._start;
        }
        public Vector2 getBonus()
        {
            return this._bonus;
        }

        public int getLengthY()
        {
            return this._data.GetLength(0);
        }
        public int getLengthX()
        {
            return this._data.GetLength(1);
        }

        public List<Rectangle> charger()
        {
            _murs.Clear();
            for (int y = 0; y < this.getLengthY(); y++)
            {
                for (int x = 0; x < this.getLengthX(); x++)
                {

                    if (_data[y, x] != 1)
                    {
                        _murs.Add(new Rectangle(-10 + (x * TileSize) + 7, y * TileSize + 7, 35, 35));
                    }
                }
            }
            return _murs;
        }

        public void afficher(SpriteBatch _sb, Texture2D myTexture)
        {
            for (int y = 0; y < this.getLengthY(); y++)
            {
                for (int x = 0; x < this.getLengthX(); x++)
                {
                    _sb.Draw(myTexture,
                            new Rectangle(-10 + (x * TileSize), y * TileSize, 50, 50),
                            new Rectangle((_data[y, x] - 1) % 10 * TileSize, (int)(_data[y, x] - 1) / 10 * TileSize, 50, 50),
                            Color.White);

                }
            }
        }

        public Boolean isEmpty(int x, int y)
        {
            if (y == this._start.X / this.TileSize && x == this._start.Y / this.TileSize)
            {
                return true;
            }
            return (_data[y, x] != 1);
        }
    }


}