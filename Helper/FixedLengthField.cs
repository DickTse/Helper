using System;
using System.Diagnostics;
using System.Globalization;

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

        /// <summary>
        /// Field name.
        /// </summary>
        public string Name { get; private set; }

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
            protected set
            {
                length = value;
                SetPaddedString();
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
                SetPaddedString();
                SetValue();
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
                SetPaddedString();
                SetValue();
            } 
        }

        /// <summary>
        /// The backing field of <see cref="PaddedString"/>.
        /// </summary>
        protected string paddedString = String.Empty;

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
        /// The backing field of <see cref="Value"/>.
        /// </summary>
        protected T value;

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

        /// <summary>
        /// Update the value of <see cref="paddedString"/> while the value of <see cref="Length"/>, <see cref="PaddingChar"/>,
        /// <see cref="PaddingCharPosition"/> or <see cref="Value"/> is being changed.
        /// </summary>
        protected virtual void SetPaddedString()
        {
            paddedString = PadChar(value?.ToString());
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
        protected string PadChar(string s)
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
                value = ConvertStringToValue(trimmedString);
            }
        }

        /// <summary>
        /// Convert a string to an object of type <see cref="T"/>/>.
        /// </summary>
        /// <param name="s">String to be converted.</param>
        /// <returns>An object of type <see cref="T"/>.</returns>
        protected virtual T ConvertStringToValue(string s)
        {
            return (T)Convert.ChangeType(s, typeof(T));
        }

        /// <summary>
        /// Initialize a new instance of <see cref="FixedLengthField{T}"/> class to a field with a given field name and field length.
        /// </summary>
        /// <param name="name">Field name.</param>
        /// <param name="length">
        /// Length of field value, including all leading or trailing padding character.
        /// </param>
        public FixedLengthField(string name, int length)
        {
            this.Name = name;
            this.length = length;
        }
    }
}