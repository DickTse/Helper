using System;

namespace Helper.Text
{
    public interface IFixedLengthFieldConverter<T>
    {
        string ConvertFieldValueToString(T value);
        T ConvertStringToFieldValue(string s);
    }
}