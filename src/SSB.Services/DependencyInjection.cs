using Microsoft.Extensions.DependencyInjection;
using SSB.Domain.Contracts;
using SSB.Services.Git;

namespace SSB.Services;

public static class DependencyInjection
{
    public static IServiceCollection AddServiceLayer(this IServiceCollection services)
    {
        services.AddScoped<IGitService, GitService>();
        return services;
    }
}
