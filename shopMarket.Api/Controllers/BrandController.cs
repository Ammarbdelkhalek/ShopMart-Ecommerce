using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ShopMarket.Services.DTOS.BrandDto;
using ShopMarket.Services.Helper;
using ShopMarket.Services.Services.BrandServices;
using Swashbuckle.AspNetCore.Annotations;

namespace shopMarket.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BrandController : ControllerBase
    {
        private readonly IBrandServices services;

        public BrandController(IBrandServices services)
        {
            this.services = services;
        }

        [SwaggerOperation(Summary = "{ List Of Product Brand Using Drop down List }")]
        [HttpGet("GetAllBrand")]
        public async Task<ActionResult<IEnumerable<BrandDto>>> GetAllBrand(CancellationToken cancellationToken)
        {
            var brand = await services.GetAll(cancellationToken);
            if (!brand.IsSuccess)
                return BadRequest(new ResponseModel{ Message = brand.Value.Message, StatusCode = brand.Value.StatusCode });
            return Ok(brand.Value.Model);
        }

        [HttpGet]
        public async Task<ActionResult<BrandDto>> GetBrandById(int BrandId, CancellationToken cancellationToken)
        {
            var brand = await services.GetById(BrandId, cancellationToken);
            if (!brand.IsSuccess)
                return BadRequest(new ResponseModel{ Message = brand.Value.Message, StatusCode = brand.Value.StatusCode });
            return Ok(brand.Value.Model);
        }

        [HttpPost]
        public async Task<IActionResult> CreateBrand(CreateOrUpdateBrandDto dto, CancellationToken cancellationToken)
        {
            var brand = await services.Create(dto, cancellationToken);
            if (!brand.IsSuccess)
                return BadRequest(new ResponseModel{ Message = brand.Value.Message, StatusCode = brand.Value.StatusCode });
            return Ok(brand.Value.Message);

        }

        [HttpPut]
        public async Task<IActionResult> UpdateBrand(int BrandId, CreateOrUpdateBrandDto dto, CancellationToken cancellationToken)
        {
            var brand = await services.Update(BrandId, dto, cancellationToken);
            if (!brand.IsSuccess)
                return BadRequest(new ResponseModel{ Message = brand.Value.Message, StatusCode = brand.Value.StatusCode });
            return Ok(brand.Value.Message);
        }

        [HttpDelete]
        public async Task<IActionResult> RemoveBrand(int BrandId, CancellationToken cancellationToken)
        {
            var brand = await services.DeleteById(BrandId, cancellationToken);
            if (!brand.IsSuccess)
                return BadRequest(new ResponseModel{ Message = brand.Value.Message, StatusCode = brand.Value.StatusCode });
            return Ok(brand.Value.Message);
        }
    }
}

    
 
