using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using E_Commerce_WebApplication.Controllers;
using E_Commerce_WebApplication.Repositories;
using E_Commerce_WebApplication.Models;
using Xunit;
using Moq;
using E_Commerce_WebApplication.Utilities;
using E_Commerce_WebApplication.UnitOfWork;
using E_Commerce_WebApplication.Repositories.Interfaces;
using System.Web.Mvc;
using Microsoft.AspNetCore.Mvc;

namespace ECommerceTests.UnitTests
{
    public class CartControllerTests
    {
        [Fact]
        public void AddToCart_LoggedInUser_AddItems()
        {
            var userId = 10;
            var productId = 3;
            var quantity = 4;

            var userid = new Mock<IUserIdUtility>();
            userid.Setup(u => u.GetUserId()).Returns(userId);

            var unitOfWork = new Mock<IUnitOfWork>();
            var cartRepository = new Mock<ICartRepository>();
            unitOfWork.SetupGet(uow=>uow.cartRepository).Returns(cartRepository.Object);

            var controller = new CartController(unitOfWork.Object,userid.Object);

            var result = controller.AddToCart(productId, quantity);

            var viewResult = Assert.IsType<RedirectToActionResult>(result);
        }
    }
}
