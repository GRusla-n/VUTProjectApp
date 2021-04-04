using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using VUTProjectApp.Abstractions;
using VUTProjectApp.Controllers;
using VUTProjectApp.Data;
using VUTProjectApp.Dto;
using VUTProjectApp.Models;
using Xunit;

namespace VUTAppProjectTests
{
    public class CategoryControllerTest : IDisposable
    {
        Mock<ICategoryRepo> mockRepo;
        AppProfile realProfile;
        MapperConfiguration configuration;
        IMapper mapper;
        List<Category> mockData;
        PaginationDto pagination;
        HttpContext httpContext;

        public CategoryControllerTest()
        {
            mockRepo = new Mock<ICategoryRepo>();
            realProfile = new AppProfile();
            configuration = new MapperConfiguration(cfg => cfg.AddProfile(realProfile));
            mapper = new Mapper(configuration);
            mockData = new List<Category>();
            pagination = new PaginationDto();
        }

        public void Dispose()
        {
            mockRepo = null;
            mapper = null;
            configuration = null;
            realProfile = null;
            mockData = null;
            pagination = null;
            httpContext = null;
        }

        [Fact]
        public async void GetGategories_ReturnListOfCategory_WhenDBIsNotEmpty()
        {                    
            mockRepo
                .Setup(repo => repo.GetAll(httpContext, pagination))
                .Returns(() => Task.FromResult(mockData));      

            var categoryController = new CategoryController(mockRepo.Object, mapper);
            var result = await categoryController.GetAllCategory(pagination);
            Assert.IsType<ActionResult<List<CategoryDto>>>(result);
        }        

        [Fact]
        public async void GetGategoryById_Returns404NotFound_WhenNonExistentIDProvider()
        {
            mockRepo
                .Setup(repo => repo.GetById(x=>x.Id == 0, null))
                .Returns(() => null);

            var categoryController = new CategoryController(mockRepo.Object, mapper);

            var result = await categoryController.GetCategoryById(1);            
            Assert.IsType<NotFoundResult>(result.Result);
        }

        [Fact]
        public async void GetGategoryById_Returns200Ok_WhenWhenValidIDProvided()
        {
            mockData.Add(new Category
            {
                Id = 1,
                Name = "Test"
            });
            mockRepo
                .Setup(repo => repo.GetById(x => x.Id == 1, null))
                .Returns(() => Task.FromResult(mockData[0]));

            var categoryController = new CategoryController(mockRepo.Object, mapper);
            var result = await categoryController.GetCategoryById(1);

            Assert.IsType<ActionResult<CategoryDto>>(result);            
        }

        [Fact]
        public void CreateCategory_ReturnsCorrectResourceType_WhenValidObjectSubmitted()
        {
            mockData.Add(new Category
            {
                Id = 0,
                Name = "test"
            });
            mockRepo.Setup(repo => repo.GetById(x => x.Id == 1, null))
                .Returns(Task.FromResult(mockData[0]));
            var controller = new CategoryController(mockRepo.Object, mapper);

            var result = controller.CreateCategory(new CategoryCreateDto());

            Assert.IsType<ActionResult<CategoryDto>>(result);

        }

        [Fact]
        public void CreateCategory_Returns201Created_WhenValidObjectSubmitted()
        {
            mockData.Add(new Category
            {
                Id = 0,
                Name = "test"
            });
            mockRepo.Setup(repo => repo.GetById(x => x.Id == 1, null))
                .Returns(Task.FromResult(mockData[0]));
            var controller = new CategoryController(mockRepo.Object, mapper);

            var result = controller.CreateCategory(new CategoryCreateDto());

            Assert.IsType<CreatedAtRouteResult>(result.Result);
        }

        [Fact]
        public void UpdateCategory_Returns204NoContent_WhenValidObjectSubmitted()
        {
            mockData.Add(new Category
            {
                Id = 0,
                Name = "Test"
            });
            mockRepo
                .Setup(repo => repo.GetById(x => x.Id == 1, null))
                .Returns(() => Task.FromResult(mockData[0]));
            var controller = new CategoryController(mockRepo.Object, mapper);

            var result = controller.UpdateCategory(1, new CategoryCreateDto());

            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public void UpdateCategory_Returns404NotFound_WhenNonExistentResourceIDSubmitted()
        {
            mockRepo
                .Setup(repo => repo.GetById(x => x.Id == 1, null))
                .Returns(() => null);
            var controller = new CategoryController(mockRepo.Object, mapper);

            var result = controller.UpdateCategory(1, new CategoryCreateDto());

            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public void DeleteCommand_Returns204NoContent_WhenValidResourceIDSubmitted()
        {
            mockData.Add(new Category
            {
                Id = 0,
                Name = "Test"
            });
            mockRepo
                .Setup(repo => repo.GetById(x => x.Id == 1, null))
                .Returns(() => Task.FromResult(mockData[0]));
            var controller = new CategoryController(mockRepo.Object, mapper);
            
            var result = controller.DeleteCategory(1);
            
            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public void DeleteCommand_Returns404NotFound_WhenNonExistentResourceIDSubmitted()
        {            
            mockRepo
                .Setup(repo => repo.GetById(x => x.Id == 0, null))
                .Returns(() => null);
            var controller = new CategoryController(mockRepo.Object, mapper);

            var result = controller.DeleteCategory(0);

            Assert.IsType<NoContentResult>(result);
        }
    }
}
