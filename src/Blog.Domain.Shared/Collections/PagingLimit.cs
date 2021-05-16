namespace Blog.Domain.Shared.Collections {
    public record PagingLimit(
        int Limit,
        int Offset = 0
    ) {
        public static PagingLimit FromPageIndexAndSize(int pageIndex, int pageSize) => new PagingLimit(pageSize, (pageIndex - 1) * pageSize);
    };
}
