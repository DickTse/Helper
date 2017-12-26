using System;
using System.ComponentModel;

namespace Helper.Text
{
    public class NonFormattableFieldValidator<T> : IFixedLengthFieldValidator<T> where T : IConvertible
    {
        public void ValidateRawString(string s)
        {
            TypeDescriptor.GetConverter(typeof(T)).ConvertFromString(s);
        }
    }
}