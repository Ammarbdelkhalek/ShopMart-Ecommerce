using AutoMapper;
using ShopMarket.Core.Entites;
using ShopMarket.Services.DTOS.ProductcDto;
using ShopMarket.Services.DTOS.ReviewDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopMarket.Services.Mapping
{
    public class ReviewProfile:Profile
    {
        public ReviewProfile()
        {
            // Domain to DTO
            CreateMap<Review, ReviewDto>().ReverseMap();            

            // DTO to Domain
            CreateMap<CreateOrUpdateReview, Review>().ReverseMap();
           
        }

    }
}
