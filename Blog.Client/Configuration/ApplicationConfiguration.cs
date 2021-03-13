using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Blog.Domain.ArticleContext;
using Blog.Infrastructure;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace Blog.Client.Configuration
{
    public static class ApplicationConfiguration
    {
        public static void AddMongoDbClient(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<MongoDbSettings>(configuration.GetSection(nameof(MongoDbSettings)));
            services.AddSingleton<IMongoDbSettings>(
                sp => sp.GetRequiredService<IOptions<MongoDbSettings>>().Value
            );
            services.AddSingleton<IMongoDatabase>(sp =>
            {
                var settings = sp.GetService<MongoDbSettings>();
                var client = new MongoClient(settings!.ConnectionString);
                return client.GetDatabase(settings.DatabaseName);
            });
        }
    }
}