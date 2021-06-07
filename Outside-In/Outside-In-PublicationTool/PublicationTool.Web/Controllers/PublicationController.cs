using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using PublicationTool.Domain;
using PublicationTool.Domain.Interfaces;
using PublicationTool.Domain.Objects;
using PublicationTool.Web.Validator;

namespace PublicationTool.Web.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class PublicationController : ControllerBase
    {
        private IPublicationRepository repositoryStub;
        private PublicationValidator publicationValidator;

        public PublicationController(IPublicationRepository repositoryStub)
        {
            this.repositoryStub = repositoryStub;
            publicationValidator = new PublicationValidator();
        }

        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new List<string>();
        }

        [HttpPost]
        public HttpResponseMessage Save(Publication publication)
        {
            var validate = publicationValidator.Validate(publication);

            if (validate.Success)
            {
                repositoryStub.Save(publication);
                return new HttpResponseMessage(HttpStatusCode.OK);
            }

            var httpResponseMessage = new HttpResponseMessage(HttpStatusCode.BadRequest);
            httpResponseMessage.Content = new StringContent(JsonSerializer.Serialize(validate));

            return httpResponseMessage;
        }
    }
}
