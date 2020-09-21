using System;
using System.Collections.Generic;
using System.Text;

namespace MarsRover.CSharp.Domain
{
    public struct Position
    {
        public Position(int x, int y)
        {
            X = x;
            Y = y;
        }

        public int X { get; }
        public int Y { get; }

        public override string ToString()
        {
            return $"{X} {Y}";
        }

        /// <summary>
        /// Tries to parse a string into a position. The expected input structure of the string is: <code>X Y</code>
        /// </summary>
        /// <param name="str">The string.</param>
        /// <returns></returns>
        public static Position? TryParse(string str)
        {
            var parts = str.Split(" ", StringSplitOptions.RemoveEmptyEntries);
            if (parts.Length != 2)
            {
                return null;
            }

            return Position.FromStrings(parts[0], parts[1]);
        }

        public static Position? FromStrings(string xStr, string yStr)
        {
            var isValidX = Int32.TryParse(xStr, out int x);
            var isValidY = Int32.TryParse(yStr, out int y);
            if (isValidX && isValidY)
            {
                return new Position(x, y);
            }
            else
            {
                return null;
            }
        }

        public static Position operator +(Position p1, Position p2)
        {
            return new Position(p1.X + p2.X, p1.Y + p2.Y);
        }
    }
}
