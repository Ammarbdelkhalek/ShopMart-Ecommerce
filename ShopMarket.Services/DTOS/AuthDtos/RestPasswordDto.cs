﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopMarket.Services.DTOS.AuthDtos
{
    public class RestPasswordDto
    {

        [Required]
        public string Token { get; set; }
        [Required(ErrorMessage = "Please Enter Your Email!")]
        [DataType(DataType.EmailAddress, ErrorMessage = "Please Enter Your Email correctly!")]
        public string Email { get; set; }
        [Required]
        public string Code { get; set; }
        [Required(ErrorMessage = "Incorrect Password !")]
        [DataType(DataType.Password)]
        [RegularExpression(@"^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[#?!@$%^&*-]).{8,}$", ErrorMessage =
            "Password must be between 6 and 20 characters and contain one uppercase letter, one lowercase letter, one digit and one special character.")]
        public string NewPassword { get; set; }
        [Required]
        [Compare(nameof(NewPassword), ErrorMessage = "Password Not Matching Please Try Again !")]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }
    }
}
