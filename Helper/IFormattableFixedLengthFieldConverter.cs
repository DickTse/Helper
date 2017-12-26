namespace Helper.Text
{
    public interface IFormattableFixedLengthFieldConverter<T> : IFixedLengthFieldConverter<T>
    {
        string Format { get; set; }
        string ToString(T value, string format);
        T Parse(string s, string format);
    }
}