using System;

namespace Helper.Text
{
    public interface IFixedLengthFieldValidator<T> where T : IConvertible
    {
        void ValidateRawString(string s); 
    }
}