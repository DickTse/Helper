using Helper.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace HelperTest
{
    [TestCategory("Unit Test")]
    [TestClass]
    public class FixedLengthDateTimeFieldUnitTests
    {
        [TestMethod]
        public void FixedLengthDateTimeField_ParsingDateInStringWithLeadingSpaceShouldReturnTheSameDateTimeValue()
        {
            var field = new FixedLengthDateTimeField("DOB")
            {
                RawString = "19851013"
            };
            Assert.AreEqual(new DateTime(1985, 10, 13), field.Value);
        }

        [TestMethod]
        public void FixedLengthDateTimeField_ParsingTimeInStringWithLeadingSpaceShouldReturnTheSameDateTimeValue()
        {
            DateTime dt = new DateTime(1985, 10, 13, 23, 59, 59, 999);
            var field = new FixedLengthDateTimeField("StartTime", "yyyyMMddHHmmssfff")
            {
                RawString = dt.ToString("yyyyMMddHHmmssfff")
            };
            Assert.AreEqual(dt, field.Value);
        }

        [TestMethod]
        [ExpectedException(typeof(FormatException))]
        public void FixedLengthDateTimeField_ParsingDateTimeInStringWithInvalidDateTimeFormatShouldThrowFormatException()
        {
            var field = new FixedLengthDateTimeField("DOB", "abcdef")
            {
                RawString = "19851013"
            };
        }

        [TestMethod]
        public void FixedLengthDateTimeField_FieldWithValidDateValue_PaddedStringShouldPaddedWithTrailingSpace()
        {
            var field = new FixedLengthDateTimeField("DOB", "yyyyMMdd", 10)
            {
                Value = new DateTime(1985, 10, 13)
            };
            Assert.AreEqual("19851013  ", field.ToPaddedString());
        }

        [TestMethod]
        public void FixedLengthDateTimeField_DateValueLeftPaddedWithZeros_PaddedStringShouldPaddedWithLeadingZeros()
        {
            var field = new FixedLengthDateTimeField("DOB", "yyyyMMdd", 10)
            {
                Value = new DateTime(1985, 10, 13),
                PaddingChar = '0',
                PaddingCharPosition = PaddingCharPosition.Left
            };
            Assert.AreEqual("0019851013", field.ToPaddedString());
        }
    }
}
