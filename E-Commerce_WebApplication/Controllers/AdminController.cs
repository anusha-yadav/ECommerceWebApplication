using E_Commerce_WebApplication.FactoryPattern;
using E_Commerce_WebApplication.Filters;
using E_Commerce_WebApplication.Models;
using E_Commerce_WebApplication.Repositories;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerce_WebApplication.Controllers
{
    [Authorize("Admin")]
    public class AdminController : Controller
    {
        private readonly IRepositoryFactory _repositoryFactory;

        /// <summary>
        /// Intializing a new instances 
        /// </summary>
        /// <param name="repositoryFactory"></param>
        public AdminController(IRepositoryFactory repositoryFactory) 
        {
            _repositoryFactory = repositoryFactory;
        }

        /// <summary>
        /// Dashboard of Admin Panel
        /// </summary>
        /// <returns></returns>
        public IActionResult Dashboard()
        {
            return View();
        }

        /// <summary>
        /// Get Users info method
        /// </summary>
        /// <returns></returns>
        public IActionResult GetAllUserInfo()
        {
            List<Users> users = _repositoryFactory.CreateUserRepository().Users();
            return View(users);
        }
    }
}
