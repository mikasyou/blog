using Blog.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Blog.DbMigrator {
    /**

     * 用来创建数据库
     */
    public class BlogContextFactory : IDesignTimeDbContextFactory<BlogDbContext> {
        public BlogDbContext CreateDbContext(string[] args) {
            var builder = new DbContextOptionsBuilder<BlogDbContext>();
            builder.UseNpgsql("Host=127.0.0.1;Port=5432;Database=blog_db;Username=postgres;Password=trust",
                        it => it.MigrationsAssembly("Blog.DbMigrator"))
                   .UseSnakeCaseNamingConvention();

            return new BlogDbContext(builder.Options);
        }
    }
}