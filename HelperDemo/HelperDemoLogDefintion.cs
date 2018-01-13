using Helper.AOP;

namespace Helper.Demo
{
    internal class HelperDemoLogDefintion : LogDefinition
    {
        public static readonly HelperDemoLogDefintion Foo = Register<HelperDemoLogDefintion>(LogSeverity.Info, "00002", "foo");
        public static readonly HelperDemoLogDefintion Bar = Register<HelperDemoLogDefintion>(LogSeverity.Info, "00003", "bar");
    }
}