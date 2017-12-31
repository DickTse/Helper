using System;

namespace Helper.Text
{
    public abstract class FormattableFixedLengthFieldValidator<T> : IFormattableFixedLengthFieldValidator<T> where T : IConvertible, IFormattable
    {
        public string Format { get; set; }

        public abstract void ValidateRawString(string s);
    }
}