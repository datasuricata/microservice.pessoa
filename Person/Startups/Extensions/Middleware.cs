using Microsoft.AspNetCore.Builder;
using Person.Startups.Middlewares;

namespace Person.Startups.Extensions {
    public static class Middleware {
        public static IApplicationBuilder UseUnitOfWork(this IApplicationBuilder builder) {
            return builder.UseMiddleware<MiddlewareUnitOfWork>();
        }

        // todo (activate filter exception)
        //public static IApplicationBuilder UseFilterException(this IApplicationBuilder builder) {
        //    return builder.UseMiddleware<MiddlewareFilterException>();
        //}
    }
}
