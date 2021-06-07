using PublicationTool.Domain.Objects;

namespace PublicationTool.Domain.Interfaces
{
    public interface IPublicationRepository
    {
        bool Save(Publication publication);
    }
}