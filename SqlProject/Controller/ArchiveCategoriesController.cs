using Service.Services;
using SqlProject.Helpers.Extentions;

namespace SqlProject.Controller
{
    public class ArchiveCategoriesController
    {
        private readonly ArchiveCategoryServices _archiveCategoryServices;

        public ArchiveCategoriesController()
        {
            _archiveCategoryServices = new ArchiveCategoryServices();
        }

        public async Task GetAllAsync()
        {
            var result=await _archiveCategoryServices.GetAllAsync();
            foreach (var item in result)
            {
                ConsoleColor.Blue.WriteConsole($"{item.Id} {item.Operation} {item.CreatedDate} {item.Name}");
            }
        }
    }
}
