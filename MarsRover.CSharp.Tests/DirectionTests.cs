using FsCheck.Xunit;
using MarsRover.CSharp.Domain;
using Shouldly;
using System;
using System.Linq;
using Xunit;

namespace MarsRover.CSharp.Tests
{
    public class DirectionTests
    {
        [Property(Verbose = true,DisplayName ="Direction - TryParse Can Handle Any Arbirtray Input Character")]
        public void TryParseCanHandleArbitraryCharacters(char input)
        {

            Direction? expectedResult = (input) switch
            {

                'N' => Direction.N,
                'n' => Direction.N,
                'E' => Direction.E,
                'e' => Direction.E,
                'S' => Direction.S,
                's' => Direction.S,
                'W' => Direction.W,
                'w' => Direction.W,
                _ => null
            };
            var actualResult = DirectionUtilities.TryParse(input);
            actualResult.ShouldBe(expectedResult);
        }

        [Fact(DisplayName = "Direction - TryParse Can Handle The Known Directions As Either LowerCase or UpperCase")]
        public void TryParseCanHandleTheKnownDirectionsAsLowerOrUpperCase()
        {
            // Arrange
            var knownDirections = "NESWwsen";
            var expectedResult = new Direction?[]
            {
                Direction.N, Direction.E, Direction.S, Direction.W,
                Direction.W, Direction.S, Direction.E, Direction.N };
            // Act
            var result = knownDirections.Select(DirectionUtilities.TryParse).ToArray();
            // Assert
            result.ShouldBe(expectedResult);
        }

        [Fact(DisplayName = "Direction - ToString Yields the Expected Value for Printing")]
        public void DirectionToStringShouldGiveTheCorrectValue()
        {
            // Arrange
            var expectedResult = "NESW";
            var input = new Direction?[]
            {
                Direction.N, Direction.E, Direction.S, Direction.W
            };
            // Act
            var result = string.Join(String.Empty,  input.Select(c => c.ToString()).ToArray());
            // Assert
            result.ShouldBe(expectedResult);
        }
    }
}
