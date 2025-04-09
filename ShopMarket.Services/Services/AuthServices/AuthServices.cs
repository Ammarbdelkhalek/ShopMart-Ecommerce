using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using ShopMarket.Core.Entites;
using ShopMarket.Core.Errors;
using ShopMarket.Services.DTOS.AuthDtos;
using ShopMarket.Services.DTOS.EmailDto;
using ShopMarket.Services.Helper;
using ShopMarket.Services.Services.EmailServices;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace ShopMarket.Services.Services.AuthServices
{
    public class AuthServices : IAuthServices
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly IOptions<JwtHelper> options;
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IEmailServices emailService;
        private static ConcurrentDictionary<string, string> OtpStorage = new ConcurrentDictionary<string, string>();

        public AuthServices(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, IOptions<JwtHelper> options,
            IHttpContextAccessor httpContextAccessor, SignInManager<ApplicationUser> signInManager, 
            IEmailServices emailService)
        {
            this.userManager = userManager;
            this.roleManager = roleManager;
            this.options = options;
            this.httpContextAccessor = httpContextAccessor;
            _signInManager = signInManager;
            this.emailService = emailService;
        }

        public async Task<AuthModel> Register(RegisterDto registerDto)
        {
            if(await userManager.FindByEmailAsync(registerDto.Email) is not null)
            {
                return new AuthModel
                {
                    Message = "The Email IS already Exist",
                    StatusCode = 400,
                    IsSuccess = false,    
                };
            }
            if(await userManager.FindByNameAsync(registerDto.Fname)is not  null)
            {
                return new AuthModel
                {
                    Message = "The FirstName is already Exist",
                    StatusCode = 400,
                    IsSuccess = false,
                };

            }
            if (await userManager.FindByNameAsync(registerDto.Lname) is not null)
            {
                return new AuthModel
                {
                    Message = "The LastName is already Exist",
                    StatusCode = 400,
                    IsSuccess = false,
                };

            }
            var user = new ApplicationUser
            {
                FirstName = registerDto.Fname,
                LastName = registerDto.Lname,
                UserName = registerDto.UserName,
                Email = registerDto.Email,

            };
            var result = await userManager.CreateAsync(user, registerDto.Password);
            if (!result.Succeeded)
            {
                string Error = string.Empty;
               foreach (var item in result.Errors)
                {
                    Error+= item.Description;
                }
                return new AuthModel
                {
                    StatusCode = 400,
                    Message = Error,
                    IsSuccess = false,
                };
            }
            //var Roles = await userManager.AddToRoleAsync(user, "User");
            var token = await GenerateUserToken(user);
            var RefreshToken = await GenerateRefreshToken();
            user.RefreshTokens?.Add(RefreshToken);
            await userManager.UpdateAsync(user);

            return new AuthModel
            {
                Message = "Account Created Sucessfuly",
                StatusCode = 200,
                UserName = user.UserName,
                Email = user.Email,
                IsSuccess = true,
                Roles = new List<string> { "User"},
                Token =  token,
                RefreshToken = RefreshToken.Token,
                RefreshTokenExpiration = RefreshToken.ExpriesOn,
            };
            
        }
        public async Task<AuthModel> Login(LoginDto loginDto)
        {
            var user = await userManager.FindByEmailAsync(loginDto.Email);
            if(user is not null && await userManager.CheckPasswordAsync(user,loginDto.Password) is false)
            {
                return new AuthModel
                {
                    Message = "invalid Email Or Password",
                    StatusCode = 400,
                    IsSuccess = false,
                };
            }
            var UserRoles = await userManager.GetRolesAsync(user);
            var token  = await GenerateUserToken(user);
            if (user.RefreshTokens.Any(x => x.IsActive))
            {
                var refreshtoken  = user.RefreshTokens.Single(x => x.IsActive);
                return new AuthModel
                {
                    Message = "You Logged in Sucessfuly",
                    StatusCode = 200,
                    Token =  token,
                    UserName = user.UserName,
                    Email = user.Email,
                    IsSuccess = true,
                    Roles = UserRoles.ToList(),
                    RefreshToken =refreshtoken.Token,
                    RefreshTokenExpiration=refreshtoken.ExpriesOn,
                };
            }
            else
            {
                var refreshToken = await GenerateRefreshToken();
                user.RefreshTokens.Add(refreshToken);
                await userManager.UpdateAsync(user);
                return new AuthModel
                {
                    Message = "You Logged in Sucessfuly",
                    StatusCode = 200,
                    Token =  token,
                    UserName = user.UserName,
                    Email = user.Email,
                    IsSuccess = true,
                    Roles = UserRoles.ToList(),
                    RefreshToken = refreshToken.Token,
                    RefreshTokenExpiration = refreshToken.ExpriesOn,
                };
            }        
        }
        public async Task LogOut()
        {
              await _signInManager.SignOutAsync();
           
        }
        
        public async Task<ResponseModel> ChangePassword(ChangePasswordDto changePasswordDto)
        {
            var user = await GetUser();
            if(user is null)
            {
                return new ResponseModel
                {
                    Message = "Invalid User",
                    StatusCode = 400,
                    IsSuccess=false,
                };
            }
            var result = await userManager.ChangePasswordAsync(user, changePasswordDto.CurrentPassword, changePasswordDto.NewPassword);
            if (!result.Succeeded)
            {
                var error = string.Empty;
                foreach (var Errors in result.Errors)
                {
                    error += $"{Errors.Description},";
                }
                return new ResponseModel
                {
                    Message = error,
                    StatusCode = 400,
                    IsSuccess=false,
                };
            }
            return new ResponseModel
            {
                Message = "password Changede Successfuly",
                StatusCode = 200,
                IsSuccess=true,
            };     
        }
        public async Task<ResponseModel> ForgetPassword(ForgetPasswordDto forgetPasswordDto)
        {
            var user = await userManager.FindByEmailAsync(forgetPasswordDto.Email);
            if (user is null)
            {
                return new ResponseModel
                {
                    StatusCode = 400,
                    Message = "the Email is not exist",
                    IsSuccess = false,
                };
            }
            var token  = await userManager.GeneratePasswordResetTokenAsync(user);
            var OtpCode = GenerateOTPCode();
            
            var email = new EmailDto
            {
                Subject = "Reset Your Password",
                Body = $"Please Enter the code to complete process: \n\n {OtpCode}",
                ToEmail = "ledoha9395@nalwan.com",

            };
            await emailService.SendEmail(email);
            OtpStorage[user.Email] = OtpCode;
            return new ResponseModel
            {
                Message = $"Password Change Request is send email <{user.Email}> ..please open your Email",
                StatusCode = 200,
                IsSuccess = true,
            };
            
        }
        public async Task<ResponseModel> ResetPassword(RestPasswordDto restPasswordDto)
        {

            if(!OtpStorage.TryGetValue(restPasswordDto.Email,out var otpStorage)|| otpStorage != restPasswordDto.Code)
            {
                return new ResponseModel
                {
                    StatusCode = 400,
                    Message = "Invalid OtpCode Please Try Again!",
                    IsSuccess = false,
                };
            }
            var user = await userManager.FindByEmailAsync(restPasswordDto.Email);
            if (user is null)
            {
                return new ResponseModel
                {
                    StatusCode = 400,
                    Message = "User Is not found",
                    IsSuccess = false,
                };
            }
            var result = await userManager.ResetPasswordAsync(user, restPasswordDto.Token, restPasswordDto.NewPassword);
            if (!result.Succeeded)
            {
                string error = string.Empty;
                foreach(var item in result.Errors)
                {
                    error += item.Description;
                };
                return new ResponseModel
                {
                    Message = error,
                    StatusCode = 400,
                    IsSuccess = false,
                };
            }
            OtpStorage.TryRemove(restPasswordDto.Email, out _);
            return new ResponseModel
            {
                Message = "Password Rest successfully",
                StatusCode = 200,
                IsSuccess = true,
            };
        }
        public async Task<ResponseModel> UpdateUserProfile(string userId,UpdateProfileDto updateProfileDto)
        {
            var user = await userManager.FindByIdAsync(userId);
            if (user is null)
            {
                return new ResponseModel
                {
                    Message = "the user is not  found",
                    StatusCode = 400,
                    IsSuccess = false,
                };
            }
            user.FirstName = updateProfileDto.FirstName;
            user.LastName = updateProfileDto.LastName;
            user.Email = updateProfileDto.Email;
            user.Address = updateProfileDto.Address;
            var result = await userManager.UpdateAsync(user);

            if(!result.Succeeded)
            {
                string error = string.Empty;
                foreach(var err in result.Errors)
                {
                    error += err.Description;
                }
                return new ResponseModel
                {
                    Message = error,
                    StatusCode = 400,
                };
            }
            return new ResponseModel
            {
                Message = "Profile Updated Successfuly",
                StatusCode = 200,
                IsSuccess = true
            };

        }
        private async Task<ApplicationUser> GetUser()
        {
            var user =  httpContextAccessor.HttpContext.User;
            var userId =  userManager.GetUserId(user);
            var users =  await userManager.Users.FirstOrDefaultAsync(x => x.Id == userId);
            return   users;
        }
        private async Task<string> GenerateUserToken(ApplicationUser user)
        {
            var Roles = await userManager.GetRolesAsync(user);
            var claims = new List<Claim> {
                new Claim(ClaimTypes.Name,user.UserName),
                new Claim(ClaimTypes.NameIdentifier,user.Id),
                new Claim(JwtRegisteredClaimNames.Sub,user.UserName),
                new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Email,user.Email),
            };
            foreach (var role in Roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }
            var SecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(options.Value.key));
            var securityCredintial = new SigningCredentials(SecurityKey, SecurityAlgorithms.Aes128CbcHmacSha256);

            var JwtSecurityToken = new JwtSecurityToken(
                issuer: options.Value.Issuer,
                audience: options.Value.Audience,
                claims: claims,
                signingCredentials: securityCredintial,
                expires: DateTime.UtcNow.AddMinutes(10)
                );
            return new JwtSecurityTokenHandler().WriteToken(JwtSecurityToken);
        }
        private async Task<RefreshToken> GenerateRefreshToken()
        {
            var randomNumber = new byte[32];
            using var generator = new RNGCryptoServiceProvider();
            generator.GetBytes(randomNumber);
            return new RefreshToken
            {
                Token = Convert.ToBase64String(randomNumber),
                ExpriesOn = DateTime.UtcNow.AddDays(7),
                CreatedAt = DateTime.UtcNow,
            };
        }
        private string GenerateOTPCode ()
        {
            string code = string.Empty;
            var random = new Random();
            for(int i = 0; i < 10;i++)
            {
                code += random.Next(0,10).ToString();
            }
            return code;

        }
         

    }
}
