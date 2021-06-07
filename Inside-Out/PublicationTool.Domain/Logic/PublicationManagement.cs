using PublicationTool.Domain.Interfaces;
using PublicationTool.Domain.Objects;

namespace PublicationTool.Domain.Logic
{
    public class PublicationManagement
    {
        private readonly IPublicationRepository publicationRepository;
        private readonly PublicationValidator publicationValidator;

        public PublicationManagement(IPublicationRepository publicationRepository)
        {
            this.publicationRepository = publicationRepository;
            this.publicationValidator = new PublicationValidator();
        }
        
        public Result Save(Publication publication)
        {
            var result = this.publicationValidator.Validate(publication);

            if(result.WasSuccessful)
            {
                this.publicationRepository.Save(publication);
            }

            return result;
        }
    }
}