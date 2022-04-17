﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Serilog;
using System;
using System.Net;
using Vol.Infrastructure;
using Vol.Models;

namespace Vol.Filters
{
    public class ExceptionFilter : IActionFilter, IOrderedFilter
    {
        public int Order { get; } = int.MaxValue - 10;

        public void OnActionExecuting(ActionExecutingContext context) { }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            if (context.Exception is BusinessException businessException)
            {
                context.Result = new ObjectResult(Response.Error(businessException.Message))
                {
                    StatusCode = (int)HttpStatusCode.OK,
                };
                context.ExceptionHandled = true;
            }
            else if (context.Exception is Exception ex)
            {
                string code = Guid.NewGuid().ToString();
                Log.Logger.Error(ex, $"Internal error code = {code}");

                context.Result = new ObjectResult(Response.Error(code))
                {
                    StatusCode = (int)HttpStatusCode.InternalServerError,
                };
                context.ExceptionHandled = true;
            }
        }
    }
}
