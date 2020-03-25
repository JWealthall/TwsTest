using System;

namespace TwsTest
{
    public class Plateau
    {
        private Coordinates _maxPosition;

        #region Constructors
        public Plateau() { }
        public Plateau(int x, int y) { MaxPosition = new Coordinates(x, y); }
        public Plateau(Coordinates maxPosition) { MaxPosition = maxPosition; }
        #endregion Constructors

        #region Members
        #endregion Members

        #region Properties
        public Coordinates MaxPosition
        {
            get => _maxPosition;
            set
            {
                _maxPosition = value;
                BuildMap();
            }
        }

        public int[,] Mapped { get; set; }
        #endregion Properties

        #region Methods
        #region Public Methods
        public bool IsInsidePlateau(Coordinates position)
        {
            return position.X >= 0 && position.Y >= 0 && position.X <= MaxPosition.X && position.Y <= MaxPosition.Y;
        }

        public void Map(Rover rover) { Map(rover.Position); }
        public void Map(Coordinates position)
        {
            if (IsInsidePlateau(position))
            {
                Mapped[position.X, position.Y] += 1;
            }
        }
        #endregion Public Methods

        #region Private Methods
        private void BuildMap()
        {
            Mapped = new int[_maxPosition.X + 1, _maxPosition.Y + 1];
            //for (int x = 0; x <= _maxPosition.X; x++)
            //{
            //    for (int y = 0; y <= _maxPosition.Y; y++)
            //    {
            //        Mapped[x, y] = 0;
            //    }

            //}
        }
        #endregion Private Methods
        #endregion Methods
    }
}
