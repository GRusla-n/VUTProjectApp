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
            return categoriesDto;
        }

        [HttpGet("{id}", Name= "GetCategoryById")]
        public async Task<ActionResult<CategoryDto>> GetCategoryById([FromRoute]int id)
        {
            var category = await repository.GetCategoryById(id);
            if(category == null)
            {
                return NotFound();
            }
            var categoryDto = mapper.Map<CategoryDto>(category);
            return categoryDto;
        }

        [HttpPost]
        public ActionResult<CategoryDto> CreateCategory([FromBody] CategoryCreateDto categoryCreateDto)
        {
            var category = mapper.Map<Category>(categoryCreateDto);
            repository.CreateCategory(category);
            repository.SaveChanges();
            var caregoryDto = mapper.Map<CategoryDto>(category);
            return CreatedAtRoute(nameof(GetCategoryById), new { caregoryDto.Id }, caregoryDto);
        }

        [HttpPut("{id}")]
        public ActionResult UpdateCategory(int id, CategoryCreateDto categoryCreateDto)
        {
            var categoryFromRepo = repository.GetCategoryById(id);
            if (categoryFromRepo == null)
            {
                return NotFound();
            }            
            mapper.Map(categoryCreateDto, categoryFromRepo.Result);            
            repository.UpdateCategory(categoryFromRepo.Result);
            repository.SaveChanges();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public ActionResult DeleteCategory(int id)
        {
            //Some changes
            var categoryFromRepo = repository.GetCategoryById(id);
            if(categoryFromRepo == null)
            {
                return NotFound();
            }
            repository.DeleteCategory(categoryFromRepo.Result);
            repository.SaveChanges();
            return NoContent();
        }
    }
}
