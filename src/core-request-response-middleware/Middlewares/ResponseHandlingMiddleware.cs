using System;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace core_request_response_middleware.Middlewares
{
    public class ResponseHandlingMiddleware
    {
        private readonly RequestDelegate _next;

        public ResponseHandlingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            var currentBody = context.Response.Body;
            MemoryStream memoryStream = null;
            try
            {
                var ct = context.Request.ContentType;
                memoryStream = new MemoryStream();
                context.Response.Body = memoryStream;

                await _next(context);

                var isJsonResponse = IsJsonResponseContentType(context);

                if (isJsonResponse)
                {
                    context.Response.Body = currentBody;
                    memoryStream?.Seek(0, SeekOrigin.Begin);

                    var readToEnd = new StreamReader(memoryStream).ReadToEndAsync();
                    var content = JsonConvert.DeserializeObject(await readToEnd);
                    var response = context.Response;
                    response.ContentType = "application/json";

                    await response.WriteAsync(JsonConvert.SerializeObject(
                        CommonApiResponse.Create((HttpStatusCode)response.StatusCode,
                            content))).ConfigureAwait(false);
                }
                else
                {
                    context.Response.Body = currentBody;
                }

            }
            catch (Exception ex)
            {
                context.Response.Body = currentBody;
                await HandleExceptionAsync(context, ex);
            }
            finally
            {
                memoryStream?.Dispose();
            }
        }

        private bool IsJsonResponseContentType(HttpContext context)
        {
            return (context.Response.ContentType != null) &&
                   (context.Response.ContentType.StartsWith(@"application/json"));
        }

        private static async Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            if (exception == null) return;

            //start:
            ////var code = HttpStatusCode.InternalServerError;

            ////if (ex is MyNotFoundException) code = HttpStatusCode.NotFound;
            ////else if (ex is MyUnauthorizedException) code = HttpStatusCode.Unauthorized;
            ////else if (ex is MyException) code = HttpStatusCode.BadRequest;

            ////await WriteExceptionAsync(context, ex, code).ConfigureAwait(false);
            //end:

            var code = context.Response.StatusCode;
            await WriteExceptionAsync(context, exception, (HttpStatusCode)code).ConfigureAwait(false);
        }

        private static async Task WriteExceptionAsync(HttpContext context, Exception ex, HttpStatusCode code)
        {
            var response = context.Response;
            response.ContentType = "application/json";
            response.StatusCode = (int)HttpStatusCode.BadRequest;
            await response.WriteAsync(JsonConvert.SerializeObject(CommonApiResponse.Create((HttpStatusCode)response.StatusCode,
                null, ex.Message))).ConfigureAwait(false);
        }

    }

    public static class ResponseHandlingMiddlewareExtensions
    {
        public static IApplicationBuilder UseResponseHandlingMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<ResponseHandlingMiddleware>();
        }
    }
}