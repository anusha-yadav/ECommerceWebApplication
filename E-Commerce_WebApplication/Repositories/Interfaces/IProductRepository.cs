using E_Commerce_WebApplication.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace E_Commerce_WebApplication.Repositories.Interfaces
{
    public interface IProductRepository
    {
        Task<List<Products>> GetSubCategory();
        Products GetProductById(int id);
        Task CreateProduct(Products product);
        Task UpdateProduct(Products products);
        bool ProductsExists(int id);
        Task SaveChangesAsync();
        Task RemoveProduct(Products products);
        List<Products> SearchProduct(string searchItem);
        SelectList CreateSubCategoryView();
        SelectList CreateSubCategoryByObj(Products product);
        Task<List<Products>> SearchProductsAsync(string searchQuery);
    }
}
