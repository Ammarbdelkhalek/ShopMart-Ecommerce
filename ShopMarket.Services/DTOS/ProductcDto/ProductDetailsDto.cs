using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopMarket.Services.DTOS.ProductcDto
{
    public class ProductDetailsDto
    {
        public string ProductName { get; set; }
        public string Description { get; set; }
        public decimal price { get; set; }
        public string Category { get; set; }
        public string Brand { get; set; }
        public string Type { get; set; }
        public string Image { get; set; }
    }
}
