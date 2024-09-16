using Domain.Common;

namespace Domain.Entities
{
    public class Product:BaseEntity
    {
        public string Name { get; set; }
        public int Count { get; set; }
        public float Price { get; set; }
        public string Description { get; set; }
        public string Color { get; set; }

        public  int CategoryId { get; set; }

        public Category Category { get; set; }

    }
}
