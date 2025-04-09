using Microsoft.AspNetCore.Identity;
using ShopMarket.Core.Entites;
using ShopMarket.Services.DTOS.AuthDtos;
using ShopMarket.Services.DTOS.RoleManagementDto;
using ShopMarket.Services.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
 

namespace ShopMarket.Services.Services.AuthurizationServices
{
    public interface IAuthorizeServices
    {
        Task<ResponseModel> AssignUserToRole( UsersRoleDto dto);
        Task<ResponseModel> CreateRole( RoleDto dto);
        Task<ResponseModel> RemoveRole(  string roleName);
        Task<ResponseModel> UpdateRoleFromUser(UsersRoleDto dto);
        Task<ResponseModel> RemoveRoleFromUser(UsersRoleDto dto);
        IEnumerable<IdentityRole> GetAllRoles();
        Task<IEnumerable<ApplicationUser>> GetAllUsers();
        Task<IEnumerable<ApplicationUser>> GetUsersInRoleAsync(string roleName);
        
    }
}
