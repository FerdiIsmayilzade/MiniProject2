using Domain.Common;

namespace Domain.Entities
{
    public class Category:BaseEntity
    {
        public string Name { get; set; }

        public ICollection<Product> Products { get; set; }

        public ICollection<ArchiveCategories> Categories { get; set; }

    }
}
