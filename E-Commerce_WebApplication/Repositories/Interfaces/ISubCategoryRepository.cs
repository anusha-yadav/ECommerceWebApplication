using E_Commerce_WebApplication.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace E_Commerce_WebApplication.Repositories.Interfaces
{
    public interface ISubCategoryRepository
    {
        Task<List<SubCategory>> GetSubCategories();
        Task<SubCategory> GetSubCategoryById(int id);
        SelectList GetCategoryView();
        Task AddSubCategory(SubCategory subCategory);
        SelectList GetCategoryByObj(SubCategory subCategory);
    }
}
