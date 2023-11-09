using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using E_Commerce_WebApplication.Data;
using E_Commerce_WebApplication.Models;
using E_Commerce_WebApplication.Filters;
using System.Web.Razor;
using E_Commerce_WebApplication.Repositories;
using E_Commerce_WebApplication.Repositories.Interfaces;

namespace E_Commerce_WebApplication.Controllers
{
    [Authorize("Admin")]
    public class CategoriesController : Controller
    {
        private readonly ICategoryRepository _categoryRepository;

        /// <summary>
        /// Intializing new instances
        /// </summary>
        /// <param name="categoryRepository"></param>
        public CategoriesController(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        /// <summary>
        /// GET : Categories
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> Index()
        {
            var categories = await _categoryRepository.GetCategoriesAsync();

            if (categories != null)
            {
                return View(categories);
            }
            else
            {
                return Problem("Entity set 'ECommerceContext.Categories' is null.");
            }
        }

        /// <summary>
        /// GET: Categories/Details/5
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var category = await _categoryRepository.GetCategoriesById(id);
            if (category == null)
            {
                return NotFound();
            }

            return View(category);
        }

        /// <summary>
        /// GET: Categories/Create
        /// </summary>
        /// <returns></returns>
        public IActionResult Create()
        {
            return View();
        }

        /// <summary>
        /// POST: Categories/Create
        /// </summary>
        /// <param name="category"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Create(Category category)
        {
            if (category!=null)
            {
                await _categoryRepository.AddCategory(category);
                return PartialView("_Success");
            }
            return View(category);
        }
    }
}
