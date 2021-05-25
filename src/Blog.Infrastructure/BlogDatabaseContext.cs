using System.Threading;
using System.Threading.Tasks;
using Blog.Domain.Articles;
using Blog.Infrastructure.EntityConfigurations;
using Blog.Infrastructure.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Blog.Infrastructure {
    public class BlogDatabaseContext : DbContext {
        private readonly IMediator mediator;
        public const string DEFAULT_SCHEMA = "ordering";

        public DbSet<Article> Articles { get; } = default!;


        public BlogDatabaseContext(DbContextOptions<BlogDatabaseContext> options, IMediator mediator) : base(options) {
            this.mediator = mediator;
        }


        protected override void OnModelCreating(ModelBuilder modelBuilder) {
            modelBuilder.ApplyConfiguration(new ArticleConfiguration());
        }

        public async Task<bool> SaveEntitiesAsync(CancellationToken cancellationToken = default(CancellationToken)) {
            await mediator.DispatchDomainEventsAsync(this);
            var result = await base.SaveChangesAsync(cancellationToken);
            return true;
        }
    }
}