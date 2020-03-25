using System;
using System.Collections.Generic;
using System.Text;

namespace TwsTest
{
    public class Coordinates
    {
        #region Constructors
        public Coordinates() { }
        public Coordinates(int x, int y)
        {
            X = x;
            Y = y;
        }
        #endregion Constructors

        #region Properties
        public int X { get; set; }
        public int Y { get; set; }
        #endregion Properties

        #region Methods
        public Coordinates Clone()
        {
            return new Coordinates(X, Y);
        }

        public bool Matches(Coordinates c)
        {
            return c.X == X && c.Y == Y;
        }
        #endregion
    }
}
