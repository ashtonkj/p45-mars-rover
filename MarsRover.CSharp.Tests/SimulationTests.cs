using MarsRover.CSharp.Domain;
using System.IO;
using Xunit;
using Shouldly;

namespace MarsRover.CSharp.Tests
{
    public class SimulationTests
    {
        [Fact(DisplayName = "Simulation - The Input Values Provided in the Instructions Yields the Expected Results")]
        public void TheProvidedSimulationFromTheInstructionsYieldsTheExpectedResults()
        {
            // Arrange
            var output = new StringWriter();
            var simulation = Simulation.FromString(@"5 5
1 2 N
LMLMLMLMM
3 3 E
MMRMMRMRRM", output);
            // Act
            simulation.Execute();
            simulation.PrintResults();
            // Assert
            var results = output.ToString();
            results.ShouldBe(@"1 3 N
5 1 E
");
        }
    }
}
