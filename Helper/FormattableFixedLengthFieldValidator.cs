using System;

namespace Helper.Text
{
    public class FormattableFixedLengthFieldValidator<T> : IFormattableFixedLengthFieldValidator<T> where T : IConvertible, IFormattable
    {
        public string Format { get; set; }

        public virtual void ValidateRawString(string s)
        {
            throw new NotImplementedException();
        }
    }
}