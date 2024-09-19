using Domain.Common;

namespace Domain.Entities
{
    public  class ArchiveCategories:BaseEntity
    {
        public string Operation { get; set; }

        public string Name { get; set; }
        public int CategoryId { get; set; }

       
    }   
}
