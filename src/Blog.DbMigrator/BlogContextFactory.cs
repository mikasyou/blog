using Blog.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Blog.DbMigrator {
    /**
     * 用来创建数据库
     */
    public class BlogContextFactory : IDesignTimeDbContextFactory<BlogDatabaseContext> {
        public BlogDatabaseContext CreateDbContext(string[] args) {
            var builder = new DbContextOptionsBuilder<BlogDatabaseContext>();
            builder.UseNpgsql(
                        "Host=127.0.0.1;Port=5455;Database=blog;Username=postgres;Password=trust",
                        it => it.MigrationsAssembly("Blog.DbMigrator")
                    )
                   .UseSnakeCaseNamingConvention();

            return new BlogDatabaseContext(builder.Options, default!);
        }
    }
}