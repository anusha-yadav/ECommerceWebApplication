using E_Commerce_WebApplication.Data;
using E_Commerce_WebApplication.Models;
using E_Commerce_WebApplication.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace E_Commerce_WebApplication.Repositories.Providers
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly ECommerceContext _context;
        public CategoryRepository(ECommerceContext context) 
        { 
            _context = context;
        }

        public async Task<List<Category>> GetCategoriesAsync()
        {
            return await _context.Categories.ToListAsync();
        }

        public async Task<Category?> GetCategoriesById(int? id)
        {
            return await _context.Categories
                .FirstOrDefaultAsync(m => m.Id == id);
        }

        public async Task AddCategory(Category category)
        {
            _context.Add(category);
            await _context.SaveChangesAsync();
        }
    }
}
