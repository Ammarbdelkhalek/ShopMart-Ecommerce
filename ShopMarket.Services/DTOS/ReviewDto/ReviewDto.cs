using ShopMarket.Core.Entites;
using ShopMarket.Services.DTOS.ProductcDto;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopMarket.Services.DTOS.ReviewDto
{
    public class ReviewDto
    {
        [Range(1, 5, ErrorMessage = "Rating must be between 1 and 5")]
        public int Rate { get; set; }

        [Required(ErrorMessage = "Comment is required")]
        public string Comment { get; set; }

        public DateTime Date { get; set; }= DateTime.Now;

        public ApplicationUser Customer { get; set; }

        public ProductDto Product { get; set; }
    }
}
