using System;

namespace Helper.Text
{
    public interface IFormattableFixedLengthFieldValidator<T> : IFixedLengthFieldValidator<T> where T : IConvertible
    {
        string Format { get; set; }
    }
}