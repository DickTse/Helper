using System;
using System.Globalization;

namespace Helper.Text
{
    internal class DateTimeFieldConverter : FormattableFixedLengthFieldConverter<DateTime>
    {
        public override DateTime Parse(string s, string format)
        {
            return DateTime.ParseExact(s, format, CultureInfo.InvariantCulture);
        }

        public override DateTime Parse(string s)
        {
            return Parse(s, Format);
        }
    }
}