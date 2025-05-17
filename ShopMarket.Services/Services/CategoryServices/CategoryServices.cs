using AutoMapper;
using ShopMarket.Core.Entites;
using ShopMarket.Core.Errors;
using ShopMarket.Infrastrcuture.interfaces;
using ShopMarket.Services.DTOS.BrandDto;
using ShopMarket.Services.DTOS.CategoryDto;
using ShopMarket.Services.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopMarket.Services.Services.CategoryServices
{
    public class CategoryServices(IUOW UnitOfWork, IMapper mapper) :ICategoryServices
    {
        public async Task<Result<ResponseModel>> GetAll(CancellationToken cancellationToken = default)
        {
            var category = await UnitOfWork.GenericRepository<Category>().GetAll();
            if (category != null && category.Count() > 0)
            {
                var Map = mapper.Map<List<categoryDto>>(category)
                     .OrderByDescending(c => c.CategoryName);
                Result<ResponseModel>.Success(new ResponseModel
                {
                    Message ="sucessfully",
                    IsSuccess = true,
                    StatusCode = 200,
                    Model = category
                });

            }
            return Result<ResponseModel>.Failure(new ResponseModel
            {
                Message = "An error occured or there is no ProductCategory in stock!",
                StatusCode = 400,
            });
        }

        public async Task<Result<ResponseModel>> GetById(int id, CancellationToken cancellationToken = default)
        {
            var Category = await UnitOfWork.GenericRepository<Category>().GetById(id);
            if (Category != null)
            {
                var map = mapper.Map<BrandDto>(Category);
                Result<ResponseModel>.Success(new ResponseModel
                {
                    IsSuccess = true,
                    StatusCode = 200,
                    Model = Category
                });
            }
            return Result<ResponseModel>.Failure(new ResponseModel
            {
                Message = "this Category  does not exist! ",
                StatusCode = 400

            });

        }
        public async Task<Result<ResponseModel>> Create(CreateOrUpdateCategoryDto dto, CancellationToken cancellationToken = default)
        {
            var Category = await UnitOfWork.GenericRepository<Category>().FindAsync(c => c.Name == dto.CategoryName);
            if (Category != null)
            {
                Result<ResponseModel>.Failure(new ResponseModel
                {
                    Message = "Category Already Exist!",
                    StatusCode = 400,
                    IsSuccess = false,

                });
            }
            var Categorymap = mapper.Map<Category>(dto);
            await UnitOfWork.GenericRepository<Category>().AddAsync(Categorymap);
            await UnitOfWork.Complete();

            return Result<ResponseModel>.Success(new ResponseModel
            {
                Message = "created sucessfully",
                StatusCode = 200,
                Model = Categorymap,
                IsSuccess = true,
            });
        }
        public async Task<Result<ResponseModel>> Update(int id, CreateOrUpdateCategoryDto dto, CancellationToken cancellationToken = default)
        {
            var category = await UnitOfWork.GenericRepository<Category>().FindAsync(c => c.Id == id);
            var CategoryName = await UnitOfWork.GenericRepository<Category>().FindAsync(c => c.Name == dto.CategoryName);
            if (category == null && CategoryName != null)
            {
                Result<ResponseModel>.Failure(new ResponseModel
                {
                    Message = "Category Updated Faild!",
                    StatusCode = 400,
                    IsSuccess = false,
                });
            }
            category.Name = dto.CategoryName;
            UnitOfWork.GenericRepository<Category>().Update(category);
            await UnitOfWork.Complete();
            return Result<ResponseModel>.Success(new ResponseModel
            {
                Message = $"category{category.Name} Update Secceeded",
                StatusCode = 200,
                IsSuccess = true,
                Model = category,
            });
        }

        public async Task<Result<ResponseModel>> DeleteById(int id, CancellationToken cancellationToken = default)
        {
            var category = await UnitOfWork.GenericRepository<Category>().FindAsync(c => c.Id == id);

            if (category == null)
            {
                Result<ResponseModel>.Failure(new ResponseModel
                {
                    Message = "category deleted Faild!",
                    StatusCode = 400,
                    IsSuccess = false,
                });
            }
            UnitOfWork.GenericRepository<Category>().Delete(category);
            await UnitOfWork.Complete();

            return Result<ResponseModel>.Success(new ResponseModel
            {
                Message = "category Deleted Successfuly",
                StatusCode = 200,
                IsSuccess = true,
            });
        }
    }
}
