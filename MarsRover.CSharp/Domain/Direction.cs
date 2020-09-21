using System;

namespace MarsRover.CSharp.Domain
{
    public enum Direction
    {
        N = 0,
        E = 1,
        S = 2,
        W = 3
    }

    public static class DirectionUtilities
    {
        public static Nullable<Direction> TryParse(string str)
        {
            if (str.Length > 1)
            {
                return null;
            }
            return DirectionUtilities.TryParse(str[0]);
        }

        public static Nullable<Direction> TryParse(char input)
        {
            switch (char.ToUpper(input))
            {
                case 'N': return Direction.N;
                case 'E': return Direction.E;
                case 'S': return Direction.S;
                case 'W': return Direction.W;
                default: return null;
            }
        }

        public static Direction RotateLeft(this Direction direction)
        {
            switch (direction)
            {
                case Direction.N: return Direction.W;
                case Direction.E: return Direction.N;
                case Direction.S: return Direction.E;
                case Direction.W: return Direction.S;
                default: throw new ArgumentOutOfRangeException(nameof(direction));
            }
        }

        public static Direction RotateRight(this Direction direction)
        {
            switch (direction)
            {
                case Direction.N: return Direction.E;
                case Direction.E: return Direction.S;
                case Direction.S: return Direction.W;
                case Direction.W: return Direction.N;
                default: throw new ArgumentOutOfRangeException(nameof(direction));
            }
        }
    }
}