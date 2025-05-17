using AutoMapper;
using ShopMarket.Core.Entites;
using ShopMarket.Services.DTOS.CategoryDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopMarket.Services.Mapping
{
    public class CategoryProfile:Profile
    {
        public CategoryProfile()
        {
            CreateMap<Category, categoryDto>()
                .ForMember(s => s.CategoryName, c => c.MapFrom(d => d.Name)).ReverseMap();
            CreateMap<Category, CreateOrUpdateCategoryDto>()
                  .ForMember(s => s.CategoryName, c => c.MapFrom(d => d.Name)).ReverseMap();
        }
    }
}
