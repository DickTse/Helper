using System;

namespace Helper.Text
{
    internal class NonFormattableFieldConverter<T> : IFixedLengthFieldConverter<T>
    {
        public string ConvertFieldValueToString(T value)
        {
            return value?.ToString();
        }

        public T ConvertStringToFieldValue(string s)
        {
            return (T)Convert.ChangeType(s, typeof(T));
        }
    }
}