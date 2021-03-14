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

        }
    }
}
