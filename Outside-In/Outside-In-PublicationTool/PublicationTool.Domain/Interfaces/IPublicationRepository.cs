using PublicationTool.Domain.Objects;

namespace PublicationTool.Domain.Interfaces
{
    public interface IPublicationRepository
    {
        void Save(Publication publication);
    }
}