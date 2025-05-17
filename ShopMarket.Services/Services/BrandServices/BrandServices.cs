using AutoMapper;
using ShopMarket.Core.Entites;
using ShopMarket.Core.Errors;
using ShopMarket.Infrastrcuture.interfaces;
using ShopMarket.Services.DTOS.BrandDto;
using ShopMarket.Services.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopMarket.Services.Services.BrandServices
{
    public class BrandServices(IUOW UnitOfWork, IMapper mapper) : IBrandServices
    {

        public async Task<Result<ResponseModel>> GetAll(CancellationToken cancellationToken = default)
        {
            var Brand = await UnitOfWork.GenericRepository<Brand>().GetAll();
            if (Brand != null && Brand.Count() > 0)
            {
                var Map = mapper.Map<List<BrandDto>>(Brand)
                     .OrderByDescending(c => c.BrandName);
                Result<ResponseModel>.Success(new ResponseModel
                {
                    IsSuccess = true,
                    StatusCode = 200,
                    Model = Brand
                });

            }
            return Result<ResponseModel>.Failure(new ResponseModel
            {
                Message = "An error occured or there is no ProductBrand in stock!",
                StatusCode = 400,
            });
        }

        public async Task<Result<ResponseModel>> GetById(int id, CancellationToken cancellationToken = default)
        {
            var brand = await  UnitOfWork.GenericRepository<Brand>().GetById(id);
            if (brand != null)
            {
                var map = mapper.Map<BrandDto>(brand);
                Result<ResponseModel>.Success(new ResponseModel
                {
                    IsSuccess = true,
                    StatusCode = 200,
                    Model = brand
                });
            }
            return Result<ResponseModel>.Failure(new ResponseModel {
                Message = "this Brand  does not exist! ",
                StatusCode = 400
            
            });

            }
        public async Task<Result<ResponseModel>> Create(CreateOrUpdateBrandDto dto, CancellationToken cancellationToken = default)
        {
            var brand = await UnitOfWork.GenericRepository<Brand>().FindAsync(c => c.Name == dto.BrandName);
            if (brand != null)
            {
                Result<ResponseModel>.Failure(new ResponseModel
                {
                    Message = "Brand Already Exist!",
                    StatusCode = 400,
                    IsSuccess = false,

                });
            }
            var brandmap = mapper.Map<Brand>(dto);
            await UnitOfWork.GenericRepository<Brand>().AddAsync(brandmap);
            await UnitOfWork.Complete();

            return Result<ResponseModel>.Success(new ResponseModel
            {
                Message = "created sucessfully",
                StatusCode = 200,
                Model = brand,
                IsSuccess = true,
            });
        }
        public async Task<Result<ResponseModel>> Update(int id, CreateOrUpdateBrandDto dto, CancellationToken cancellationToken = default)
        {
            var brand = await UnitOfWork.GenericRepository<Brand>().FindAsync(c => c.Id == id);
            var brandName = await UnitOfWork.GenericRepository<Brand>().FindAsync(c => c.Name == dto.BrandName);
            if (brand == null&&brandName!=null)
            {
                Result<ResponseModel>.Failure(new ResponseModel 
                { 
                    Message = "Brand Updated Faild!",
                    StatusCode = 400,
                    IsSuccess =false,
                });
            }
            brand.Name = dto.BrandName;
            UnitOfWork.GenericRepository<Brand>().Update(brand);
            await UnitOfWork.Complete();
            return Result<ResponseModel>.Success(new ResponseModel { 
                Message = $"Brand{brand.Name} Update Secceeded",
                StatusCode = 200,
                IsSuccess=true,
                Model = brand,
            });
        }

        public async Task<Result<ResponseModel>> DeleteById(int id, CancellationToken cancellationToken = default)
        {
            var brand = await UnitOfWork.GenericRepository<Brand>().FindAsync(c => c.Id == id);
            
            if (brand == null )
            {
                Result<ResponseModel>.Failure(new ResponseModel
                {
                    Message = "Brand deleted Faild!",
                    StatusCode = 400,
                    IsSuccess = false,
                });
            }
             UnitOfWork.GenericRepository<Brand>().Delete(brand);
            await UnitOfWork.Complete();

            return Result<ResponseModel>.Success(new ResponseModel
            {
                Message = "Brand Deleted Successfuly",
                StatusCode = 200,
                IsSuccess =true,
            });
        }

      

       

       
    }
}
