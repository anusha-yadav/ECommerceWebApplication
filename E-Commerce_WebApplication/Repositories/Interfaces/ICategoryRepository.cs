using E_Commerce_WebApplication.Models;

namespace E_Commerce_WebApplication.Repositories.Interfaces
{
    public interface ICategoryRepository
    {
        Task<List<Category>> GetCategoriesAsync();
        Task<Category?> GetCategoriesById(int? id);
        Task AddCategory(Category category);
    }
}
