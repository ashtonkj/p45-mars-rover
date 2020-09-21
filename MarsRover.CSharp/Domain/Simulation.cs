using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Linq;

namespace MarsRover.CSharp.Domain
{
    public class Simulation
    {
        private readonly TextReader _reader;
        private readonly TextWriter _writer;

        public Simulation(TextReader reader, TextWriter writer)
        {
            _reader = reader;
            _writer = writer;
            Plateau = ReadPlateau();
            Rovers = new List<Rover>();
        }

        public Plateau Plateau { get; }
        public List<Rover> Rovers { get; private set; }

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

        public void HandleRover(Func<string> roverPositionLine, Func<string> instructionLine)
        {
            var rover = Rover.TryParse(Plateau, roverPositionLine());
            if (rover != null)
            {
                var instructions = instructionLine().Select(InstructionUtilities.TryParse);
                foreach (var instruction in instructions)
                {
                    if (instruction != null)
                    {
                        rover.Handle(instruction.Value);
                    }
                }
                Rovers.Add(rover);
            }
        }

        private IEnumerable<Instruction?> ParseInstructionLine(string instructionLine)
        {
            return instructionLine.Select(InstructionUtilities.TryParse);
        }

        private bool IsMoreToRead()
        {
            return _reader.Peek() != -1;
        }

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
                            foreach (var instruction in instructions)
                            {
                                if (instruction != null)
                                {
                                    rover.Handle(instruction.Value);
                                }
                            }
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

        public static Simulation FromString(string str)
        {
            var reader = new StringReader(str);
            TextWriter writer = Console.Out;
            return new Simulation(reader, writer);
        }
    }
}