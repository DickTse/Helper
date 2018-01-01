using Helper.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;

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
        public void FixedLengthFieldString_RawStringTooLongShouldThrowMalformedRawStringException()
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
        #endregion

        #region Unit tests for testing parsing of string into FixedLengthField.
        [TestMethod]
        public void FixedLengthFieldString_SingleStringIsParsedIntoFixedLengthFieldSuccessfully()
        {
            FixedLengthFieldCollection fields = new FixedLengthFieldCollection() {
                new FixedLengthStringField("HKID", 10)
            };
            string rawStr = "A123456(7)";
            FixedLengthFieldString fixedStr = new FixedLengthFieldString(fields, rawStr);
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
            FixedLengthFieldString fixedStr = new FixedLengthFieldString(fields, rawStr);
            Assert.AreEqual(100, fixedStr.Fields["Age"]);
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
            FixedLengthFieldString fixedStr = new FixedLengthFieldString(fields, rawStr);
            Assert.AreEqual("A123456(7)", fixedStr.Fields["HKID"]);
            Assert.AreEqual("CHAN TAI MAN", fixedStr.Fields["Name"]);
        }
    }
}
