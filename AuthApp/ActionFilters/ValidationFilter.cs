using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace AuthApp.ActionFilters;

public class ValidationFilter : IActionFilter
{
    public void OnActionExecuting(ActionExecutingContext context)
    {

        var controller = context.RouteData.Values["controller"];
        var action = context.RouteData.Values["action"];
        
        var dto = context.ActionArguments.
            SingleOrDefault(arg =>
            arg.ToString().Contains("Dto")).Value;

        if (dto == null)
        {
            context.Result = new BadRequestObjectResult(
                $"A dto object can't be null. Action - {action}, controller - {controller}");
            
            return;
        }

        if (context.ModelState.IsValid is false)
            context.Result = new UnprocessableEntityObjectResult(context.ModelState);

    }

    public void OnActionExecuted(ActionExecutedContext context)
    {
        
    }
}