using System;
using System.Linq;
using NUnit.Framework;
using PublicationTool.Domain.Interfaces;
using PublicationTool.Domain.Logic;
using PublicationTool.Domain.Objects;

namespace PublicationTool.Domain.Tests
{
    public class PublicationManagementTests
    {
        private PublicationManagement sut;
        private PublicationRepositoryMock publicationRepositoryMock;

        [SetUp]
        public void Init()
        {
            publicationRepositoryMock = new PublicationRepositoryMock();
            sut = new PublicationManagement(publicationRepositoryMock);
        }

        [Test]
        public void Empty_publication_cannot_saved()
        {
            var result = sut.Save(new Publication());

            Assert.False(result.WasSuccessful);
            Assert.False(publicationRepositoryMock.SaveWasCalled);
        }

        [Test]
        public void Publication_can_be_saved_if_it_contains_Title_and_Publication_Date()
        {
            var publication = new Publication();
            publication.Title = "Title";
            publication.Date = DateTime.Now;

            var result = sut.Save(publication);

            Assert.True(result.WasSuccessful);
            Assert.True(publicationRepositoryMock.SaveWasCalled);
        }

        [Test]
        public void Error_text_is_provided_if_title_is_too_short()
        {
            var publication = new Publication();
            publication.Title = "T";
            publication.Date = DateTime.Now;

            Result result = sut.Save(publication);

            Assert.True(result.Errors.Any(x => x.ToLower().Contains("title")));
        }

        [Test]
        public void Error_text_is_provided_if_title_is_too_long()
        {
            var publication = new Publication();

            publication.Title = "x";
            for (int i = 0; i < 130; i++)
            {
                publication.Title += "x";
            }

            publication.Date = DateTime.Now;

            Result result = sut.Save(publication);

            Assert.True(result.Errors.Any(x => x.ToLower().Contains("title")));
        }

        [Test]
        public void Error_text_is_provided_if_date_is_not_set()
        {
            var publication = new Publication();
            publication.Title = "Title";
            publication.Date = null;

            Result result = sut.Save(publication);

            Assert.True(result.Errors.Any(x => x.ToLower().Contains("date")));
        }

        [Test]
        public void Error_text_is_provided_for_all_invalid_properties_of_publication()
        {
            var publication = new Publication();
            publication.Title = "T";
            publication.Date = null;

            Result result = sut.Save(publication);

            Assert.True(result.Errors.Any(x => x.ToLower().Contains("date")), "No error text was provided for date.");
            Assert.True(result.Errors.Any(x => x.Contains("Title")), "no error text was provided for title.");
        }
    }

    public class PublicationRepositoryMock : IPublicationRepository
    {
        public bool SaveWasCalled { get; set; }

        public bool Save(Publication publication)
        {
            SaveWasCalled = true;
            return true;
        }
    }
}