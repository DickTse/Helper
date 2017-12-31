using System;

namespace Helper
{
    public static class StringHelper
    {
        public static string Pad(this string s, int length, PaddingCharPosition position, char paddingChar)
        {
            ParameterGuard.NullCheck(s, nameof(s));

            switch (position)
            {
                case PaddingCharPosition.Left:
                    return s.PadLeft(length, paddingChar);
                case PaddingCharPosition.Right:
                    return s.PadRight(length, paddingChar);
                default:
                    throw new NotSupportedException($"The value of {nameof(position)} ({position}) is not supported.");
            }
        }

        public static string TrimPaddingChar(this string s, PaddingCharPosition position, char paddingChar)
        {
            ParameterGuard.NullCheck(s, nameof(s));

            switch (position)
            {
                case PaddingCharPosition.Left:
                    return s?.TrimStart(paddingChar);
                case PaddingCharPosition.Right:
                    return s?.TrimEnd(paddingChar);
                default:
                    throw new NotSupportedException($"The value of {nameof(position)} ({position}) is not supported.");
            }
        }
    }
}
