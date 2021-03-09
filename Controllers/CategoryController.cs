using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using VUTProjectApp.Data;
using VUTProjectApp.Dto;
using VUTProjectApp.Models;

namespace VUTProjectApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : Controller
    {
        private readonly ICategoryAPIRepo repository;
        private readonly IMapper mapper;

        public CategoryController(ICategoryAPIRepo repository, IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<List<CategoryDto>>> GetAllCategory()
        {
            var categories = await repository.GetAllCategory();
            var categoriesDto = mapper.Map<List<CategoryDto>>(categories);
            return Ok(categoriesDto);
        }

        [HttpGet("Id")]
        public async Task<ActionResult<CategoryDto>> GetCategoryById([FromQuery] int id)
        {
            var category = await repository.GetCategoryById(id);
            var categoryDto = mapper.Map<CategoryDto>(category);
            return Ok(categoryDto);
        }
    }
}
