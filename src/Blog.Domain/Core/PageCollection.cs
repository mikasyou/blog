using System.Collections.Generic;

namespace Blog.Domain.Core {
    public class PageCollection<T> {
        public IEnumerable<T> Items { get; init; }
        public int Total { get; init; }

        public PageCollection(IEnumerable<T> items, int total) {
            this.Items = items;
            this.Total = total;
        }
    }
}
