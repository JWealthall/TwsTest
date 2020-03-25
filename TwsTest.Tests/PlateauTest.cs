using System;
using TwsTest;
using Xunit;

namespace TwsTest.Tests
{
    public class PlateauTest
    {
        private readonly Plateau _plateau;
        public PlateauTest()
        {
            _plateau = new Plateau(10, 10);
        }

        [Theory]
        [InlineData(0, 0)]
        [InlineData(10, 20)]
        [InlineData(22, 22)]
        public void Create(int x, int y)
        {
            var p = new Plateau(x, y);
            Assert.Equal(x, p.MaxPosition.X);
            Assert.Equal(y, p.MaxPosition.Y);  // Note this could be made into a separate test
        }

        [Theory]
        [InlineData(0, 0)]
        [InlineData(10, 20)]
        [InlineData(22, 22)]
        public void CreateCoordinates(int x, int y)
        {
            var p = new Plateau(new Coordinates( x, y));
            Assert.Equal(x, p.MaxPosition.X);
            Assert.Equal(y, p.MaxPosition.Y);
        }

        [Theory]
        [InlineData(0, 0)]
        [InlineData(0, 10)]
        [InlineData(5, 5)]
        public void IsInsidePlateau(int x, int y)
        {
            Assert.True(_plateau.IsInsidePlateau(new Coordinates(x, y)));
        }

        [Theory]
        [InlineData(0, -1)]
        [InlineData(0, 11)]
        [InlineData(-5, 15)]
        public void IsOutsidePlateau(int x, int y)
        {
            Assert.False(_plateau.IsInsidePlateau(new Coordinates(x, y)));
        }
    }
}
