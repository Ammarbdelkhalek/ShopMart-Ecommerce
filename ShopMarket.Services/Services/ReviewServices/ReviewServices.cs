using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ShopMarket.Core.Data;
using ShopMarket.Core.Entites;
using ShopMarket.Core.Errors;
using ShopMarket.Services.DTOS.ReviewDto;
using ShopMarket.Services.Helper;
using ShopMarket.Services.Pagination;

namespace ShopMarket.Services.Services.ReviewServices
{
    public class ReviewServices : IReviewServices
    {
        private readonly ApplicationDbContext context;
        
        private  IMapper mapper;

        public ReviewServices(ApplicationDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public async Task<Result<IEnumerable<ReviewDto>>> GetAllAsync()
        {
            throw new NotImplementedException();
        }
        public Task<Result<ResponseModel>> AddAsync(CreateOrUpdateReview Dto)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Result<IEnumerable<ReviewDto>>> GetByCustomerIdAsync(int customerId)
        {
            throw new NotImplementedException();
        }

        public Task<Result<ReviewDto>> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Result<IEnumerable<ReviewDto>>> GetByProductIdAsync(int productId)
        {
            throw new NotImplementedException();
        }

        public Task<Result<IEnumerable<Review>>> GetFilteredAsync(int? productId, int? customerId, int? minRating)
        {
            throw new NotImplementedException();
        }

        public Task<Result<Pagination<ReviewDto>>> GetPaginatedAsync(int? productId, int? customerId, int? minRating, int pageNumber, int pageSize)
        {
            throw new NotImplementedException();
        }

        public Task<int> GetTotalCountAsync(int? productId, int? customerId, int? minRating)
        {
            throw new NotImplementedException();
        }

        public Task<Result<ResponseModel>> UpdateAsync(CreateOrUpdateReview dto)
        {
            throw new NotImplementedException();
        }
    }
}
