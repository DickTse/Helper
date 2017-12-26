namespace Helper.Text
{
    /// <summary>
    /// An interface for accessing <see cref="FixedLengthField&lt;T&gt;"/> class.
    /// </summary>
    public interface IFixedLengthField
    {
        /// <summary>
        /// Field name.
        /// </summary>
        string Name { get; }

        /// <summary>
        /// Field length.
        /// </summary>
        int Length { get; }
        
        /// <summary>
        /// The raw string the includes the actual value of the field, together leading or trailing padding character(s).
        /// </summary>
        string RawString { get; set; }

        /// <summary>
        /// The actual value of the field. The type of value is determined at runtime. It is identical to the generic type 
        /// given while the fixed-length-field object is declared.
        /// </summary>
        dynamic Value { get; set; }

        /// <summary>
        /// String formed by <see cref="RawString"/> // <see cref="Value"/> padded with leading // trailing padding characters.
        /// </summary>
        /// <returns></returns>
        string ToPaddedString();
    }
}   
