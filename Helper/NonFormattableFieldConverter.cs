using System;

namespace Helper.Text
{
    internal class NonFormattableFieldConverter<T> : IFixedLengthFieldConverter<T>
    {
        public string ToString(T value)
        {
            return value?.ToString();
        }

        public T Parse(string s)
        {
            return (T)Convert.ChangeType(s, typeof(T));
        }
    }
}