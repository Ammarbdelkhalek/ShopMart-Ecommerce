using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopMarket.Services.DTOS.ProductcDto
{
    public class ProductDto
    {
        public string ProductName { get; set; }
        public double Price { get; set; }
        public string Image { get; set; }

    }
}
