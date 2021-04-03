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
using Microsoft.EntityFrameworkCore;

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
        public async Task<ActionResult<List<ProductDto>>> GetAllProducts()
        {
            var products = await repository.GetAll(x => x.Category, y => y.Producer);

            var productsDto = mapper.Map<List<ProductDto>>(products);
            return productsDto;
        }

        [HttpGet("{id}", Name= "GetProductById")]
        public async Task<ActionResult<ProductDto>> GetProductById([FromRoute]int id)
        {
            var product = await repository.GetById(x=>x.Id==id, y=>y.Category);
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
        public ActionResult UpdateProduct(int id, ProductCreateDto productCreateDto)
        {
            var productFromRepo = repository.GetById(x => x.Id == id, y => y.Category);
            if (productFromRepo == null)
            {
                return NotFound();
            }            
            mapper.Map(productCreateDto, productFromRepo.Result);            
            repository.Update(productFromRepo.Result);
            repository.SaveChanges();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public ActionResult DeleteProduct(int id)
        {            
            var productFromRepo = repository.GetById(x => x.Id == id, y => y.Category);
            if(productFromRepo == null)
            {
                return NotFound();
            }
            repository.Delete(productFromRepo.Result);
            repository.SaveChanges();
            return NoContent();
        }
    }
}
