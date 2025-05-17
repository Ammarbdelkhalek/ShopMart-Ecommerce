using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ShopMarket.Core.Entites;
using ShopMarket.Infrastrcuture.Specification.ProductWithSpecification;
using ShopMarket.Services.DTOS.ProductcDto;
using ShopMarket.Services.Helper;
using ShopMarket.Services.Pagination;
using ShopMarket.Services.Services.ProductServices;

namespace shopMarket.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService services;

        public ProductController(IProductService services)
        {
            this.services = services;
        }

        [HttpGet("GetAllproducts")]
        
        public async Task<ActionResult<IEnumerable<ProductDto>>> GetAllproducts([FromQuery] ProductSpecification specs,CancellationToken cancellationToken)
        {
            var products = await services.GetAllProduct(specs,cancellationToken);
            return Ok(products);
        }

        
        [HttpGet("GetPaginatdProducts")]
        public async Task<ActionResult<Pagination<ProductDto>>> GetProducts([FromQuery] ProductSpecification specification, CancellationToken cancellationToken)
        {
            var products = await services.GetAllProductwithpagination(specification,cancellationToken);
            return Ok(products);
        }
         
        [HttpGet]
        public async Task<ActionResult<ProductDetailsDto>> GetProductById(int ProductId, CancellationToken cancellationToken)
        {
            var product = await services.GetProductById(ProductId,cancellationToken);
            if (!product.IsSuccess)
                return BadRequest(new ResponseModel {
                    Message = "Some THing Went Wrong",
                    StatusCode = 400,
                
                });
                       
            return Ok(product);
        }

        [HttpPost]
        public async Task<IActionResult> CreateProduct([FromForm] CreateProductDto dto, CancellationToken cancellationToken)
        {
            var product = await services.CreateProduct(dto,cancellationToken);
            if (!product.IsSuccess)
                return BadRequest(new ResponseModel  
                {
                    Message = product.Value.Message,
                    StatusCode = product.Value.StatusCode,

                });
            return Ok( new { message  = product.Value.Message,model = product.Value.Model});
        }

        [HttpPut]
        public async Task<IActionResult> UpdateProduct(int ProductId, [FromForm] UpdateProductDto dto, CancellationToken cancellationToken)
        {
            var product = await services.UpdateProduct(ProductId, dto);
            if (!product.IsSuccess)
                return BadRequest(new ResponseModel
                {
                    Message = product.Value.Message,
                    StatusCode = product.Value.StatusCode,

                });
            return Ok(product.Value.Message);
        }

        [HttpDelete]
        public async Task<IActionResult> RemoveProduct(int productId, CancellationToken cancellationToken)
        {
            var product = await services.DeleteProduct(productId);
            if (!product.IsSuccess)
                return BadRequest(new ResponseModel{ Message = product.Value.Message, StatusCode = product.Value.StatusCode }  );
            return Ok(product.Value.Message);
        }

    }
}

