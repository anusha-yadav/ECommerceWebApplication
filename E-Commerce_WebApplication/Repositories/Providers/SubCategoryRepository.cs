using E_Commerce_WebApplication.Data;
using E_Commerce_WebApplication.Models;
using E_Commerce_WebApplication.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace E_Commerce_WebApplication.Repositories.Providers
{
    public class SubCategoryRepository : ISubCategoryRepository
    {
        private readonly ECommerceContext _context;

        public SubCategoryRepository(ECommerceContext context)
        {
            _context = context;
        }

        public async Task<List<SubCategory>> GetSubCategories()
        {
            return await _context.SubCategories.
                Include(s => s.Category).ToListAsync();
        }

        public async Task<SubCategory> GetSubCategoryById(int id)
        {
            return await _context.SubCategories
                .Include(s => s.Category)
                .FirstOrDefaultAsync(m => m.Id == id);
        }

        public SelectList GetCategoryView()
        {
            return new SelectList(_context.Categories, "Id", "Name");
        }

        public async Task AddSubCategory(SubCategory subCategory)
        {
            _context.Add(subCategory);
            await _context.SaveChangesAsync();
        }

        public SelectList GetCategoryByObj(SubCategory subCategory)
        {
            return new SelectList(_context.Categories, "Id", "Name",subCategory.Name);
        }
    }
}
