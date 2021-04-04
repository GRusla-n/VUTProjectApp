using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VUTProjectApp.Abstractions;
using VUTProjectApp.Controllers;
using VUTProjectApp.Dto;
using VUTProjectApp.Models;
using VUTProjectApp.Services;
using Xunit;

namespace VUTAppProjectTests
{
    public class ProductControllerTest : IDisposable
    {
        Mock<IProductRepo> mockRepo;
        AppProfile realProfile;
        MapperConfiguration configuration;
        IMapper mapper;
        List<Product> mockData;
        Mock<IFileStorage> fileStorage;
        PaginationDto pagination;
        HttpContext httpContext;


        public ProductControllerTest()
        {
            mockRepo = new Mock<IProductRepo>();
            realProfile = new AppProfile();
            configuration = new MapperConfiguration(cfg => cfg.AddProfile(realProfile));
            mapper = new Mapper(configuration);
            mockData = new List<Product>();
            fileStorage = new Mock<IFileStorage>();            
        }

        public void Dispose()
        {
            mockRepo = null;
            mapper = null;
            configuration = null;
            realProfile = null;
            mockData = null;
            fileStorage = null;
            pagination = null;
            httpContext = null;
        }

        [Fact]
        public async void GetProducts_ReturnListOfProducts_WhenDBIsNotEmpty()
        {
            mockRepo
                .Setup(repo => repo.GetAll(httpContext, pagination))
                .Returns(() => Task.FromResult(mockData));

            var productController = new ProductController(mockRepo.Object, mapper, fileStorage.Object);
            var result = await productController.GetAllProducts(pagination);
            Assert.IsType<ActionResult<List<ProductDto>>>(result);
        }
    }
}
