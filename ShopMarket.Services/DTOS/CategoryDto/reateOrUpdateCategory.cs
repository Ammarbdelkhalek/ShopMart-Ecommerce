using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopMarket.Services.DTOS.CategoryDto
{
    public class CreateOrUpdateCategoryDto
    {
        [Required(ErrorMessage = "Please Enter Category Name!")]
        [Display(Name = "Category Name")]
        public string CategoryName { get; set; }
    }
}
