namespace Fintranet.Api3.Helpers;

public static class CorsConfiguration
{
    public static void AddCorsDependencyInjections(this IServiceCollection services)
    {
        services.AddCors(options =>
        {
            options.AddPolicy("AllowAllOrigins",
                builder => builder
                    .AllowAnyOrigin()
                    .AllowAnyHeader()
                    .AllowAnyMethod()
                //.AllowCredentials()
            );
        });
    }
    public static void ConfigureCors(IApplicationBuilder app)
    {
        app.UseCors("AllowAllOrigins");
    }
}