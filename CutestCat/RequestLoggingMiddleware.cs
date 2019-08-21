//using System;
//using System.Diagnostics;
//using System.Threading.Tasks;
//using Microsoft.AspNetCore.Http;
//using Microsoft.Extensions.Logging;
//using Serilog.Context;

//namespace CareApi.Infrastructure.Configurations
//{
//    public class RequestLoggingMiddleware
//    {
//        private readonly ILogger<RequestLoggingMiddleware> _logger;
//        private readonly RequestDelegate next;

//        public RequestLoggingMiddleware(RequestDelegate next, ILogger<RequestLoggingMiddleware> logger)
//        {
//            this.next = next;
//            _logger = logger;
//        }

//        public async Task Invoke(HttpContext context /* other dependencies */)
//        {
//            var stopwatch = new Stopwatch();
//            stopwatch.Start();
//            LogContext.PushProperty("requestId", Guid.NewGuid());
//            _logger.LogInformation("REQUEST START");
//            await next(context);
//            stopwatch.Stop();
//            LogContext.PushProperty("timeExecution", stopwatch.ElapsedMilliseconds);
//            _logger.LogInformation("REQUEST END");
//        }
//    }
//}
