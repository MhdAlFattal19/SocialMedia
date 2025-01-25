using Newtonsoft.Json;
using SocialMedia.Domain.Enums;
using SocialMedia.Domain.Responses;
using System.Net;

namespace SocialMedia.API.Middlewares
{
    public class InterceptorMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<InterceptorMiddleware> _logger;
        public InterceptorMiddleware(RequestDelegate next, ILogger<InterceptorMiddleware> logger)
        {
            _logger = logger;
            _next = next;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Exception occured in {httpContext.Request.Path}");
                await HandleExceptionAsync(httpContext, ex);
            }
        }
        private async Task HandleExceptionAsync(HttpContext context, Exception ex)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            var response = new BaseServiceResponse()
            {
                Status = (int)APIResponseEnum.Exception,
                StatusCode = (int)HttpStatusCode.InternalServerError,
                MessageDTOs = new()
               {
                   new()
                   {
                      Message = ex.Message,
                      Type = MessageTypeEnum.Technical
                   }
               }
            };
            await context.Response.WriteAsync(JsonConvert.SerializeObject(response));
        }
    }

}
