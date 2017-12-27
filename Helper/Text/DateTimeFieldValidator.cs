using System;
using System.Globalization;

namespace Helper.Text
{
    internal class DateTimeFieldValidator : FormattableFixedLengthFieldValidator<DateTime>
    {
        public override void ValidateRawString(string s)
        {
            if (!DateTime.TryParseExact(s, Format, CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime d))
                throw new FormatException($"\"{s}\" cannot be parsed into {nameof(DateTime)}.");
        }
    }
}