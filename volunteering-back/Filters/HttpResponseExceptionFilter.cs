using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Volunteer.Filters
{
    public class HttpResponseExceptionFilter : IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
            var exception = context.Exception;
            context.Result = new JsonResult(new
            {
                statusCode = 400,
                message = exception.Message,
            })
            {
                StatusCode = 400,
            };
            context.ExceptionHandled = true;
        }
    }
}
