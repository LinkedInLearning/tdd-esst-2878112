using NUnit.Framework;
using PublicationTool.Domain;
using PublicationTool.Web.Validator;

namespace PublicationTool.Web.Tests
{
    public class StringValidationRuleTests
    {
        [TestCase(null, 1, 0, TestName = "null")]
        [TestCase("ABC", 1, 0, TestName = "too long")]
        [TestCase("A", 10, 2, TestName = "too short")]
        [TestCase(1, 10, 2, TestName = "wrong type")]
        public void wrong_values_result_in_errors(object value, int maxLength, int minLength)
        {
            var sut = new StringValidationRule();
            sut.ErrorIfNull = true;
            sut.PropertyName = "Test";
            sut.MaxLength = maxLength;
            sut.MinLength = minLength;

            var result = sut.Validate(value, out var errorText);

            Assert.IsFalse(result, "Validation error for null should be reported.");
            Assert.IsTrue(errorText.Contains(sut.PropertyName), "Error text was: " + errorText);
        }
    }
}