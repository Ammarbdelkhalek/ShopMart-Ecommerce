using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace shopMarket.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
     
    
    public class testController : ControllerBase
    { 
        [HttpGet]
         
        public string Get() {
            return "Welcome Home Baby";
        
        }
    }
}
