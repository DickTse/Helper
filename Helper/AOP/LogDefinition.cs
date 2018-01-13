using System;
using System.Collections.Generic;

namespace Helper.AOP
{
    public class LogDefinition : ILogDefinition 
    {
        private static Dictionary<(LogSeverity, string), ILogDefinition> _registeredDefinitions = new Dictionary<(LogSeverity, string), ILogDefinition>();

        public static readonly LogDefinition ApplicationLaunch = Register(LogSeverity.Info, "00001", "Application {0} start.");
        public static readonly LogDefinition ApplicationExit = Register(LogSeverity.Info, "99999", "Application {0} exit.");

        public LogSeverity Severity { get; set; }
        public string Code { get; set; }
        public string Text { get; set; }

        //protected LogDefinition() { }

        protected static LogDefinition Register(LogSeverity severity, string code, string text)
        {
            return Register<LogDefinition>(severity, code, text);
        }

        protected static TDerived Register<TDerived>(LogSeverity severity, string code, string text)
            where TDerived : ILogDefinition, new()
        {
            if (_registeredDefinitions.ContainsKey((severity, code)))
                throw new ArgumentException($"Log definition with the severity ({severity}) and code (\"{code}\") cannot be registered. It has been registered before.");
            TDerived def = new TDerived()
            {
                Severity = severity,
                Code = code,
                Text = text
            };
            _registeredDefinitions.Add((severity, code), def);
            return def;
        }
    }
}
