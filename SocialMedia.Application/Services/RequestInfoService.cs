using Microsoft.AspNetCore.Http;
using SocialMedia.Domain.Contracts;
using System.Security.Claims;

namespace SocialMedia.Application.Services
{
    public class RequestInfoService : IRequestInfoService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public string? CorrelationId { get; set; }

        public Guid? UserId { get; }

        public string? AccessToken { get; }

        public string LanguageCode { get; set; } = "en";

        public RequestInfoService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
            var currentContext = _httpContextAccessor.HttpContext;
            if (currentContext is null)
                return;
            GenerateOrSetCorrelationId(currentContext);
            //is authenticated request
            if (currentContext.User.Identity is not null && currentContext.User.Identity.IsAuthenticated)
            {
                string? id = currentContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? currentContext.User.Claims.FirstOrDefault(c => c.Type == "user_Id")?.Value;
                if (id is not null)
                {
                    UserId = new Guid(id);
                    AccessToken = currentContext.Request.Headers["Authorization"].ToString().Split(' ')[1];
                }
            }
            if (!string.IsNullOrEmpty(currentContext.Request.Headers["lang"]))
            {
                LanguageCode = currentContext.Request.Headers["lang"].ToString();
            }

        }
        private void GenerateOrSetCorrelationId(HttpContext httpContext)
        {
            if (httpContext.Request.Headers.TryGetValue("X-Correlation-Id", out var correlationId))
            {
                CorrelationId = correlationId.ToString();
            }
            if (string.IsNullOrEmpty(CorrelationId))
            {
                CorrelationId = Guid.NewGuid().ToString(); ;
                httpContext.Response.Headers.Add("X-Correlation-Id", CorrelationId);
            }
        }
    }
}
