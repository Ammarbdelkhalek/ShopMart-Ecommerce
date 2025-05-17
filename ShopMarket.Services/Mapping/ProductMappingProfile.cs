using AutoMapper;
using ShopMarket.Core.Entites;
using ShopMarket.Services.DTOS.ProductcDto;
using ShopMarket.Services.ImageResolver;


namespace ShopMarket.Services.Mapping
{
    public class ProductMappingProfile:Profile
    {
        public ProductMappingProfile()
        {
            CreateMap<Product, ProductDto>()
               .ForMember(d => d.ProductName, c => c.MapFrom(s => s.Name))
               .ForMember(d => d.Image, s => s.MapFrom<ProductImageResolver>())
               .ReverseMap();

            CreateMap<Product, ProductDetailsDto>()
                .ForMember(d => d.ProductName, c => c.MapFrom(s => s.Name))
               .ForMember(d => d.Brand, c => c.MapFrom(s => s.Brand.Name))
               .ForMember(d => d.Category, c => c.MapFrom(s => s.Category.Name))
               .ForMember(d => d.Image, s => s.MapFrom<ProductDeatailsImageResolver>())
               .ReverseMap();


            CreateMap<Product, CreateProductDto>()
                .ForMember(d => d.ProductName, c => c.MapFrom(s => s.Name))
                .ForMember(d => d.Description, c => c.MapFrom(s => s.Description))
                .ForMember(d => d.Price, c => c.MapFrom(s => s.Price))
                .ForMember(d => d.BrandId, c => c.MapFrom(s => s.Brand.Id))
                .ForMember(d => d.CategoryId, c => c.MapFrom(s => s.Category.Id))
                .ForMember(d => d.Image, c => c.Ignore())
                .ReverseMap();
        }
    }
}
 