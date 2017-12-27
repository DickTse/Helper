namespace Helper.Text
{
    public sealed class FixedLengthStringField : FixedLengthField<string>
    {
        /// <summary>
        /// Initialize a new instance of <see cref="FixedLengthStringField"/> class to a field with a given field name and field length.
        /// </summary>
        /// <param name="name">Field name.</param>
        /// <param name="length">
        /// Length of field value, including all leading or trailing padding character.
        /// </param>
        public FixedLengthStringField(string name, int length) : base(name, length)
        {
        }
    }
}
