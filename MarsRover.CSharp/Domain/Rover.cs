using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace MarsRover.CSharp.Domain
{
    public class Rover
    {
        public Rover(Plateau plateau, Position position, Direction direction)
        {
            if (!plateau.Contains(position))
            {
                throw new ArgumentOutOfRangeException(nameof(position), "Position must be within the plateau.");
            }
            Position = position;
            Direction = direction;
            Plateau = plateau;
        }

        public Position Position { get; private set; }
        public Direction Direction { get; private set; }
        public Plateau Plateau { get; }

        public override string ToString()
        {
            return $"{Position.X} {Position.Y} {Direction}";
        }

        private void HandleMovement()
        {
            Position offset;
            switch (Direction)
            {
                case Direction.N: offset = new Position(0, 1); break;
                case Direction.E: offset = new Position(1, 0); break;
                case Direction.S: offset = new Position(0, -1); break;
                case Direction.W: offset = new Position(-1, 0); break;
                default: offset = new Position(0, 0); break;
            }
            var newPosition = Position + offset;
            if (Plateau.Contains(newPosition))
            {
                Position = newPosition;
            }
        }

        public void Handle(Instruction instruction)
        {
            switch (instruction)
            {
                case Instruction.L: Direction = Direction.RotateLeft(); break;
                case Instruction.M: HandleMovement(); break;
                case Instruction.R: Direction = Direction.RotateRight(); break;
                default: throw new ArgumentOutOfRangeException(nameof(instruction));
            }
        }

        [return: MaybeNull]
        public static Rover TryParse(Plateau plateau, string str)
        {
            var parts = str.Split(" ", StringSplitOptions.RemoveEmptyEntries);
            if (parts.Length != 3)
            {
                return null;
            }
            else
            {
                var position = Position.FromStrings(parts[0], parts[1]);
                var direction = DirectionUtilities.TryParse(parts[2]);
                if (position != null && direction != null)
                {
                    return new Rover(plateau,position.Value, direction.Value);
                }
                else
                {
                    return null;
                }
            }
        }
    }
}
