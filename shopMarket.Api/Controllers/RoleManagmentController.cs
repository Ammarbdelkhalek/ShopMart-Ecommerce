using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Org.BouncyCastle.Asn1.Crmf;
using ShopMarket.Core.Entites;
using ShopMarket.Services.DTOS.AuthDtos;
using ShopMarket.Services.DTOS.RoleManagementDto;
using ShopMarket.Services.Helper;
using ShopMarket.Services.Services.AuthurizationServices;

namespace shopMarket.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoleManagmentController (IAuthorizeServices authservice) : ControllerBase
    {
        [HttpGet("Roles")]
        public async Task<IActionResult> GetAllRoles()
        {
            var result = authservice.GetAllRoles();
             return Ok(result);
        }

        [HttpGet("Users")]
        public async Task <IActionResult> GetAllUsers()
        {

            var result = await authservice.GetAllUsers();
            return Ok(result);
        }

        [HttpPost("CreateRole")]
        public async Task<IActionResult> CreateRole (RoleDto dto)
        {
            var result = await authservice.CreateRole(dto);
            if(!result.IsSuccess)
            {
                return BadRequest(result);

            }
            return Ok(result);

        }
        [HttpPost("CreateUserRole")]
        public async Task <IActionResult> CreateUserRole(UsersRoleDto dto)
        {
            var result = await authservice.AssignUserToRole(dto);
            if (!result.IsSuccess)
            {
                return BadRequest(result);

            }
            return Ok(result);
        }
        [HttpDelete("DeleteUserRole")]
        public async Task<IActionResult> DeleteUserRole(UsersRoleDto dto)
        {
            var result = await authservice.RemoveRoleFromUser( dto);
            if (!result.IsSuccess)
            {
                return BadRequest(result);

            }
            return Ok(result);
        }

        [HttpPost("UpdateUserRole")]
        public async Task<IActionResult> UpdateUserRole(UsersRoleDto dto)
        {
            var result = await authservice.UpdateRoleFromUser(dto);
            if (!result.IsSuccess)
            {
                return BadRequest(result);

            }
            return Ok(result);
        }
        [HttpDelete("RemoveRole")]
        public async Task<IActionResult> RemoveRole(string roleName)
        {
            var result = await authservice.RemoveRole(roleName);
            if (!result.IsSuccess)
            {
                return BadRequest(result);

            }
            return Ok(result);

        }
        [HttpGet("GetUsersInRole")]
        public async Task<IActionResult> GetUsersInRole(string roleName)
        {
            var result = await authservice.GetUsersInRoleAsync(roleName);
            if (result== null)
            {
                return BadRequest(result);

            }
            return Ok(result);
        }

    }
}
