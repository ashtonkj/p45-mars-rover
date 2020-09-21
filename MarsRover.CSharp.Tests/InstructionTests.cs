using FsCheck;
using FsCheck.Xunit;
using MarsRover.CSharp.Domain;
using Shouldly;
using System;
using System.Linq;
using Xunit;

namespace MarsRover.CSharp.Tests
{
    public class InstructionTests
    {
        [Property(Verbose = true, DisplayName = "Instruction - TryParse Can Handle Any Arbitrary Input Character Correctly")]
        public void TryParseCanHandleArbitraryCharacters(char input)
        {

            Nullable<Instruction> expectedResult = (input) switch
            {

                'l' => Instruction.L,
                'L' => Instruction.L,
                'm' => Instruction.M,
                'M' => Instruction.M,
                'r' => Instruction.R,
                'R' => Instruction.R,
                _ => null
            };
            var actualResult = InstructionUtilities.TryParse(input);
            actualResult.ShouldBe(expectedResult);
        }

        [Fact(DisplayName = "Instruction - TryParse Can Handle The Known Instructions as Either LowerCase or UpperCase")]
        public void TryParseCanHandleTheKnownInstructionsAsLowerOrUpperCase()
        {
            // Arrange
            var knownInstructions = "LlMmRr";
            var expectedResult = new Nullable<Instruction>[] { Instruction.L, Instruction.L, Instruction.M, Instruction.M, Instruction.R, Instruction.R };
            // Act
            var result = knownInstructions.Select(InstructionUtilities.TryParse).ToArray();
            // Assert
            result.ShouldBe(expectedResult);
        }
    }
}
