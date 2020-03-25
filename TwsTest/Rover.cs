using System;
using System.Collections.Generic;
using System.Text;

namespace TwsTest
{
    public class Rover
    {
        #region Constructors
        // Example of overloaded constructors
        // Would normally only add what was required when required or object initializers
        public Rover()
        {
            Position = new Coordinates();
        }

        public Rover(int x, int y, Directions direction)
        {
            Position = new Coordinates(x, y);
            Direction = direction;
        }

        public Rover(int x, int y, string direction)
        {
            Position = new Coordinates(x, y);
            Direction = GetDirection(direction);
        }

        // Note: A constructor which takes a string made up of x, y & direction is a possible option
        #endregion Constructors

        #region Properties
        public Coordinates Position { get; set; }
        public Directions Direction { get; set; } = Directions.North;
        public bool Crashed { get; set; } = false;      // This can be used if decide to continue the mission after crashing the rover
        #endregion Properties

        #region Methods
        #region Public Methods
        // Move the rover
        public void MoveForward()
        {
            if (Crashed) return;
            switch (Direction)
            {
                case Directions.North:
                    Position.Y += 1;
                    break;
                case Directions.East:
                    Position.X += 1;
                    break;
                case Directions.South:
                    Position.Y -= 1;
                    break;
                case Directions.West:
                    Position.X -= 1;
                    break;
            }
        }

        // Turn the rover anticlockwise by 90 degrees
        public void TurnLeft()
        {
            Direction -= 1;
            if (Direction < Directions.North) Direction = Directions.West;
        }

        // Turn the rover clockwise by 90 degrees
        public void TurnRight()
        {
            Direction += 1;
            if (Direction > Directions.West) Direction = Directions.North;
        }

        // Where will the rover move to if asked to move
        // Used by the mission coordinator to check if allowed to move
        public Coordinates WillMoveForwardTo()
        {
            var newPosition = Position.Clone();
            switch (Direction)
            {
                case Directions.North:
                    newPosition.Y += 1;
                    break;
                case Directions.East:
                    newPosition.X += 1;
                    break;
                case Directions.South:
                    newPosition.Y -= 1;
                    break;
                case Directions.West:
                    newPosition.X -= 1;
                    break;
            }
            return newPosition;
        }
        #endregion Public Methods

        #region Private Methods
        private Directions GetDirection(string direction)
        {
            switch (direction)
            {
                case "N":
                    return Directions.North;
                case "E":
                    return Directions.East;
                case "S":
                    return Directions.South;
                case "W":
                    return Directions.West;
                default:
                    throw new ArgumentOutOfRangeException(nameof(direction), "Not a point of the compass");
            }
        }
        #endregion Private Methods

        #endregion Methods
    }
}
