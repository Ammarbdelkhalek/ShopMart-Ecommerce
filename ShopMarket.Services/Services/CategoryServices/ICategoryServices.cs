using Org.BouncyCastle.Asn1.Ocsp;
using ShopMarket.Core.Errors;
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
    public interface ICategoryServices
    {
        Task<Result<ResponseModel>> GetAll(CancellationToken cancellationToken = default);
        Task<Result<ResponseModel>> GetById(int id, CancellationToken cancellationToken = default);
        Task<Result<ResponseModel>> Create(CreateOrUpdateCategoryDto dto, CancellationToken cancellationToken = default);
        Task<Result<ResponseModel>> Update(int id, CreateOrUpdateCategoryDto dto, CancellationToken cancellationToken = default);
        Task<Result<ResponseModel>> DeleteById(int id, CancellationToken cancellationToken = default);
    }
}
