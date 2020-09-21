namespace MarsRover.CSharp.Domain
{
    public enum Instruction
    {
        /// <summary>
        /// Rotate Left 90 Degrees
        /// </summary>
        L = 0,
        /// <summary>
        /// Move forward
        /// </summary>
        M = 1,
        /// <summary>
        /// Rotate Right 90 Degrees
        /// </summary>
        R = 2
    }

    public static class InstructionUtilities
    {
        public static Instruction? TryParse(char input)
        {

            switch (char.ToUpper(input))
            {
                case 'L': return Instruction.L;
                case 'M': return Instruction.M;
                case 'R': return Instruction.R;
                default: return null;
            }
        }
    }
}
