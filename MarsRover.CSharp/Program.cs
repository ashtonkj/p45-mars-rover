using MarsRover.CSharp.Domain;
using System;
using System.IO;

namespace MarsRover.CSharp
{
    class Program
    {
        static void Main(string[] args)
        {
            // If no command line args are provided, we'll run the standard provided simulation.
            // Otherwise we'll assume the input is the path to a text file.
            if (args == null || args.Length == 0)
            {
                var simulation = Simulation.FromString(@"5 5
1 2 N
LMLMLMLMM
3 3 E
MMRMMRMRRM");
                simulation.Execute();
                simulation.PrintResults();
            }
            else
            {
                var filePath = args[0];
                if (!string.IsNullOrEmpty(filePath))
                {
                    if (File.Exists(filePath))
                    {
                        using var stream = new FileStream(filePath, FileMode.Open);
                        using var reader = new StreamReader(stream);
                        var simulation = new Simulation(reader, Console.Out);
                        simulation.Execute();
                        simulation.PrintResults();
                    }
                }
            }
        }
    }
}
