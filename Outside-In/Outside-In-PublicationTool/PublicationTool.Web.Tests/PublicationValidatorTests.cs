using System;
using System.Linq;
using NUnit.Framework;
using PublicationTool.Domain.Objects;
using PublicationTool.Web.Validator;

namespace PublicationTool.Web.Tests
{
    public class PublicationValidatorTests
    {
        [Test]
        public void Title_is_reported_as_invalid_if_it_was_not_set()
        {
            var sut = new PublicationValidator();
            var result = sut.Validate(new Publication(null, DateTime.Now));

            Assert.IsFalse(result.Success);
            Assert.IsTrue(result.Errors.Any(x => x.Contains("Title")), "Title was not set but was not reported");
        }

        [Test]
        public void Title_is_reported_as_invalid_if_shorter_than_3_characters()
        {
            var sut = new PublicationValidator();
            var result = sut.Validate(new Publication("a", DateTime.Now));

            Assert.IsFalse(result.Success);
            Assert.IsTrue(result.Errors.Any(x => x.Contains("Title")), "Title was to short but was not reported");
        }

        [Test]
        public void Title_is_reported_as_invalid_if_longer_than_120_characters()
        {
            var sut = new PublicationValidator();
            string title = GetStringWithLength(121);

            var result = sut.Validate(new Publication(title, DateTime.Now));

            Assert.IsFalse(result.Success);
            Assert.IsTrue(result.Errors.Any(x => x.Contains("Title")), "Title was to long but was not reported");
        }

        [Test]
        public void Date_is_reported_as_invalid_if_it_was_not_set()
        {
            var sut = new PublicationValidator();
            var result = sut.Validate(new Publication("Title", null));

            Assert.IsFalse(result.Success);
            Assert.IsTrue(result.Errors.Any(x => x.Contains("Date")), "Date was not set.");
        }

        private string GetStringWithLength(int length)
        {
            string result = "";
            for (int i = 0; i < length; i++)
            {
                result += "a";
            }

            return result;
        }
    }
}