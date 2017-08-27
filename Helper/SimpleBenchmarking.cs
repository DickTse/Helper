using System;
using System.Diagnostics;

namespace Helper.Optimization
{
    /// <summary>
    /// A class for benchmarking.
    /// </summary>
    public static class SimpleBenchmarking
    {
        /// <summary>
        /// Benchmark an action, and display the elapsed time and number of collections GC has taken at the end.
        /// </summary>
        /// <param name="actionName">Name of the action that will be displayed along with the result.</param>
        /// <param name="action">The actual action to be benchmarked.</param>
        /// <remark>
        /// To make the result more realistic, before benchmarking starts, the GC will be cleaned.
        /// </remark>
        public static void Benchmark(string actionName, Action action)
        {
            CleanupGC();
            int collectionCount = GC.CollectionCount(0);
            Stopwatch sw = Stopwatch.StartNew();
            try
            {
                action();
            }
            finally
            {
                Console.WriteLine($"{sw.Elapsed} (GC={GC.CollectionCount(0) - collectionCount}) {actionName}");    
            }
        }

        private static void CleanupGC()
        {
            GC.Collect();
            GC.WaitForPendingFinalizers();
            GC.Collect();
        }
    }
}