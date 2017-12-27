using Helper;
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
        private const string Message = "bar";
        private const string NonEmptyString = "foo";

        [TestMethod]
        public void ParameterGuard_NullCheck_CheckNonNullValueWithGivenMessage_ShouldNotThrowException()
        {
            object nonNullObject = new object();
            ParameterGuard.NullCheck(nonNullObject, ParameterName, Message);
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
                ParameterGuard.NullCheck<object>(null, ParameterName, Message);
            }
            catch (ArgumentNullException ex) when (ex.ParamName == ParameterName && ex.Message == $"{Message}\r\nParameter name: {ParameterName}")
            {
                // ParamName and Message are both expected. Just rethrow the ArgumentNullException and leave it to
                // ExpectedException attribute.
                throw;
            }
            catch (ArgumentNullException ex) when (ex.ParamName != ParameterName)
            {
                // ArgumentNullException is caught, but the ParamName is not expected. So, assert an failure before 
                // rethrowing the exception.
                Assert.Fail(new StringBuilder(300)
                    .Append($"{nameof(ArgumentNullException)}, which is expected, is thrown. However, the value of ")
                    .Append($"{nameof(ArgumentNullException.ParamName)} property is incorrect.\r\nExpected ")
                    .Append($"{nameof(ArgumentNullException.ParamName)}:<{ParameterName}>. Actual:<{ex.ParamName}>.")
                    .ToString());
            }
            catch (ArgumentNullException ex) when (ex.Message != $"{Message}\r\nParameter name: {ParameterName}")
            {
                // ArgumentNullException is caught, but the Message is not expected. So, assert an failure before 
                // rethrowing the exception.
                Assert.Fail(new StringBuilder(300)
                    .Append($"{nameof(ArgumentNullException)}, which is expected, is thrown. However, the value of ")
                    .Append($"{nameof(ArgumentNullException.Message)} property is incorrect.\r\nExpected ")
                    .Append($"{nameof(ArgumentNullException.Message)}:<{Message}>. Actual:<{ex.Message}>.")
                    .ToString());
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
            catch (ArgumentNullException ex) when (ex.ParamName == ParameterName)
            {
                // ParamName is expected. Just rethrow the ArgumentNullException and leave it to ExpectedException 
                // attribute.
                throw;
            }
            catch (ArgumentNullException ex) when (ex.ParamName != ParameterName)
            {
                // ArgumentNullException is caught, but the ParamName is not expected. So, assert an failure before 
                // rethrowing the exception.
                Assert.Fail(new StringBuilder(300)
                    .Append($"{nameof(ArgumentNullException)}, which is expected, is thrown. However, the value of ")
                    .Append($"{nameof(ArgumentNullException.ParamName)} property is incorrect.\r\nExpected ")
                    .Append($"{nameof(ArgumentNullException.ParamName)}:<{ParameterName}>. Actual:<{ex.ParamName}>.")
                    .ToString());
            }
        }

        [TestMethod]
        public void ParameterGuard_DefaultValueCheck_CheckNonDefaultValueWithGivenMessage_ShouldNotThrowException()
        {
            int i = 100;
            ParameterGuard.DefaultValueCheck(i, ParameterName, Message);
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
                ParameterGuard.DefaultValueCheck<int>(default(int), ParameterName, Message);
            }
            catch (ArgumentException ex) when (ex.ParamName == ParameterName && ex.Message == $"{Message}\r\nParameter name: {ParameterName}")
            {
                // ParamName and Message are both expected. Just rethrow the ArgumentNullException and leave it to
                // ExpectedException attribute.
                throw;
            }
            catch (ArgumentException ex) when (ex.ParamName != ParameterName)
            {
                // ArgumentException is caught, but the ParamName is not expected. So, assert an failure before 
                // rethrowing the exception.
                Assert.Fail(new StringBuilder(300)
                    .Append($"{nameof(ArgumentException)}, which is expected, is thrown. However, the value of ")
                    .Append($"{nameof(ArgumentException.ParamName)} property is incorrect.\r\nExpected ")
                    .Append($"{nameof(ArgumentException.ParamName)}:<{ParameterName}>. Actual:<{ex.ParamName}>.")
                    .ToString());
            }
            catch (ArgumentException ex) when (ex.Message != $"{Message}\r\nParameter name: {ParameterName}")
            {
                // ArgumentException is caught, but the Message is not expected. So, assert an failure before 
                // rethrowing the exception.
                Assert.Fail(new StringBuilder(300)
                    .Append($"{nameof(ArgumentException)}, which is expected, is thrown. However, the value of ")
                    .Append($"{nameof(ArgumentException.Message)} property is incorrect.\r\nExpected ")
                    .Append($"{nameof(ArgumentException.Message)}:<{Message}>. Actual:<{ex.Message}>.")
                    .ToString());
            }
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void ParameterGuard_DefaultValueCheck_CheckDefaultValueWithoutGivenMessage_ShouldThrowArgumentExceptionWithGivenParamNameAndMessage()
        {
            try
            {
                ParameterGuard.DefaultValueCheck<int>(default(int), ParameterName);
            }
            catch (ArgumentException ex) when (ex.ParamName == ParameterName)
            {
                // ParamName is expected. Just rethrow the ArgumentNullException and leave it to ExpectedException
                // attribute.
                throw;
            }
            catch (ArgumentException ex) when (ex.ParamName != ParameterName)
            {
                // ArgumentException is caught, but the ParamName is not expected. So, assert an failure before 
                // rethrowing the exception.
                Assert.Fail(new StringBuilder(300)
                    .Append($"{nameof(ArgumentException)}, which is expected, is thrown. However, the value of ")
                    .Append($"{nameof(ArgumentException.ParamName)} property is incorrect.\r\nExpected ")
                    .Append($"{nameof(ArgumentException.ParamName)}:<{ParameterName}>. Actual:<{ex.ParamName}>.")
                    .ToString());
            }
        }

        [TestMethod]
        public void ParameterGuard_EmptyStringCheck_CheckNonEmptyAndNonNullValueWithGivenMessage_ShouldNotThrowException()
        {
            ParameterGuard.EmptyStringCheck(NonEmptyString, ParameterName, Message);
        }

        [TestMethod]
        public void ParameterGuard_EmptyStringCheck_CheckNonEmptyAndNonNullValueWithoutGivenMessage_ShouldNotThrowException()
        {
            ParameterGuard.EmptyStringCheck(NonEmptyString, ParameterName);
        }

        [TestMethod]
        public void ParameterGuard_EmptyStringCheck_CheckNullValueWithGivenMessage_ShouldNotThrowException()
        {
            ParameterGuard.EmptyStringCheck(null, ParameterName, Message);
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
                ParameterGuard.EmptyStringCheck(String.Empty, ParameterName, Message);
            }
            catch (ArgumentException ex) when (ex.ParamName == ParameterName && ex.Message == $"{Message}\r\nParameter name: {ParameterName}")
            {
                // ParamName and Message are both expected. Just rethrow the ArgumentNullException and leave it to  
                // ExpectedException attribute.
                throw;
            }
            catch (ArgumentException ex) when (ex.ParamName != ParameterName)
            {
                // ArgumentException is caught, but the ParamName is not expected. So, assert an failure before 
                // rethrowing the exception.
                Assert.Fail(new StringBuilder(300)
                    .Append($"{nameof(ArgumentException)}, which is expected, is thrown. However, the value of ")
                    .Append($"{nameof(ArgumentException.ParamName)} property is incorrect.\r\nExpected ")
                    .Append($"{nameof(ArgumentException.ParamName)}:<{ParameterName}>. Actual:<{ex.ParamName}>.")
                    .ToString());
                throw;
            }
            catch (ArgumentException ex) when (ex.Message != $"{Message}\r\nParameter name: {ParameterName}")
            {
                // ArgumentException is caught, but the Message is not expected. So, assert an failure before 
                // rethrowing the exception.
                Assert.Fail(new StringBuilder(300)
                    .Append($"{nameof(ArgumentException)}, which is expected, is thrown. However, the value of ")
                    .Append($"{nameof(ArgumentException.Message)} property is incorrect.\r\nExpected ")
                    .Append($"{nameof(ArgumentException.Message)}:<{Message}>. Actual:<{ex.Message}>.")
                    .ToString());
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
            catch (ArgumentException ex) when (ex.ParamName == ParameterName && ex.Message == $"{Message}\r\nParameter name: {ParameterName}")
            {
                // ParamName and Message are both expected. Just rethrow the ArgumentNullException and leave it to
                // ExpectedException attribute.
                throw;
            }
            catch (ArgumentException ex) when (ex.ParamName != ParameterName)
            {
                // ArgumentException is caught, but the ParamName is not expected. So, assert an failure before 
                // rethrowing the exception.
                Assert.Fail(new StringBuilder(300)
                    .Append($"{nameof(ArgumentException)}, which is expected, is thrown. However, the value of ")
                    .Append($"{nameof(ArgumentException.ParamName)} property is incorrect.\r\nExpected ")
                    .Append($"{nameof(ArgumentException.ParamName)}:<{ParameterName}>. Actual:<{ex.ParamName}>.")
                    .ToString());
                throw;
            }
        }

        [TestMethod]
        public void ParameterGuard_NullOrEmptyStringCheck_CheckNonEmptyAndNonNullValueWithGivenMessage_ShouldNotThrowException()
        {
            ParameterGuard.NullOrEmptyStringCheck(NonEmptyString, ParameterName, Message);
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
                ParameterGuard.NullOrEmptyStringCheck(null, ParameterName, Message);
            }
            catch (ArgumentNullException ex) when (ex.ParamName == ParameterName && ex.Message == $"{Message}\r\nParameter name: {ParameterName}")
            {
                // ParamName and Message are both expected. Just rethrow the ArgumentNullException and leave it to
                // ExpectedException attribute.
                throw;
            }
            catch (ArgumentNullException ex) when (ex.ParamName != ParameterName)
            {
                // ArgumentNullException is caught, but the ParamName is not expected. So, assert an failure before 
                // rethrowing the exception.
                Assert.Fail(new StringBuilder(300)
                    .Append($"{nameof(ArgumentNullException)}, which is expected, is thrown. However, the value of ")
                    .Append($"{nameof(ArgumentNullException.ParamName)} property is incorrect.\r\nExpected ")
                    .Append($"{nameof(ArgumentNullException.ParamName)}:<{ParameterName}>. Actual:<{ex.ParamName}>.")
                    .ToString());
                throw;
            }
            catch (ArgumentNullException ex) when (ex.Message != $"{Message}\r\nParameter name: {ParameterName}")
            {
                // ArgumentNullException is caught, but the Message is not expected. So, assert an failure before 
                // rethrowing the exception.
                Assert.Fail(new StringBuilder(300)
                    .Append($"{nameof(ArgumentNullException)}, which is expected, is thrown. However, the value of ")
                    .Append($"{nameof(ArgumentNullException.Message)} property is incorrect.\r\nExpected ")
                    .Append($"{nameof(ArgumentNullException.Message)}:<{Message}>. Actual:<{ex.Message}>.")
                    .ToString());
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
            catch (ArgumentNullException ex) when (ex.ParamName == ParameterName)
            {
                // ParamName is expected. Just rethrow the ArgumentNullException and leave it to ExpectedException
                // attribute.
                throw;
            }
            catch (ArgumentNullException ex) when (ex.ParamName != ParameterName)
            {
                // ArgumentNullException is caught, but the ParamName is not expected. So, assert an failure before 
                // rethrowing the exception.
                Assert.Fail(new StringBuilder(300)
                    .Append($"{nameof(ArgumentNullException)}, which is expected, is thrown. However, the value of ")
                    .Append($"{nameof(ArgumentNullException.ParamName)} property is incorrect.\r\nExpected ")
                    .Append($"{nameof(ArgumentNullException.ParamName)}:<{ParameterName}>. Actual:<{ex.ParamName}>.")
                    .ToString());
                throw;
            }
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void ParameterGuard_NullOrEmptyStringCheck_CheckEmptyStringWithGivenMessage_ShouldThrowArgumentException()
        {
            try
            {
                ParameterGuard.NullOrEmptyStringCheck(String.Empty, ParameterName, Message);
            }
            catch (ArgumentException ex) when (ex.ParamName == ParameterName && ex.Message == $"{Message}\r\nParameter name: {ParameterName}")
            {
                // ParamName and Message are both expected. Just rethrow the ArgumentNullException and leave it to
                // ExpectedException attribute.
                throw;
            }
            catch (ArgumentException ex) when (ex.ParamName != ParameterName)
            {
                // ArgumentException is caught, but the ParamName is not expected. So, assert an failure before 
                // rethrowing the exception.
                Assert.Fail(new StringBuilder(300)
                    .Append($"{nameof(ArgumentException)}, which is expected, is thrown. However, the value of ")
                    .Append($"{nameof(ArgumentException.ParamName)} property is incorrect.\r\nExpected ")
                    .Append($"{nameof(ArgumentException.ParamName)}:<{ParameterName}>. Actual:<{ex.ParamName}>.")
                    .ToString());
                throw;
            }
            catch (ArgumentException ex) when (ex.Message != $"{Message}\r\nParameter name: {ParameterName}")
            {
                // ArgumentException is caught, but the Message is not expected. So, assert an failure before 
                // rethrowing the exception.
                Assert.Fail(new StringBuilder(300)
                    .Append($"{nameof(ArgumentException)}, which is expected, is thrown. However, the value of ")
                    .Append($"{nameof(ArgumentException.Message)} property is incorrect.\r\nExpected ")
                    .Append($"{nameof(ArgumentException.Message)}:<{Message}>. Actual:<{ex.Message}>.")
                    .ToString());
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
            catch (ArgumentException ex) when (ex.ParamName == ParameterName)
            {
                // ParamName is expected. Just rethrow the ArgumentNullException and leave it to ExpectedException 
                // attribute.
                throw;
            }
            catch (ArgumentException ex) when (ex.ParamName != ParameterName)
            {
                // ArgumentException is caught, but the ParamName is not expected. So, assert an failure before 
                // rethrowing the exception.
                Assert.Fail(new StringBuilder(300)
                    .Append($"{nameof(ArgumentException)}, which is expected, is thrown. However, the value of ")
                    .Append($"{nameof(ArgumentException.ParamName)} property is incorrect.\r\nExpected ")
                    .Append($"{nameof(ArgumentException.ParamName)}:<{ParameterName}>. Actual:<{ex.ParamName}>.")
                    .ToString());
                throw;
            }
        }
    }
}
