using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using VUTProjectApp.Data;
using VUTProjectApp.Models;

namespace VUTProjectApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : Controller
    {
        private readonly ICategoryAPIRepo repository;

        public CategoryController(ICategoryAPIRepo repository)
        {
            this.repository = repository;
        }

        [HttpGet]
        public async Task<ActionResult<List<Category>>> GetAllCategory()
        {
            var categories = await repository.GetAllCategory();
            return Ok(categories);
        }

        [HttpGet("Id")]
        public async Task<ActionResult<Category>> GetCategoryById([FromQuery] int id)
        {
            var category = await repository.GetCategoryById(id);
            return category;
        }
    }
}
