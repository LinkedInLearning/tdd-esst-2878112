using System;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;
using NUnit.Framework;
using PublicationTool.Domain;
using PublicationTool.Domain.Interfaces;
using PublicationTool.Domain.Objects;
using PublicationTool.Web.Controllers;


namespace PublicationTool.Web.Tests
{
    public class PublicationControllerTests
    {
        private PublicationController sut;
        private PublicationRepositoryStub publicationRepositoryMock;

        [SetUp]
        public void Init()
        {
            publicationRepositoryMock = new PublicationRepositoryStub();
            sut = new PublicationController(publicationRepositoryMock);
        }

        [Test]
        public void A_publication_without_any_data_should_not_be_saved()
        {
            var publication = new Publication();

            var result = sut.Save(publication);

            Assert.AreEqual(HttpStatusCode.BadRequest, result.StatusCode, "Result reports success but should not be successful.");
            Assert.IsFalse(publicationRepositoryMock.SaveWasCalled, "Publication was saved in repository even it was invalid.");
        }

        [Test]
        [TestCase("Title", null, TestName = "A publication containing only a valid title should not be saved.")]
        [TestCase(null, "22.10.2020", TestName = "A publication containing only a valid publication date should not be saved.")]
        public void A_publication_with_invalid_data_cannot_be_saved(string title, string publicationDate)
        {
            DateTime? date = null;
            if (publicationDate != null) date = DateTime.Parse(publicationDate);

            var publication = new Publication { Title = title, Date = date };

            var result = sut.Save(publication);

            Assert.AreEqual(HttpStatusCode.BadRequest, result.StatusCode);
            Assert.IsFalse(publicationRepositoryMock.SaveWasCalled, "Publication was saved in repository even it was invalid.");
        }

        [Test]
        [TestCase("title", TestName = "Missing title was not mentioned in error text.")]
        [TestCase("date", TestName = "Missing publication date was not mentioned in error text.")]
        public async Task An_error_text_is_provided_for_each_invalid_property(string propertyName)
        {
            var result = sut.Save(new Publication());

            var resultContent = await result.Content.ReadAsStringAsync();

            Assert.IsTrue(resultContent.ToLower().Contains(propertyName));
            Assert.IsFalse(publicationRepositoryMock.SaveWasCalled, "Publication was saved in repository even it was invalid.");
        }

        [Test]
        public void Publications_with_valid_data_should_be_saved()
        {
            var publication = new Publication("Date", DateTime.Now);

            var result = sut.Save(publication);

            Assert.AreEqual(HttpStatusCode.OK, result.StatusCode, "Save was not successful even all properties have valid data.");
            Assert.IsTrue(publicationRepositoryMock.SaveWasCalled, "Save was not successful even all properties have valid data.");
        }
    }

    public class PublicationRepositoryStub : IPublicationRepository
    {
        public Publication Publication { get; private set; }
        public bool SaveWasCalled { get; set; }

        public void Save(Publication publication)
        {
            SaveWasCalled = true;
            this.Publication = publication;
        }
    }
}