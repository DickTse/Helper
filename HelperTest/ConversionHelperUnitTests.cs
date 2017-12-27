using Helper;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace HelperTest
{
    [TestClass]
    [TestCategory("Unit Test")]
    public class ConversionHelperUnitTests
    {
        [TestMethod]
        public void ConversionHelper_TryParse_ParseNormalIntegerStringIntoInteger_ShouldReturnTrueAndAnInteger()
        {
            string s = "1";
            int expectedInt = Int32.Parse(s);
            if (ConversionHelper.TryParse<int>(s, out int i))
                Assert.AreEqual(expectedInt, i, 
                    $"{nameof(ConversionHelper.TryParse)} returns an unexpected value in its out parameter even it returns true.");
            else
                Assert.Fail($"{nameof(ConversionHelper.TryParse)} returns false while parsing a normal integer.");
        }

        [TestMethod]
        public void ConversionHelper_TryParse_ParseHugeNumberInStringIntoInteger_ShouldReturnFalseAndDefaultIntegerValue()
        {
            string s = "9999999999";
            Int64 expectedInt = Int64.Parse(s);
            bool result = ConversionHelper.TryParse<int>(s, out int i);
            Assert.AreEqual(false, result, 
                $"{nameof(ConversionHelper.TryParse)} does not return false even it tries to parse a string with a huge number in it to int32.");
            Assert.AreEqual(default(int), i,
                $"A non-default integer value is returned even TryParse fails to parse a string into int32.");
        }

        [TestMethod]
        public void ConversionHelper_TryParse_ParseTinyNegativeNumberInStringIntoInteger_ShouldReturnFalseAndDefaultIntegerValue()
        {
            string s = "-9999999999";
            Int64 expectedInt = Int64.Parse(s);
            bool result = ConversionHelper.TryParse<int>(s, out int i);
            Assert.AreEqual(false, result,
                $"{nameof(ConversionHelper.TryParse)} does not return false even it tries to parse a string with a tiny negative number in it to int32.");
            Assert.AreEqual(default(int), i,
                $"A non-default integer value is returned even TryParse fails to parse a string into int32.");
        }

        [TestMethod]
        public void ConversionHelper_CanParse_ParseNormalIntegerStringIntoInteger_ShouldReturnTrue()
        {
            string s = "1";
            if (!(ConversionHelper.CanParse<int>(s)))
                Assert.Fail($"{nameof(ConversionHelper.CanParse)} returns false while parsing a normal integer.");
        }

        [TestMethod]
        public void ConversionHelper_CanParse_ParseHugeNumberInStringIntoInteger_ShouldReturnFalse()
        {
            string s = "9999999999";
            if (ConversionHelper.CanParse<int>(s))
                Assert.Fail($"{nameof(ConversionHelper.CanParse)} does not return false while parsing a huge number into an integer.");
        }

        [TestMethod]
        public void ConversionHelper_CanParse_ParseTinyNegativeNumberInStringIntoInteger_ShouldReturnFalse()
        {
            string s = "-9999999999";
            if (ConversionHelper.CanParse<int>(s))
                Assert.Fail($"{nameof(ConversionHelper.CanParse)} does not return false while parsing a tiny negative number into an integer.");
        }
    }
}
