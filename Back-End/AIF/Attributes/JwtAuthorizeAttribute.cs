using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Threading.Tasks;
using AIF.Services;

namespace AIF.Attributes
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class JwtAuthorizeAttribute : Attribute, IAsyncAuthorizationFilter
    {
        public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
        {
            var authService = (IAuthService)context.HttpContext.RequestServices.GetService(typeof(IAuthService));

            var token = context.HttpContext.Request.Headers["Authorization"].ToString();
            var isValid = await authService.ValidateTokenAsync(token);

            if (!isValid)
            {
                context.Result = new UnauthorizedObjectResult("Invalid or missing token");
                return;
            }
        }
    }
}

