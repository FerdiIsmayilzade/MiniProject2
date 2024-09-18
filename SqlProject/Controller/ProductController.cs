using Service.Services;

namespace SqlProject.Controller
{
    public class ProductController
    {
        private readonly ProductServices _productServices;

        public ProductController()
        {
            _productServices = new ProductServices();
        }

    }
}
