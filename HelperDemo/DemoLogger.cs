using Helper.AOP;
using System;

namespace Helper.Demo
{
    internal class DemoLogger : ILogger
    {
        public void Log(ILogDefinition def, params object[] args)
        {
            Console.WriteLine(def.Code + "\t" + def.Text, args);
        }
    }
}