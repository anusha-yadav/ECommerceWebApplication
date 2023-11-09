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
using System.Drawing.Text;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.VisualStudio.Web.CodeGeneration;
using System.IO;

namespace ECommerceTests
{
    public class ProductControllerTests
    {
        private readonly IWebHostEnvironment _environment;
        private readonly IRepositoryFactory _repositoryFactory;

        public ProductControllerTests()
        {
            _environment = new Mock<IWebHostEnvironment>().Object;
            _repositoryFactory = new Mock<IRepositoryFactory>().Object;
        }

        /*        [Fact]
                public async Task Create_WithValidImageFile_ReturnsPartialViewResult()
                {
                    // Arrange
                    var repositoryFactoryMock = new Mock<IRepositoryFactory>();
                    var environmentMock = new Mock<IWebHostBuilder>();
                    var productRepositoryMock = new Mock<IProductRepository>();
                    repositoryFactoryMock.Setup(rf => rf.CreateProductRepository()).Returns(productRepositoryMock.Object);

                    var formFileMock = new Mock<IFormFile>();
                    var products = new Products(); // Initialize your products object here.

                    var mockFileStream = new MemoryStream(); // Mock a stream for the file
                    formFileMock.Setup(ff => ff.OpenReadStream()).Returns(mockFileStream);
                    formFileMock.Setup(ff => ff.FileName).Returns("testfile.jpg");

                    var tempDirectory = Path.Combine(Directory.GetCurrentDirectory(), "tempTestDir");
                    Directory.CreateDirectory(tempDirectory);

                    var controller = new ProductsController(_environment, _repositoryFactory);

                    // Act
                    var result = await controller.Create(formFileMock.Object, products);

                    // Assert
                    Assert.IsType<PartialViewResult>(result);
                    Assert.Equal("_Success", (result as PartialViewResult).ViewName);

                    // Verify that other interactions with the dependencies are as expected
                    productRepositoryMock.Verify(pr => pr.CreateProduct(products), Times.Once);
                }*/

        [Fact]
        public void Create_ReturnsViewResultWithSelectList()
        {
            var repoFactMock = new Mock<IRepositoryFactory>();
            var productRepoMock = new Mock<IProductRepository>();

            repoFactMock.Setup(repo => repo.CreateProductRepository()).Returns(productRepoMock.Object);

            SelectList list = new SelectList(new List<SelectListItem>
            {
                new SelectListItem {Value = "SmartPhones", Text = "1"},
                new SelectListItem {Value = "Watches", Text = "2"},
            },"Value","Text");

            productRepoMock.Setup(pr=>pr.CreateSubCategoryView()).Returns(list);

            var controller = new ProductsController(_environment, repoFactMock.Object);
            var result = controller.Create() as ViewResult;

            Assert.IsType<ViewResult>(result);
            var subCategoryData = result.ViewData["SubCategoryId"] as SelectList;
            Assert.NotNull(subCategoryData);
            Assert.Equal(list,subCategoryData);
        }

    }
}





