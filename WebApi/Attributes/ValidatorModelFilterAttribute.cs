using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace WebApi.Attributes;

public class ValidatorModelFilterAttribute : ActionFilterAttribute
{
    /// <summary>
    /// Action for checking model state
    /// </summary>
    /// <param name="context"></param>
    public override void OnActionExecuting(ActionExecutingContext context)
    {
        if (context.ModelState.IsValid)
        {
            return;
        }

        var errors = context.ModelState.Values.Where(v => v.Errors.Any())
            .SelectMany(v => v.Errors)
            .Select(v => v.ErrorMessage)
            .ToList();

        List<ProblemDetails> errorResponses =
            errors.Select(errorMessage => new ProblemDetails() { Detail = errorMessage }).ToList();

        context.Result = new JsonResult(errorResponses) { StatusCode = 400 };
    }
}