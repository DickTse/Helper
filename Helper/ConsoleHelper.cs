using System;
using System.Diagnostics.CodeAnalysis;

namespace Helper
{
    /// <summary>
    /// A class for providing some helper methods for console applications.
    /// </summary>
    public static class ConsoleHelper
    {
        [ExcludeFromCodeCoverage]
        public static void Pause()
        {
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
        }
    }
}