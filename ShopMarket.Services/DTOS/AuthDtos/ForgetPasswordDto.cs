using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopMarket.Services.DTOS.AuthDtos
{
    public class ForgetPasswordDto
    {
        [Required(ErrorMessage =" email is required ")]
        [DataType(DataType.EmailAddress,ErrorMessage ="Email is in invalid Format")]
        public string Email { get; set; }
    }
}
