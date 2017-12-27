using System;

namespace Helper.Text
{
    public sealed class FixedLengthInt64Field : FixedLengthField<Int64>
    {
        /// <summary>
        /// Initialize a new instance of <see cref="FixedLengthInt64Field"/> class to a field with a given field name and field length.
        /// </summary>
        /// <param name="name">Field name.</param>
        /// <param name="length">
        /// Length of field value, including all leading or trailing padding character.
        /// </param>
        public FixedLengthInt64Field(string name, int length) : base(name, length)
        {
        }
    }
}