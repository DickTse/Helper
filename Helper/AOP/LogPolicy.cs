using System;

namespace Helper.AOP
{
    public static class LogPolicy
    {
        private static ILogger _logger;

        public static void Register(ILogger logger)
        {
            _logger = logger;
        }

        public static void Log(ILogDefinition def, params object[] args)
        {
            ILogger logger = _logger;
            ParameterGuard.NullCheck(logger, nameof(_logger));
            logger.Log(def, args);
        }
    }
}
