using AutoMapper;
using VUTProjectApp.Models;

namespace VUTProjectApp.Dto
{
    public class AppProfile : Profile
    {
        public AppProfile()
        {
            CreateMap<Category, CategoryDto>();
            CreateMap<CategoryCreateDto, Category>();
            CreateMap<Product, ProductDto>();
            CreateMap<ProductCreateDto, Product>();
            CreateMap<Producer, ProducerDto>();
            CreateMap<ProducerCreateDto, Producer>();
            CreateMap<Producer, ProducerForProductDto>(); 
            CreateMap<Product, ProductForProducerDto>();

            CreateMap<Rating, RatingDto>();
            CreateMap<RatingCreateDto, Rating>();

        }
    }
}
