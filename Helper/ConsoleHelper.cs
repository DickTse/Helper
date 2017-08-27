using System;

namespace Helper
{
    /// <summary>
    /// A class for providing some helper methods for console applications.
    /// </summary>
    public class ConsoleHelper
    {
        public static void Pause()
        {
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
        }
    }
}