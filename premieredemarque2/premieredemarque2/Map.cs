
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
        public int[,] _data;
        public List<Rectangle> _murs;
        public Texture2D _rect;

        public int TileWidth = 50;
        public int TileHeight = 50;

        int[,] level1 =
            {
                { 4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4, },
                { 4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4, },
                { 1,1,1,1,1,1,1,3,3,3,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1, },
                { 1,1,1,1,1,1,1,3,3,3,1,1,1,1,1,1,1,5,1,1,1,1,1,1,1,1, },
                { 4,4,4,4,4,1,1,1,3,1,1,1,4,4,4,1,1,5,1,1,1,1,1,4,4,4, },
                { 4,4,4,4,4,1,1,1,1,1,1,1,4,4,4,1,1,5,1,1,1,1,1,4,4,4, },
                { 1,1,1,1,1,1,1,1,1,1,1,1,4,4,4,1,1,5,1,1,1,1,1,1,1,1, },
                { 1,1,1,1,1,1,1,1,6,1,1,1,1,1,1,1,1,5,1,1,1,1,1,1,1,1, },
                { 4,4,4,4,4,1,1,6,1,6,1,1,1,1,1,1,1,1,1,1,2,1,1,4,4,4, },
                { 4,4,4,4,4,1,1,1,6,1,1,1,1,1,1,1,1,1,1,1,2,1,1,4,4,4, },
                { 1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,5,1,1,1,1,1,1,1,1, },
                { 1,1,1,1,1,1,1,1,1,1,1,1,4,4,4,1,1,5,1,1,1,1,1,1,1,1, },
                { 4,4,4,4,4,1,1,1,1,1,1,1,4,4,4,1,1,5,1,1,1,1,1,4,4,4, },
                { 4,4,4,4,4,1,1,1,1,1,1,1,4,4,4,1,1,5,1,1,1,1,1,4,4,4, },
                { 1,1,1,1,1,1,1,1,2,1,1,1,1,1,1,1,1,5,1,1,1,1,1,1,1,1, },
                { 1,1,1,1,1,1,1,2,2,2,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1 }
            };
        int[,] level2 = 
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
        int[,] level3 = 
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
            _data = getLevel(level);
            _murs = murs;
            _rect = new Texture2D(graphicsDevice.GraphicsDevice, 50, 50);
            Color[] data = new Color[50 * 50];
            for (int i = 0; i < data.Length; ++i) data[i] = Color.Black;
            _rect.SetData(data);
        }

        public int[,] getLevel(int level)
        {
            switch (level)
            {
                case 1:
                    return level1;
                    break;
                case 2:
                    return level2;
                    break;
                case 3:
                    return level3;
                    break;
                default:
                    return level1;
                    break;
            }
        }

        public void setLevel(int level)
        {
            switch (level)
            {
                case 1:
                    _data = level1;
                    break;
                case 2:
                    _data = level2;
                    break;
                case 3:
                    _data = level3;
                    break;
                default:
                    _data = level1;
                    break;
            }
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
            for (int y = 0; y < _data.GetLength(0); y++)
            {
                for (int x = 0; x < _data.GetLength(1); x++)
                {

                    if (_data[y, x] != 1)
                    {
                        Rectangle dest = new Rectangle(-10 + (x * TileWidth), y * TileHeight, 50, 50);
                        _murs.Add(dest);
                    }
                }
            }
            return _murs;
        }

        public void afficher(SpriteBatch _sb, Texture2D myTexture)
        {
            for (int y = 0; y < _data.GetLength(0); y++)
            {
                for (int x = 0; x < _data.GetLength(1); x++)
                {
                    Rectangle dest = new Rectangle(-10 + (x * TileWidth), y * TileHeight, 50, 50);

                    _sb.Draw(myTexture,
                            dest,
                            new Rectangle((_data[y, x] - 1) * TileWidth, 0, 50, 50),
                            Color.White);

                }
            }
        }


        public Boolean isEmpty(int x, int y)
        {

            return (_data[y, x] != 1);
        }
    }


}