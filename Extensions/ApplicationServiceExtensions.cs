using Mongo.API.MongoSettings;
using Mongo.API.Services;

namespace Mongo.API.Extensions;

public static class ApplicationServiceExtensions
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration config)
    {
        // mongo dependencies
        services.Configure<MongoDBSettings>(config.GetSection("MongoDBSettings"));
        services.AddSingleton<MongoDBService>();

        // Add Automapper
        services.AddAutoMapper(typeof(Program));

        return services;
    }
}
