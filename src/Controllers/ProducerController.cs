using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using VUTProjectApp.Abstractions;
using VUTProjectApp.Dto;
using VUTProjectApp.Models;
using VUTProjectApp.Services;

namespace VUTProjectApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProducerController : Controller
    {
        private readonly IProducerRepo repository;
        private readonly IMapper mapper;
        private readonly IFileStorage fileStorage;
        private readonly string containerName = "producers";

        public ProducerController(IProducerRepo repository, IMapper mapper, IFileStorage fileStorage)
        {
            this.repository = repository;
            this.mapper = mapper;
            this.fileStorage = fileStorage;
        }

        [HttpGet]
        public async Task<ActionResult<List<ProducerDto>>> GetAllProducers()
        {
            var producer = await repository.GetAll(x => x.Products);

            var producerDto = mapper.Map<List<ProducerDto>>(producer);
            return producerDto;
        }

        [HttpGet("{id}", Name = "GetProducerById")]
        public async Task<ActionResult<ProducerDto>> GetProducerById([FromRoute] int id)
        {
            var producer = await repository.GetById(x => x.Id == id, y => y.Products);
            if (producer == null)
            {
                return NotFound();
            }
            var producerDto = mapper.Map<ProducerDto>(producer);
            return producerDto;
        }

        [HttpPost]
        public async Task<ActionResult<ProducerDto>> CreateProducer([FromForm] ProducerCreateDto producerCreateDto)
        {
            var producer = mapper.Map<Producer>(producerCreateDto);
            if(producerCreateDto.Logo != null)
            {
                using (var memoryStream = new MemoryStream())
                {
                    await producerCreateDto.Logo.CopyToAsync(memoryStream);
                    var content = memoryStream;
                    var extension = Path.GetExtension(producerCreateDto.Logo.FileName);
                    producer.Logo =
                        await fileStorage.SaveFile(content, extension, containerName);
                }
            }
            
            repository.Create(producer);
            repository.SaveChangesAsync();
            var productDto = mapper.Map<ProducerDto>(producer);
            return CreatedAtRoute(nameof(GetProducerById), new { productDto.Id }, productDto);
        }
    }
}
 