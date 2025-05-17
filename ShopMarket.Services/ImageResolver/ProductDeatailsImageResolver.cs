using AutoMapper;
using Microsoft.Extensions.Configuration;
using ShopMarket.Core.Entites;
using ShopMarket.Services.DTOS.ProductcDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopMarket.Services.ImageResolver
{
    public class ProductDeatailsImageResolver : IValueResolver<Product, ProductDetailsDto, string>
    {
        private readonly IConfiguration confiq;

        public ProductDeatailsImageResolver(IConfiguration Confiq)
        {
            confiq = Confiq;
        }
        public string Resolve(Product source, ProductDetailsDto destination, string destMember, ResolutionContext context)
        {
            if (!string.IsNullOrEmpty(source.Image))
                return confiq["BaseUrl"] + source.Image;
            return null;


        }
    }
}
