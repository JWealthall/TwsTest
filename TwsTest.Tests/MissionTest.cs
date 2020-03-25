using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace TwsTest.Tests
{
    public class MissionTest
    {
        private readonly Mission _mission;
        private readonly string _nl = Environment.NewLine;

        public MissionTest()
        {
            _mission = new Mission();
        }

        [Fact]
        public void BaseTest()
        {
            var sb = new StringBuilder(100);
            sb.AppendLine("5 5");
            sb.AppendLine("1 2 N");
            sb.AppendLine("LMLMLMLMM");
            sb.AppendLine("3 3 E");
            sb.AppendLine("MMRMMRMRRM");
            Assert.True(_mission.RunMission(sb.ToString()));
            Assert.Equal("1 3 N" + _nl + "5 1 E", _mission.Result);
        }

        [Fact]
        public void FailParamsBlank()
        {
            Assert.False(_mission.RunMission(""));
            Assert.Equal("No mission parameters supplied", _mission.Message);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        public void FailParamsLines(int lines)
        {
            var sb = new StringBuilder(20);
            sb.Append(".");
            while (lines > 1)
            {
                sb.AppendLine();
                sb.Append(".");
                lines -= 1;
            }
            Assert.False(_mission.RunMission(sb.ToString()));
            Assert.Equal("Invalid mission parameters supplied - Not enough lines", _mission.Message);
        }

        [Fact]
        public void FailParamsPlateau()
        {
            var sb = new StringBuilder(100);
            sb.AppendLine("?");
            sb.AppendLine("1 2 N");
            sb.AppendLine("LMLMLMLMM");
            Assert.False(_mission.RunMission(sb.ToString()));
            Assert.Equal("Invalid mission parameters supplied - Plateau size invalid", _mission.Message);
        }

        [Theory]
        [InlineData("x 5")]
        [InlineData("-1 5")]
        public void FailParamsPlateauX(string line1)
        {
            var sb = new StringBuilder(100);
            sb.AppendLine(line1);
            sb.AppendLine("1 2 N");
            sb.AppendLine("LMLMLMLMM");
            Assert.False(_mission.RunMission(sb.ToString()));
            Assert.Equal("Invalid mission parameters supplied - Plateau maximum X is not a positive integer", _mission.Message);
        }

    }
}
