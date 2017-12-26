using System;

namespace Helper.Text
{
    public interface IFixedLengthFieldConverter<T>
    {
        string ToString(T value);
        T Parse(string s);
    }
}