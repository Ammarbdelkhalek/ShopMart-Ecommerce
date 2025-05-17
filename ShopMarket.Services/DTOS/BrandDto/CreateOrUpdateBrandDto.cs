using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopMarket.Services.DTOS.BrandDto
{
    public class CreateOrUpdateBrandDto
    {
        [Required(ErrorMessage = "Please Enter BrandName !")]
        public string BrandName { get; set; }

    }
}
