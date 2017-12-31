using Helper;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace HelperTest
{
    [TestClass]
    [TestCategory("Unit Test")]
    public class ConversionHelperUnitTests
    {
        private const string RegularPositiveInt32String = "1";
        private const string HugePositiveNumberString = "9999999999";
        private const string TinyNegativeNumberString = "-9999999999";
        private const string AlphabeticalCharacterString = "foo";
        private const string NonNullString = "foo";
        private const string NullString = null;

        [TestMethod]
        public void ConversionHelper_TryParse_ParseRegularInt32StringIntoInt32_ShouldReturnTrueAndParsedInt32Value()
        {
            string s = RegularPositiveInt32String;
            int expectedInt = Int32.Parse(s);
            bool result = ConversionHelper.TryParse(s, out int i);
            Assert.AreEqual(true, result);
            Assert.AreEqual(expectedInt, i);
        }

        [TestMethod]
        public void ConversionHelper_TryParse_ParseHugePositiveNumberStringIntoInt32_ShouldReturnFalseAndDefaultInt32Value()
        {
            bool result = ConversionHelper.TryParse(HugePositiveNumberString, out int i);
            Assert.AreEqual(false, result);
            Assert.AreEqual(default(int), i);
        }

        [TestMethod]
        public void ConversionHelper_TryParse_ParseTinyNegativeNumberStringIntoInt32_ShouldReturnFalseAndDefaultInt32Value()
        {
            bool result = ConversionHelper.TryParse(TinyNegativeNumberString, out int i);
            Assert.AreEqual(false, result);
            Assert.AreEqual(default(int), i);
        }

        [TestMethod]
        public void ConversionHelper_TryParse_ParseNullStringToInt32_ShouldReturnFalseAndDefaultInt32Value()
        {
            bool result = ConversionHelper.TryParse(NullString, out int i);
            Assert.AreEqual(false, result);
            Assert.AreEqual(default(int), i);
        }

        [TestMethod]
        public void ConversionHelper_TryParse_ParseAlphabeticalCharacterToInteger_ShouldReturnFalseAndDefaultInt32Value()
        {
            bool result = ConversionHelper.TryParse(AlphabeticalCharacterString, out int i);
            Assert.AreEqual(false, result);
            Assert.AreEqual(default(int), i);
        }

        [TestMethod]
        public void ConversionHelper_TryParse_ParseNonNullStringToObjectCannotBeConvertedFromString_ShouldReturnFalseAndDefaultValue()
        {
            bool result = ConversionHelper.TryParse(NonNullString, out object o);
            Assert.AreEqual(false, result);
            Assert.AreEqual(default(object), o);
        }

        [TestMethod]
        public void ConversionHelper_CanParse_ParseRegularInt32StringIntoInt32_ShouldReturnTrue()
        {
            bool result = ConversionHelper.CanParse<int>(RegularPositiveInt32String);
            Assert.AreEqual(true, result);
        }

        [TestMethod]
        public void ConversionHelper_CanParse_ParseHugePositiveNumberStringIntoInt32_ShouldReturnFalse()
        {
            bool result = ConversionHelper.CanParse<int>(HugePositiveNumberString);
            Assert.AreEqual(false, result);
        }

        [TestMethod]
        public void ConversionHelper_CanParse_ParseTinyNegativeNumberStringIntoInt32_ShouldReturnFalse()
        {
            bool result = ConversionHelper.CanParse<int>(TinyNegativeNumberString);
            Assert.AreEqual(false, result);
        }

        [TestMethod]
        public void ConversionHelper_CanParse_ParseNullStringToInt32_ShouldReturnFalse()
        {
            bool result = ConversionHelper.CanParse<int>(NullString);
            Assert.AreEqual(false, result);
        }

        [TestMethod]
        public void ConversionHelper_CanParse_ParseAlphabeticalCharacterStringToInt32_ShouldReturnFalse()
        {
            bool result = ConversionHelper.CanParse<int>(AlphabeticalCharacterString);
            Assert.AreEqual(false, result);
        }

        [TestMethod]
        public void ConversionHelper_CanParse_ParseNonNullStringToObject_ShouldReturnFalse()
        {
            bool result = ConversionHelper.CanParse<object>(NonNullString);
            Assert.AreEqual(false, result);
        }
    }
}
