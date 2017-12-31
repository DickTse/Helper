namespace Helper
{
    /// <summary>
    /// This enumeration type declares a set of positions that a padding character can be padded into the value of
    /// a <see cref="FixedLengthField"/> field.
    /// </summary>
    public enum PaddingCharPosition
    {
        /// <summary>
        /// The padding character is / is going to be padded to the left of the field value.
        /// </summary>
        Left = 0,
        /// <summary>
        /// The padding character is / is going to be padded to the right of the field value.
        /// </summary>
        Right = 1
    }
}