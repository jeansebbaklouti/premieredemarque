
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
        public List<Level> _levels;
        private Level _activeLevel;
        private int[,] _data;
        private Vector2 _start;
        private Vector2 _bonus;
        private List<Rectangle> _murs;
        private Texture2D _rect;
        private int TileSize = 50;
        private Boolean first = true;
        public Rectangle sortie;
        public static Random rd;
        public int alea;

        int[,] map0 = 
            {
                { 2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,                },
                 {  2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,              },
                 {  17,23,3,1,1,1,1,1,1,1,1,3,5,1,1,1,1,1,1,1,1,1,1,1,6,8,            },
                 {  28,35,1,1,1,1,1,1,1,1,1,2,6,1,1,1,1,1,1,1,1,1,1,1,1,8,            },
                 {  28,35,1,1,1,3,46,1,1,1,1,5,3,1,1,1,1,1,41,1,1,1,1,3,1,8,          },
                 {  32,26,1,1,1,46,3,1,1,1,1,1,1,1,1,1,1,1,45,21,12,13,24,1,1,8,      },
                 {  9,10,42,1,1,1,1,1,1,1,1,41,1,1,1,1,1,1,42,34,28,28,35,1,1,8,      },
                 {  19,20,63,1,1,1,1,5,3,1,1,3,1,45,45,1,1,1,42,34,28,28,35,1,1,8,    },
                 {  52,1,1,1,1,1,1,46,2,1,1,6,1,45,45,1,1,1,45,27,31,32,25,1,1,8,     },
                 {  53,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,41,1,1,1,1,2,1,8,            },
                 {  9,10,63,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,8,            },
                 {  19,20,42,1,1,1,1,21,11,12,13,14,15,16,17,24,1,1,1,1,1,8,1,1,1,8,  },
                 {  14,24,1,1,1,1,1,1,1,1,3,1,1,46,1,1,1,1,1,1,1,1,1,1,1,8,           },
                 {  28,35,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,8,            },
                 {  32,25,1,1,1,5,49,5,1,1,1,6,3,1,1,1,47,49,5,48,41,1,41,48,49,47,   },
                 {  5,49,5,1,42,49,47,49,61,1,1,5,2,1,1,61,48,5,49,47,8,1,8,49,47,49 }
            };
        int[,] map1 = 
            {
                { 11,12,13,14,15,16,17,18,11,12,13,14,15,16,17,11,12,13,14,15,11,12,13,14,15,16,}, 
                { 12,13,14,15,16,17,11,12,13,14,15,16,11,12,13,14,15,16,11,12,13,14,15,16,17,18,}, 
                { 1,1,41,1,1,1,41,1,1,1,41,1,1,1,41,1,1,1,41,1,61,1,41,1,1,1,                   }, 
                { 1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,61,1,                         }, 
                { 1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,                          }, 
                { 1,3,3,3,1,5,5,5,1,6,1,1,1,42,1,1,1,21,18,17,1,45,45,45,1,1,                   }, 
                { 1,3,1,1,1,5,1,5,1,6,1,1,1,42,42,1,1,34,1,1,1,45,1,1,1,1,                      }, 
                { 1,3,1,1,1,5,1,5,1,6,1,1,1,42,1,42,1,34,1,1,1,45,1,1,1,1,                      }, 
                { 1,3,3,3,1,5,1,5,1,6,1,1,1,42,1,42,1,34,28,28,1,45,45,45,1,1,                  }, 
                { 1,1,1,3,1,5,1,5,1,6,1,1,1,42,1,42,1,34,1,1,1,1,1,45,1,1,                      }, 
                { 1,1,1,3,1,5,1,5,1,6,1,1,1,42,1,42,1,34,1,1,1,1,1,45,1,1,                      }, 
                { 1,3,3,3,1,5,5,5,1,6,6,6,1,42,42,42,1,27,33,32,1,45,45,45,1,1,                 }, 
                { 1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,                          }, 
                { 1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,                          }, 
                { 1,1,2,1,1,1,2,1,1,1,2,1,1,1,2,1,1,1,2,1,1,1,2,1,61,1,                         }, 
                { 1,55,2,1,1,1,2,1,1,1,2,1,1,1,2,1,1,1,2,1,1,1,2,1,1,1 }
            };
        int[,] map2 = 
        {
            { 4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,             },
            { 4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,             },
            { 8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,             },
            { 8,3,6,1,1,1,1,1,1,1,1,1,42,45,42,1,1,1,1,1,1,1,1,1,1,41,         },
            { 8,2,46,1,1,1,1,61,9,10,1,1,1,5,1,1,1,9,10,61,1,1,1,1,1,45,       },
            { 8,1,1,1,1,1,1,1,19,20,1,1,1,1,1,1,1,19,20,1,1,1,1,1,1,8,         },
            { 8,1,1,2,5,47,1,1,1,1,1,1,22,16,24,1,1,1,1,1,1,1,1,1,1,8,         },
            { 8,1,1,49,48,2,1,1,2,3,1,1,34,28,35,1,41,6,5,3,2,45,1,1,1,41,     },
            { 52,1,1,1,1,1,1,1,5,6,1,1,27,31,26,1,45,2,49,6,46,41,1,1,1,45,    },
            { 53,1,1,1,1,1,1,1,1,1,1,1,41,45,41,1,1,1,1,1,1,1,1,1,1,41,        },
            { 8,1,1,1,41,1,1,1,9,10,1,1,1,1,1,1,1,9,10,1,1,1,1,1,1,8,          },
            { 8,1,1,41,45,41,1,61,19,20,1,1,1,1,1,1,1,19,20,61,1,1,1,1,1,8,    },
            { 8,1,1,1,41,1,1,1,1,1,1,1,2,3,2,1,1,1,1,1,1,1,1,1,1,45,           },
            { 8,1,1,1,1,1,1,1,1,1,1,1,48,6,5,1,1,1,1,1,1,1,1,1,1,41,           },
            { 8,2,48,3,6,5,2,6,3,49,6,3,49,2,3,6,49,3,6,2,3,6,2,2,47,42,       },
            { 8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8              },
        };
        int[,] map3 =
            {
                { 4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4, },
                { 4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4, },
                { 1,1,1,1,1,1,1,3,3,3,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1, },
                { 1,1,1,1,1,1,1,3,3,3,1,1,1,1,1,1,1,5,1,1,1,1,1,1,1,1, },
                { 4,4,4,4,4,1,1,1,3,1,1,1,4,4,4,1,1,5,1,1,1,1,1,4,4,4, },
                { 4,4,4,4,4,1,1,1,1,1,1,1,4,4,4,1,1,5,1,1,1,1,1,4,4,4, },
                { 1,1,1,1,1,1,1,1,1,1,1,1,4,4,4,1,1,5,1,1,1,1,1,1,61,1, },
                { 1,1,1,1,1,1,1,1,6,1,1,1,1,1,1,1,1,5,1,1,1,1,1,1,61,1, },
                { 4,4,4,4,4,1,1,6,1,6,1,1,1,1,1,1,1,1,1,1,2,1,1,4,4,4, },
                { 4,4,4,4,4,1,1,1,6,1,1,1,1,1,1,1,1,1,1,1,2,1,1,4,4,4, },
                { 1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,5,1,1,1,1,1,1,1,1, },
                { 1,1,1,1,1,1,1,1,1,1,1,1,4,4,4,1,1,5,1,1,1,1,1,1,1,1, },
                { 4,4,4,4,4,1,1,1,1,1,1,1,4,4,4,1,1,5,1,1,1,1,1,4,4,4, },
                { 4,4,4,4,4,1,1,1,1,1,1,1,4,4,4,1,1,5,1,1,1,1,1,4,4,4, },
                { 1,1,1,1,1,1,1,1,2,1,1,1,1,1,1,1,1,5,1,1,1,1,1,1,1,1, },
                { 1,55,1,1,1,1,1,2,2,2,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1 }
            };

        public Map(GraphicsDeviceManager graphicsDevice, int level, List<Rectangle> murs)
        {
            rd = new Random();
            this._levels = new List<Level>();
            _levels.Add(new Level(map0, new int[] {1, 9}, new int[] {4, 15}));
            _levels.Add(new Level(map1, new int[] {1, 15}, new int[] {4, 15}));
            _levels.Add(new Level(map2, new int[] {1, 15}, new int[] {4, 15}));
            _levels.Add(new Level(map3, new int[] {1, 15}, new int[] {4, 15}));

            this.setLevel(level);
            this._murs = murs;
            this._rect = new Texture2D(graphicsDevice.GraphicsDevice, 50, 50);
            Color[] data = new Color[50 * 50];
            for (int i = 0; i < data.Length; ++i) data[i] = Color.Black;
            this._rect.SetData(data);

            // alea = rd.Next(1, NbCaisses()+1);
        }

        public void setLevel(int level)
        {
            this._activeLevel = this._levels[level - 1];
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

                    if (_data[y, x] != 1 && (x != this._activeLevel.getStartX() || y != this._activeLevel.getStartY()))
                    {
                        _murs.Add(new Rectangle(-10 + (x * TileSize) + 7, y * TileSize + 7, 35, 35));
                    }
                }
            }
            return _murs;
        }

        public void afficher(SpriteBatch _sb, Texture2D myTexture, Boolean B)
        {
            if (B == false)
            {
                for (int y = 0; y < _data.GetLength(0); y++)
                {
                    for (int x = 0; x < _data.GetLength(1); x++)
                    {
                        Rectangle dest = new Rectangle(-10 + (x * TileSize), y * TileSize, 50, 50);

                        _sb.Draw(myTexture,
                                dest,
                                new Rectangle((_data[y, x] - 1) % 10 * TileSize, (_data[y, x] - 1) / 10 * TileSize, 50, 50),
                                Color.White);

                    }
                }
            }
            else
            {
                for (int y = 0; y < _data.GetLength(0); y++)
                {
                    for (int x = 0; x < _data.GetLength(1); x++)
                    {
                        if (_data[y, x] == 61 && first)
                        {
                            this.sortie = new Rectangle(-10 + (x * TileSize), y * TileSize, 50, 50);
                            _sb.Draw(myTexture,
                                new Rectangle(-10 + (x * TileSize), y * TileSize, 50, 50),
                                new Rectangle((_data[y, x]) % 10 * TileSize, (_data[y, x]) / 10 * TileSize, 50, 50),
                                Color.White);
                            first =false;
                        }
                        else
                        {
                            _sb.Draw(myTexture,
                                    new Rectangle(-10 + (x * TileSize), y * TileSize, 50, 50),
                                    new Rectangle((_data[y, x] - 1) % 10 * TileSize, (_data[y, x] - 1) / 10 * TileSize, 50, 50),
                                    Color.White);
                        }
                    }
                }
                first = true;
            }
        }

        public Boolean isEmpty(int x, int y)
        {
            return (_data[y, x] != 1);
        }

        public int NbCaisses()
        {
            int nb = 0;
            for (int y = 0; y < _data.GetLength(0); y++)
                {
                    for (int x = 0; x < _data.GetLength(1); x++)
                    {
                        if (_data[y, x] == 61)
                            nb++;
                    }
            }
            return nb;
        }
    }


}