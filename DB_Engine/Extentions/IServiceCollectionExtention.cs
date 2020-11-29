using System;
using System.Collections.Generic;
using System.Text;
using DB_Engine.Factories;
using DB_Engine.Factories.Implementations;
using Microsoft.Extensions.DependencyInjection;
namespace DB_Engine.Extentions
{
    public static class IServiceCollectionExtention
    {
        public static IServiceCollection AddCustomDbEngine(this IServiceCollection services)
        {
            services.AddScoped<IDataBaseServiceFactory, DataBaseServiceFactory>();
            services.AddScoped<IStorageFactory, StorageFactory>();
            services.AddScoped<ISourceFactory, SourceFactory>();
            services.AddScoped<IEntityServiceFactory, EntityServiceFactory>();
            services.AddScoped<IDbProviderFactory, DbProviderFactory>();

            return services;
        }
    }
}
