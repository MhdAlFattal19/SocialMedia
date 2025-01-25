using Microsoft.Extensions.DependencyInjection;
using SocialMedia.Application.Helpers;
using SocialMedia.Application.Services;
using SocialMedia.Domain.Contracts;

namespace SocialMedia.Application
{
    public static class ConfigureServices
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddTransient<ISocialMediaService, SocialMediaService>();
            services.AddTransient<IAuthService, AuthService>();
            services.AddTransient<ICacheManager, CacheManager>();
            services.AddScoped<IRequestInfoService, RequestInfoService>();
            services.AddMemoryCache();
            return services;
        }
    }
}
