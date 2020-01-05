
using Microsoft.AspNetCore.Builder;

namespace Chess.WebSite.Framework
{
    public static class Extensions
    {
        public static IApplicationBuilder UseMyExceptionHandler(this IApplicationBuilder builder)
            => builder.UseMiddleware(typeof(ExceptionHandlerMiddlewere));
    }
}