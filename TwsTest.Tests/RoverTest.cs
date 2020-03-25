using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace TwsTest.Tests
{
    public class RoverTest
    {
        private readonly Rover _rover;
        private readonly string _nl = Environment.NewLine;

        public RoverTest()
        {
            _rover = new Rover(3, 3, "N");
        }

        [Fact]
        public void MoveEast()
        {
            _rover.Direction = Directions.East;
            _rover.MoveForward();
            Assert.Equal(4, _rover.Position.X);
            Assert.Equal(3, _rover.Position.Y);
        }

        [Fact]
        public void MoveNorth()
        {
            _rover.MoveForward();
            Assert.Equal(3, _rover.Position.X);
            Assert.Equal(4, _rover.Position.Y);
        }

        [Fact]
        public void MoveSouth()
        {
            _rover.Direction = Directions.South;
            _rover.MoveForward();
            Assert.Equal(3, _rover.Position.X);
            Assert.Equal(2, _rover.Position.Y);
        }

        [Fact]
        public void MoveWest()
        {
            _rover.Direction = Directions.West;
            _rover.MoveForward();
            Assert.Equal(2, _rover.Position.X);
            Assert.Equal(3, _rover.Position.Y);
        }

        [Fact]
        public void TurnLeftFromEast()
        {
            _rover.Direction = Directions.East;
            _rover.TurnLeft();
            Assert.Equal(Directions.North, _rover.Direction);
        }

        [Fact]
        public void TurnLeftFromNorth()
        {
            _rover.TurnLeft();
            Assert.Equal(Directions.West, _rover.Direction);
        }

        [Fact]
        public void TurnLeftFromSouth()
        {
            _rover.Direction = Directions.South;
            _rover.TurnLeft();
            Assert.Equal(Directions.East, _rover.Direction);
        }

        [Fact]
        public void TurnLeftFromWest()
        {
            _rover.Direction = Directions.West;
            _rover.TurnLeft();
            Assert.Equal(Directions.South, _rover.Direction);
        }

        [Fact]
        public void TurnRightFromEast()
        {
            _rover.Direction = Directions.East;
            _rover.TurnRight();
            Assert.Equal(Directions.South, _rover.Direction);
        }

        [Fact]
        public void TurnRightFromNorth()
        {
            _rover.TurnRight();
            Assert.Equal(Directions.East, _rover.Direction);
        }

        [Fact]
        public void TurnRightFromSouth()
        {
            _rover.Direction = Directions.South;
            _rover.TurnRight();
            Assert.Equal(Directions.West, _rover.Direction);
        }

        [Fact]
        public void TurnRightFromWest()
        {
            _rover.Direction = Directions.West;
            _rover.TurnRight();
            Assert.Equal(Directions.North, _rover.Direction);
        }

        [Fact]
        public void WillMoveToEast()
        {
            _rover.Direction = Directions.East;
            var c = _rover.WillMoveForwardTo();
            Assert.Equal(4, c.X);
            Assert.Equal(3, c.Y);
            Assert.Equal(3, _rover.Position.X);
            Assert.Equal(3, _rover.Position.Y);
        }

        [Fact]
        public void WillMoveToNorth()
        {
            var c = _rover.WillMoveForwardTo();
            Assert.Equal(3, c.X);
            Assert.Equal(4, c.Y);
            Assert.Equal(3, _rover.Position.X);
            Assert.Equal(3, _rover.Position.Y);
        }

        [Fact]
        public void WillMoveToSouth()
        {
            _rover.Direction = Directions.South;
            var c = _rover.WillMoveForwardTo();
            Assert.Equal(3, c.X);
            Assert.Equal(2, c.Y);
            Assert.Equal(3, _rover.Position.X);
            Assert.Equal(3, _rover.Position.Y);
        }

        [Fact]
        public void WillMoveToWest()
        {
            _rover.Direction = Directions.West;
            var c = _rover.WillMoveForwardTo();
            Assert.Equal(2, c.X);
            Assert.Equal(3, c.Y);
            Assert.Equal(3, _rover.Position.X);
            Assert.Equal(3, _rover.Position.Y);
        }

    }
}
