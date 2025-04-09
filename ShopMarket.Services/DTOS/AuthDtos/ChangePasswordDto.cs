using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopMarket.Services.DTOS.AuthDtos
{
    public class ChangePasswordDto
    {
        /*[Required(ErrorMessage = "UserId is required")]
        public string UserId { get; set; }*/
        [Required(ErrorMessage ="Current password  is Required")]
        [DataType(DataType.Password)]
        public string CurrentPassword { get; set; }
        [Required(ErrorMessage = "Current password  is Required")]

        public string NewPassword { get; set; }
        [Required(ErrorMessage = "Current password  is Required")]
        [Compare(nameof(NewPassword),ErrorMessage ="Password Doesn't match")]
        public string ConfirmPassword { get; set; }
    }
}
