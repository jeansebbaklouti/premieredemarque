using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace premieredemarque2
{
    class Bonus
    {
      private String _texToDisplay;
      private double _createTime;
      private int _type;
      private Vector2 _position;

      public Bonus(String text, double time, int type, Vector2 position)
      {
          _texToDisplay = text;
          _createTime = time;
          _type = type;
          _position = position;

      }

      public String texToDisplay
      {

          get
          {
              return _texToDisplay;
          }
      }

      public double createTime
      {

          get
          {
              return _createTime;
          }
      }

      public int type
      {

          get
          {
              return _type;
          }
      }

      public Vector2 position
      {

          get
          {
              return _position;
          }
      }

    }
}
