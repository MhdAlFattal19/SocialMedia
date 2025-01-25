using SocialMedia.Application;
using SocialMedia.Application.Helpers;
using SocialMedia.Infrastructure;
using Serilog;
using SocialMedia.API.Extensions;
using SocialMedia.Domain.Contracts;
using SocialMedia.API.Middlewares;


var builder = WebApplication.CreateBuilder(args);
builder.Host.UseSerilog((hostingContext, loggerConfiguration) => loggerConfiguration.ReadFrom.Configuration(hostingContext.Configuration));
var configuration = builder.Configuration;


// Add services to the container.

builder.Services
    .AddApplication()
    .AddInfrastructure(configuration.GetConnectionString("ConnectionString"));

// auto mappper configuration
builder.ConfigureAutoMapper();

//identity configuration
builder.ConfigureAuthentication();

builder.ConfigureAPIKey();

// core Policy configuration
builder.ConfigureCorePolicy("DefaultCorsPolicy");

builder.Services.AddSingleton<IResponseHelper, ResponseHelper>();
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddHttpContextAccessor(); builder.Services.AddOpenApi();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseSwagger();
app.UseSwaggerUI();
app.UseRouting();


app.UseCors("DefaultCorsPolicy");


app.UseHttpsRedirection();


app.UseAuthorization();
app.UseAuthentication();


app.UseMiddleware<InterceptorMiddleware>();
app.UseMiddleware<ApiKeyMiddleware>();

app.MapControllers();

app.Run();
