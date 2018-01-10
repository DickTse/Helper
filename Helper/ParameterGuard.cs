using System;
using System.Collections.Generic;

namespace Helper
{
    /// <summary>
    /// Provides static methods for validating parameter values.
    /// </summary>
    public class ParameterGuard
    {
        /// <summary>
        /// Check whether a parameter value is null or not. If it is null, throw an <see cref="ArgumentNullException"/>.
        /// </summary>
        /// <typeparam name="T">The type of the parameter value to be validated.</typeparam>
        /// <param name="paramValue">The parameter value to be validated.</param>
        /// <param name="paramName">The name of the parameter to be validated.</param>
        /// <param name="message">The message to be returned in the exception message if the value is null.</param>
        public static void NullCheck<T>(T paramValue, string paramName, 
            string message = "Parameter must not be null.") where T : class
        {
            if (paramValue is null)
                throw new ArgumentNullException(paramName, message);
        }

        /// <summary>
        /// Check whether a parameter value equals to the default value of it's own type or not. If it equals to the
        /// default value, throw an <see cref="ArgumentException"/>.
        /// </summary>
        /// <typeparam name="T">The type of the parameter value to be validated.</typeparam>
        /// <param name="paramValue">The parameter value to be validated.</param>
        /// <param name="paramName">The name of the parameter to be validated.</param>
        /// <param name="message">
        /// The message to be returned in the exception message if the value equals to it's default value.
        /// </param>
        public static void DefaultValueCheck<T>(T paramValue, string paramName, 
            string message = "Parameter must not be undefined.") where T : struct
        {
            if (EqualityComparer<T>.Default.Equals(paramValue, default(T)))
                throw new ArgumentException(message, paramName);
        }

        /// <summary>
        /// Check whether a string parameter is empty or not. If it is empty, throw an <see cref="ArgumentException"/>.
        /// </summary>
        /// <param name="s">The string parameter to be validated.</param>
        /// <param name="paramName">The name of the parameter to be validated.</param>
        /// <param name="message">
        /// The message to be returned in the exception message if the string parameter is empty.
        /// </param>
        public static void EmptyStringCheck(string s, string paramName, 
            string message = "String parameter must not be empty.")
        {
            if (s?.Length == 0)
                throw new ArgumentException(message, paramName);
        }

        /// <summary>
        /// Check whether a string parameter is null or empty or not. If it is null, throw an <see cref="ArgumentNullException"/>;
        /// If it is empty, throw an <see cref="ArgumentException"/>.
        /// </summary>
        /// <param name="s">The string parameter to be validated.</param>
        /// <param name="paramName">The name of the parameter to be validated.</param>
        /// <param name="message">
        /// The message to be returned in the exception message if the string parameter is null or empty.
        /// </param>
        public static void NullOrEmptyStringCheck(string s, string paramName, 
            string message = "String parameter must not be null or empty.")
        {
            if (s is null)
                throw new ArgumentNullException(paramName, message);
            if (s.Length == 0)
                throw new ArgumentException(message, paramName);
        }
    }
}