namespace Blog.Domain.Articles {
    public class Tag {
        public Tag(string id, string value) {
            Id = id;
            Value = value;
        }

        public string Id { get; private set; }
        public string Value { get; private set; }
    }
}