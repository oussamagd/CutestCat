//using System;
//using System.Net;
//using System.Threading.Tasks;
//using CareApi.Controllers.Exceptions;
//using CareApi.Infrastructure.Resources;
//using Microsoft.AspNetCore.Http;
//using Microsoft.Extensions.Logging;
//using Newtonsoft.Json;

//namespace CareApi.Infrastructure.Configurations
//{
//    public class ErrorHandlingMiddleware
//    {
//        private readonly ILogger<ErrorHandlingMiddleware> _logger;
//        private readonly RequestDelegate _next;

//        public ErrorHandlingMiddleware(RequestDelegate next, ILogger<ErrorHandlingMiddleware> logger)
//        {
//            _next = next;
//            _logger = logger;
//        }

//        public async Task Invoke(HttpContext context)
//        {
//            try
//            {
//                await _next(context);
//            }
//            catch (Exception ex)
//            {
//                await HandleExceptionAsync(context, ex);
//            }
//        }

//        private Task HandleExceptionAsync(HttpContext context, Exception ex)
//        {
//            HttpStatusCode code;
//            ErrorMessageResource errorMessageResource;

//            if (ex is NotFoundException notFoundException)
//            {
//                code = HttpStatusCode.NotFound;
//                errorMessageResource =
//                    new ErrorMessageResource(notFoundException.error, notFoundException.errorDescription);
//            }
//            else if (ex is UnauthorizedException unauthorizedException)
//            {
//                code = HttpStatusCode.Unauthorized;
//                errorMessageResource =
//                    new ErrorMessageResource(unauthorizedException.error, unauthorizedException.errorDescription);
//            }
//            else
//            {
//                _logger.LogError(ex, "");
//                code = HttpStatusCode.InternalServerError;
//                errorMessageResource = new ErrorMessageResource("server_error", "Oops! Something went wrong...");
//            }


//            var result = JsonConvert.SerializeObject(errorMessageResource);
//            context.Response.ContentType = "application/json; charset=utf-8";
//            context.Response.StatusCode = (int)code;
//            return context.Response.WriteAsync(result);
//        }
//    }
//}
