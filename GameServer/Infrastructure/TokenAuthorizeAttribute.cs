using GameServer.Helpers;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;

namespace GameServer.Services
{
    public class TokenAuthorizeAttribute : Attribute, IAsyncActionFilter
    {
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var authClient = context.HttpContext.RequestServices.GetRequiredService<AuthGrpcClient>();
            var token = context.HttpContext.Request.Headers["Authorization"].ToString().Replace("Bearer ", "");

            var userId = await authClient.VerifyTokenAsync(token);
            if (userId == null)
            {
                context.Result = new UnauthorizedResult();
                return;
            }

            context.HttpContext.Items["UserId"] = userId.Value;

            await next();
        }
    }
}
