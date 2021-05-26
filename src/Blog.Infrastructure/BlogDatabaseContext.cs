using System.Threading;
using System.Threading.Tasks;
using Blog.Domain.Articles;
using Blog.Infrastructure.EntityConfigurations;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Blog.Infrastructure {
    public class BlogDatabaseContext : DbContext {
        private readonly IMediator mediator;

        public DbSet<Article> Articles { get; private set; } = default!;
        public DbSet<Tag> Tags { get; private set; } = default!;
        public DbSet<ArticleComment> ArticleComments { get; private set; } = default!;


        public BlogDatabaseContext(DbContextOptions<BlogDatabaseContext> options, IMediator mediator) : base(options) {
            this.mediator = mediator;
        }


        protected override void OnModelCreating(ModelBuilder modelBuilder) {
            modelBuilder.ApplyConfiguration(new ArticleConfiguration())
                        .ApplyConfiguration(new ArticleCommentConfiguration())
                        .ApplyConfiguration(new TagConfiguration());
        }

        public async Task<bool> SaveEntitiesAsync(CancellationToken cancellationToken = default(CancellationToken)) {
            await mediator.DispatchDomainEventsAsync(this);
            var result = await base.SaveChangesAsync(cancellationToken);
            return true;
        }
    }
}