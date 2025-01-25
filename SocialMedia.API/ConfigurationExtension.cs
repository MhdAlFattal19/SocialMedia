using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using SocialMedia.Application.Helpers;
using SocialMedia.Domain.Mappers;
using SocialMedia.Infrastructure.Contexts;
using System.Text;

namespace SocialMedia.API.Extensions
{
    public static class ConfigurationExtension
    {
        public static void ConfigureAuthentication(this WebApplicationBuilder builder)
        {
            var jwtTokenConfig = builder.Configuration.GetIdentityConfiguration();

            builder.Services.AddSingleton(jwtTokenConfig);

            builder.Services.AddIdentity<IdentityUser, IdentityRole>()
                            .AddEntityFrameworkStores<SocialMediaIdentityDbContext>()
                            .AddRoles<IdentityRole>()
                            .AddDefaultTokenProviders();


            builder.Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            // Adding Jwt Bearer
            .AddJwtBearer(options =>
            {
                options.SaveToken = true;
                options.RequireHttpsMetadata = false;
                options.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidAudience = jwtTokenConfig.Audience,
                    ValidIssuer = jwtTokenConfig.Issuer,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtTokenConfig.Secret))
                };
            });
        }
        public static void ConfigureAutoMapper(this WebApplicationBuilder builder)
        {
            var mapperConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new MapperProfile());
            });
            IMapper mapper = mapperConfig.CreateMapper();

            builder.Services.AddSingleton(mapper);
        }
        public static void ConfigureCorePolicy(this WebApplicationBuilder builder, string defaultApiCorsPolicy)
        {
            var policy = builder.Configuration.GetCorsConfiguration();
            builder.Services.AddCors(options =>
            {
                options.AddPolicy(
                    name: defaultApiCorsPolicy,
                    p =>
                    {
                        p.AllowAnyOrigin()
                         .AllowAnyHeader()
                         .AllowAnyMethod();
                    });
            });
        }

        public static void ConfigureAPIKey(this WebApplicationBuilder builder)
        {
            var apiKey = builder.Configuration.GetSection("Api:APIKey");

           builder.Services.AddSingleton(apiKey?.Get<APIKeyConfig>() ?? new APIKeyConfig());
        }


        public static JwtTokenConfig GetIdentityConfiguration(this IConfiguration configuration)
        {
            if (configuration is null)
            {
                throw new ArgumentNullException(nameof(configuration));
            }

            var identityConfiguration = configuration.GetSection("jwtTokenConfig");

            return identityConfiguration?.Get<JwtTokenConfig>() ?? new JwtTokenConfig();
        }

        public static CorsConfigurationModel GetCorsConfiguration(this IConfiguration configuration)
        {
            if (configuration is null)
            {
                throw new ArgumentNullException(nameof(configuration));
            }

            var corsConfiguration = configuration.GetSection("Api:Cors");

            return corsConfiguration?.Get<CorsConfigurationModel>() ?? new CorsConfigurationModel();
        }
    }
}
