using Helper.AOP;
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
        /// Initalizes a <see cref="FixedLengthFieldString"/> object with a definition of a set of fixed-length fields.
        /// </summary>
        /// <param name="fields">Defintion of fields in the string.</param>
        public FixedLengthFieldString(FixedLengthFieldCollection fields)
        {
            ParameterGuard.NullCheck(fields, nameof(fields));
            Fields = fields;
        }

        /// <summary>
        /// Initalizes a <see cref="FixedLengthFieldString"/> object with a definition of a set of fixed-length fields
        /// and a string. The string will be parsed and values stored in the string will be assigned to the fields.
        /// </summary>
        /// <param name="fields">Defintion of fields in the string.</param>
        /// <param name="s">The string to be parsed in to the fields.</param>
        public FixedLengthFieldString(FixedLengthFieldCollection fields, string s)
        {
            ParameterGuard.NullCheck(s, nameof(s));
            ParameterGuard.NullCheck(fields, nameof(fields));
            Fields = fields;
            Parse(s);
        }

        /// <summary>
        /// Parse a string and assign the values to fixed-length fields defined in <see cref="Fieilds"/> property.
        /// </summary>
        /// <param name="s">The string to be parsed.</param>
        public void Parse(string s)
        {
            rawString = s;
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