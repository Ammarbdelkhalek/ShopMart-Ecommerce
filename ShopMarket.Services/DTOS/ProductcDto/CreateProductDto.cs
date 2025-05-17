﻿using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopMarket.Services.DTOS.ProductcDto
{
    public class CreateProductDto
    {
        [Required(ErrorMessage = "Please Enter Product Name !")]
        public string ProductName { get; set; }
        [Required]
        public string Description { get; set; }
        [Required(ErrorMessage = "Please Enter Product Price !")]
        public decimal Price { get; set; }

        //public int Brand {  get; set; }
        //public int Category { get; set; }
        //public int Type { get; set; }       

        public int CategoryId { get; set; }

        public int BrandId { get; set; }
        [Required]
        public IFormFile Image { get; set; }


    }
}
