using Fintranet.Api3.Helpers;
using Fintranet.Contracts;
using Fintranet.Repositories;
using Fintranet.Repositories.Helpers;
using Fintranet.Repositories.User;
using Microsoft.OpenApi.Models;
using Swashbuckle.Swagger;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.Configure<AppSettings>(builder.Configuration.GetSection("AppSettings"));
builder.Services.AddCors();
SwaggerConfiguration.ConfigureSwaggerService(builder.Services);
builder.Services.AddControllers()
.AddNewtonsoftJson(x =>
{
    x.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
});

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddDatabaseDependencyInjections(builder.Configuration.GetSection("ConnectionStrings").GetSection("DefaultConnection").Value);
builder.Services.AddRepositoryDependencyInjections();

builder.Services.AddScoped<IUserRepository, UserRepository>();

var app = builder.Build();

ExceptionConfiguration.AddExceptionConfiguration(app);

// Configure the HTTP request pipeline.
app.UseCors(x => x
    .AllowAnyOrigin()
    .AllowAnyMethod()
    .AllowAnyHeader());
app.UseMiddleware<JwtMiddleware>();

SwaggerConfiguration.ConfigureSwagger(app);
app.UseSwagger();
// Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.),
// specifying the Swagger JSON endpoint.
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
});

app.UseAuthorization();

app.MapControllers();

app.Run();