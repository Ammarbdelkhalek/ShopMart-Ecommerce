using AutoMapper;
using Azure;
using Microsoft.AspNetCore.Http;
using ShopMarket.Core.Data;
using ShopMarket.Core.Entites;
using ShopMarket.Core.Errors;
using ShopMarket.Infrastrcuture.interfaces;
using ShopMarket.Infrastrcuture.Specification.ProductWithSpecification;
using ShopMarket.Services.DTOS.ProductcDto;
using ShopMarket.Services.Helper;
using ShopMarket.Services.Pagination;
using Microsoft.AspNetCore.Hosting;
using Org.BouncyCastle.Asn1.Ocsp;
using Org.BouncyCastle.Asn1.Crmf;
using ShopMarket.Services.Utilities;



namespace ShopMarket.Services.Services.ProductServices
{
    public class ProductServices:IProductService
    {
    private readonly ApplicationDbContext context;
    private readonly IUOW UnitOfWork;
    private readonly IMapper mapper;
    private new List<string> allowextention = new List<string>() { ".jpg", ".png", "jpeg" };
    private long maxsizeimage = 109951163;//2MB
    private readonly IHostingEnvironment webHost;

        public ProductServices(ApplicationDbContext context, IUOW unitOfWork, IMapper mapper, List<string> allowextention, long maxsizeimage)
        {
            this.context = context;
            UnitOfWork = unitOfWork;
            this.mapper = mapper;
            this.allowextention = allowextention;
            this.maxsizeimage = maxsizeimage;
        }

        public async Task<Result<IEnumerable<ProductDto>>> GetAllProduct(ProductSpecification specification, CancellationToken cancellationToken)
        {
            var spec = new ProductWithCatgeoryAndBrandAndTypeSpecs(specification);
            var products = await UnitOfWork.GenericRepository<Product>().GetAllEntityWithSpecs(spec);
            var productMap = mapper.Map<List<ProductDto>>(products);
             return  Result<IEnumerable<ProductDto>>.Success (productMap);
             
             
        }
        public async Task<Result<Pagination<ProductDto>>> GetAllProductwithpagination(ProductSpecification specification, CancellationToken cancellationToken)
        {
            var spec = new ProductWithCatgeoryAndBrandAndTypeSpecs(specification);
            var products = await UnitOfWork.GenericRepository<Product>().GetEntityWithSpec(spec);
            var count = await UnitOfWork.GenericRepository<Product>().GetCount(spec);
            var productMap = mapper.Map<List<ProductDto>>(products);
            var paginationProduct = new Pagination<ProductDto>(specification.PageSize, specification.PageIndex, count, productMap);
            return Result<Pagination<ProductDto>>.Success(paginationProduct);

        }
        public async Task<Result<ProductDto>> GetProductById(int? id, CancellationToken cancellationToken)
        {
            var spec = new ProductWithCatgeoryAndBrandAndTypeSpecs(id);
            var product = await UnitOfWork.GenericRepository<Product>().GetEntityWithSpec(spec);
            var productMap = mapper.Map<ProductDto>(product);
            return Result<ProductDto>.Success(productMap);
        }
        
        public async  Task<Result<ResponseModel>> CreateProduct(CreateProductDto dto, CancellationToken cancellationToken)
        {
          
            if (await UnitOfWork.GenericRepository<Product>().FindAsync(c => c.Name == dto.ProductName) != null)
            {
                return Result<ResponseModel>.Failure(new ResponseModel { Message = "Product Already Exsit!",StatusCode = 400 });
               

            }
            if (!await UnitOfWork.GenericRepository<Category>().Isvalid(dto.CategoryId))
            {
                return Result<ResponseModel>.Failure(new ResponseModel{ Message = "Category Not Found!", StatusCode = 400 });
                
            }
             
            if (!await UnitOfWork.GenericRepository<Brand>().Isvalid(dto.BrandId))
            {
                return Result<ResponseModel>.Failure(new ResponseModel{ Message = "Brand Not Found!", StatusCode = 400 });
                 
            }

            var uploadfile = Path.Combine(webHost.WebRootPath, "Images/Products");
            var uniquefile = Guid.NewGuid().ToString() + "_" + dto.Image.FileName;
            var PathFile = Path.Combine(uploadfile, uniquefile);
     
            var product = mapper.Map<Product>(dto);
            await dto.Image.ToByteArray();
            product.Image = "Images/Products/" + uniquefile.ToString();
            await UnitOfWork.GenericRepository<Product>().AddAsync(product);
            await UnitOfWork.Complete();
            return Result<ResponseModel>.Success(new ResponseModel { Message = "Product Created Successfuly", StatusCode = 200, IsSuccess = true, Model = product });
        
                
            
                 
        }
        public async Task<Result<ResponseModel>> UpdateProduct(int id, UpdateProductDto dto, CancellationToken cancellationToken)
        {
            var product = await UnitOfWork.GenericRepository<Product>().FindAsync(c => c.Id == id);
            if(product == null)
            {
                return Result<ResponseModel>.Failure(new ResponseModel {
                     Message=  "Product NOt Found",
                      StatusCode = 400 });
                
            }
            var uploadfile = Path.Combine(webHost.WebRootPath, "Images/Products");
            var uniquefile = Guid.NewGuid().ToString() + "_" + dto.Image.FileName;
            var PathFile = Path.Combine(uploadfile, uniquefile);
            await dto.Image.ToByteArray();
    
            product.Image = "Images/Products/" + uniquefile.ToString();

            product.Name = dto.ProductName;
            product.Description = dto.Description;
            product.Price = dto.Price;
            product.CategoryId = dto.CategoryId;
            product.BrandId = dto.BrandId;
             

            UnitOfWork.GenericRepository<Product>().Update(product);
            await UnitOfWork.Complete();

            return Result<ResponseModel>.Success(new ResponseModel
            {
                 Message ="Product Created Successfuly",
                 StatusCode = 200,
                 IsSuccess= true,
                 Model =product
            });

        }

        public async Task<Result<ResponseModel>> DeleteProduct(int id, CancellationToken cancellationToken)
        {
            var product = await UnitOfWork.GenericRepository<Product>().FindAsync(c => c.Id == id);
            if (product == null)
            {
                return Result<ResponseModel>.Failure(new ResponseModel
                {
                    Message = "Product not found",
                    StatusCode =  400
                });
            }
            UnitOfWork.GenericRepository<Product>().Delete(product);
            await UnitOfWork.Complete();

            return Result<ResponseModel>.Success(new ResponseModel
            {
                Message = "Product Deleted Sucessfully",
                StatusCode = 200
            });
             

        }

        
    }
}
