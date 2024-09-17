using Domain.Entities;

namespace Repository.Repositories.Interfaces
{
    public interface IArchiveCategoryRepository
    {
        Task<IEnumerable<ArchiveCategories>> GetAllAsync();
    }
}
