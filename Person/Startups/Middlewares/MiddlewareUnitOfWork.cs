using Microsoft.AspNetCore.Http;
using Person.Services.Base;
using System.Threading.Tasks;

namespace Person.Startups.Middlewares {
    public class MiddlewareUnitOfWork {
        private readonly RequestDelegate _next;

        public MiddlewareUnitOfWork(RequestDelegate next) {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext httpContext) {
            await _next(httpContext);
            var core = (IServiceBase)httpContext.RequestServices.GetService(typeof(IServiceBase));
            await core.Commit();
        }
    }
}
