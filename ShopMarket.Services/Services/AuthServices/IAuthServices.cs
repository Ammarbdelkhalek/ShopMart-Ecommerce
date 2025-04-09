using ShopMarket.Core.Errors;
using ShopMarket.Services.DTOS.AuthDtos;
using ShopMarket.Services.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.XPath;

namespace ShopMarket.Services.Services.AuthServices
{
    public interface IAuthServices
    {
        public Task<AuthModel> Login(LoginDto loginDto);
        public Task<AuthModel> Register(RegisterDto registerDto);
        public Task LogOut();
        public Task<ResponseModel> ResetPassword(RestPasswordDto restPasswordDto);
        public Task<ResponseModel> ForgetPassword(ForgetPasswordDto forgetPasswordDto);
        public Task<ResponseModel>  UpdateUserProfile(string userId,UpdateProfileDto updateProfileDto);
        public Task<ResponseModel> ChangePassword(ChangePasswordDto changePasswordDto);
       
        

    }
}
