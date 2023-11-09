using E_Commerce_WebApplication.FactoryPattern;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerce_WebApplication.ViewComponents
{
    public class ProductSearchViewComponent : ViewComponent
    {
        private readonly IRepositoryFactory _repositoryFactory; 

        public ProductSearchViewComponent(IRepositoryFactory repositoryFactory)
        {
            _repositoryFactory = repositoryFactory;
        }

        public async Task<IViewComponentResult> InvokeAsync(string searchQuery)
        {
            var products = await _repositoryFactory.CreateProductRepository().SearchProductsAsync(searchQuery); 
            return View(products);
        }
    }
}
