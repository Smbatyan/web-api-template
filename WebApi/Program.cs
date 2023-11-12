using Application;
using Infrastructure;
using Microsoft.FeatureManagement;
using Newtonsoft.Json.Serialization;
using WebApi.Extensions;
using WebApi.Middlewares;

var builder = WebApplication.CreateBuilder(args);

builder.AddSerilog(); // Move to common
builder.AddCors(); // Move to common

builder.Services
    .AddPandaSwaggerGen(builder.Configuration) // Move to common
    .AddCustomFluentValidation()
    .AddMediator()
    .AddAppSettings(builder.Configuration)
    .AddApplication()
    .AddInfrastructure(builder.Configuration)
    .AddApiVersioningFromHeader()
    .AddEndpointsApiExplorer()
    .AddFeatureManagement().Services
    .AddControllers()
    .AddNewtonsoftJson();

builder.Services.AddMvc();

    builder.Services.Configure<RouteOptions>(options => options.LowercaseUrls = true); 

var app = builder.Build();

app
    .UseCorrelationId() // Move to common
    .UseRequestResponseLogging() // Move to common
    .UseMiddleware<ErrorHandlerMiddleware>() // Move to common
    .UseHttpsRedirection()
    .UseRouting()
    .UsePandaSwagger(builder.Configuration); // Move to common

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});

app.Run();