using System;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace CutestCat
{
    public class ErrorHandlingMiddleware
    {
        private readonly RequestDelegate _next;

        public ErrorHandlingMiddleware(RequestDelegate next)
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
                await HandleExceptionAsync(context, ex);
            }
        }

        // à faire evoluer (ajout loggeur, gestion differents cas d'exception...)
        private Task HandleExceptionAsync(HttpContext context, Exception ex)
        {
            ErrorMessageResource errorMessageResource;
            HttpStatusCode code;
            if (ex is UnauthorizedAccessException)
            {
                errorMessageResource = new ErrorMessageResource("unauthorized", "you are not authorized to access");
                code = HttpStatusCode.Unauthorized;
            }
            else
            {
                errorMessageResource = new ErrorMessageResource("server_error", "Oops! Something went wrong...");
                code = HttpStatusCode.InternalServerError;
            }

            var result = JsonConvert.SerializeObject(errorMessageResource);
            context.Response.ContentType = "application/json; charset=utf-8";
            context.Response.StatusCode = (int)code;
            return context.Response.WriteAsync(result);
        }
    }


    public class ErrorMessageResource
    {
        public string Code { get; set; }
        public string Message { get; set; }
        public ErrorMessageResource(string code, string message)
        {
            Message = message;
            Code = code;
        }
    }
}
