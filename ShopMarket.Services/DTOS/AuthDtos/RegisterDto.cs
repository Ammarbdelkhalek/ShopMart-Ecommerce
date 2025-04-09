using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopMarket.Services.DTOS.AuthDtos
{
    public class RegisterDto
    {
        [Required(ErrorMessage ="First name is Required") ,MaxLength(50 ,ErrorMessage =" the maximum length is 50")]
        public string Fname { get; set; }  =string .Empty;
        [Required(ErrorMessage = "Last name is Required"),MaxLength(50,ErrorMessage = "the maximum length is 50")]

        public string Lname { get; set; }

        public string ? UserName => $"{Fname}" + $"{Lname}";
        [Required(ErrorMessage = "Email name is Required"),EmailAddress(ErrorMessage ="the Email Address in valid Format ")]

        public string Email { get; set; }
        [Required(ErrorMessage = "Email name is Required")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Compare(nameof(Password) ,ErrorMessage =" Password Not Match !!")]
        public string ConfirmPassword { get; set; }
    }
}
