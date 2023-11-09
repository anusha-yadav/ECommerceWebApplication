using Microsoft.AspNetCore.Mvc;
using E_Commerce_WebApplication.Models;
using E_Commerce_WebApplication.Repositories;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using E_Commerce_WebApplication.Utilities;
using E_Commerce_WebApplication.FactoryPattern;

namespace E_Commerce_WebApplication.Controllers
{
    public class AccountController : Controller
    {
        private readonly IRepositoryFactory _repositoryFactory;
        private readonly IUserIdUtility _userIdUtility;

        /// <summary>
        /// Intializing a new instances for services
        /// </summary>
        /// <param name="repositoryFactory"></param>
        /// <param name="userIdUtility"></param>
        public AccountController(IRepositoryFactory repositoryFactory,IUserIdUtility userIdUtility)
        {
            _userIdUtility = userIdUtility;
            _repositoryFactory = repositoryFactory;
        }

        /// <summary>
        /// Account registration
        /// </summary>
        /// <returns></returns>
        public IActionResult Register()
        {
            return View();
        }

        /// <summary>
        /// POST: /Account/Register
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                // Check if the email is already registered
                var existingUser = _repositoryFactory.CreateUserRepository().GetUserByUsernameAndEmail(model.Username,model.Email);
                if (existingUser!=null)
                {
                    ModelState.AddModelError("Username", "Email is already in use and User alerady exists");
                    return View(model);
                }

                // Create a new user
                var user = new Users
                {
                    Name = model.Name,
                    Username = model.Username,
                    Email = model.Email,
                    Password = model.Password,
                    PhoneNumber = model.PhoneNumber,
                };

                if (IsUserAdmin(model.Email))
                {
                    user.Roles = Role.Admin;
                }
                else
                {
                    user.Roles = Role.User;
                }

                // Hash the password and save the user to the database
                user.Password = _repositoryFactory.CreateUserRepository().Encrypt(model.Password);
                _repositoryFactory.CreateUserRepository().AddUser(user);
                return RedirectToAction("Login", "Account");
            }
            return View(model);
        }

        /// <summary>
        /// GET: /Account/Login
        /// </summary>
        /// <returns></returns>
        public IActionResult Login()
        {
            return View();
        }

        /// <summary>
        /// POST: /Account/Login
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = _repositoryFactory.CreateUserRepository().GetUserByUsername(model.Username);

                if (user!=null && _repositoryFactory.CreateUserRepository().AuthenticateUser(user.Username,model.Password))
                {
                    HttpContext.Session.SetString("user", model.Username);
                    HttpContext.Session.SetInt32("userid", user.Id);
                    HttpContext.Session.SetString("usertype", user.Roles);
                    return RedirectToAction("Index", "Home"); 
                }
                ModelState.AddModelError("", "Invalid login attempt.");
            }
            return View(model);
        }

        /// <summary>
        /// Session logout of particular user
        /// </summary>
        /// <returns></returns>
        public IActionResult Logout()
        {
            HttpContext.Session.Remove("user");
            return RedirectToAction("Index", "Home");
        }

        /// <summary>
        /// Get profile of a particular user
        /// </summary>
        /// <returns></returns>
        public IActionResult Profile()
        {
            int? userid = _userIdUtility.GetUserId();
            var user = _repositoryFactory.CreateUserRepository().GetUserById(userid.Value);
            return View(user);
        }

        /// <summary>
        /// Checking for admin mail
        /// </summary>
        /// <param name="userEmail"></param>
        /// <returns></returns>
        private bool IsUserAdmin(string userEmail)
        {
            return userEmail.EndsWith("@cognine.com", StringComparison.OrdinalIgnoreCase);
        }
    }
}
