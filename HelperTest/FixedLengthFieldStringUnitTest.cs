using Helper.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace HelperTest
{
    [TestCategory("Unit Test")]
    [TestClass]
    public class FixedLengthFieldStringUnitTest
    {
        [TestMethod]
        public void FixedLengthFieldString_ParseStringsIntoMultipleFixedLengthFieldsUsingConstructor_ShouldBeParsedSuccessfully()
        {
            FixedLengthFieldCollection fields = new FixedLengthFieldCollection()
            {
                new FixedLengthStringField("HKID", 10),
                new FixedLengthStringField("Name", 20)
            };
            string rawStr = "A123456(7)CHAN TAI MAN        ";
            FixedLengthFieldString fixedStr = new FixedLengthFieldString(fields, rawStr);
            Assert.AreEqual("A123456(7)", fixedStr.Fields["HKID"]);
            Assert.AreEqual("CHAN TAI MAN", fixedStr.Fields["Name"]);
        }

        [TestMethod]
        public void FixedLengthFieldString_ParseStringIntoMultipleFixedLengthFieldsUsingParseMethod_ShouldBeParsedSuccessfully()
        {
            FixedLengthFieldCollection fields = new FixedLengthFieldCollection()
            {
                new FixedLengthStringField("HKID", 10),
                new FixedLengthStringField("Name", 20),
                new FixedLengthInt32Field("Age", 3),
                new FixedLengthDateTimeField("DOB")
            };
            FixedLengthFieldString fixedStr = new FixedLengthFieldString(fields);
            string rawStr = "A123456(7)CHAN TAI MAN        32 19850305";
            fixedStr.Parse(rawStr);
            Assert.AreEqual("A123456(7)", fixedStr.Fields["HKID"]);
            Assert.AreEqual("CHAN TAI MAN", fixedStr.Fields["Name"]);
            Assert.AreEqual(32, fixedStr.Fields["Age"]);
            Assert.AreEqual(new DateTime(1985, 3, 5), fixedStr.Fields["DOB"]);
        }

        [TestMethod]
        public void FixedLengthFieldString_AssignValueToEachField_ToStringMethodShouldReturnExpectedString()
        {
            FixedLengthFieldCollection fields = new FixedLengthFieldCollection()
            {
                new FixedLengthStringField("HKID", 10) {Value = "A123456(7)"},
                new FixedLengthStringField("Name", 20) {Value = "CHAN TAI MAN"},
                new FixedLengthInt32Field("Age", 3) {Value = 32},
                new FixedLengthDateTimeField("DOB") {Value = new DateTime(1985, 3, 5)}
            };
            FixedLengthFieldString fixedStr = new FixedLengthFieldString(fields);
            string expectedStr = "A123456(7)CHAN TAI MAN        32 19850305";
            Assert.AreEqual(expectedStr, fixedStr.ToString());
        }

        [TestMethod]
        [ExpectedException(typeof(MalformedRawStringException))]
        public void FixedLengthFieldString_RawStringTooShort_ShouldThrowMalformedRawStringException()
        {
            FixedLengthFieldCollection fields = new FixedLengthFieldCollection() {
                new FixedLengthStringField("HKID", 10),
                new FixedLengthStringField("Name", 20)
            };
            string rawStr = "A123456CHAN TAI MAN";
            try
            {
                new FixedLengthFieldString(fields, rawStr);
            }
            catch (MalformedRawStringException ex)
            {
                Assert.AreEqual("The raw string is too short.", ex.Message);
                throw;
            }
        }

        [TestMethod]
        [ExpectedException(typeof(MalformedRawStringException))]
        public void FixedLengthFieldString_RawStringTooLong_ShouldThrowMalformedRawStringException()
        {
            FixedLengthFieldCollection fields = new FixedLengthFieldCollection() {
                new FixedLengthStringField("HKID", 10),
                new FixedLengthStringField("Name", 10),
            };
            string rawStr = "A123456(7)CHAN TAI MAN";
            try
            {
                new FixedLengthFieldString(fields, rawStr);
            }
            catch (MalformedRawStringException ex)
            {
                Assert.AreEqual("The raw string is too long.", ex.Message);
                throw;
            }
        }
    }
}
