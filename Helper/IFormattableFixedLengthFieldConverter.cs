namespace Helper.Text
{
    public interface IFormattableFixedLengthFieldConverter<T> : IFixedLengthFieldConverter<T>
    {
        string Format { get; set; }
        string ConvertFieldValueToString(string format, T value);
        T ConvertStringToFieldValue(string format, string s);
    }
}