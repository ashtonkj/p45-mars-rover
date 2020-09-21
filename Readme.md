# Mars Rover Project

## General Comments

### Solution Layout

This solution is for the Platform45 developer assessment. Included in the solution are 3 projects. 

| Project | Description |
| ------- | ----------- |
| MarsRover.Prototype | This project is a functional prototype of the intended solution. I created it as a means for me to think through the problem domain and see what kind of edge cases might be encountered, and what kind of solution would be viable. |
| MarsRover.CSharp | The main project that includes the more OO style of the assessment |
| MarsRover.CSharp.Tests | The test suite which contains a mixture of property based / normal unit tests |


### A Note on Language Features

Except in tests, where the newer sytax features are very useful, I have intentionally not used all of the advanced features and sytax of the C# language as I am not sure how familiar the interviewer will be with those features so I decided to err on the side of features (and syntax) that are generally available in other languages. An example of that would be in `Domain\Instruction.cs` where instead of using a switch expression, I have used a switch statement. The differences between these two approaches can be seen below:

#### Switch Statement

```CSharp
switch (char.ToUpper(input))
{
    case 'L': return Instruction.L;
    case 'M': return Instruction.M;
    case 'R': return Instruction.R;
    default: return null;
}
```

#### Switch Expression


```CSharp
return (str.ToUpper()) switch
{
    "L" => Instruction.L,
    "M" => Instruction.M,
    "R" => Instruction.R,
    _ => null,
};
```

### Executing the Solution

The simplest way to run the solution (provided you have .NET Core installed) would be from a command line in the root of the solution directory:

`dotnet run -p MarsRover.CSharp\MarsRover.CSharp.csproj`

This will run the solution with the default input data provided in Instructions.md.

If you want to run the solution with an external file as input run the following (A sample text file has been included in the root of the MarsRover.CSharp project directory):

`dotnet run -p MarsRover.CSharp\MarsRover.CSharp.csproj "<FULL PATH TO TEXT FILE>"`

---

## Assumptions

### Plateau Size

* Assuming a first line input of 5,5 this implies that the plateau size is actually 6 * 6 (As the plateau grid coordinates are 0 based)
* No upper bound has been specified for the plateau top and right coordinates. This solution has arbitrarily limited these coordinates to the max value of a 32 bit signed integer (therefore the maximum upper right coordinates would be: `(2147483647, 2147483647)`)
* No lower bound has been specified for the plateau top and right coordinates. This solutions has arbitrarily selected a minimum plateau size of `2 * 2`, with the upper right coordinates being `(1, 1)`.

### Rovers

* No move can cause any rover to exceed the bounds of the plateau (We're assuming that if a rover were to drive off the edge of the plateau it would plummet to its doom which would not be a desireable outcome).
* There is no limit to the number of rovers that can occupy any grid square of the plateau at a time.
* In theory there is no upper bound to the number of rovers that can be on the grid as a whole; practically this is limited by the resources available.

---

## Testing

The test suite is a single project consisting of two primary types of tests:

* Property based tests which test that certain properties always hold true (by generating a random set of inputs and attempting to falsify the property).
* Standard unit tests where well known inputs and outputs are tested.

The property based tests will all be marked as with the `[Property]` attribute whereas the standard unit tests will be marked with the `[Fact]` attribute. Most property based tests will be set as verbose so that the input can be inspected.