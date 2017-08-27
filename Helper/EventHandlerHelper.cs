using System;
using System.Threading;

namespace Helper.Threading
{
    public static class EventHandlerHelper
    {
        public static void Raise<TEventArgs>(this EventHandler<TEventArgs> eventDelegate, Object sender, TEventArgs e)
            where TEventArgs : EventArgs
        {
            // Copy a reference to the delegate field into a temporary variable now for thread safety.
            // Volatile.Read is used here to prevent the temporary variable from being removed by the compiler during
            // optimization.
            EventHandler<TEventArgs> temp = Volatile.Read(ref eventDelegate);

            // If any methods registered interest with our event, notify them.
            if (temp != null)
                temp(sender, e);
        }
    }
}