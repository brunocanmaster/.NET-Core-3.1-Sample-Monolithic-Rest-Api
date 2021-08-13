using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace MonolithicSampleRestApi.FilterAttributes
{
    public class ApiAuthorizationAttribute : ActionFilterAttribute, IActionFilter
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {

            bool isThereAuthorization = context.HttpContext.Request.Headers.ContainsKey("Authorization");
            bool isValid = isThereAuthorization && context.HttpContext.Request.Headers["Authorization"].Equals(Settings.MonolithicSampleRestApiSdkSettings.BasicToken);

            if (!isValid)
            {
                context.Result = new UnauthorizedObjectResult("");
            }

            base.OnActionExecuting(context);
        }

        public override void OnActionExecuted(ActionExecutedContext context)
        {
            base.OnActionExecuted(context);
        }
    }
}
