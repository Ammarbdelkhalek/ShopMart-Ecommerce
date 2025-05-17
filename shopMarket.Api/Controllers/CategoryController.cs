using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ShopMarket.Services.DTOS.CategoryDto;
using ShopMarket.Services.Helper;
using ShopMarket.Services.Services.CategoryServices;
using Swashbuckle.AspNetCore.Annotations;

namespace shopMarket.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryServices services;

        public CategoryController(ICategoryServices services)
        {
            this.services = services;
        }

        [SwaggerOperation(Summary = "{ List Of  Category  Using Drop down List }")]
        [HttpGet("GetAllCategory")]
        public async Task<ActionResult<IEnumerable<categoryDto>>> GetAllCategory(CancellationToken cancellationToken)
        {
            var category = await services.GetAll(cancellationToken);
            if (!category.IsSuccess)
                return BadRequest(new ResponseModel{ Message = category.Value.Message, StatusCode = category.Value.StatusCode });
            return Ok(category.Value.Model);
        }


        [HttpGet]
        public async Task<ActionResult<categoryDto>> GetCatgeoryById(int categoryId, CancellationToken cancellationToken)
        {
            var category = await services. GetById(categoryId, cancellationToken);
            if (!category.IsSuccess)
                return BadRequest(new ResponseModel{ Message = category.Value.Message, StatusCode = category.Value.StatusCode });
            return Ok(category.Value.Model);
        }

        [HttpPost]
        public async Task<IActionResult> CreateCategory(CreateOrUpdateCategoryDto dto, CancellationToken cancellationToken)
        {
            var category = await services.Create(dto, cancellationToken);
            if (!category.IsSuccess)
                return BadRequest(new ResponseModel{ Message = category.Value.Message, StatusCode = category.Value.StatusCode });
            return Ok(category.Value.Model);

        }
        [HttpPut]
        public async Task<IActionResult> UpdateCategory(int CategoryId, CreateOrUpdateCategoryDto dto, CancellationToken cancellationToken)
        {
            var category = await services.Update(CategoryId, dto, cancellationToken);
            if (!category.IsSuccess)
                return BadRequest(new ResponseModel{ Message = category.Value.Message, StatusCode = category.Value.StatusCode });
            return Ok(category.Value.Model);
        }

        [HttpDelete]
        public async Task<IActionResult> RemoveCategory(int CategoryId, CancellationToken cancellationToken)
        {
            var category = await services.DeleteById(CategoryId, cancellationToken);
            if (!category.IsSuccess)
                return BadRequest(new ResponseModel{ Message = category.Value.Message, StatusCode = category.Value.StatusCode });
            return Ok(category.Value.Message);
        }
    }
}

