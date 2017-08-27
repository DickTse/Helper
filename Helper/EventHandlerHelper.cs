using System;
using System.Threading;

namespace Helper.Threading
{
    /// <summary>
    /// Helper class for handling events.
    /// </summary>
    public static class EventHandlerHelper
    {
        /// <summary>
        /// Raise event in a thread-safe way.
        /// </summary>
        /// <param name="eventDelegate">The delegate of the event being raised.</param>
        /// <param name="sender">The sender object.</param>
        /// <param name="e">Event arguments.</param>
        /// <remark>
        /// Usually, while firing an event, we will do something like this:
        ///     if (eventDelegate != null) eventDelegate(sender, e);
        /// However, in a multi-thread environment, sometimes the event delegate becomes NULL after the NULL-checking
        /// and before the event is fired. In that case, it results in an Null Exception.
        /// 
        /// EventHandlerHelper.Raise solves this problem by first of all copying the delegate to a temporary variable.
        /// After that, all the NULL-checking and subsequent event-firing actions are done on the temporary variable
        /// rather than the given event-delegate itself.
        /// </remark>
        public static void Raise<TEventArgs>(this EventHandler<TEventArgs> eventDelegate, Object sender, TEventArgs e)
            where TEventArgs : EventArgs
        {
            // Copy a reference to the delegate field into a temporary variable now for thread safety.
            // Volatile.Read is used here to prevent the temporary variable from being removed by the compiler during
            // optimization.
            EventHandler<TEventArgs> tempDelegate = Volatile.Read(ref eventDelegate);

            // If any methods registered interest with our event, notify them.
            if (tempDelegate != null)
                tempDelegate(sender, e);
        }
    }
}