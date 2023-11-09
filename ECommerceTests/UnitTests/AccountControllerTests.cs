using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using E_Commerce_WebApplication.Controllers;
using E_Commerce_WebApplication.Repositories;
using E_Commerce_WebApplication.Models;
using Xunit;
using E_Commerce_WebApplication.Repositories.Interfaces;
using Moq;
using E_Commerce_WebApplication.FactoryPattern;
using E_Commerce_WebApplication.Utilities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Caching.Distributed;
using System.Net.Http;
using System.Diagnostics;

namespace ECommerceTests.UnitTests
{
    public class AccountControllerTests
    {
        [Fact]
        public void Register_ValidModel_ReturnsRedirectToAction()
        {
            //Arrange
            var userRepository = new Mock<IUserRepository>(); 
            // for no existing user
            var mockRepo = userRepository.Setup(repo => repo.GetUserByUsernameAndEmail(It.IsAny<string>(), It.IsAny<string>())).Returns((Users)null);
            var repositoryFactory = new Mock<IRepositoryFactory>(); 
            repositoryFactory.Setup(factory=> factory.CreateUserRepository()).Returns(userRepository.Object);

            var useridUtility = new Mock<IUserIdUtility>();
            useridUtility.Setup(utility => utility.GetUserId()).Returns(123);
            var controller = new AccountController(repositoryFactory.Object,useridUtility.Object);
            //controller.ModelState.AddModelError("", "Error");

            var model = new RegisterViewModel
            {
                Name = "Anusha",
                Username = "anusha",
                Password = "Anusha@123",
                ConfirmPassword = "Anusha@123",
                Email = "anusha@gmail.com"
            };

            //Act
            var result = controller.Register(model);

            //Asser
            Assert.True(controller.ModelState.IsValid);
            Assert.NotNull(result);
            var redirectionResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("Login", redirectionResult.ActionName);
            mockRepo.Verifiable();
        }

        [Fact]
        public void Register_DuplicateEmail_ReturnsViewWithModelError()
        {
            var userRepository = new Mock<IUserRepository>();
            userRepository.Setup(repo => repo.GetUserByUsernameAndEmail(It.IsAny<string>(), It.IsAny<string>())).Returns((new Users()));
            var repositoryFactory = new Mock<IRepositoryFactory>();
            repositoryFactory.Setup(factory => factory.CreateUserRepository()).Returns(userRepository.Object);

            var userid = new Mock<IUserIdUtility>();
            userid.Setup(utility => utility.GetUserId()).Returns(123);

            var controller = new AccountController(repositoryFactory.Object,userid.Object);
            var model = new RegisterViewModel
            {
                Name = "Anusha",
                Username = "Priya01",
                Password = "Priya@123",
                ConfirmPassword = "Priya@123",
                Email = "anusha@gmail.com",
                PhoneNumber = "9618560411",
            };

            var result = controller.Register(model) as RedirectToActionResult;

            Assert.Null(result);
            Assert.False(controller.ModelState.IsValid);
            Assert.Contains(controller.ModelState.Keys, key => key == "Username");
        }

        [Fact]
        public void Register_InValidModel()
        {
            var mockRepo = new Mock<IUserRepository>();
            var userid = new Mock<IUserIdUtility>();
            userid.Setup(utility => utility.GetUserId()).Returns(123);

            var factory = new Mock<IRepositoryFactory>();
            factory.Setup(f => f.CreateUserRepository()).Returns(mockRepo.Object);

            var controller = new AccountController(factory.Object, userid.Object);
            var model = new RegisterViewModel
            {
                Username = "Priya",
            };

            controller.ModelState.AddModelError("Email", "Required");

            var result = controller.Register(model);

            var viewResult = Assert.IsType<ViewResult>(result);
        }

       [Fact]
        public void Login_ValidModel_ReturnsRedirectToActionResult()
        {
            var model = new LoginViewModel
            {
                Username = "anusha",
                Password = "Anusha@123",
                
            };

            var resultModel = new Users
            {
                Username = "anusha",
                Password = "Anusha@123",
                Roles = "User"
            };

            var userRepository = new Mock<IUserRepository>();

            userRepository.Setup(repo=>repo.GetUserByUsername(It.IsAny<string>())).Returns(resultModel);
            userRepository.Setup(repo => repo.AuthenticateUser(It.IsAny<string>(), It.IsAny<string>())).Returns(true);

            var repositoryFactory = new Mock<IRepositoryFactory>();
            repositoryFactory.Setup(repo=>repo.CreateUserRepository()).Returns(userRepository.Object);


            var httpContext = new DefaultHttpContext();
            httpContext.Session = new Mock<ISession>().Object;
            var userid = httpContext.Session.GetInt32("userid");
            Debug.WriteLine(userid);

        /*    httpContext.Session = new MockHttpSession(); 
            httpContext.Session.SetString("user", model.Username);
            httpContext.Session.SetInt32("userid", 1);
            httpContext.Session.SetString("usertype","User");*/

            var controller = new AccountController(repositoryFactory.Object, new Mock<IUserIdUtility>().Object)
            {
                ControllerContext = new ControllerContext { HttpContext = httpContext }
            };

            var result = controller.Login(model) as RedirectToActionResult;

            Assert.NotNull(result);
            Assert.Equal("Index", result.ActionName);
        }

    }

    public class MockHttpSession : ISession
    {
        private readonly Dictionary<string, byte[]> _sessionData = new Dictionary<string, byte[]>();

        public bool IsAvailable => true;

        public string Id { get; set; }

        public IEnumerable<string> Keys => _sessionData.Keys;

        public void Clear()
        {
            _sessionData.Clear();
        }

        public Task CommitAsync(CancellationToken cancellationToken = default)
        {
            return Task.CompletedTask;
        }

        public Task LoadAsync(CancellationToken cancellationToken = default)
        {
            return Task.CompletedTask;
        }

        public void Remove(string key)
        {
            _sessionData.Remove(key);
        }

        public void Set(string key, byte[] value)
        {
            _sessionData[key] = value;
        }

        public bool TryGetValue(string key, out byte[] value)
        {
            return _sessionData.TryGetValue(key, out value);
        }

        // Helper methods for working with string values
        public void SetString(string key, string value)
        {
            Set(key, Encoding.UTF8.GetBytes(value));
        }

        public string GetString(string key)
        {
            if (TryGetValue(key, out var valueBytes))
            {
                return Encoding.UTF8.GetString(valueBytes);
            }
            return null;
        }
    }
}
