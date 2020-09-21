using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace MarsRover.CSharp.Domain
{
    public class Plateau
    {
        public Plateau(int right, int top)
        {
            if (right <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(right), "The right parameter must be greater than 0");
            }
            if (top <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(top), "The top parameter must be greater than 0");
            }
            Top = top;
            Right = right;
        }

        public int Top { get; }
        public int Right { get; }
        public int Bottom => 0;
        public int Left => 0;

        public bool Contains(Position position)
        {
            // We intentionally don't handle the case where position is null here as we have specifically enabled the
            // nullable reference types feature of C# so we can clearly see when null is passed and can avoid it
            // at the code level.
            return Top >= position.Y && Bottom <= position.Y && Left <= position.X && Right >= position.X;
        }

        [return: MaybeNull]
        public static Plateau TryParse(string str)
        {
            var position = Position.TryParse(str);
            if (position == null)
            {
                return null;
            }
            else
            {
                return new Plateau(position.Value.X, position.Value.Y);
            }
        }
    }
}