namespace Helper.Text
{
    public sealed class FixedLengthDecimalField : FixedLengthField<decimal>
    {
        /// <summary>
        /// Initialize a new instance of <see cref="FixedLengthDecimalField"/> class to a field with a given field name and field length.
        /// </summary>
        /// <param name="name">Field name.</param>
        /// <param name="length">
        /// Length of field value, including all leading or trailing padding character.
        /// </param>
        public FixedLengthDecimalField(string name, int length) : base(name, length)
        {
        }
    }
}
