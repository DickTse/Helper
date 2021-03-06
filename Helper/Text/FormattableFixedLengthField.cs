﻿using System;

namespace Helper.Text
{
    public abstract class FormattableFixedLengthField<T> : FixedLengthField<T> where T : IConvertible, IFormattable
    {
        private IFormattableFixedLengthFieldConverter<T> converter;
        private IFormattableFixedLengthFieldValidator<T> validator;

        /// <summary>
        /// Backing field of <see cref="Format"/>.
        /// </summary>
        private string format;

        /// <summary>
        /// The format for parsing the value between <see cref="FixedLengthField{T}.RawString"/> and <see cref="FixedLengthField{T}.Value"/>.
        /// </summary>
        public string Format {
            get => format;
            set => SetFormat(value);
        }

        protected void SetFormat(string format)
        {
            this.format = format;
            converter.Format = format;
            validator.Format = format;
        }
        /// <summary>
        /// Initialize a <see cref="FormattableFixedLengthField{T}"/> class to a field with a given field name, a format 
        /// for formatting the string being parsed in and out the field, and field length.
        /// </summary>
        /// <param name="name">Field name.</param>
        /// <param name="length">
        /// Length of field value, including all leading or trailing padding character.
        /// </param>
        /// <param name="format">Format for formatting strings.</param>
        /// <param name="converter">
        /// Converter for converting field value to padding string, and vice versa.
        /// </param>
        /// <param name="validator">
        /// Validator for validating field's correctness.
        /// </param>
        /// <remarks>
        /// Value of <paramref name="length"/> must not be shorter than the length of <paramref name="format"/>.
        /// </remarks>
        public FormattableFixedLengthField(
            string name, int length, string format, IFormattableFixedLengthFieldConverter<T> converter, IFormattableFixedLengthFieldValidator<T> validator) 
            : base(name, length, converter, validator)
        {
            this.converter = converter;
            this.validator = validator;
            SetFormat(format);
        }

        public override string ToPaddedString()
        {
            return converter
                .ToString(InternalValue)
                .Pad(Length, PaddingCharPosition, PaddingChar);
        }
    }
}
