using System;

namespace Helper.Text
{
    /// <summary>
    /// A class for setting the definition of a fixed-length field and storing the value of the field.
    /// </summary>
    public abstract class FixedLengthField<T> : IFixedLengthField where T : IConvertible
    {
        /// <summary>
        /// The default value of <see cref="PaddingChar"/>.
        /// </summary>
        private const char DefaultPaddingChar = ' ';

        private IFixedLengthFieldConverter<T> converter;

        private IFixedLengthFieldValidator<T> validator;

        private bool isValueEverSet = false;

        /// <summary>
        /// Field name.
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// Field Length.
        /// </summary>
        /// <value>
        /// The pre-defined field length. All the characters in the actual value, and leading characters / trailing 
        /// characters are counted.
        /// </value>
        public int Length { get; private set; }

        /// <summary>
        /// The leading / trailing character that is padded to the field.
        /// </summary>
        public char PaddingChar { get; set; } = DefaultPaddingChar;

        /// <summary>
        /// The position of the padding character to be padded to the field.
        /// </summary>
        public PaddingCharPosition PaddingCharPosition { get; set; } = PaddingCharPosition.Right;

        /// <summary>
        /// The backing field of <see cref="RawString"/>.
        /// </summary>
        private string rawString = String.Empty;

        /// <summary>
        /// The raw string includes the actual value of the field, together leading or trailing padding character(s).
        /// Setting <see cref="RawString"/> also overwrites the value of <see cref="Value"/>.
        /// </summary>
        public string RawString 
        {
            get => rawString;
            set
            {
                ValidateRawString(value);
                rawString = value;
            } 
        }

        private void ValidateRawString(string s)
        {
            validator.ValidateRawString(s);
        }

        /// <summary>
        /// The backing field of <see cref="Value"/>, which belongs to type <see cref="typeof(T)"/> instead of <see cref="nameof(dynamic)"/>.
        /// </summary>
        protected T InternalValue { get; private set; }

        /// <summary>
        /// The actual value of the field. The type of value is determined at runtime, which is identical to the type
        /// specified in the type-parameter.
        /// </summary>
        public dynamic Value
        {
            get
            {
                if (!isValueEverSet)
                    InternalValue = GetValueFromRawString(rawString);
                dynamic dynamicObject = this;
                return dynamicObject.InternalValue;
            }
            set
            {
                InternalValue = value;
                isValueEverSet = true;
            }
        }

        private T GetValueFromRawString(string rawString)
        {
            string trimmedString = rawString.TrimPaddingChar(PaddingCharPosition, PaddingChar);
            if (!String.IsNullOrEmpty(trimmedString))
                return converter.Parse(trimmedString);
            else
                return default(T);
        }

        /// <summary>
        /// Pad the field value with leading/trailing padding characters.
        /// </summary>
        /// <returns>A string in which the field value is padded with leading / trailing padding characters.</returns>
        public virtual string ToPaddedString()
        {
            string s = converter.ToString(InternalValue);
            if (s.Length > Length)
                return s.Substring(0, Length);
            else
                return s.Pad(Length, PaddingCharPosition, PaddingChar);
        }

        /// <summary>
        /// Initialize a new instance of <see cref="FixedLengthField{T}"/> class to a field with a given field name and field length.
        /// </summary>
        /// <param name="name">Field name.</param>
        /// <param name="length">
        /// Length of field value, including all leading or trailing padding character.
        /// </param>
        public FixedLengthField(string name, int length) 
            : this(name, length, new NonFormattableFieldConverter<T>(), new NonFormattableFieldValidator<T>())
        {
        }

        /// <summary>
        /// Initialize a new instance of <see cref="FixedLengthField{T}"/> class to a field with a given field name and field length.
        /// </summary>
        /// <param name="name">Field name.</param>
        /// <param name="length">
        /// Length of field value, including all leading or trailing padding character.
        /// </param>
        /// <param name="converter">
        /// Converter for converting field value to padding string, and vice versa.
        /// </param>
        /// <param name="validator">
        /// Validator for validating field's correctness.
        /// </param>
        public FixedLengthField(string name, int length, IFixedLengthFieldConverter<T> converter, IFixedLengthFieldValidator<T> validator)
        {
            Name = name;
            Length = length;
            this.converter = converter;
            this.validator = validator;
        }
    }
}