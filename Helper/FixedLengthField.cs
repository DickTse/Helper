using System;
using System.Diagnostics;
using System.Globalization;

namespace Helper.Text
{
    /// <summary>
    /// Provides a means for setting the definition of a fixed-length field and storing the value of the field.
    /// </summary>
    public class FixedLengthField<T> : IFixedLengthField where T : IConvertible
    {
        private const string DefaultDateTimeFormatString = "yyyyMMdd";

        private int length = 0;
        private char paddingChar;
        private PaddingCharPosition paddingCharPosition = PaddingCharPosition.Right;
        private string paddedString = String.Empty;
        private T value;

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
        public int Length 
        { 
            get
            {
                return length;
            }
            private set
            {
                length = value;
                SetPaddedString();
            } 
        }

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
                SetPaddedString();
                SetValue();
            } 
        }

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
                SetPaddedString();
                SetValue();
            } 
        }

        /// <summary>
        /// The raw string the includes the actual value of the field, together leading or trailing padding character(s).
        /// </summary>
        public string PaddedString 
        { 
            get
            {
                return paddedString;
            }
            set
            {
                paddedString = value;
                SetValue();
            } 
        }

        /// <summary>
        /// The actual value of the field. The type of value is determined at runtime. It is identical to the generic type 
        /// given while the fixed-length-field object is declared.
        /// </summary>
        public dynamic Value
        {
            get
            {
                dynamic dynamicObject = this;
                return dynamicObject.value;
            }
            set
            {
                this.value = value;
                SetPaddedString();
            }
        }

        private void SetPaddedString()
        {
            string s = (typeof(T) == typeof(DateTime))? ((DateTime)Convert.ChangeType(value, typeof(DateTime))).ToString(DateTimeFormatString) : value?.ToString();
            switch (paddingCharPosition)
            {
                case PaddingCharPosition.Left:
                    paddedString = s?.PadLeft(length, paddingChar);
                    break;
                case PaddingCharPosition.Right:
                    paddedString = s?.PadRight(length, paddingChar);
                    break;
            }
        }

        private void SetValue()
        {
            string trimmedString = GetTrimmedString();
            SetValueInDefinedType(trimmedString);
        }

        private string GetTrimmedString()
        {
            switch (paddingCharPosition)
            {
                case PaddingCharPosition.Left:
                    return paddedString?.TrimStart(paddingChar);
                case PaddingCharPosition.Right:
                    return paddedString?.TrimEnd(paddingChar);
                default:
                    return null;
            }
        }

        private void SetValueInDefinedType(string trimmedString)
        {
            if (!String.IsNullOrEmpty(trimmedString))
            {
                if (typeof(T) == typeof(DateTime))
                {
                    DateTime dateTimeFromString = DateTime.ParseExact(trimmedString, DateTimeFormatString, CultureInfo.InvariantCulture);
                    value = (T)Convert.ChangeType(dateTimeFromString, typeof(T));
                }
                else
                    value = (T)Convert.ChangeType(trimmedString, typeof(T));
            }
        }

        /// <summary>
        /// The format string that will be used to parse the datetime string into field value, or vice versa. It will
        /// be ignored if the field is not a DateTime field.
        /// </summary>
        public string DateTimeFormatString { get; set; } = DefaultDateTimeFormatString;

        /// <summary>
        /// Constructor of a fixed-length-field object.
        /// </summary>
        /// <param name="name">Field name.</param>
        /// <param name="length">Length of field value, including all leading or trailing padding character.</param>
        public FixedLengthField(string name, int length)
        {
            this.Name = name;
            this.length = length;
        }
    }
}