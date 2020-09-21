using FsCheck.Xunit;
using MarsRover.CSharp.Domain;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Text;

namespace MarsRover.CSharp.Tests
{
    public class RoverTests
    {
        public RoverTests()
        {
            TestPlateau = new Plateau(10, 10);
        }

        public Plateau TestPlateau { get; }

        [Property(Verbose =true)]
        public void AnArbirtarySetOfInstructionsCanNeverLeaveTheRoverOffThePlateau(Instruction[] instructions)
        {
            var rover = new Rover(TestPlateau, new Position(0, 0), Direction.N);
            foreach (var instruction in instructions)
            {
                rover.Handle(instruction);
            }
            TestPlateau.Contains(rover.Position).ShouldBeTrue();
        }

        [Property(Verbose =true)]
        public void AParsedRoverToStringShouldEqualTheOrignalRover(Position position, Direction d)
        {
            if (TestPlateau.Contains(position))
            {
                var rover = new Rover(TestPlateau, position, d);
                var intermediate = rover.ToString();
                var parsed = Rover.TryParse(TestPlateau, intermediate);
                parsed.ShouldBe(rover);
            }
        }
    }
}
