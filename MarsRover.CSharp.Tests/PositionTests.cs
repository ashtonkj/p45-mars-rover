using FsCheck.Xunit;
using MarsRover.CSharp.Domain;
using Shouldly;

namespace MarsRover.CSharp.Tests
{
    public class PositionTests
    {
        [Property(Verbose =true, DisplayName ="Position - A Position When Converted to a String and then Parsed Back to a Position Equals the Original Position")]
        public void APositionToStringShouldBeParseableToTheSameOriginalPositionValue(Position position)
        {
            var str = position.ToString();
            var actual = Position.TryParse(str);
            actual.ShouldBe(position);
        }
    }
}
