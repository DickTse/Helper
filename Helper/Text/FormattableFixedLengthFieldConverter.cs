using System;
using System.Globalization;

namespace Helper.Text
{
    internal class FormattableFixedLengthFieldConverter<T> : IFormattableFixedLengthFieldConverter<T> where T : IConvertible, IFormattable
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

        public virtual T Parse(string s, string format)
        {
            throw new System.NotImplementedException();
        }

        public virtual T Parse(string s)
        {
            throw new System.NotImplementedException();
        }
    }
}