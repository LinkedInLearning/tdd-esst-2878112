using System;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using NUnit.Framework;
using PublicationTool.Domain;
using PublicationTool.Domain.Objects;

namespace PublicationTool.AcceptanceTests
{
    public class PublicationTests
    {
        private HttpClient httpClient;
        private static string requestUri = "Publication";

        [SetUp]
        public void Init()
        {
            httpClient = new HttpClient {BaseAddress = new Uri("http://localhost:56985/")};
        }

        [Test]
        public async Task Publication_service_can_be_reached()
        {
            var result = await httpClient.GetAsync(requestUri);

            Assert.AreEqual(HttpStatusCode.OK, result.StatusCode);
        }

        [Test]
        public async Task Valid_publication_can_be_saved_without_any_errors()
        {
            var publication = new Publication {Date = DateTime.Now, Title = "Title"};
            var serializedPublication = JsonSerializer.Serialize(publication);

            var result = await httpClient.PostAsync(requestUri, new StringContent(serializedPublication, Encoding.UTF8, "application/json"));

            Assert.AreEqual(HttpStatusCode.OK, result.StatusCode);
        }

        [Test]
        public async Task Invalid_properties_are_reported_separatly()
        {
            var publication = new Publication();
            var serializedPublication = JsonSerializer.Serialize(publication);

            var result = await httpClient.PostAsync(requestUri, new StringContent(serializedPublication, Encoding.UTF8, "application/json"));
            var content = await result.Content.ReadAsStringAsync();

            Assert.AreEqual(HttpStatusCode.BadRequest, result.StatusCode);
            Assert.IsTrue(content.ToLower().Contains("title"));
            Assert.IsTrue(content.ToLower().Contains("date"));
        }
    }
}