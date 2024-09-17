using Domain.Entities;

namespace Service.Services.Interfaces
{
    public interface IArchiveCategoryServices
    {
        Task<IEnumerable<ArchiveCategories>> GetAllAsync();
    }
}
