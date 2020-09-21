using FsCheck.Xunit;
using MarsRover.CSharp.Domain;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Text;

namespace MarsRover.CSharp.Tests
{
    public class PositionTests
    {
        [Property(Verbose =true)]
        public void APositionToStringShouldBeParseableToTheSameOriginalPositionValue(Position position)
        {
            var str = position.ToString();
            var actual = Position.TryParse(str);
            actual.ShouldBe(position);
        }
    }
}
