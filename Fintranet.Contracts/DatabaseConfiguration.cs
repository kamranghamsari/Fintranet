using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Fintranet.Contracts
{
    public static class DatabaseConfiguration
    {
        public static void AddDatabaseDependencyInjections(this IServiceCollection services, string connectionString)
        {
            services.AddDbContext<RepositoryContext>(options =>
            {
                options.UseSqlServer(connectionString);
            });
        }
    }
}
