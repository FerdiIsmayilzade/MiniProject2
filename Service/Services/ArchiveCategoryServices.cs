using Domain.Entities;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Repository.Repositories;
using Repository.Repositories.Interfaces;
using Service.Services.Interfaces;

namespace Service.Services
{
    public class ArchiveCategoryServices : IArchiveCategoryServices
    {
        private readonly IArchiveCategoryRepository _archiveCategoryRepository;

        public ArchiveCategoryServices()
        {
            _archiveCategoryRepository=new ArchiveCategoryRepository();
        }
        public async Task<IEnumerable<ArchiveCategories>> GetAllAsync()
        {
           return await _archiveCategoryRepository.GetAllAsync();
        }
    }
}
