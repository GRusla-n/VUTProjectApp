using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VUTProjectApp.Abstractions;
using VUTProjectApp.Dto;
using VUTProjectApp.Models;

namespace VUTProjectApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RatingController : Controller
    {

        private readonly IRatingRepo repository;
        private readonly IMapper mapper;

        public RatingController(IRatingRepo repository, IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<List<RatingDto>>> GetAllRatings([FromQuery] PaginationDto pagination)
        {
            var ratings = await repository.GetAll(HttpContext, pagination);
            var ratingsDto = mapper.Map<List<RatingDto>>(ratings);
            return ratingsDto;
        }

        [HttpGet("filter")]
        public async Task<ActionResult<List<RatingDto>>> Filter([FromQuery] string name)
        {
            var filter = await repository.Filter(x => x.Product.Name == name);
            var filterDto = mapper.Map<List<RatingDto>>(filter);
            return filterDto;
        }

        [HttpGet("{id}", Name = "GetRatingById")]
        public async Task<ActionResult<RatingDto>> GetRatingById([FromRoute] int id)
        {
            var rating = await repository.GetById(x => x.Id == id);
            if (rating == null)
            {
                return NotFound();
            }
            var ratingDto = mapper.Map<RatingDto>(rating);
            return ratingDto;
        }

        [HttpPost]
        public ActionResult<RatingDto> CreateCategory([FromBody] RatingCreateDto ratingCreateDto)
        {
            var rating = mapper.Map<Rating>(ratingCreateDto);
            repository.Create(rating);
            repository.SaveChanges();
            var ratingDto = mapper.Map<RatingDto>(rating);
            return CreatedAtRoute(nameof(GetRatingById), new { ratingDto.Id }, ratingDto);
        }

        [HttpPut("{id}")]
        public ActionResult UpdateRating(int id, RatingCreateDto ratingCreateDto)
        {
            var ratingFromRepo = repository.GetById(x => x.Id == id);
            if (ratingFromRepo == null)
            {
                return NotFound();
            }
            mapper.Map(ratingCreateDto, ratingFromRepo.Result);
            repository.Update(ratingFromRepo.Result);
            repository.SaveChanges();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public ActionResult DeleteRating(int id)
        {            
            var ratingFromRepo = repository.GetById(x => x.Id == id);
            if (ratingFromRepo == null)
            {
                return NotFound();
            }
            repository.Delete(ratingFromRepo.Result);
            repository.SaveChanges();
            return NoContent();
        }
    }
}
