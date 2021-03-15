using Blog.Infrastructure.Repositories;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;

namespace Blog.Infrastructure {
    public class BlogDbOptions {
        public string ArticleCollectionName { get; set; }
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
    }


    public static class ApplicationConfiguration {
        public static void AddMongoDbClient(this IServiceCollection services, BlogDbOptions options) {
            //BsonClassMap.RegisterClassMap<ArticlePO>(it => {
            //    it.AutoMap();
            //});
            var client = new MongoClient(options.ConnectionString);
            var database = client.GetDatabase(options.DatabaseName);
            services.AddSingleton(sp => database.GetCollection<ArticlePO>(options.ArticleCollectionName));
        }
    }
}