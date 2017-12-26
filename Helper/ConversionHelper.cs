using System;
using System.ComponentModel;

namespace Helper
{
    public static class ConversionHelper
    {
        public static bool TryParse<T>(string s, out T o)
        {
            o = default(T);
            if (TypeDescriptor.GetConverter(typeof(T)).CanConvertFrom(typeof(string)))
            {
                try
                {
                    o = (T)TypeDescriptor.GetConverter(typeof(T)).ConvertFromString(s);
                    return true;
                }
                catch (NotSupportedException)
                {
                    return false;
                }
                catch (Exception ex) when (ex.InnerException is FormatException)
                {
                    return false;
                }
                catch (Exception ex) when (ex.InnerException is OverflowException)
                {
                    return false;
                }
            }
            return false;
        }

        public static bool CanParse<T>(string s)
        {
            if (TypeDescriptor.GetConverter(typeof(T)).CanConvertFrom(typeof(string)))
            {
                try
                {
                    TypeDescriptor.GetConverter(typeof(T)).ConvertFromString(s);
                    return true;
                }
                catch (NotSupportedException)
                {
                    return false;
                }
                catch (Exception ex) when (ex.InnerException is FormatException)
                {
                    return false;
                }
                catch (Exception ex) when (ex.InnerException is OverflowException)
                {
                    return false;
                }
            }
            return false;
        }
    }
}
