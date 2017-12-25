using System;
using System.Globalization;

namespace Helper.Text
{
    internal class DateTimeFieldConverter : FormattableFixedLengthFieldConverter<DateTime>
    {
        public override DateTime ConvertStringToFieldValue(string format, string s)
        {
            return DateTime.ParseExact(s, format, CultureInfo.InvariantCulture);
        }

        public override DateTime ConvertStringToFieldValue(string s)
        {
            return ConvertStringToFieldValue(Format, s);
        }
    }
}