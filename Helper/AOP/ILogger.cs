namespace Helper.AOP
{
    public interface ILogger
    {
        void Log(ILogDefinition def, params object[] args);
    }
}