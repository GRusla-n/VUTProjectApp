using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using VUTProjectApp.Services;
using VUTProjectApp.Abstractions;
using VUTProjectApp.Dto;
using VUTProjectApp.Models;

namespace VUTProjectApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : Controller
    {
        private readonly IProductRepo repository;
        private readonly IMapper mapper;
        private readonly IFileStorage fileStorage;
        private readonly string containerName = "products";

        public ProductController(IProductRepo repository, IMapper mapper, IFileStorage fileStorage)
        {
            this.repository = repository;
            this.mapper = mapper;
            this.fileStorage = fileStorage;
        }

        [HttpGet]
        public async Task<ActionResult<List<ProductDto>>> GetAllProducts([FromQuery] PaginationDto pagination)
        {
            var products = await repository.GetAll(HttpContext, pagination, x => x.Category, y => y.Producer, z => z.Ratings);

            var productsDto = mapper.Map<List<ProductDto>>(products);
            return productsDto;
        }

        [HttpGet("{id}", Name= "GetProductById")]
        public async Task<ActionResult<ProductDto>> GetProductById([FromRoute]int id)
        {
            var product = await repository.GetById(x=>x.Id==id, y=>y.Category, z => z.Ratings);
            if(product == null)
            {
                return NotFound();
            }
            var ProductDto = mapper.Map<ProductDto>(product);
            return ProductDto;
        }

        [HttpPost]
        public async Task<ActionResult<ProductDto>> CreateProduct([FromForm] ProductCreateDto productCreateDto)
        {               
            var product = mapper.Map<Product>(productCreateDto);
            if(productCreateDto.Image != null)
            {
                using (var memoryStream = new MemoryStream())
                {
                    await productCreateDto.Image.CopyToAsync(memoryStream);
                    var content = memoryStream;
                    var extension = Path.GetExtension(productCreateDto.Image.FileName);
                    product.Image =
                        await fileStorage.SaveFile(content, extension, containerName);
                }
            }
            
            repository.Create(product);
            repository.SaveChangesAsync();
            var productDto = mapper.Map<ProductDto>(product);
            return CreatedAtRoute(nameof(GetProductById), new { productDto.Id }, productDto);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateProduct(int id, ProductCreateDto productCreateDto)
        {
            var productFromRepo = repository.GetById(x => x.Id == id);

            if (productFromRepo == null)
            {
                return NotFound();
            }

            if (productCreateDto.Image != null)
            {
                using (var memoryStream = new MemoryStream())
                {
                    await productCreateDto.Image.CopyToAsync(memoryStream);
                    var content = memoryStream;
                    var extension = Path.GetExtension(productCreateDto.Image.FileName);
                    var fileRoute = productFromRepo.Result.Image;
                    productFromRepo.Result.Image =
                        await fileStorage.EditFile(content, extension, containerName, fileRoute);
                }
            }

                mapper.Map(productCreateDto, productFromRepo.Result);
                repository.Update(productFromRepo.Result);
                repository.SaveChangesAsync();
                return NoContent();
            }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteProduct(int id)
        {            
            var productFromRepo = repository.GetById(x => x.Id == id, y => y.Category);
            if(productFromRepo == null)
            {
                return NotFound();
            }
            await fileStorage.DeleteFile(productFromRepo.Result.Image, containerName);
            repository.Delete(productFromRepo.Result);
            repository.SaveChangesAsync();
            return NoContent();
        }
    }
}
