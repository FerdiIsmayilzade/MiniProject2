using Service.Services;
using SqlProject.Helpers.Extentions;

namespace SqlProject.Controller
{
    public class ProductController
    {
        private readonly ProductServices _productServices;

        public ProductController()
        {
            _productServices = new ProductServices();
        }

        public async Task GetAll()
        {
            var result=await _productServices.GetAllAsync();
            foreach (var item in result)
            {
                ConsoleColor.Blue.WriteConsole($@"
{item.Id}-
ProductName:{item.Name}
ProductPrice:{item.Price}
ProductDescription:{item.Description}
ProductColor:{item.Color}
ProductCount:{item.Count}
CreatedDate:{item.CreatedDate}");
            }
        }


    }
}
