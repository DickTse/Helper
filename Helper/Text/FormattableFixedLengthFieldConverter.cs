using System;
using System.Globalization;

namespace Helper.Text
{
    internal abstract class FormattableFixedLengthFieldConverter<T> : IFormattableFixedLengthFieldConverter<T> where T : IConvertible, IFormattable
    {
        public string Format { get; set; }

        public virtual string ToString(T value, string format)
        {
            return ((T)Convert.ChangeType(value, typeof(T))).ToString(format, CultureInfo.InvariantCulture);
        }

        public virtual string ToString(T value)
        {
            return ToString(value, Format);
        }

        public abstract T Parse(string s, string format);

        public abstract T Parse(string s);
    }
}