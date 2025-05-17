using AutoMapper;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
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
   public class ProductImageResolver:IValueResolver<Product ,ProductDto,string>
    {
        private readonly IConfiguration _confiq;
        public ProductImageResolver(IConfiguration config)
        {
            config =_confiq;
            
        }
        public string Resolve (Product source,ProductDto detination,string destMember,ResolutionContext context)
        {
            if(!string.IsNullOrEmpty(source.Image))
                return _confiq["BaseUrl"] +source.Image;
            return null;
        } 
    }
}
