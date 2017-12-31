using System;

namespace Helper.Text
{
    /// <summary>
    /// A class for setting the definition of a fixed-length field and storing the value of the field.
    /// </summary>
    public abstract class FixedLengthField<T> : IFixedLengthField where T : IConvertible
    {
        /// <summary>
        /// The default value of <see cref="paddingChar"/>.
        /// </summary>
        protected const char DefaultPaddingChar = ' ';

        protected IFixedLengthFieldConverter<T> converter;

        protected IFixedLengthFieldValidator<T> validator;

        private bool isValueEverSet = false;

        /// <summary>
        /// Field name.
        /// </summary>
        public string Name { get; protected set; }

        /// <summary>
        /// The backing field of <see cref="Length"/>.
        /// </summary>
        protected int length;

        /// <summary>
        /// Field Length.
        /// </summary>
        /// <value>
        /// The pre-defined field length. All the characters in the actual value, and leading characters / trailing 
        /// characters are counted.
        /// </value>
        public virtual int Length 
        { 
            get
            {
                return length;
            }
            private set
            {
                length = value;
            } 
        }

        /// <summary>
        /// The backing field of <see cref="PaddingChar"/>
        /// </summary>
        protected char paddingChar = DefaultPaddingChar;

        /// <summary>
        /// The leading / trailing character that is padded to the field.
        /// </summary>
        public char PaddingChar 
        { 
            get
            {
                return paddingChar;
            }
            set
            {
                paddingChar = value;
            }
        }

        /// <summary>
        /// The backing field of <see cref="PaddingCharPosition"/>.
        /// </summary>
        protected PaddingCharPosition paddingCharPosition = PaddingCharPosition.Right;

        /// <summary>
        /// The position of the padding character to be padded to the field.
        /// </summary>
        public PaddingCharPosition PaddingCharPosition 
        { 
            get
            {
                return paddingCharPosition;
            }
            set
            {
                paddingCharPosition = value;
            }
        }

        /// <summary>
        /// The backing field of <see cref="RawString"/>.
        /// </summary>
        protected string rawString = String.Empty;

        /// <summary>
        /// The raw string includes the actual value of the field, together leading or trailing padding character(s).
        /// Setting <see cref="RawString"/> also overwrites the value of <see cref="Value"/>.
        /// </summary>
        public string RawString 
        { 
            get
            {
                return rawString;
            }
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
        /// The backing field of <see cref="Value"/>.
        /// </summary>
        protected T value;

        /// <summary>
        /// The actual value of the field. The type of value is determined at runtime, which is identical to the type
        /// specified in the type-parameter.
        /// </summary>
        public dynamic Value
        {
            get
            {
                if (!isValueEverSet)
                    value = GetValueFromRawString(rawString);
                dynamic dynamicObject = this;
                return dynamicObject.value;
            }
            set
            {
                this.value = value;
                isValueEverSet = true;
            }
        }

        private T GetValueFromRawString(string rawString)
        {
            string trimmedString = TrimPaddingChar(rawString);
            if (!String.IsNullOrEmpty(trimmedString))
                return converter.Parse(trimmedString);
            else
                return default(T);
        }

        private string TrimPaddingChar(string s)
        {
            switch (paddingCharPosition)
            {
                case PaddingCharPosition.Left:
                    return s?.TrimStart(paddingChar);
                case PaddingCharPosition.Right:
                    return s?.TrimEnd(paddingChar);
                default:
                    return null;
            }
        }

        /// <summary>
        /// Pad the field value with leading/trailing padding characters.
        /// </summary>
        /// <returns>A string in which the field value is padded with leading / trailing padding characters.</returns>
        public virtual string ToPaddedString()
        {
            string s = converter.ToString(value);
            if (s.Length > length)
                return s.Substring(0, length);
            else
                return PadPaddingChar(s);
        }

        /// <summary>
        /// Pad the <see cref="paddingChar"/> into the string.
        /// </summary>
        /// <param name="s">The string that the <see cref="paddingChar"/> is going to be padded to.</param>
        /// <returns>
        /// A string with <see cref="paddingChar"/> padded into it.
        /// </returns>
        /// <remarks>
        /// The <see cref="paddingChar"/> can be padded to the left or the right of the string, subjected to the value
        /// of <see cref="paddingCharPosition"/>.
        /// </remarks>
        protected string PadPaddingChar(string s)
        {
            switch (paddingCharPosition)
            {
                case PaddingCharPosition.Left:
                    return s?.PadLeft(length, paddingChar);
                case PaddingCharPosition.Right:
                    return s?.PadRight(length, paddingChar);
                default:
                    return null;
            }
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
            this.Name = name;
            this.length = length;
            this.converter = converter;
            this.validator = validator;
        }
    }
}