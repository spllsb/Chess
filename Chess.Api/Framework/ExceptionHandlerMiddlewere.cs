using System;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace Chess.Api.Framework
{
    public class ExceptionHandlerMiddlewere
    {
        private readonly RequestDelegate _next;
        public ExceptionHandlerMiddlewere(RequestDelegate next)
        {
            _next = next;
        }
        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex) 
            {
                await HandleExceptionAsync(context,ex);
            }
        }
        private static Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            var errorCode = "error";
            var statusCode = HttpStatusCode.BadRequest;
            var exceptionType = exception.GetType();
            switch(exception)
            {
                case Exception e when exceptionType == typeof(UnauthorizedAccessException):
                    statusCode = HttpStatusCode.Unauthorized;
                    break;
                //todo custom exception
                default:
                    statusCode = HttpStatusCode.InternalServerError;
                    break;
            }
            //UWAGA
            //Nie używa się takiego typu wyjątków na środowisku proudkyjnym. Wyjątki taki mogą zwrać zby dużo informaji o aplikacji
            var response = new { code = errorCode, message = exception.Message};
            var payload = JsonConvert.SerializeObject(response);
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)statusCode;

            return context.Response.WriteAsync(payload);
        }
    }
}