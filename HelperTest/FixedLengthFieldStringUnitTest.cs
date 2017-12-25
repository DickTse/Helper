using Helper.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace HelperTest
{
    [TestCategory("Unit Test")]
    [TestClass]
    public class FixedLengthFieldStringUnitTest
    {
        #region Unit tests for testing validations of the whole raw string
        [TestMethod]
        [ExpectedException(typeof(MalformedRawStringException))]
        public void FixedLengthFieldString_RawStringTooShortShouldThrowMalformedRawStringException()
        {
            FixedLengthFieldCollection fields = new FixedLengthFieldCollection() {
                new FixedLengthStringField("HKID", 10),
                new FixedLengthStringField("Name", 20)
            };
            string rawStr = "A123456CHAN TAI MAN";
            try
            {
                new FixedLengthFieldString(rawStr, fields);
            }
            catch (MalformedRawStringException ex)
            {
                Assert.AreEqual("The raw string is too short.", ex.Message);
                throw;
            }
        }

        [TestMethod]
        [ExpectedException(typeof(MalformedRawStringException))]
        public void FixedLengthFieldString_RawStringTooLongShouldThrowMalformedRawStringException()
        {
            FixedLengthFieldCollection fields = new FixedLengthFieldCollection() {
                new FixedLengthStringField("HKID", 10),
                new FixedLengthStringField("Name", 10),
            };
            string rawStr = "A123456(7)CHAN TAI MAN";
            try
            {
                new FixedLengthFieldString(rawStr, fields);
            }
            catch (MalformedRawStringException ex)
            {
                Assert.AreEqual("The raw string is too long.", ex.Message);
                throw;
            }
        }
        #endregion

        #region Unit tests for testing parsing of string into FixedLengthField.
        [TestMethod]
        public void FixedLengthFieldString_SingleStringIsParsedIntoFixedLengthFieldSuccessfully()
        {
            FixedLengthFieldCollection fields = new FixedLengthFieldCollection() {
                new FixedLengthStringField("HKID", 10)
            };
            string rawStr = "A123456(7)";
            FixedLengthFieldString fixedStr = new FixedLengthFieldString(rawStr, fields);
            Assert.AreEqual("A123456(7)", fixedStr.Fields["HKID"]);
        }
        #endregion

        #region Unit tests for testing parsing of integer into FixedLengthField.
        [TestMethod]
        public void FixedLengthFieldString_SingleIntegerIsParsedIntoFixedLengthFieldSuccessfully()
        {
            FixedLengthFieldCollection fields = new FixedLengthFieldCollection() {
                new FixedLengthInt32Field("Age", 3)
            };
            string rawStr = "100";
            FixedLengthFieldString fixedStr = new FixedLengthFieldString(rawStr, fields);
            Assert.AreEqual(100, fixedStr.Fields["Age"]);
        }

        [TestMethod]
        public void FixedLengthFieldString_IntegerWithTrailingSpaceShouldNotThrowException()
        {
            FixedLengthFieldCollection fields = new FixedLengthFieldCollection() {
                new FixedLengthInt32Field("Age", 3)
            };
            string rawStr = "9  ";
            try
            {
                new FixedLengthFieldString(rawStr, fields);
            }
            catch
            {
                Assert.Fail("Parsing integer with trailing space to FixedLengthFieldString should not throw any exception.");
            }
        }

        [TestMethod]
        public void FixedLengthFieldString_IntegerWithLeadingSpaceShouldNotThrowException()
        {
            FixedLengthFieldCollection fields = new FixedLengthFieldCollection() {
                new FixedLengthInt32Field("Age", 3)
            };
            string rawStr = "  9";
            try
            {
                new FixedLengthFieldString(rawStr, fields);
            }
            catch
            {
                Assert.Fail("Parsing integer with leading space to FixedLengthFieldString should not throw any exception.");
            }
        }

        [TestMethod]
        public void FixedLengthFieldString_IntegerWithTrailingSpaceIsParsedIntoFixedLengthFieldSuccessfully()
        {
            FixedLengthFieldCollection fields = new FixedLengthFieldCollection() {
                new FixedLengthInt32Field("Age", 3)
            };
            string rawStr = "9  ";
            FixedLengthFieldString fixedStr = new FixedLengthFieldString(rawStr, fields);
            Assert.AreEqual(9, fixedStr.Fields["Age"]);
        }

        [TestMethod]
        public void FixedLengthFieldString_IntegerWithLeadingSpaceIsParsedIntoFixedLengthFieldSuccessfully()
        {
            FixedLengthFieldCollection fields = new FixedLengthFieldCollection() {
                new FixedLengthInt32Field("Age", 3)
            };
            string rawStr = "  9";
            FixedLengthFieldString fixedStr = new FixedLengthFieldString(rawStr, fields);
            Assert.AreEqual(9, fixedStr.Fields["Age"]);
        }

        [TestMethod]
        [ExpectedException(typeof(FormatException))]
        public void FixedLengthFieldString_IntegerWithNonNumericalCharacterShouldThrowException()
        {
            FixedLengthFieldCollection fields = new FixedLengthFieldCollection() {
                new FixedLengthInt32Field("Age", 3)
            };
            string rawStr = "99A";
            FixedLengthFieldString fixedStr = new FixedLengthFieldString(rawStr, fields);
        }
        #endregion

        #region Unit tests for testing parsing of DateTime value into FixedLengthField.
        [TestMethod]
        public void FixedLengthFieldString_DateTimeValueIsParsedIntoFixedLengthFieldSuccessfully()
        {
            FixedLengthFieldCollection fields = new FixedLengthFieldCollection()
            {
                new FixedLengthDateTimeField("DOB")
            };
            string rawStr = "19851013";
            FixedLengthFieldString fixedStr = new FixedLengthFieldString(rawStr, fields);
            Assert.AreEqual(new DateTime(1985, 10, 13), fixedStr.Fields["DOB"]);
        }

        [TestMethod]
        [ExpectedException(typeof(FormatException))]
        public void FixedLengthFieldString_InvalidDateTimeFormatStringShouldThrowException()
        {
            FixedLengthFieldCollection fields = new FixedLengthFieldCollection()
            {
                new FixedLengthDateTimeField("DOB") {Format = "abcdef"}
            };
            string rawStr = "19851013";
            new FixedLengthFieldString(rawStr, fields);
        }
        #endregion

        [TestMethod]
        public void FixedLengthFieldString_MultipleStringsAreAllParsedIntoFixedLengthFieldsSuccessfully()
        {
            FixedLengthFieldCollection fields = new FixedLengthFieldCollection()
            {
                new FixedLengthStringField("HKID", 10),
                new FixedLengthStringField("Name", 20)
            };
            string rawStr = "A123456(7)CHAN TAI MAN        ";
            FixedLengthFieldString fixedStr = new FixedLengthFieldString(rawStr, fields);
            Assert.AreEqual("A123456(7)", fixedStr.Fields["HKID"]);
            Assert.AreEqual("CHAN TAI MAN", fixedStr.Fields["Name"]);
        }
    }
}
