using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using ShopMarket.Core.Entites;
using ShopMarket.Services.DTOS.AuthDtos;
using ShopMarket.Services.DTOS.RoleManagementDto;
using ShopMarket.Services.Helper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace ShopMarket.Services.Services.AuthurizationServices
{
    public class AuthorizationServices(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager) : IAuthorizeServices
    {

        public IEnumerable<IdentityRole> GetAllRoles()
        {
            var roles = roleManager.Roles.ToList();
            if (roles == null)
            {
                return Enumerable.Empty<IdentityRole>();

            }
            return roles;
        }

        public async Task<IEnumerable<ApplicationUser>> GetAllUsers()
        {
            var users = await userManager.Users.ToArrayAsync();
            if (users == null)
            {
                return Enumerable.Empty<ApplicationUser>();
            }
            return users;
        }
        
        public async Task<ResponseModel> AssignUserToRole(UsersRoleDto dto)
        {
            
            var user = await userManager.FindByIdAsync(dto.UserId);
            if (user is  null)
            {
                return new ResponseModel
                {
                    StatusCode = 400,
                    Message = " user not exist",
                    IsSuccess = false,
                };
            }
            var isRoleExist = await roleManager.RoleExistsAsync(dto.RoleName);
            if (!isRoleExist)
            {
                var result = await userManager.AddToRoleAsync(user, dto.RoleName);
                if (!result.Succeeded)
                {
                    var error = "";
                    foreach(var err in result.Errors)
                    {
                        error += err.Description;
                    }
                    return new ResponseModel
                    {
                        StatusCode = 400,
                        Message = error,
                        IsSuccess = false,
                    };
                }   
            }
            return new ResponseModel
            {
                StatusCode = 201,
                Message = $"role {dto.RoleName} added successfully to user {user.Email} ",
                IsSuccess = true,

            };

        }
        public async Task<ResponseModel> CreateRole(RoleDto dto)
        {
            var role = await roleManager.RoleExistsAsync(dto.Name);
            if (!role)
            {
               
                var result = await roleManager.CreateAsync(new IdentityRole { Name = dto.Name });
                if (!result.Succeeded)
                {
                    string error = "";
                    foreach (var err in result.Errors)
                    {
                        error += err.Description;
                    }
                    return new ResponseModel
                    {
                        StatusCode = 400,
                        Message = error.ToString(),
                        IsSuccess = false,
                    };
                }
            }
            return new ResponseModel
            {
                StatusCode = 201,
                Message = "Role Created successfully",
                IsSuccess = true,
            };


        }
        public async Task<ResponseModel> RemoveRoleFromUser(UsersRoleDto dto)
        {
            var user = await userManager.FindByIdAsync(dto.UserId);
            if(user == null)
            {
                return new ResponseModel
                {
                    StatusCode = 400,
                    Message = "user is not exist ",
                    IsSuccess=false,
                };
            }
            var roleExist = await roleManager.RoleExistsAsync(dto.RoleName);
            if(!roleExist)
            {
                return new ResponseModel
                {
                    StatusCode = 400,
                    Message = "role dosent exist",
                    IsSuccess = false,
                };
            }
            var result = await userManager.RemoveFromRoleAsync(user, dto.RoleName);
            if(!result.Succeeded) 
            {
                string error = "";
                foreach (var err in result.Errors)
                {
                    error += err.Description;
                }
                return new ResponseModel
                {
                    StatusCode = 400,
                    Message= error,
                    IsSuccess = false,
                };
            
            }
            return new ResponseModel
            {
                StatusCode = 200,
                Message = "Role Removed Successfully",
                IsSuccess = true,
            };
   
        }
        public async Task<ResponseModel> UpdateRoleFromUser(UsersRoleDto dto)
        {
            var user = await userManager.FindByIdAsync(dto.UserId);
            if (user == null)
            {
                return new ResponseModel
                {
                    StatusCode = 400,
                    Message = "user is not exist ",
                    IsSuccess = false,
                };
            }
            var roleExist = await roleManager.RoleExistsAsync(dto.RoleName);
            if (!roleExist)
            {
                return new ResponseModel
                {
                    StatusCode = 400,
                    Message = "role dosent exist",
                    IsSuccess = false,
                };
            }
            var newRole = new IdentityRole
            {
                Name = dto.RoleName,
            };
            var result = await roleManager.UpdateAsync(newRole);
            if (!result.Succeeded)
            {
                string error = "";
                foreach (var err in result.Errors)
                {
                    error += err.Description;
                }
                return new ResponseModel
                {
                    StatusCode = 400,
                    Message = error,
                    IsSuccess = false,
                };

            }
            return new ResponseModel
            {
                StatusCode = 200,
                Message = "Role Updated Successfully",
                IsSuccess = true,
            };

        }
        public async Task<ResponseModel> RemoveRole(string roleName)
        {
            var role = await roleManager.FindByNameAsync(roleName);
            if (role == null)
            {
                return new ResponseModel
                {
                    StatusCode = 400,
                    Message = "Role not Exist",
                    IsSuccess = false,
                };
            }
            var result = await roleManager.DeleteAsync(role);
            if (!result.Succeeded)
            {
                string error = "";
                foreach (var err in result.Errors)
                {
                    error += err.Description;
                }
                return new ResponseModel
                {
                    StatusCode = 400,
                    Message = error,
                    IsSuccess = false,
                };
            }
            return new ResponseModel
            {
                StatusCode = 200,
                Message = "Role Deleted Scucessfully",
                IsSuccess = true,
            };
        }
        public async Task<IEnumerable<ApplicationUser>> GetUsersInRoleAsync(string roleName)
        {
           
            // Check if role exists
            var roleExists = await roleManager.RoleExistsAsync(roleName);
            if (!roleExists)
            {
                throw new InvalidOperationException($"Role '{roleName}' does not exist.");
            }

            // Get users in role
            var usersInRole = await userManager.GetUsersInRoleAsync(roleName);
            return usersInRole ?? Enumerable.Empty<ApplicationUser>();
        }

        
    }

}

       
   

