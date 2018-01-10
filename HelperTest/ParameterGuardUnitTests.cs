using Helper.AOP;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Text;

namespace HelperTest
{
    [TestClass]
    [TestCategory("Unit Test")]
    public class ParameterGuardUnitTests
    {
        private const string ParameterName = "foo";
        private const string CustomExceptionMessage = "bar";
        private const string NonEmptyString = "foo";

        #region Methods called by unit tests
        private string GetExpectedExceptionMessage<TException>(TException ex, string customExceptionMessage) where TException : ArgumentException
        {
            return customExceptionMessage + "\r\nParameter name: " + ex.ParamName;
        }

        private void ValidateExceptionParameterName<TException>(TException ex, string expectedParamName) where TException : ArgumentException
        {
            Assert.AreEqual(expectedParamName, ex.ParamName, new StringBuilder(300)
                .Append($"{typeof(TException)}, which is expected, is thrown. However, the value of ")
                .Append($"{nameof(ex.ParamName)} property is incorrect.\r\nExpected ")
                .Append($"{nameof(ex.ParamName)}:<{expectedParamName}>. Actual:<{ex.ParamName}>.")
                .ToString());
        }

        private void ValidateExceptionMessage<TException>(TException ex, string expectedMessage) where TException : ArgumentException
        {
            Assert.AreEqual(expectedMessage, ex.Message, new StringBuilder(300)
                .Append($"{typeof(TException)}, which is expected, is thrown. However, the value of ")
                .Append($"{nameof(ex.Message)} property is incorrect.\r\nExpected ")
                .Append($"{nameof(ex.Message)}:<{expectedMessage}>. Actual:<{ex.Message}>.")
                .ToString());
        }
        #endregion

        [TestMethod]
        public void ParameterGuard_NullCheck_CheckNonNullValueWithGivenMessage_ShouldNotThrowException()
        {
            object nonNullObject = new object();
            ParameterGuard.NullCheck(nonNullObject, ParameterName, CustomExceptionMessage);
        }

        [TestMethod]
        public void ParameterGuard_NullCheck_CheckNonNullValueWithoutGivenMessage_ShouldNotThrowException()
        {
            object nonNullObject = new object();
            ParameterGuard.NullCheck(nonNullObject, ParameterName);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ParameterGuard_NullCheck_CheckNullValueWithGivenMessage_ShouldThrowArgumentNullExceptionWithGivenParamNameAndMessage()
        {
            try
            {
                ParameterGuard.NullCheck<object>(null, ParameterName, CustomExceptionMessage);
            }
            catch (ArgumentNullException ex)
            {
                ValidateExceptionParameterName(ex, ParameterName);
                ValidateExceptionMessage(ex, GetExpectedExceptionMessage(ex, CustomExceptionMessage));

                // If values of both ex.Message are ex.ParamName are expected, rethrow the ArgumentNullException to
                // make the test pass.
                throw;
            }
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ParameterGuard_NullCheck_CheckNullValueWithoutGivenMessage_ShouldThrowArgumentNullExceptionWithGivenParamNameAndMessage()
        {
            try
            {
                ParameterGuard.NullCheck<object>(null, ParameterName);
            }
            catch (ArgumentNullException ex)
            {
                ValidateExceptionParameterName(ex, ParameterName);

                // If value of ex.ParamName is expected, rethrow the ArgumentNullException to
                // make the test pass.
                throw;
            }
        }

        [TestMethod]
        public void ParameterGuard_DefaultValueCheck_CheckNonDefaultValueWithGivenMessage_ShouldNotThrowException()
        {
            int i = 100;
            ParameterGuard.DefaultValueCheck(i, ParameterName, CustomExceptionMessage);
        }

        [TestMethod]
        public void ParameterGuard_DefaultValueCheck_CheckNonDefaultValueWithoutGivenMessage_ShouldNotThrowException()
        {
            int i = 100;
            ParameterGuard.DefaultValueCheck(i, ParameterName);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void ParameterGuard_DefaultValueCheck_CheckDefaultValueWithGivenMessage_ShouldThrowArgumentExceptionWithGivenParamNameAndMessage()
        {
            try
            {
                ParameterGuard.DefaultValueCheck(default(int), ParameterName, CustomExceptionMessage);
            }
            catch (ArgumentException ex)
            {
                ValidateExceptionParameterName(ex, ParameterName);
                ValidateExceptionMessage(ex, GetExpectedExceptionMessage(ex, CustomExceptionMessage));

                // If values of both ex.Message are ex.ParamName are expected, rethrow the ArgumentException to
                // make the test pass.
                throw;
            }
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void ParameterGuard_DefaultValueCheck_CheckDefaultValueWithoutGivenMessage_ShouldThrowArgumentExceptionWithGivenParamNameAndMessage()
        {
            try
            {
                ParameterGuard.DefaultValueCheck(default(int), ParameterName);
            }
            catch (ArgumentException ex)
            {
                ValidateExceptionParameterName(ex, ParameterName);

                // If value of ex.ParamName is expected, rethrow the ArgumentException to
                // make the test pass.
                throw;
            }
        }

        [TestMethod]
        public void ParameterGuard_EmptyStringCheck_CheckNonEmptyAndNonNullValueWithGivenMessage_ShouldNotThrowException()
        {
            ParameterGuard.EmptyStringCheck(NonEmptyString, ParameterName, CustomExceptionMessage);
        }

        [TestMethod]
        public void ParameterGuard_EmptyStringCheck_CheckNonEmptyAndNonNullValueWithoutGivenMessage_ShouldNotThrowException()
        {
            ParameterGuard.EmptyStringCheck(NonEmptyString, ParameterName);
        }

        [TestMethod]
        public void ParameterGuard_EmptyStringCheck_CheckNullValueWithGivenMessage_ShouldNotThrowException()
        {
            ParameterGuard.EmptyStringCheck(null, ParameterName, CustomExceptionMessage);
        }

        [TestMethod]
        public void ParameterGuard_EmptyStringCheck_CheckNullValueWithoutGivenMessage_ShouldNotThrowException()
        {
            ParameterGuard.EmptyStringCheck(null, ParameterName);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void ParameterGuard_EmptyStringCheck_CheckEmptyStringWithGivenMessage_ShouldThrowArgumentExceptionWithGivenParamNameAndMessage()
        {
            try
            {
                ParameterGuard.EmptyStringCheck(String.Empty, ParameterName, CustomExceptionMessage);
            }
            catch (ArgumentException ex)
            {
                ValidateExceptionParameterName(ex, ParameterName);
                ValidateExceptionMessage(ex, GetExpectedExceptionMessage(ex, CustomExceptionMessage));

                // If values of both ex.Message are ex.ParamName are expected, rethrow the ArgumentException to
                // make the test pass.
                throw;
            }
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void ParameterGuard_EmptyStringCheck_CheckEmptyStringWithoutGivenMessage_ShouldThrowArgumentExceptionWithGivenParamNameAndMessage()
        {
            try
            {
                ParameterGuard.EmptyStringCheck(String.Empty, ParameterName);
            }
            catch (ArgumentException ex)
            {
                ValidateExceptionParameterName(ex, ParameterName);

                // If value of ex.ParamName is expected, rethrow the ArgumentException to
                // make the test pass.
                throw;
            }
        }

        [TestMethod]
        public void ParameterGuard_NullOrEmptyStringCheck_CheckNonEmptyAndNonNullValueWithGivenMessage_ShouldNotThrowException()
        {
            ParameterGuard.NullOrEmptyStringCheck(NonEmptyString, ParameterName, CustomExceptionMessage);
        }

        [TestMethod]
        public void ParameterGuard_NullOrEmptyStringCheck_CheckNonEmptyAndNonNullValueWithoutGivenMessage_ShouldNotThrowException()
        {
            ParameterGuard.NullOrEmptyStringCheck(NonEmptyString, ParameterName);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ParameterGuard_NullOrEmptyStringCheck_CheckNullValueWithGivenMessage_ShouldThrowArgumentNullException()
        {
            try
            {
                ParameterGuard.NullOrEmptyStringCheck(null, ParameterName, CustomExceptionMessage);
            }
            catch (ArgumentNullException ex) 
            {
                ValidateExceptionParameterName(ex, ParameterName);
                ValidateExceptionMessage(ex, GetExpectedExceptionMessage(ex, CustomExceptionMessage));

                // If values of both ex.Message are ex.ParamName are expected, rethrow the ArgumentNullException to
                // make the test pass.
                throw;
            }
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ParameterGuard_NullOrEmptyStringCheck_CheckNullValueWithoutGivenMessage_ShouldThrowArgumentNullException()
        {
            try
            {
                ParameterGuard.NullOrEmptyStringCheck(null, ParameterName);
            }
            catch (ArgumentNullException ex)
            {
                ValidateExceptionParameterName(ex, ParameterName);

                // If value of both ex.ParamName is expected, rethrow the ArgumentNullException to
                // make the test pass.
                throw;
            }
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void ParameterGuard_NullOrEmptyStringCheck_CheckEmptyStringWithGivenMessage_ShouldThrowArgumentException()
        {
            try
            {
                ParameterGuard.NullOrEmptyStringCheck(String.Empty, ParameterName, CustomExceptionMessage);
            }
            catch (ArgumentException ex) 
            {
                ValidateExceptionParameterName(ex, ParameterName);
                ValidateExceptionMessage(ex, GetExpectedExceptionMessage(ex, CustomExceptionMessage));

                // If values of both ex.Message are ex.ParamName are expected, rethrow the ArgumentException to
                // make the test pass.
                throw;
            }
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void ParameterGuard_NullOrEmptyStringCheck_CheckEmptyStringWithoutGivenMessage_ShouldThrowArgumentException()
        {
            try
            {
                ParameterGuard.NullOrEmptyStringCheck(String.Empty, ParameterName);
            }
            catch (ArgumentException ex)
            {
                ValidateExceptionParameterName(ex, ParameterName);

                // If value of ex.ParamName is expected, rethrow the ArgumentException to
                // make the test pass.
                throw;
            }
        }
    }
}
