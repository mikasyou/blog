namespace Blog.Domain.Shared.Article {
    public class ArticleTag {
        public ArticleTag(string id, string value) {
            ID = id;
            Value = value;
        }

        public string ID { get; private set; }
        public string Value { get; private set; }
    }
}