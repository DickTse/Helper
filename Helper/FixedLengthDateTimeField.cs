using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Helper.Text
{
    public sealed class FixedLengthDateTimeField : FormattableFixedLengthField<DateTime>
    {
        private const string DefaultDateTimeFormat = "yyyyMMdd";

        /// <summary>
        /// Initialize a <see cref="FixedLengthDateTimeField{T}"/> class to a field with a given field name.
        /// </summary>
        /// <param name="name">Field name.</param>
        /// <remarks>
        /// As no particular format is given in this constructor, <see cref="FormattableFixedLengthField{T}.Format"/> 
        /// will be set to the value of <see cref="DefaultDateTimeFormat"/>.
        /// </remarks>
        public FixedLengthDateTimeField(string name) 
            : this(name, DefaultDateTimeFormat, DefaultDateTimeFormat.Length)
        {
        }

        /// <summary>
        /// Initialize a <see cref="FixedLengthDateTimeField{T}"/> class to a field with a given field name and a format 
        /// for formatting the string being parsed in and out the field.
        /// </summary>
        /// <param name="name">Field name.</param>
        /// <param name="format">Format for parsing the <see cref="DateTime"/> value into and out of string.</param>
        public FixedLengthDateTimeField(string name, string format) 
            : this(name, format, format.Length)
        {
        }

        /// <summary>
        /// Initialize a <see cref="FixedLengthDateTimeField{T}"/> class to a field with a given field name and field length.
        /// </summary>
        /// <param name="name">Field name.</param>
        /// <param name="length">
        /// Length of field value, including all leading or trailing padding character.
        /// </param>
        /// <remarks>
        /// As no particular format is given in this constructor, <see cref="FormattableFixedLengthField{T}.Format"/> 
        /// will be set to the value of <see cref="DefaultDateTimeFormat"/>.
        /// </remarks>
        public FixedLengthDateTimeField(string name, int length) 
            : this(name, DefaultDateTimeFormat, length)
        {
        }

        /// <summary>
        /// Initialize a <see cref="FixedLengthDateTimeField{T}"/> class to a field with a given field name, a format 
        /// for formatting the string being parsed in and out the field, and field length.
        /// </summary>
        /// <param name="name">Field name.</param>
        /// <param name="length">
        /// Length of field value, including all leading or trailing padding character.
        /// </param>
        /// <param name="format">Format for parsing the <see cref="DateTime"/> value into and out of string.</param>
        public FixedLengthDateTimeField(string name, string format, int length) 
            : base(name, length, format, new DateTimeFieldConverter())
        {
        }
    }
}
