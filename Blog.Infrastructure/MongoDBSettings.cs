namespace Blog.Infrastructure
{
    public class MongoDbSettings : IMongoDbSettings
    {
        public string ArticleCollectionName { get; set; }
        public string ConnectionString      { get; set; }
        public string DatabaseName          { get; set; }
    }


    public interface IMongoDbSettings
    {
        string ArticleCollectionName { get; set; }
        string ConnectionString      { get; set; }
        string DatabaseName          { get; set; }
    }
}