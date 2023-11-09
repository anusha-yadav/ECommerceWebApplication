using E_Commerce_WebApplication.Data;
using E_Commerce_WebApplication.FactoryPattern;
using E_Commerce_WebApplication.Repositories;
using E_Commerce_WebApplication.Utilities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace E_Commerce_WebApplication.Controllers
{
    public class OrderController : Controller
    {
        private readonly IUserIdUtility _userIdUtility;
        private readonly IRepositoryFactory _repositoryFactory;

        /// <summary>
        /// Intializing new instances 
        /// </summary>
        /// <param name="userIdUtility"></param>
        /// <param name="repositoryFactory"></param>
        public OrderController(IUserIdUtility userIdUtility, IRepositoryFactory repositoryFactory)
        {
            _userIdUtility = userIdUtility;
            _repositoryFactory = repositoryFactory;
        }

        /// <summary>
        /// Index Page of Orders
        /// </summary>
        /// <returns></returns>
        public IActionResult Index()
        {
            int? userid = _userIdUtility.GetUserId();
            var orders = _repositoryFactory.CreateOrderRepository().CreateOrderForMyOrders(userid.Value);
            return View(orders);
        }

    }
}
