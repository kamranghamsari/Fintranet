using Fintranet.Repositories.Helpers;
using Fintranet.Repositories.User;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Fintranet.Repositories;

public static class RepositoryConfiguration
{
    public static void AddRepositoryDependencyInjections(this IServiceCollection services)
    {
        services.AddScoped<IPagingHelper<string>, PagingHelper<string>>();
        services.AddScoped<ISortHelper<string>, SortHelper<string>>();

        services.AddScoped<IPagingHelper<Contracts.DataModels.User>, PagingHelper<Contracts.DataModels.User>>();
        services.AddScoped<ISortHelper<Contracts.DataModels.User>, SortHelper<Contracts.DataModels.User>>();
        services.AddScoped<IUserRepository, UserRepository>();

        services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
    }
}