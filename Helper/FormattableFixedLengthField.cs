using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Helper.Text
{
    public abstract class FormattableFixedLengthField<T> : FixedLengthField<T> where T : IConvertible, IFormattable
    {
        /// <summary>
        /// The format for parsing the value between <see cref="FixedLengthField{T}.PaddedString"/> and <see cref="FixedLengthField{T}.Value"/>.
        /// </summary>
        public string Format { get; set; }

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
                if (length < Format.Length)
                    throw new ArgumentOutOfRangeException(nameof(Length), $"Value of {nameof(Length)} property cannot be smaller than the actual length of {nameof(Format)} property.");
                length = value;
                SetPaddedString();
            }
        }

        /// <summary>
        /// Initialize a <see cref="FormattableFixedLengthField{T}"/> class to a field with a given field name and a format 
        /// for formatting the string being parsed in and out the field.
        /// </summary>
        /// <param name="name">Field name.</param>
        /// <param name="format">Format for formatting strings.</param>
        public FormattableFixedLengthField(string name, string format) : base(name, format.Length)
        {
            Format = format;
        }

        /// <summary>
        /// Initialize a <see cref="FormattableFixedLengthField{T}"/> class to a field with a given field name and field length.
        /// </summary>
        /// <param name="name">Field name.</param>
        /// <param name="length">
        /// Length of field value, including all leading or trailing padding character.
        /// </param>
        public FormattableFixedLengthField(string name, int length) : base(name, length)
        {
            if (length < Format.Length)
                throw new ArgumentOutOfRangeException(
                    nameof(length), 
                    $"Value of {nameof(length)} argument cannot be smaller than the actual length of {nameof(Format)} property."
                );
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
        public FormattableFixedLengthField(string name, string format, int length) : base(name, length)
        {
            if (length < format.Length)
                throw new ArgumentOutOfRangeException(
                    nameof(length), 
                    $"Value of {nameof(length)} argument cannot be smaller than the actual length of {nameof(format)} arguement."
                );
            Format = format;
        }

        protected override void SetPaddedString()
        {
            string formattedString = GetFormattedStringFromValue();
            paddedString = PadChar(formattedString);
        }

        protected abstract string GetFormattedStringFromValue();
    }
}
