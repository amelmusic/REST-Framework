using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using log4net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using X.Core.Validation;

namespace X.Core.WebAPI.Filters
{
    public class ErrorFilter : ExceptionFilterAttribute
    {
        ILogger _logger;
        public ErrorFilter(ILoggerFactory loggerFactory)
        {
            _logger = loggerFactory.CreateLogger<ErrorFilter>();
        }


        public override void OnException(ExceptionContext context)
        {
            if (_logger.IsEnabled(LogLevel.Error))
            {
                _logger.Log(LogLevel.Error, context.Exception, "Something went wrong");
            }
            if (context.Exception is UserException)
            {
                context.ModelState.AddModelError("ERROR", context.Exception.Message);
                context.HttpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
            }
            else if (context.Exception is ValidationException)
            {
                var validation = context.Exception as ValidationException;
                validation.ValidationResult.ResultList.Where(x => x.Level == ValidationResultLevelEnum.Error)
                    .ToList().ForEach(x =>
                    {
                        context.ModelState.AddModelError(x.Key, x.Description);
                    });
                
                context.HttpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
            }
            else
            {
                context.ModelState.AddModelError("ERROR", "Greška na serveru");
                context.HttpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            }

            //da zadrzimo konvenciju kakvu vraća validation filter
            var list = context.ModelState.Where(x => x.Value.Errors.Count > 0).ToDictionary(x => x.Key, y => y.Value.Errors.Select(z => z.ErrorMessage));

            context.Result = new JsonResult(list);
        }
    }
}
