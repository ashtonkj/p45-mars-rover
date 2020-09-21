using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace MarsRover.CSharp.Domain
{
    public class Simulation
    {
        private readonly TextReader _reader;
        private readonly TextWriter _writer;

        private IEnumerable<Instruction> ParseInstructionLine(string instructionLine)
        {
            var result = new List<Instruction>();
            foreach (var instruction in instructionLine.Select(InstructionUtilities.TryParse))
            {
                if (instruction != null)
                {
                    result.Add(instruction.Value);
                }
            }
            return result;
        }

        private bool IsMoreToRead()
        {
            return _reader.Peek() != -1;
        }

        private Plateau ReadPlateau()
        {
            var plateauLine = _reader.ReadLine();
            if (string.IsNullOrEmpty(plateauLine))
            {
                throw new InvalidOperationException("First line doesn't contain any data");
            }
            var plateau = Plateau.TryParse(plateauLine);
            if (plateau == null)
            {
                throw new InvalidOperationException("First line doesn't contain plateau coordinate data");
            }
            return plateau;
        }

        public Simulation(TextReader reader, TextWriter writer)
        {
            _reader = reader;
            _writer = writer;
            Plateau = ReadPlateau();
            Rovers = new List<Rover>();
        }

        public Plateau Plateau { get; }
        public List<Rover> Rovers { get; private set; }

        public void Execute()
        {
            while (IsMoreToRead())
            {
                var roverLine = _reader.ReadLine();
                if (!string.IsNullOrEmpty(roverLine))
                {
                    var rover = Rover.TryParse(Plateau, roverLine);
                    if (rover != null)
                    {
                        Rovers.Add(rover);
                        if (!IsMoreToRead())
                        {
                            return;
                        }
                        var instructionLine = _reader.ReadLine();
                        if (!string.IsNullOrEmpty(instructionLine))
                        {
                            var instructions = ParseInstructionLine(instructionLine);
                            rover.HandleInstructions(instructions);
                        }
                    }
                }
            }
        }

        public void PrintResults()
        {
            foreach (var rover in Rovers)
            {
                _writer.WriteLine(rover);
            }
        }

        public static Simulation FromString(string str, TextWriter output)
        {
            var reader = new StringReader(str);
            return new Simulation(reader, output);
        }

        public static Simulation FromString(string str)
        {
            return FromString(str, Console.Out);
        }
    }
}