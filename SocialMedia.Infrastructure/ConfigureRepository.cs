using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using SocialMedia.Domain.IRepositories;
using SocialMedia.Infrastructure.Contexts;
using SocialMedia.Infrastructure.Repositories;

namespace SocialMedia.Infrastructure
{
    public static class ConfigureRepository
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, string dbConnectionString)
        {
            services.AddDbContext<SocialMediaDbContext>(options => options.UseSqlServer(dbConnectionString));
            services.AddDbContext<SocialMediaIdentityDbContext>(options => options.UseSqlServer(dbConnectionString));
            services.AddTransient<ISocialMediaUnitOfWork, SocialMediaUnitOfWork>();
            services.AddTransient<ISocialMediaRepository, SocialMediaRepository>();
            return services;
        }
    }
}
