using System;

namespace Helper.Text
{
    public class NonFormattableFieldValidator<T> : IFixedLengthFieldValidator<T>
    {
        public void ValidateRawString(string s)
        {
            if (!ConversionHelper.CanParse<T>(s))
                throw new FormatException($"\"{s}\" cannot be parsed into {typeof(T).FullName}.");
        }
    }
}