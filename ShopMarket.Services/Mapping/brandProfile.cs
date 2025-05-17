using AutoMapper;
using ShopMarket.Core.Entites;
using ShopMarket.Services.DTOS.BrandDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopMarket.Services.Mapping
{
    public class BrandProfile:Profile
    {
        public BrandProfile()
        {
            CreateMap<Brand, BrandDto>()
               .ForMember(s => s.BrandName, c => c.MapFrom(d => d.Name))
               .ReverseMap();

            CreateMap<Brand, CreateOrUpdateBrandDto>()
                .ForMember(s => s.BrandName, c => c.MapFrom(d => d.Name))
                .ReverseMap();
        }
    }
}
