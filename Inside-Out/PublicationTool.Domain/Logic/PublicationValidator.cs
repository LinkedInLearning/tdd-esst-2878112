using PublicationTool.Domain.Interfaces;
using PublicationTool.Domain.Objects;

namespace PublicationTool.Domain.Logic
{
    public class PublicationValidator
    {
        public Result Validate(Publication publication)
        {
            var result = new Result();

            if (publication.Date == null)
            {
                result.Errors.Add("Publication date is wrong!");
            }

            if (publication.Title == null || publication.Title?.Length < 3 || publication.Title?.Length > 120)
            {
                result.Errors.Add("Title is wrong!");
            }

            return result;
        }
    }
}