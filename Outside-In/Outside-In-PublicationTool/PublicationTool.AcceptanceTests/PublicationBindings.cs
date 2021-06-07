using System;
using System.Globalization;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Gherkin.Ast;
using NUnit.Framework;
using PublicationTool.Domain.Interfaces;
using PublicationTool.Domain.Objects;
using TechTalk.SpecFlow;

namespace PublicationTool.AcceptanceTests
{
    [Binding]
    public class PublicationBindings
    {
        private ScenarioContext scenarioContext;

        public PublicationBindings(ScenarioContext scenarioContext)
        {
            this.scenarioContext = scenarioContext;
        }

        [When(@"the publication is stored")]
        public async Task WhenThePublicationIsStored()
        {
            var publication = scenarioContext.Get<Publication>();

            var serializedPublication = JsonSerializer.Serialize(publication);
            var httpClient = new HttpClient { BaseAddress = new Uri("http://localhost:5000/") };
            var publishedContent = new StringContent(serializedPublication, Encoding.UTF8, "application/json");

            var resultMessage = await httpClient.PostAsync("Publication", publishedContent);
            var resultContent = await resultMessage.Content.ReadAsStringAsync();

            var result = JsonSerializer.Deserialize<Result>(resultContent);

            scenarioContext.Set(resultMessage);
            scenarioContext.Set(result);
        }

        [Given(@"is a publication with title ""(.*)"" published on ""(.*)""")]
        public void GivenIsAPublicationWithTitlePublishedOn(string title, string date)
        {
            var publication = CreatePublication(title, date);
            scenarioContext.Set(publication);
        }

        [Then(@"a publication ""(.*)"" published on ""(.*)"" can be found in the data base")]
        public void ThenAPublicationPublishedOnCanBeFoundInTheDataBase(string p0, string p1)
        {
            
        }

        [Then(@"no errors are reported")]
        public void ThenNoErrorsAreReported()
        {
            var message = this.scenarioContext.Get<HttpResponseMessage>();

            Assert.AreEqual(HttpStatusCode.OK, message.StatusCode);
        }


        private static Publication CreatePublication(string title, string date)
        {
            var publication = new Publication();
            publication.Title = title;
            publication.Date = DateTime.ParseExact(date, "dd.MM.yyyy", new DateTimeFormatInfo());
            return publication;
        }
    }
}
