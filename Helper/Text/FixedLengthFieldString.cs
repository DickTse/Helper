using System.Linq;

namespace Helper.Text
{
    /// <summary>
    /// A class for splitting a given raw string into <see cref="IFixedLengthField"/> fields based on a set of field 
    /// defintion, or assembling a set of <see cref="IFixedLengthField"/> fields to build a string.
    /// </summary>
    public sealed class FixedLengthFieldString
    {
        private string rawString = null;
        
        /// <summary>
        /// A collection of <see cref="IFixedLengthField"/> fields.
        /// </summary>
        public FixedLengthFieldCollection Fields { get; private set; }

        /// <summary>
        /// Initalizes a <see cref="FixedLengthFieldString"/> object with a given set of field definitions. This
        /// constructor is essential for assembling a set of <see cref="IFixedLengthField"/> fields to build a string.
        /// </summary>
        /// <param name="fields">A set of defintions that defines the fields in the string.</param>
        public FixedLengthFieldString(FixedLengthFieldCollection fields)
        {
            ParameterGuard.NullCheck(fields, nameof(fields));
            Fields = fields;
        }

        /// <summary>
        /// Initalizes a <see cref="FixedLengthFieldString"/> object with a given raw string and a given set of field 
        /// definitions, and parsing the raw string and split it into <see cref="IFixedLengthField"/> fields.
        /// </summary>
        /// <param name="rawString">The raw string, which contains the values of a number of fixed-length fields.</param>
        /// <param name="fields">A set of defintions that defines the fields in the string.</param>
        public FixedLengthFieldString(FixedLengthFieldCollection fields, string rawString)
        {
            ParameterGuard.NullCheck(rawString, nameof(rawString));
            ParameterGuard.NullCheck(fields, nameof(fields));

            Fields = fields;
            this.rawString = rawString;
            ParseRawString();
        }

        private void ParseRawString()
        {
            ValidateRawString();
            SplitRawStringIntoFields();
        }

        private void ValidateRawString()
        {
            if (rawString.Length < Fields.TotalFieldLength)
                throw new MalformedRawStringException("The raw string is too short.", rawString);
            if (rawString.Length > Fields.TotalFieldLength)
                throw new MalformedRawStringException("The raw string is too long.", rawString);
        }

        private void SplitRawStringIntoFields()
        {
            string s = rawString;
            foreach (var field in Fields)
            {
                field.RawString = s.Substring(0, field.Length);
                s = s.Substring(field.Length);
            }
        }

        /// <summary>
        /// Converts the <see cref="FixedLengthFieldString"/> to its equivalent string representation.
        /// </summary>
        /// <returns>
        /// The raw string if the raw string is given in the constructor; otherwise, the string assembled from a set of
        /// <see cref="IFixedLengthField"/> fields.
        /// </returns>
        public override string ToString()
        {
            return rawString?? string.Join("", Fields.Select(f => f.ToPaddedString()).ToArray());
        }
    }
}