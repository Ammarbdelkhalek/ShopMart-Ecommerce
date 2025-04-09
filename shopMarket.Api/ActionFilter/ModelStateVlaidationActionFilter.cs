using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Serilog;

namespace shopMarket.Api.ActionFilter
{
    public class ModelStateVlaidationActionFilter:ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            Log.Information("here check id the model follow the validation rule or not ");
            if(!context.ModelState.IsValid) {
                var error = context.ModelState.Where(x => x.Value.Errors.Any()).Select(x => new
                {
                    field = x.Key,
                    Error = x.Value.Errors.Select(x => x.ErrorMessage)

                }) ;
                Log.Error($"Model validation failed : > {error}");
                context.Result = new BadRequestObjectResult(new
                {
                    Error = error,
                    Message = "Model validation failed"
                });
            }
            Log.Information("checking if the model is null or not if null throw exception BadRequest()");
            if(context.ActionArguments.Any(x=>x.Value== null) )
            {
                Log.Error("object should't be null");
                context.Result = new BadRequestObjectResult("object should't be null");
            }
            
        }
    }
}
