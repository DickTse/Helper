using System;
using System.Globalization;

namespace Helper.Text
{
    internal class FormattableFixedLengthFieldConverter<T> : IFormattableFixedLengthFieldConverter<T> where T : IFormattable
    {
        public string Format { get; set; }

        public virtual string ConvertFieldValueToString(string format, T value)
        {
            return ((T)Convert.ChangeType(value, typeof(T))).ToString(format, CultureInfo.InvariantCulture);
        }

        public virtual string ConvertFieldValueToString(T value)
        {
            return ConvertFieldValueToString(Format, value);
        }

        public virtual T ConvertStringToFieldValue(string format, string s)
        {
            throw new System.NotImplementedException();
        }

        public virtual T ConvertStringToFieldValue(string s)
        {
            throw new System.NotImplementedException();
        }
    }
}