using E_Commerce_WebApplication.Data;
using E_Commerce_WebApplication.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace E_Commerce_WebApplication.Controllers
{
    public class HomeController : Controller
    {
        private readonly ECommerceContext _context;

        public HomeController(ECommerceContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Home index Page
        /// </summary>
        /// <returns></returns>
        public IActionResult Index()
        {
            var electronicsProduct = _context.Products
                .Include(p => p.SubCategory)
                .Where(c => c.SubCategory.CategoryId == 1).Take(4).ToList();

            var appliancesProducts = _context.Products
                .Include(p => p.SubCategory).Where(c=>c.SubCategory.CategoryId==2).Take(4).ToList();

            var viewModel = new HomePageViewModel
            {
                ElectronicsProducts = electronicsProduct,
                AppliancesProducts = appliancesProducts
            };
            return View(viewModel);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        /// <summary>
        /// Fetching Electronics products 
        /// </summary>
        /// <returns></returns>
        public IActionResult AllElectronicsProducts()
        {
            var allElectronicsProducts = _context.Products
                .Include(p=>p.SubCategory)
                .Where(c=>c.SubCategory.CategoryId==1).ToList();
            return View(allElectronicsProducts);
        }

        /// <summary>
        /// Fetching Home Appliances Products
        /// </summary>
        /// <returns></returns>
        public IActionResult AllAppliancesProducts()
        {
            var allAppliancesProducts = _context.Products
                .Include(p => p.SubCategory)
                .Where(c => c.SubCategory.CategoryId == 2).ToList();
            return View(allAppliancesProducts);
        }

        /// <summary>
        /// Unauthorized Page
        /// </summary>
        /// <returns></returns>
        public IActionResult UnAuthorized()
        {
            return View("~/Views/Home/Error.cshtml");
        }
    }
}