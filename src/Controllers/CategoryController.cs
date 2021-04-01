using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using VUTProjectApp.Abstractions;
using VUTProjectApp.Data;
using VUTProjectApp.Dto;
using VUTProjectApp.Models;

namespace VUTProjectApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : Controller
    {
        private readonly ICategoryRepo repository;
        private readonly IMapper mapper;

        public CategoryController(ICategoryRepo repository, IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<List<CategoryDto>>> GetAllCategory()
        {
            var categories = await repository.GetAll();
            var categoriesDto = mapper.Map<List<CategoryDto>>(categories);
            return categoriesDto;
        }

        [HttpGet("{id}", Name= "GetCategoryById")]
        public async Task<ActionResult<CategoryDto>> GetCategoryById([FromRoute]int id)
        {
            var category = await repository.GetById(id, x => x.Id == id);
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
            repository.Create(category);
            repository.SaveChanges();
            var caregoryDto = mapper.Map<CategoryDto>(category);
            return CreatedAtRoute(nameof(GetCategoryById), new { caregoryDto.Id }, caregoryDto);
        }

        [HttpPut("{id}")]
        public ActionResult UpdateCategory(int id, CategoryCreateDto categoryCreateDto)
        {
            var categoryFromRepo = repository.GetById(id, x => x.Id == id);
            if (categoryFromRepo == null)
            {
                return NotFound();
            }            
            mapper.Map(categoryCreateDto, categoryFromRepo.Result);            
            repository.Update(categoryFromRepo.Result);
            repository.SaveChanges();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public ActionResult DeleteCategory(int id)
        {
            //Some changes
            var categoryFromRepo = repository.GetById(id, x => x.Id == id);
            if(categoryFromRepo == null)
            {
                return NotFound();
            }
            repository.Delete(categoryFromRepo.Result);
            repository.SaveChanges();
            return NoContent();
        }
    }
}
