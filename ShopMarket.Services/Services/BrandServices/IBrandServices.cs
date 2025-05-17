using Org.BouncyCastle.Asn1.Ocsp;
using ShopMarket.Core.Errors;
using ShopMarket.Services.DTOS.BrandDto;
using ShopMarket.Services.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopMarket.Services.Services.BrandServices
{
    public interface IBrandServices
    {
        Task<Result<ResponseModel>> GetAll(CancellationToken cancellationToken = default);
        Task<Result<ResponseModel>> GetById(int id, CancellationToken cancellationToken = default);
        Task<Result<ResponseModel>> Create(CreateOrUpdateBrandDto dto, CancellationToken cancellationToken = default);
        Task<Result<ResponseModel>> Update(int id, CreateOrUpdateBrandDto dto, CancellationToken cancellationToken = default);
        Task<Result<ResponseModel> > DeleteById(int id, CancellationToken cancellationToken = default);
    }
}
