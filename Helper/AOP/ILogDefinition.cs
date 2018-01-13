namespace Helper.AOP
{
    public interface ILogDefinition
    {
        LogSeverity Severity { get; set; }
        string Code { get; set; }
        string Text { get; set; }
    }
}