using Common.Database;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Common.Controller
{
    [ApiController]
    public abstract class BaseController : ControllerBase
    {
        protected async Task RunInTransactionAsync(int serverId, Func<Task> action)
        {
            await AutoUnifiedTransactionHelper.RunAsync(HttpContext.RequestServices, serverId, action);
        }

        protected async Task<T> RunInTransactionAsync<T>(int serverId, Func<Task<T>> action)
        {
            T result = default!;
            await AutoUnifiedTransactionHelper.RunAsync(HttpContext.RequestServices, serverId, async () =>
            {
                result = await action();
            });
            return result;
        }
    }
}
