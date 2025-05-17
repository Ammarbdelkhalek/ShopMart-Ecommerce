using Azure;
using ShopMarket.Core.Entites;
using ShopMarket.Core.Errors;
using ShopMarket.Services.DTOS.ReviewDto;
using ShopMarket.Services.Helper;
using ShopMarket.Services.Pagination;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopMarket.Services.Services.ReviewServices
{
    public interface IReviewServices
    {
        Task<Result<ReviewDto>> GetByIdAsync(int id);
        Task<Result<IEnumerable<ReviewDto>>> GetAllAsync();
        Task<Result<IEnumerable<ReviewDto>>> GetByProductIdAsync(int productId);
        Task<Result<IEnumerable<ReviewDto>>> GetByCustomerIdAsync(int customerId);
        Task<Result<IEnumerable<Review>>> GetFilteredAsync(int? productId, int? customerId, int? minRating);
        Task< Result<Pagination<ReviewDto>>> GetPaginatedAsync(int? productId, int? customerId, int? minRating, int pageNumber, int pageSize);
        Task<int> GetTotalCountAsync(int? productId, int? customerId, int? minRating);
        Task<Result<ResponseModel>> AddAsync(CreateOrUpdateReview Dto);
        Task<Result<ResponseModel>> UpdateAsync(CreateOrUpdateReview dto);
        Task DeleteAsync(int id);
    }
}
