using System;
using System.Globalization;

namespace Helper.Text
{
    internal class DateTimeFieldValidator : FormattableFixedLengthFieldValidator<DateTime>
    {
        public override void ValidateRawString(string s)
        {
            DateTime.ParseExact(s, Format, CultureInfo.InvariantCulture);
        }
    }
}