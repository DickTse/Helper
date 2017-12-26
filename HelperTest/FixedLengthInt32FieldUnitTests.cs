using Helper.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace HelperTest
{
    [TestCategory("Unit Test")]
    [TestClass]
    public class FixedLengthInt32FieldUnitTests
    {
        [TestMethod]
        public void FixedLengthInt32Field_InitiateWithNameAndLength_ShouldNotThrowAnyException()
        {
            try
            {
                var field = new FixedLengthInt32Field("Height", 3);
            }
            catch
            {
                Assert.Fail("Exception is caught while initiating FixedLengthInt32Field with name and length argument.");
            }
        }

        [TestMethod]
        public void FixedLengthInt32Field_InitiateWithNameAndLength_ShouldResultInSameValuesInNameAndLengthProperties()
        {
            var field = new FixedLengthInt32Field("Height", 3);
            Assert.AreEqual("Height", field.Name);
            Assert.AreEqual(3, field.Length);
        }

        [TestMethod]
        public void FixedLengthInt32Field_AssignValue_ShouldReturnIntegerValue()
        {
            decimal d = 9.0M;
            var field = new FixedLengthInt32Field("Height", 3)
            {
                Value = (int)d
            };
            if (field.Value.GetType() != typeof(int))
                Assert.Fail("Return value of integer-type FixedLengthField is not integer.");
        }

        [TestMethod]
        public void FixedLengthInt32Field_FieldWithNegativeSignInPaddedStringShouldNotThrowException()
        {
            try
            {
                var field = new FixedLengthInt32Field("Age", 3)
                {
                    RawString = "-9 "
                };
            }
            catch (OverflowException)
            {
                Assert.Fail("Assigning negative number into PaddedString property of integer-type FixedLengthField should not throw Overflow exception.");
            }
        }

        [TestMethod]
        public void FixedLengthInt32Field_FieldPaddedStringPaddedWithDefaultPaddingCharInDefaultPaddingCharPositionShouldPadTrailingSpace()
        {
            var field = new FixedLengthInt32Field("Age", 3)
            {
                Value = 9
            };
            Assert.AreEqual("9  ", field.ToPaddedString());
        }

        [TestMethod]
        public void FixedLengthInt32Field_FieldPaddedStringPaddedWithDefaultPaddingCharInPaddingCharPositionLeftShouldPadLeadingingSpace()
        {
            var field = new FixedLengthInt32Field("Age", 3)
            {
                PaddingCharPosition = PaddingCharPosition.Left
            };
            field.Value = 9;
            Assert.AreEqual("  9", field.ToPaddedString());
        }

        [TestMethod]
        public void FixedLengthInt32Field_FieldPaddedStringWithPaddingPaddingCharPositionRightShouldPadTrailingSpace()
        {
            var field = new FixedLengthInt32Field("Age", 3)
            {
                PaddingCharPosition = PaddingCharPosition.Right
            };
            field.Value = 9;
            Assert.AreEqual("9  ", field.ToPaddedString());
        }

        [TestMethod]
        public void FixedLengthInt32Field_IntegerWithTrailingSpaceShouldNotThrowException()
        {
            var field = new FixedLengthInt32Field("Age", 3);
            try
            {
                field.RawString = "9  ";
            }
            catch
            {
                Assert.Fail($"Parsing integer with trailing space to {nameof(FixedLengthInt32Field)} should not throw any exception.");
            }
        }

        [TestMethod]
        public void FixedLengthInt32Field_IntegerWithLeadingSpaceShouldNotThrowException()
        {
            var field = new FixedLengthInt32Field("Age", 3);
            try
            {
                field.RawString = "  9";
            }
            catch
            {
                Assert.Fail($"Parsing integer with leading space to {nameof(FixedLengthInt32Field)} should not throw any exception.");
            }
        }

        [TestMethod]
        public void FixedLengthInt32Field_ParsingIntegerInStringWithTrailingSpaceShouldReturnTheSameIntegerValue()
        {
            var field = new FixedLengthInt32Field("Age", 3)
            {
                RawString = "9  "
            };
            Assert.AreEqual(9, field.Value);
        }

        [TestMethod]
        public void FixedLengthInt32Field_ParsingIntegerInStringWithLeadingSpaceShouldReturnTheSameIntegerValue()
        {
            var field = new FixedLengthInt32Field("Age", 3)
            {
                RawString = "  9"
            };
            Assert.AreEqual(9, field.Value);
        }

        [TestMethod]
        [ExpectedException(typeof(FormatException))]
        public void FixedLengthInt32Field_ParsingIntegerWithNonNumericalCharacterShouldThrowFormatException()
        {
            var field = new FixedLengthInt32Field("Age", 3)
            {
                RawString = "99A"
            };
        }
    }
}
