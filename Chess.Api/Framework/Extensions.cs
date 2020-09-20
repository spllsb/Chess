
using Microsoft.AspNetCore.Builder;

namespace Chess.Api.Framework
{
    public static class Extensions
    {
        public static IApplicationBuilder UseMyExceptionHandler(this IApplicationBuilder builder)
            => builder.UseMiddleware(typeof(ExceptionHandlerMiddlewere));
    }
}