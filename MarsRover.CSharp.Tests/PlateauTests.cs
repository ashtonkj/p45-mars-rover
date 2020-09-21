using FsCheck.Xunit;
using MarsRover.CSharp.Domain;
using Shouldly;
using System;

namespace MarsRover.CSharp.Tests
{
    public class PlateauTests
    {
        const int PlateauRight = 15;
        const int PlateauTop = 10;

        public PlateauTests()
        {
            TestPlateau = new Plateau(PlateauRight, PlateauTop);
        }

        public Plateau TestPlateau { get; }

        [Property(Verbose =true, DisplayName ="Plateau - Contains Will Return True For A Valid Position")]
        public void ContainsWillReturnCorrectlyIfPositionIsValid(Position position)
        {
            var expectedResult = (position.X >= 0 && position.X <= PlateauRight) && (position.Y >= 0 && position.Y <= PlateauTop);
            var actualResult = TestPlateau.Contains(position);
            actualResult.ShouldBe(expectedResult);
        }

        [Property(Verbose = true, DisplayName = "Plateau - Contains Will Always  Return False For Any X Position Less Than 0")]
        public void ContainsWillAlwaysReturnFalseForAnyPositionWhereXIsLessThan0(Position position)
        {
            if (position.X < 0)
            {
                TestPlateau.Contains(position).ShouldBeFalse();
            }
        }

        [Property(Verbose = true, DisplayName = "Plateau - Contains Will Always  Return False For Any Y Position Less Than 0")]
        public void ContainsWillAlwaysReturnFalseForAnyPositionWhereYIsLessThan0(Position position)
        {
            if (position.Y < 0)
            {
                TestPlateau.Contains(position).ShouldBeFalse();
            }
        }

        [Property(Verbose = true, DisplayName = "Plateau - Contains Will Always  Return False For Any Y Position Greater Than Plateau Top")]
        public void ContainsWillAlwaysReturnFalseForAnyPositionWhereYIsGreaterThanTop(Position position)
        {
            if (position.Y > PlateauTop)
            {
                TestPlateau.Contains(position).ShouldBeFalse();
            }
        }

        [Property(Verbose = true, DisplayName = "Plateau - Contains Will Always  Return False For Any X Position Greater Than Plateau Right")]
        public void ContainsWillAlwaysReturnFalseForAnyPositionWhereXIsGreaterThanRight(Position position)
        {
            if (position.X > PlateauRight)
            {
                TestPlateau.Contains(position).ShouldBeFalse();
            }
        }

        [Property(Verbose=true, DisplayName = "Plateau - A Plateau That is Created With Either a Top or Right Value of 0 or Less Will Throw an ArgumentOutOfRangeException")]
        public void APlateauThatIsCreatedWithEitherATopOrRightValueOfZeroOrLessWillThrowAnArgumentOutOfRangeException(Position position)
        {
            if (position.X <= 0 || position.Y <= 0)
            {
                Should.Throw<ArgumentOutOfRangeException>(() => new Plateau(position.X, position.Y));
            }
        }
    }
}
