using E_Commerce_WebApplication.Data;
using E_Commerce_WebApplication.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using E_Commerce_WebApplication.Repositories.Interfaces;

namespace E_Commerce_WebApplication.Repositories.Providers
{
    public class ProductRepository : IProductRepository
    {
        private readonly ECommerceContext _context;

        public ProductRepository(ECommerceContext context)
        {
            _context = context;
        }

        public async Task<List<Products>> GetSubCategory()
        {
            return await _context.Products.Include(p => p.SubCategory).ToListAsync();
        }

        public Products GetProductById(int id)
        {
            return  _context.Products
                .Include(p => p.SubCategory)
                .FirstOrDefault(m => m.Id == id);
        }

        public async Task CreateProduct(Products product)
        {
            _context.Products.Add(product);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateProduct(Products products)
        {
            _context.Products.Update(products);
            await _context.SaveChangesAsync();
        }

        public bool ProductsExists(int id)
        {
            return (_context.Products?.Any(e => e.Id == id)).GetValueOrDefault();
        }

        public async Task RemoveProduct(Products products)
        {
            _context.Products.Remove(products);
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }

        public List<Products> SearchProduct(string searchItem)
        {
            return _context.Products.Where(product => product.ProductName.Contains(searchItem)).ToList();
        }

        public SelectList CreateSubCategoryView()
        {
           return new SelectList(_context.SubCategories, "Id", "Name");
        }

        public SelectList CreateSubCategoryByObj(Products product)
        {
            return new SelectList(_context.SubCategories, "Id", "Name", product.SubCategory.Name);
        }

        public async Task<List<Products>> SearchProductsAsync(string searchQuery)
        {
            string[] searchWords = searchQuery.Split(' ');
            var products = await _context.Products.
                Where(product=>searchWords.Any(word=>product.ProductName.Contains(word))).ToListAsync();
/*               .Where(p => p.ProductName.Contains(searchQuery)).ToListAsync();
*/            return products;
        }
    }
}
