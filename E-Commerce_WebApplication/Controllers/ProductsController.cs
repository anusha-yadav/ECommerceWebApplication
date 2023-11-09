using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using E_Commerce_WebApplication.Data;
using E_Commerce_WebApplication.Models;
using System.Diagnostics;
using System.Reflection.Metadata.Ecma335;
using E_Commerce_WebApplication.Repositories;
using E_Commerce_WebApplication.FactoryPattern;
using System.Data.Entity;
using E_Commerce_WebApplication.Filters;
using E_Commerce_WebApplication.Builder;
using E_Commerce_WebApplication.Repositories.Interfaces;

namespace E_Commerce_WebApplication.Controllers
{
    [TypeFilter(typeof(ExceptionFilter))]
    public class ProductsController : Controller
    {
        private readonly IWebHostEnvironment _environment;
        private readonly IRepositoryFactory _repositoryFactory;

        /// <summary>
        /// Intializing new instances
        /// </summary>
        /// <param name="environment"></param>
        /// <param name="repositoryFactory"></param>
        public ProductsController(IWebHostEnvironment environment, IRepositoryFactory repositoryFactory)
        {
            _environment = environment;
            _repositoryFactory = repositoryFactory;
        }

        /// <summary>
        /// GET: Products
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> Index()
        {
            var eCommerceContext = _repositoryFactory.CreateProductRepository().GetSubCategory();
            return View(await eCommerceContext);
        }

        /// <summary>
        /// GET: Products/Details/5
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var products = _repositoryFactory.CreateProductRepository().GetProductById(id.Value);

            if (products == null)
            {
                return NotFound();
            }

            return View(products);
        }

        /// <summary>
        ///  GET: Products/Create
        /// </summary>
        /// <returns></returns>
        public IActionResult Create()
        {
            SelectList list = _repositoryFactory.CreateProductRepository().CreateSubCategoryView();

            if (list != null)
            {
                foreach (var item in list)
                {
                    Debug.WriteLine("item value :" + item.Value);
                }
                ViewData["SubCategoryId"] = list;
            }
            else
            {
                ViewData["SubCategoryId"] = new SelectList(new List<SelectListItem>());
            }
            return View();
        }

        /// <summary>
        /// POST: Products/Create
        /// </summary>
        /// <param name="ImageFile"></param>
        /// <param name="products"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Create(IFormFile ImageFile, Products products)
        {
            IProductRepository productObj = _repositoryFactory.CreateProductRepository();
            if (ImageFile != null)
            {
                Debug.WriteLine("Hi");
                var uploadDirectory = Path.Combine(Directory.GetCurrentDirectory(), "wwwRoot/uploads");
                if (uploadDirectory == null)
                {
                    Directory.CreateDirectory(uploadDirectory);
                }

                string uniqueFileName = Guid.NewGuid().ToString() + "_" + ImageFile.FileName;
                string filepath = Path.Combine(uploadDirectory, uniqueFileName);

                using (var fileStream = new FileStream(filepath, FileMode.Create))
                {
                    await ImageFile.CopyToAsync(fileStream);
                }

                products.ImageUrl = "/uploads/" + uniqueFileName;

                products = new ProductsBuilder()
                    .WithName(products.ProductName)
                    .WithDescription(products.Description)
                    .WithPrice(products.Price)
                    .WithImageUrl("/uploads/" + uniqueFileName)
                    .WithSubCategoryId(products.SubCategoryId)
                    .Build();
                
                await productObj.CreateProduct(products);
                return PartialView("_Success");
            }
            else
            {
                products.ImageUrl = null;
            }
            //productObj = _repositoryFactory.CreateProductRepository();
            Debug.WriteLine("Product Object " + productObj);
            ViewData["SubCategoryId"] = productObj.CreateSubCategoryByObj(products);
            return View(products);
        }

        /// <summary>
        /// GET: Products/Edit/5
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var products = _repositoryFactory.CreateProductRepository().GetProductById(id.Value);
            if (products == null)
            {
                return NotFound();
            }
            ViewData["SubCategoryId"] = _repositoryFactory.CreateProductRepository().CreateSubCategoryView();
            return View(products);
        }


        /// <summary>
        /// POST: Products/Edit/5
        /// </summary>
        /// <param name="id"></param>
        /// <param name="products"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Edit(int id, [Bind("Id,ProductName,Price,SubCategoryId,ImageUrl,Description")] Products products)
        {
            if (id != products.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _repositoryFactory.CreateProductRepository().UpdateProduct(products);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_repositoryFactory.CreateProductRepository().ProductsExists(products.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["SubCategoryId"] = _repositoryFactory.CreateProductRepository().CreateSubCategoryView();
            return View(products);
        }

        /// <summary>
        /// Product details page
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public IActionResult ProductDetail(int id)
        {
            var product = _repositoryFactory.CreateProductRepository().GetProductById(id);
            if (product == null)
            {
                return NotFound(); // Handle product not found
            }
            return View(product);
        }

        /// <summary>
        /// Search products method
        /// </summary>
        /// <param name="searchQuery"></param>
        /// <returns></returns>
        public IActionResult Search(string searchQuery)
        {
            ViewData["SearchQuery"] = searchQuery;
            return View();
        }

        /*
                public IActionResult GiveRating(int productId)
                {
                    int? userid = HttpContext.Session.GetInt32("userid");
                    var product = _context.Products.Find(productId);

                    if (product == null)
                    {
                        return NotFound();
                    }

                    // Check if the user is authenticated
                    if (!userid.HasValue)
                    {
                        return RedirectToAction("Login", "Account"); // Redirect to login if not authenticated
                    }

                    // Check if the user has already rated this product
                    var existingRating = _context.Ratings.SingleOrDefault(r => r.ProductId == productId && r.UserId == userid.Value);

                    if (existingRating != null)
                    {
                        TempData["ErrorMessage"] = "You have already rated this product.";
                        return RedirectToAction("Details", "Product", new { id = productId });
                    }

                    var model = new RatingViewModel
                    {
                        ProductId = productId,
                        ProductName = product.ProductName,
                    };

                    return View(model);
                }
        */

    }
}
