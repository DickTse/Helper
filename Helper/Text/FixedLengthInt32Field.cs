namespace Helper.Text
{
    public sealed class FixedLengthInt32Field : FixedLengthField<int>
    {
        /// <summary>
        /// Initialize a new instance of <see cref="FixedLengthInt32Field"/> class to a field with a given field name and field length.
        /// </summary>
        /// <param name="name">Field name.</param>
        /// <param name="length">
        /// Length of field value, including all leading or trailing padding character.
        /// </param>
        public FixedLengthInt32Field(string name, int length) : base(name, length)
        {
        }
    }
}
