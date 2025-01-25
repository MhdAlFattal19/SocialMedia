using SocialMedia.Application.Helpers;

namespace SocialMedia.API.Middlewares
{
    public class ApiKeyMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly APIKeyConfig _apiKeyConfig;

        public ApiKeyMiddleware(RequestDelegate next, APIKeyConfig apiKeyConfig)
        {
            _next = next;
            _apiKeyConfig = apiKeyConfig;
        }

        public async Task Invoke(HttpContext context)
        {

            var path = context.Request.Path.Value;
            if (path == "/" || path.StartsWith("/swagger"))
            {
                await _next(context);
                return;
            }

            if (!context.Request.Headers.TryGetValue("X-API-KEY", out var extractedApiKey))
            {
                context.Response.StatusCode = 401;
                await context.Response.WriteAsync("API Key is missing.");
                return;
            }

            if (_apiKeyConfig.ApiKey != extractedApiKey)
            {
                context.Response.StatusCode = 401;
                await context.Response.WriteAsync("Invalid API Key.");
                return;
            }
            await _next(context);
        }
    }
}
