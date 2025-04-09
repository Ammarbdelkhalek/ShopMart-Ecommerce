using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Org.BouncyCastle.Asn1.Cms;
using ShopMarket.Services.DTOS.AuthDtos;
using ShopMarket.Services.Helper;
using ShopMarket.Services.Services.AuthServices;
using System.Net.WebSockets;

namespace shopMarket.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController(IAuthServices authServices) : ControllerBase
    {
        [HttpPost("login")]
        public async Task<IActionResult>Login([FromBody]LoginDto loginDto)
        {
            var result = await authServices.Login(loginDto);
            if(result.IsSuccess) {
                return Ok(result);
            }
            return BadRequest(result);
        }
        [HttpPost("Register")]
        public async Task<IActionResult> Register([FromBody]RegisterDto registerDto)
        {
            var result = await authServices.Register(registerDto);
            if (result.IsSuccess)
            {
                return Ok(result);
            }
            return BadRequest(result); 
        }
        [HttpPost("ChangePassword")]
        //[Authorize]
        public async Task<IActionResult> ChangePassword([FromBody]ChangePasswordDto changePasswordDto)
        {
            var result  = await authServices.ChangePassword(changePasswordDto);
            if(result.IsSuccess)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpPut("Update")]
        //[Authorize]
        public async Task<IActionResult>UpdateUserProfile([FromBody]UpdateProfileDto updateProfile,[FromRoute]string UserId)
        {
            var result = await authServices.UpdateUserProfile( UserId,updateProfile);
            if(result.IsSuccess)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpPost("logOut")]
        //[Authorize]
        public async Task<IActionResult> LogOut()
        {
            var result = authServices.LogOut();
            return Ok(new { message = "You Logged in Successfully"});
        }

        [HttpPost("ForgetPassword")]
        public async Task<IActionResult> ForgetPassword([FromQuery]ForgetPasswordDto dto)
        {
            var result  = await authServices.ForgetPassword(dto);
            if (!result.IsSuccess)
            {
                return BadRequest(result);

            }
            return Ok(result);
        }
        [HttpPost("RestPassword")]
        public async Task<IActionResult> RestPassword([FromQuery]RestPasswordDto dto)
        {
            var result = await authServices.ResetPassword(dto);
            if (!result.IsSuccess)
            {
                return BadRequest(result);

            }
            return Ok(result);
        }

        private void SetRefreshTokenInCookie(string refreshToken,DateTime time)
        {
            var cookie = new CookieOptions
            {
                HttpOnly = true,
                Expires = time.ToLocalTime(),
            };

            Response.Cookies.Append("RefreshToken",refreshToken,cookie);
        }

    }
}
