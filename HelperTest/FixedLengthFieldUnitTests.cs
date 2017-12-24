using Helper.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace HelperTest
{
    [TestCategory("Unit Test")]
    [TestClass]
    public class FixedLengthFieldUnitTests
    {
        [TestMethod]
        public void FixedLengthField_DecimalFieldShouldReturnDecimalValue()
        {
            int i = 9;
            var field = new FixedLengthField<decimal>("Height", 3);
            field.Value = i;
            if (field.Value.GetType() != typeof(decimal))
                Assert.Fail("Return value of decimal-type FixedLengthField is not decimal.");
        }

        [TestMethod]
        public void FixedLengthField_DecimalValueInStringShouldBeParsedIntoFixedLengthFieldSuccessfully()
        {
            var field = new FixedLengthField<decimal>("Height", 3);
            field.PaddedString = "9.1";
            Assert.AreEqual(9.1M, field.Value);
        }

        [TestMethod]
        public void FixedLengthField_IntegerFieldShouldReturnIntegerValue()
        {
            decimal d = 9.0M;
            var field = new FixedLengthField<int>("Height", 3);
            field.Value = (int)d;
            if (field.Value.GetType() != typeof(int))
                Assert.Fail("Return value of integer-type FixedLengthField is not integer.");
        }

        [TestMethod]
        public void FixedLengthField_IntegerFieldWithNegativeSignInPaddedStringShouldNotThrowException()
        {
            try
            {
                var field = new FixedLengthField<int>("Age", 3);
                field.PaddedString = "-9 ";
            }
            catch (OverflowException)
            {
                Assert.Fail("Assigning negative number into PaddedString property of integer-type FixedLengthField should not throw Overflow exception.");
            }
        }

        [TestMethod]
        public void FixedLengthField_IntegerFieldPaddedStringWithDefaultPaddingPaddingCharPositionShouldPadTrailingSpace()
        {
            var field = new FixedLengthField<int>("Age", 3);
            field.Value = 9;
            Assert.AreEqual("9  ", field.PaddedString);
        }

        public void FixedLengthField_IntegerFieldPaddedStringWithPaddingPaddingCharPositionLeftShouldPadLeadingingSpace()
        {
            var field = new FixedLengthField<int>("Age", 3)
            {
                PaddingCharPosition = PaddingCharPosition.Left
            };
            field.Value = 9;
            Assert.AreEqual("  9", field.PaddedString);
        }

        public void FixedLengthField_IntegerFieldPaddedStringWithPaddingPaddingCharPositionRightShouldPadTrailingSpace()
        {
            var field = new FixedLengthField<int>("Age", 3)
            {
                PaddingCharPosition = PaddingCharPosition.Right
            };
            field.Value = 9;
            Assert.AreEqual("9  ", field.PaddedString);
        }
    }
}
