using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Extensions;

namespace core_request_response_middleware.Middlewares
{
    public class RequestHandlingMiddleware
    {
        private readonly RequestDelegate _next;

        public RequestHandlingMiddleware(RequestDelegate next)
        {
            this._next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            var requestBodyStream = new MemoryStream();
            var originalRequestBody = context.Request.Body;

            await context.Request.Body.CopyToAsync(requestBodyStream);
            requestBodyStream.Seek(0, SeekOrigin.Begin);

            var url = context.Request.GetDisplayUrl();
            var requestBodyText = new StreamReader(requestBodyStream).ReadToEnd();

            requestBodyStream.Seek(0, SeekOrigin.Begin);
            context.Request.Body = requestBodyStream;

            await _next(context);
            context.Request.Body = originalRequestBody;
        }
    }

    public static class RequestHandlingMiddlewareMiddlewareExtensions
    {
        public static IApplicationBuilder UseRequestHandlingMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<RequestHandlingMiddleware>();
        }
    }
}