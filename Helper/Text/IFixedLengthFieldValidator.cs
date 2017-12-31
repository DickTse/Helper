namespace Helper.Text
{
    public interface IFixedLengthFieldValidator<T>
    {
        void ValidateRawString(string s); 
    }
}