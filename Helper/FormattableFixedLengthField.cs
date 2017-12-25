using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Helper.Text
{
    public abstract class FormattableFixedLengthField<T> : FixedLengthField<T> where T : IConvertible, IFormattable
    {
        /// <summary>
        /// Backing field of <see cref="Format"/>.
        /// </summary>
        private string format;

        /// <summary>
        /// The format for parsing the value between <see cref="FixedLengthField{T}.PaddedString"/> and <see cref="FixedLengthField{T}.Value"/>.
        /// </summary>
        public string Format {
            get => format;
            set => SetFormat(value);
        }

        protected void SetFormat(string format)
        {
            this.format = format;
            if (converter is IFormattableFixedLengthFieldConverter<T>)
                ((IFormattableFixedLengthFieldConverter<T>)converter).Format = format;
        }

        /// <summary>
        /// Field Length.
        /// </summary>
        /// <value>
        /// The pre-defined field length. All the characters in the actual value, and leading characters / trailing 
        /// characters are counted.
        /// It's value must be at least equal to the actual length of <see cref="Format"/>.
        /// </value>
        public override int Length 
        {
            get 
            {
                return length;
            }
            protected set 
            {
                //if (length < Format.Length)
                //    throw new ArgumentOutOfRangeException(nameof(Length), $"Value of {nameof(Length)} property cannot be smaller than the actual length of {nameof(Format)} property.");
                length = value;
                UpdatePaddedString();
            }
        }

        /// <summary>
        /// Initialize a <see cref="FormattableFixedLengthField{T}"/> class to a field with a given field name and a format 
        /// for formatting the string being parsed in and out the field.
        /// </summary>
        /// <param name="name">Field name.</param>
        /// <param name="format">Format for formatting strings.</param>
        public FormattableFixedLengthField(string name, string format) : this(name, format, format.Length)
        {
        }

        /// <summary>
        /// Initialize a <see cref="FormattableFixedLengthField{T}"/> class to a field with a given field name and field length.
        /// </summary>
        /// <param name="name">Field name.</param>
        /// <param name="length">
        /// Length of field value, including all leading or trailing padding character.
        /// </param>
        public FormattableFixedLengthField(string name, int length) : this(name, null, length)
        {
        }

        /// <summary>
        /// Initialize a <see cref="FormattableFixedLengthField{T}"/> class to a field with a given field name, a format 
        /// for formatting the string being parsed in and out the field, and field length.
        /// </summary>
        /// <param name="name">Field name.</param>
        /// <param name="format">Format for formatting strings.</param>
        /// <param name="length">
        /// Length of field value, including all leading or trailing padding character.
        /// </param>
        /// <remarks>
        /// Value of <paramref name="length"/> must not be shorter than the length of <paramref name="format"/>.
        /// </remarks>
        public FormattableFixedLengthField(string name, string format, int length): base(name, length, new FormattableFixedLengthFieldConverter<T>())
        {
            SetFormat(format);
        }

        /// <summary>
        /// Initialize a <see cref="FormattableFixedLengthField{T}"/> class to a field with a given field name, a format 
        /// for formatting the string being parsed in and out the field, and field length.
        /// </summary>
        /// <param name="name">Field name.</param>
        /// <param name="format">Format for formatting strings.</param>
        /// <param name="length">
        /// Length of field value, including all leading or trailing padding character.
        /// </param>
        /// <param name="converter">
        /// Converter for converting field value to padding string, and vice versa.
        /// </param>
        /// <remarks>
        /// Value of <paramref name="length"/> must not be shorter than the length of <paramref name="format"/>.
        /// </remarks>
        public FormattableFixedLengthField(string name, string format, int length, IFormattableFixedLengthFieldConverter<T> converter) : base(name, length, converter)
        {
            SetFormat(format);
        }

        protected override void UpdatePaddedString()
        {
            string formattedString = converter.ConvertFieldValueToString(value);
            paddedString = PadChar(formattedString);
        }
    }
}
