using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using E_Commerce_WebApplication.Data;
using E_Commerce_WebApplication.Models;
using E_Commerce_WebApplication.Filters;
using E_Commerce_WebApplication.Repositories.Interfaces;
using Microsoft.AspNetCore.Authorization;
using AuthorizeAttribute = E_Commerce_WebApplication.Filters.AuthorizeAttribute;

namespace E_Commerce_WebApplication.Controllers
{
    [Authorize("Admin")]
    public class SubCategoriesController : Controller
    {
        private readonly ISubCategoryRepository _subCategoryRepository;

        /// <summary>
        /// Intializing new instances
        /// </summary>
        /// <param name="subCategoryRepository"></param>
        public SubCategoriesController(ISubCategoryRepository subCategoryRepository)
        {
            _subCategoryRepository = subCategoryRepository;
        }

        /// <summary>
        ///  GET: SubCategories
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> Index()
        {
            var eCommerceContext = _subCategoryRepository.GetSubCategories();
            return View(await eCommerceContext);
        }

        /// <summary>
        /// GET: SubCategories/Details/5
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var subCategory = await _subCategoryRepository.GetSubCategoryById(id.Value);
            if (subCategory == null)
            {
                return NotFound();
            }

            return View(subCategory);
        }

        /// <summary>
        /// GET: SubCategories/Create
        /// </summary>
        /// <returns></returns>
        public IActionResult Create()
        {
            ViewData["CategoryId"] = _subCategoryRepository.GetCategoryView();
            return View();
        }

        /// <summary>
        /// POST: SubCategories/Create
        /// </summary>
        /// <param name="subCategory"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Create(SubCategory subCategory)
        {
            if (subCategory!=null)
            {
                _subCategoryRepository.GetCategoryView();
                _subCategoryRepository.AddSubCategory(subCategory);
                return PartialView("_Success");
            }
            ViewData["CategoryId"] = _subCategoryRepository.GetCategoryByObj(subCategory);
            return View(subCategory);
        }
    }
}
