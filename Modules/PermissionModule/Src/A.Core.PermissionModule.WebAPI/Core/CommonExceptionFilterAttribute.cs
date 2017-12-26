using A.Core.Validation;
using log4net;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http.Filters;


namespace A.Core.PermissionModule.WebAPI.Core
{
    public class CommonExceptionFilterAttribute : ExceptionFilterAttribute
    {
        private static ILog Log = log4net.LogManager.GetLogger(typeof(CommonExceptionFilterAttribute));

        public JsonSerializerSettings JsonSerializerSettings { get; set; }

        public CommonExceptionFilterAttribute()
        {

        }

        public override void OnException(HttpActionExecutedContext context)
        {
            if (Log.IsErrorEnabled)
            {
                if(context.Request.Properties.ContainsKey("RequestId"))
                {
                   var requestId = context.Request.Properties["RequestId"].ToString();
                   log4net.LogicalThreadContext.Properties["RequestId"] = requestId;
                }
                if (context.Request.Properties.ContainsKey("UserName"))
                {
                    var userName = context.Request.Properties["UserName"].ToString();
                    log4net.LogicalThreadContext.Properties["UserName"] = userName;
                }
                
                Log.Error(context.Exception.Message, context.Exception);
            }

            if (context.Exception is ValidationException)
            {
                //context.Controller.ViewData.ModelState
                context.Response = context.Request.CreateResponse<ValidationResult>(System.Net.HttpStatusCode.BadRequest, ((ValidationException)context.Exception).ValidationResult);
            }
            else if (context.Exception is UserException)
            {
                ValidationResult validation = new ValidationResult();
                validation.ResultList.Add(new ValidationResultItem() { Key = "USER_EXCEPTION", Description = context.Exception.Message, Level = ValidationResultLevelEnum.Error });
                //context.Controller.ViewData.ModelState
                context.Response = context.Request.CreateResponse<ValidationResult>(System.Net.HttpStatusCode.BadRequest, validation);
            }
            else
            {
#if !DEBUG
                ValidationResult validation = new ValidationResult();
                validation.ResultList.Add(new ValidationResultItem() { Key = "UNKOWN_EXCEPTION", Description = "Unknown error while processing request", Level = ValidationResultLevelEnum.Error });
                //context.Controller.ViewData.ModelState
                context.Response = context.Request.CreateResponse<ValidationResult>(System.Net.HttpStatusCode.BadRequest, validation);
#else
                ValidationResult validation = new ValidationResult();
                validation.ResultList.Add(new ValidationResultItem() { Key = "UNKOWN_EXCEPTION", Description = context.Exception.ToString(), Level = ValidationResultLevelEnum.Error });
                //context.Controller.ViewData.ModelState
                context.Response = context.Request.CreateResponse<ValidationResult>(System.Net.HttpStatusCode.BadRequest, validation);
#endif

            }
        }
    }
}