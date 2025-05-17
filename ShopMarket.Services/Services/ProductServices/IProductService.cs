using Org.BouncyCastle.Asn1.Ocsp;
using ShopMarket.Core.Errors;
using ShopMarket.Infrastrcuture.Specification.ProductWithSpecification;
using ShopMarket.Services.DTOS.ProductcDto;
using ShopMarket.Services.Helper;
using ShopMarket.Services.Pagination;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopMarket.Services.Services.ProductServices
{
    public interface IProductService
    {
        Task<Result<IEnumerable<ProductDto>>> GetAllProduct(ProductSpecification specification,CancellationToken cancellationToken =default);
        Task<Result<ProductDto>> GetProductById(int? id, CancellationToken cancellationToken = default);
        Task<Result<Pagination<ProductDto>>> GetAllProductwithpagination(ProductSpecification specification, CancellationToken cancellationToken = default);
        Task<Result<ResponseModel>> CreateProduct(CreateProductDto dto, CancellationToken cancellationToken = default);
        Task<Result<ResponseModel>> UpdateProduct(int id, UpdateProductDto dto, CancellationToken cancellationToken = default);
        Task<Result<ResponseModel>> DeleteProduct(int id, CancellationToken cancellationToken = default);

    }
}
