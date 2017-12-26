using Helper.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace HelperTest
{
    [TestCategory("Unit Test")]
    [TestClass]
    public class FixedLengthDecimalFieldUnitTests
    {
        [TestMethod]
        public void FixedLengthDecimalField_ShouldReturnDecimalValue()
        {
            int i = 9;
            var field = new FixedLengthDecimalField("Height", 3);
            field.Value = i;
            if (field.Value.GetType() != typeof(decimal))
                Assert.Fail("Return value of decimal-type FixedLengthField is not decimal.");
        }

        [TestMethod]
        public void FixedLengthDecimalField_ValueInStringShouldBeParsedIntoFixedLengthFieldSuccessfully()
        {
            var field = new FixedLengthDecimalField("Height", 3);
            field.RawString = "9.1";
            Assert.AreEqual(9.1M, field.Value);
        }
    }
}
