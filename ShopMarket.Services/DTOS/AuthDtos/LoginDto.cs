using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopMarket.Services.DTOS.AuthDtos
{
    public class LoginDto
    {
        [Required(ErrorMessage ="Email address is required"),EmailAddress(ErrorMessage ="invalid email Address Format")]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
