using System.Collections.Generic;

namespace Blog.Domain.Shared.Collections {
    public class Page<T> {
        public IEnumerable<T> Items { get; init; }
        public int Total { get; init; }

        public Page(IEnumerable<T> items, int total) {
            this.Items = items;
            this.Total = total;
        }
    }
}
